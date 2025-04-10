using LogAnalizerServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogAnalizerServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController : ControllerBase
{
    private readonly LogService _logService;

    public LogController(LogService logService)
    {
        _logService = logService;
    }

    /// <summary>
    /// Импорт логов в базу данных
    /// </summary>
    [HttpPost("import")]
    public async Task<IActionResult> ImportLogs([FromQuery] string filePath, [FromQuery] LogWeekType weekType)
    {
        await _logService.ImportLogsAsync(filePath, weekType);
        return Ok("Import finished successful.");
    }

    /// <summary>
    /// Сравнение двух недель и возврат результата без сохранения в базу
    /// </summary>
    [HttpGet("compare/result")]
    public async Task<IActionResult> CompareAndReturnResult([FromQuery] LogWeekType week1,
        [FromQuery] LogWeekType week2)
    {
        var result = await _logService.CompareWeeksInMemoryAsync(week1, week2);
        return Ok(result);
    }

    /// <summary>
    /// Получение списка доступных недель
    /// </summary>
    [HttpGet("available-weeks")]
    public async Task<IActionResult> GetAvailableWeeks()
    {
        var weeks = await _logService.GetAvailableWeekTypesAsync();
        return Ok(weeks);
    }

    /// <summary>
    /// Получение логов по выбранной неделе
    /// </summary>
    [HttpGet("logs-by-week")]
    public async Task<IActionResult> GetLogsByWeek([FromQuery] LogWeekType week)
    {
        var logs = await _logService.GetLogsByWeekAsync(week);
        return Ok(logs);
    }
}