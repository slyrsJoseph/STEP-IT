
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
namespace LogAnalizerServer
{
    public class LogService
    {
        private readonly ILogger<LogService> _logger;
        private readonly LogAnalizerServerDbContext _context;

        public LogService(LogAnalizerServerDbContext context, ILogger<LogService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Imports log entries from a .txt log file into the AlarmLogs table.
        /// </summary>
        /// <param name="filePath">The path to the .txt log file.</param>
        ///
        /// 
       /*public async Task ImportLogsAsync(string filePath, LogWeekType weekType)
    {
    if (string.IsNullOrEmpty(filePath))
    {
        Console.WriteLine("ImportLogsAsync: filePath is null or empty.");
        return;
    }

    try
    {
        using var reader = new StreamReader(filePath);
        string line;
        int lineNumber = 0;
        var logsToAdd = new List<AlarmLog>();

        while ((line = await reader.ReadLineAsync()) != null)
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(';');
            if (parts.Length < 15)
            {
                Console.WriteLine($"[WARNING] Line {lineNumber} has unexpected format.");
                continue;
            }

            try
            {
                var alarmLog = new AlarmLog
                {
                    TimeWhenLogged = DateTime.Parse(parts[0].Trim()),
                    LocalZoneTime = DateTime.Parse(parts[1].Trim()),
                    SequenceNumber = int.Parse(parts[2].Trim()),
                    AlarmId = parts[3].Trim(),
                    AlarmClass = parts[4].Trim(),
                    Resource = parts[5].Trim(),
                    LoggedBy = parts[6].Trim(),
                    Reference = parts[7].Trim(),
                    PrevState = parts[8].Trim(),
                    LogAction = parts[9].Trim(),
                    FinalState = parts[10].Trim(),
                    AlarmMessage = parts[11].Trim(),
                    GenerationTime = DateTime.Parse(parts[12].Trim()),
                    GenerationTimeUtc = DateTime.Parse(parts[13].Trim()),
                    Project = parts[14].Trim(),
                    WeekType = weekType
                };

                logsToAdd.Add(alarmLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"String parsing error {lineNumber}: {ex.Message}");
            }
        }

        if (logsToAdd.Any())
        {
            _context.AlarmLogs.AddRange(logsToAdd);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Succsesfully imported {logsToAdd.Count} logs.");
        }
        else
        {
            Console.WriteLine("No valid logs for save.");
        }
    }
    catch (Exception exFile)
    {
        Console.WriteLine($"Error couldnt read file: {exFile.Message}");
    }
}*/
public async Task ImportLogsAsync(string filePath,LogWeekType weekType)
{
    // Открываем файл на чтение
    using var stream = File.OpenRead(filePath);
    using var reader = new StreamReader(stream);

    string headerLine = await reader.ReadLineAsync();
    if (headerLine == null)
    {
        // Файл пустой, нечего импортировать
        return;
    }

    int lineNumber = 1;  // номер текущей строки (учитывая заголовок)
    string line;
    while ((line = await reader.ReadLineAsync()) != null)
    {
        lineNumber++;
        if (string.IsNullOrWhiteSpace(line))
        {
            continue; // пропускаем пустые строки, если таковые есть
        }

        // Разбираем строку CSV корректно, учитывая кавычки и запятые в тексте
        string[] fields = ParseCsvLine(line);
        if (fields.Length != 15)
        {
            _logger.LogWarning($"Line {lineNumber} has unexpected format");
            continue;
        }

        // Теперь fields содержит 15 элементов, включая последнее пустое поле (project).
        // Можно выполнить дальнейшую обработку: парсинг по типам и сохранение данных.
        try 
        {
            DateTime timestamp = DateTime.Parse(fields[0]);
            DateTime timestamp2 = DateTime.Parse(fields[1]);
            long eventId = long.Parse(fields[2]);
            string code = fields[3];
            string category = fields[4];
            string device = fields[5];
            string source = fields[6];
            string tag = fields[7];
            string status1 = fields[8];
            string status2 = fields[9];
            string status3 = fields[10];
            string message = fields[11];
            DateTime loggedTime = DateTime.Parse(fields[12]);
            DateTime anotherTime = DateTime.Parse(fields[13]);
            string project = fields[14]; // может быть пустой строкой

            // ... (код сохранения или использования прочитанных данных)
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Line {lineNumber} has invalid data: {ex.Message}");
            continue;
        }
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
                    .Where(log => log.WeekType == week1)
                    .GroupBy(log => log.AlarmMessage)
                    .Select(g => new { AlarmMessage = g.Key, Count = g.Count() })
                    .ToListAsync();

                
                var week2Groups = await _context.AlarmLogs
                    .Where(log => log.WeekType == week2)
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
