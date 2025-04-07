
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogAnalizerServer.Data;
using LogAnalizerServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
namespace LogAnalizerServer

{
    using EFCore.BulkExtensions;
    public class LogService
    {
        private readonly ILogger<LogService> _logger;
        private readonly LogAnalizerServerDbContext _context;

        public LogService(LogAnalizerServerDbContext context, ILogger<LogService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
       
public async Task ImportLogsAsync(string filePath, LogWeekType weekType)
{
    using var stream = File.OpenRead(filePath);
    using var reader = new StreamReader(stream);

    string headerLine = await reader.ReadLineAsync();
    if (headerLine == null)
    {
        _logger.LogWarning("ImportLogsAsync: файл пустой.");
        return;
    }

    int lineNumber = 1;
    string line;
    var logsToAdd = new List<AlarmLog>();

    while ((line = await reader.ReadLineAsync()) != null)
    {
        lineNumber++;
        if (string.IsNullOrWhiteSpace(line))
            continue;

        string[] fields = ParseCsvLine(line);
        if (fields.Length != 15)
        {
            _logger.LogWarning($"Line {lineNumber} has unexpected format");
            continue;
        }

        try
        {
            var alarmLog = new AlarmLog
            {
                TimeWhenLogged = DateTime.Parse(fields[0]),
                LocalZoneTime = DateTime.Parse(fields[1]),
                SequenceNumber = long.Parse(fields[2]),
                AlarmId = fields[3],
                AlarmClass = fields[4],
                Resource = fields[5],
                LoggedBy = fields[6],
                Reference = fields[7],
                PrevState = fields[8],
                LogAction = fields[9],
                FinalState = fields[10],
                AlarmMessage = fields[11],
                GenerationTime = DateTime.Parse(fields[12]),
                GenerationTimeUtc = DateTime.Parse(fields[13]),
                Project = fields[14],
                WeekType = weekType // 🟢 Указан тип недели
            };

            logsToAdd.Add(alarmLog);
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Line {lineNumber} has invalid data: {ex.Message}");
        }
    }

    if (logsToAdd.Any())
    {
        /*await _context.AlarmLogs.AddRangeAsync(logsToAdd);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"ImportLogsAsync: Импортировано {logsToAdd.Count} логов.");*/
        await _context.BulkInsertAsync(logsToAdd);
        _logger.LogInformation($"ImportLogsAsync: Импортировано {logsToAdd.Count} логов через BulkInsert.");
    }
    else
    {
        _logger.LogWarning("ImportLogsAsync: Нет корректных логов для сохранения.");
    }
}


        private string[] ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();
            bool inQuotes = false;
            var field = new System.Text.StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '\"')
                {
                    // Если встречаем кавычку:
                    if (inQuotes && i < line.Length - 1 && line[i + 1] == '\"')
                    {
                        // Двойные кавычки внутри цитаты -> добавляем одну кавычку в значение
                        field.Append('\"');
                        i++; // пропустить экранирующую кавычку
                    }
                    else
                    {
                        // Переключаем состояние "внутри кавычек"
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    // Разделитель поля (запятая вне кавычек)
                    fields.Add(field.ToString().Trim());
                    field.Clear();
                }
                else
                {
                    // Обычный символ
                    field.Append(c);
                }
            }

            // Добавить последнее поле (после последней запятой или конец строки)
            fields.Add(field.ToString().Trim());
            return fields.ToArray();
        }
      
        public async Task CompareWeeksAsync(LogWeekType week1, LogWeekType week2)
        {
            try
            {
                
                var week1Groups = await _context.AlarmLogs
                    .Where(log => log.WeekType == week1 && log.FinalState == "G")
                    .GroupBy(log => log.AlarmMessage)
                    .Select(g => new { AlarmMessage = g.Key, Count = g.Count() })
                    .ToListAsync();

                
                var week2Groups = await _context.AlarmLogs
                    .Where(log => log.WeekType == week2 && log.FinalState == "G")
                    .GroupBy(log => log.AlarmMessage)
                    .Select(g => new { AlarmMessage = g.Key, Count = g.Count() })
                    .ToListAsync();

               
                var week1Dict = week1Groups.ToDictionary(x => x.AlarmMessage, x => x.Count);
                var week2Dict = week2Groups.ToDictionary(x => x.AlarmMessage, x => x.Count);

                
                var allMessages = new HashSet<string>(week1Dict.Keys);
                allMessages.UnionWith(week2Dict.Keys);

               
                var comparisonResults = new List<ComparisonResult>();
                foreach (var message in allMessages)
                {
                    week1Dict.TryGetValue(message, out int count1);
                    week2Dict.TryGetValue(message, out int count2);

                    comparisonResults.Add(new ComparisonResult
                    {
                        AlarmMessage = message,
                        CountWeek1 = count1,
                        CountWeek2 = count2,
                        Week1Type = week1,
                        Week2Type = week2
                    });
                }

                try
                {
                    
                    var existingResults = await _context.ComparisonResults
                        .Where(r => r.Week1Type == week1 && r.Week2Type == week2)
                        .ToListAsync();
                    if (existingResults.Any())
                    {
                        _context.ComparisonResults.RemoveRange(existingResults);
                    }

                    
                    await _context.ComparisonResults.AddRangeAsync(comparisonResults);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"CompareWeeksAsync: Comparison between {week1} and {week2} saved with {comparisonResults.Count} results.");
                }
                catch (Exception exSave)
                {
                    Console.WriteLine($"CompareWeeksAsync: Error saving comparison results to database: {exSave.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CompareWeeksAsync: Error during comparison of weeks {week1} and {week2}: {ex.Message}");
            }
        }
        
        public async Task<List<ComparisonResult>> CompareWeeksInMemoryAsync(LogWeekType week1, LogWeekType week2)
        {
            var week1Groups = await _context.AlarmLogs
                .Where(log => log.WeekType == week1 && log.FinalState == "G")
                .GroupBy(log => log.AlarmMessage)
                .Select(g => new { AlarmMessage = g.Key, Count = g.Count() })
                .ToListAsync();

            var week2Groups = await _context.AlarmLogs
                .Where(log => log.WeekType == week2 && log.FinalState == "G")
                .GroupBy(log => log.AlarmMessage)
                .Select(g => new { AlarmMessage = g.Key, Count = g.Count() })
                .ToListAsync();

            var week1Dict = week1Groups.ToDictionary(x => x.AlarmMessage, x => x.Count);
            var week2Dict = week2Groups.ToDictionary(x => x.AlarmMessage, x => x.Count);

            var allMessages = new HashSet<string>(week1Dict.Keys);
            allMessages.UnionWith(week2Dict.Keys);

            var comparisonResults = new List<ComparisonResult>();
            foreach (var message in allMessages)
            {
                week1Dict.TryGetValue(message, out int count1);
                week2Dict.TryGetValue(message, out int count2);

                comparisonResults.Add(new ComparisonResult
                {
                    AlarmMessage = message,
                    CountWeek1 = count1,
                    CountWeek2 = count2,
                    Week1Type = week1,
                    Week2Type = week2
                });
            }

            return comparisonResults;
        }

    
        public async Task<List<ComparisonResult>> GetComparisonResultsAsync(LogWeekType week1, LogWeekType week2)
        {
            try
            {
                return await _context.ComparisonResults
                    .Where(r => r.Week1Type == week1 && r.Week2Type == week2)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetComparisonResultsAsync: Error retrieving comparison results for {week1} vs {week2}: {ex.Message}");
                return new List<ComparisonResult>();
            }
        }
        public async Task<List<LogWeekType>> GetAvailableWeekTypesAsync()
        {
            return await _context.AlarmLogs
                .Select(log => log.WeekType)
                .Distinct()
                .ToListAsync();
        }
        
        public async Task<List<AlarmLog>> GetLogsByWeekAsync(LogWeekType week)
        {
            return await _context.AlarmLogs
                .Where(log => log.WeekType == week)
                .ToListAsync();
        }
        
    }
}
