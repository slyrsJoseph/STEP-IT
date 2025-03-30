
/*
using System.Globalization;
using LogAnalizerServer.Data;
using LogAnalizerServer.Models;

namespace LogAnalizerServer.LogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using LogAnalizerServer.Data;
//using LogAnalizerServer.Models;

public class LogService
    {
        private readonly LogAnalizerServerDbContext _context;

        public LogService(LogAnalizerServerDbContext context)
        {
            _context = context;
        }

        // Добавление нового лога
        public async Task AddLogAsync(AlarmLog log)
        {
            _context.AlarmLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        // Получение логов за текущую и прошлую неделю
        public async Task<(List<AlarmLog> currentWeek, List<AlarmLog> previousWeek)> GetLogsForComparison()
        {
            var now = DateTime.UtcNow;
            var startOfCurrentWeek = now.AddDays(-(int)now.DayOfWeek);  // Начало текущей недели
            var startOfPreviousWeek = startOfCurrentWeek.AddDays(-7);   // Начало прошлой недели

            var currentWeekLogs = await _context.AlarmLogs
                .Where(log => log.TimeWhenLogged >= startOfCurrentWeek)
                .ToListAsync();

            var previousWeekLogs = await _context.AlarmLogs
                .Where(log => log.TimeWhenLogged >= startOfPreviousWeek && log.TimeWhenLogged < startOfCurrentWeek)
                .ToListAsync();

            return (currentWeekLogs, previousWeekLogs);
        }

       
        
        /*
        public async Task<bool> ImportLogsFromFileAsync(string filePath,LogWeekType weekType)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[ERROR] Файл '{filePath}' не найден."); // Логируем
                return false; // Просто возвращаем false вместо исключения
            }

            try
            {
                var logs = new List<AlarmLog>();

                using (var reader = new StreamReader(filePath))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(';');
                        if (parts.Length < 9) continue;

                        if (!DateTime.TryParseExact(parts[0], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var timeLogged) ||
                            !DateTime.TryParseExact(parts[7], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var generationTime))
                        {
                            Console.WriteLine($"[WARNING] Ошибка парсинга даты в строке: {line}");
                            continue;
                        }

                        var log = new AlarmLog
                        {
                            TimeWhenLogged = DateTime.Parse(parts[0], CultureInfo.InvariantCulture),
                            LocalZoneTime = DateTime.Parse(parts[1], CultureInfo.InvariantCulture), 
                            SequenceNumber = int.Parse(parts[2]),
                            AlarmId = parts[3],
                            AlarmClass = parts[4],
                            Resource = parts[5],
                            LoggedBy = parts[6],
                            Reference = parts[7],
                            PrevState = parts[8],
                            LogAction = parts[9],
                            FinalState = parts[10],
                            AlarmMessage = parts[11],
                            GenerationTime = DateTime.Parse(parts[12], CultureInfo.InvariantCulture),
                            GenerationTimeUtc = DateTime.Parse(parts[13], CultureInfo.InvariantCulture),
                            Project = parts[14],
                            WeekType = weekType
                        };

                        logs.Add(log);
                    }
                }

                _context.AlarmLogs.AddRange(logs);
                await _context.SaveChangesAsync();
                Console.WriteLine($"[INFO] Импортировано {logs.Count} логов.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Ошибка при импорте логов: {ex.Message}");
                return false;
            }
        }
        #1#
        
        public async Task ImportLogsFromCsvAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            string headerLine = await reader.ReadLineAsync(); // пропустить заголовок, если есть
            string line;
            var logs = new List<AlarmLog>();
            while ((line = await reader.ReadLineAsync()) != null)
            {
                string[] fields = line.Split(',');  // Разбиение по запятой
                var log = new AlarmLog
                {
                    TimeWhenLogged    = DateTime.Parse(fields[0]),   // пример преобразования в DateTime
                    LocalZoneTime     = DateTime.Parse(fields[1]),
                    SequenceNumber    = long.Parse(fields[2]),
                    AlarmId           = fields[3],
                    AlarmClass        = fields[4],
                    Resource          = fields[5],
                    LoggedBy          = fields[6],
                    Reference         = fields[7],
                    PrevState         = fields[8],
                    LogAction         = fields[9],
                    FinalState        = fields[10],
                    AlarmMessage      = fields[11],
                    GenerationTime    = DateTime.Parse(fields[12]),
                    GenerationTimeUtc = DateTime.Parse(fields[13]),
                    Project           = fields[14]
                };
                logs.Add(log);
            }
            // Сохранение списка логов в базу данных через контекст EF
            _context.AlarmLogs.AddRange(logs);
            await _context.SaveChangesAsync();
        }
        
        public async Task<List<ComparisonResult>> CompareWeeksAsync(LogWeekType week1, LogWeekType week2)
        {
            var logs = await _context.AlarmLogs
                .Where(l => l.WeekType == week1 || l.WeekType == week2)
                .ToListAsync();

            var grouped = logs
                .GroupBy(log => new { log.Resource, log.AlarmId })
                .Select(g => new ComparisonResult
                {
                    MachineName = g.Key.Resource, 
                    AlarmId = g.Key.AlarmId,
                    CountWeek1 = g.Count(x => x.WeekType == week1),
                    CountWeek2 = g.Count(x => x.WeekType == week2)
                })
                .Where(result => result.CountWeek1 != result.CountWeek2)
                .ToList();

            return grouped;
        }
        
        
    }
    */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogAnalizerServer.Data;
using LogAnalizerServer.Models;
using Microsoft.EntityFrameworkCore;

namespace LogAnalizerServer
{
    public class LogService
    {
        private readonly LogAnalizerServerDbContext _context;

        public LogService(LogAnalizerServerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Imports log entries from a .txt log file into the AlarmLogs table.
        /// </summary>
        /// <param name="filePath">The path to the .txt log file.</param>
       public async Task ImportLogsAsync(string filePath, LogWeekType weekType)
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
                Console.WriteLine($"[ERROR] Ошибка парсинга строки {lineNumber}: {ex.Message}");
            }
        }

        if (logsToAdd.Any())
        {
            _context.AlarmLogs.AddRange(logsToAdd);
            await _context.SaveChangesAsync();
            Console.WriteLine($"[INFO] Успешно импортировано: {logsToAdd.Count} логов.");
        }
        else
        {
            Console.WriteLine("[INFO] Нет валидных логов для сохранения.");
        }
    }
    catch (Exception exFile)
    {
        Console.WriteLine($"[ERROR] Ошибка чтения файла: {exFile.Message}");
    }
}

        /// <summary>
        /// Compares alarm message occurrences between two specified weeks and stores the result in the ComparisonResults table.
        /// </summary>
        public async Task CompareWeeksAsync(LogWeekType week1, LogWeekType week2)
        {
            try
            {
                // Retrieve and group alarm messages for week1
                var week1Groups = await _context.AlarmLogs
                    .Where(log => log.WeekType == week1)
                    .GroupBy(log => log.AlarmMessage)
                    .Select(g => new { AlarmMessage = g.Key, Count = g.Count() })
                    .ToListAsync();

                // Retrieve and group alarm messages for week2
                var week2Groups = await _context.AlarmLogs
                    .Where(log => log.WeekType == week2)
                    .GroupBy(log => log.AlarmMessage)
                    .Select(g => new { AlarmMessage = g.Key, Count = g.Count() })
                    .ToListAsync();

                // Use dictionaries for quick lookup of counts
                var week1Dict = week1Groups.ToDictionary(x => x.AlarmMessage, x => x.Count);
                var week2Dict = week2Groups.ToDictionary(x => x.AlarmMessage, x => x.Count);

                // Get the set of all unique alarm messages in either week
                var allMessages = new HashSet<string>(week1Dict.Keys);
                allMessages.UnionWith(week2Dict.Keys);

                // Prepare comparison results
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
                    // Remove existing comparison results for these week combinations to avoid duplicates
                    var existingResults = await _context.ComparisonResults
                        .Where(r => r.Week1Type == week1 && r.Week2Type == week2)
                        .ToListAsync();
                    if (existingResults.Any())
                    {
                        _context.ComparisonResults.RemoveRange(existingResults);
                    }

                    // Save new comparison results
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

        /// <summary>
        /// Retrieves the comparison results for the given two week types.
        /// </summary>
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
