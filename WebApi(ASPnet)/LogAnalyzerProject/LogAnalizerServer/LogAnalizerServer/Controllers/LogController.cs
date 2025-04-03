
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

    
    [HttpPost("import")]
    public async Task<IActionResult> ImportLogs([FromQuery] string filePath, [FromQuery] LogWeekType weekType)
    {
        await _logService.ImportLogsAsync(filePath, weekType);
        return Ok("Import finished succesful.");
    }

   
    [HttpPost("compare")]
    public async Task<IActionResult> CompareWeeks([FromQuery] LogWeekType week1, [FromQuery] LogWeekType week2)
    {
        await _logService.CompareWeeksAsync(week1, week2);
        return Ok("Comparison has finished and saved.");
    }

    [HttpGet("compare/result")]
    public async Task<IActionResult> CompareAndReturnResult([FromQuery] LogWeekType week1, [FromQuery] LogWeekType week2)
    {
        var result = await _logService.CompareWeeksInMemoryAsync(week1, week2); 
        return Ok(result);
    }
    
    
    [HttpGet("results")]
    public async Task<IActionResult> GetComparisonResults([FromQuery] LogWeekType week1, [FromQuery] LogWeekType week2)
    {
        var results = await _logService.GetComparisonResultsAsync(week1, week2);
        return Ok(results);
    }
    
    [HttpGet("available-weeks")]
    public async Task<IActionResult> GetAvailableWeeks()
    {
        var weeks = await _logService.GetAvailableWeekTypesAsync();
        return Ok(weeks);
    }
    
    [HttpGet("logs-by-week")]
    public async Task<IActionResult> GetLogsByWeek([FromQuery] LogWeekType week)
    {
        var logs = await _logService.GetLogsByWeekAsync(week);
        return Ok(logs);
    }
    
}