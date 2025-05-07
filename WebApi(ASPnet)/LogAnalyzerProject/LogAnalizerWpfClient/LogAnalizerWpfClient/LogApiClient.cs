using System;
using System.Collections.Generic;

namespace LogAnalizerWpfClient;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LogAnalizerShared;

public class LogApiClient
{
   
    private readonly HttpClient _httpClient;

    public LogApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task ImportLogsAsync(string filePath, LogWeekType weekType)
    {
        var response = await _httpClient.PostAsync(
            $"api/log/import?filePath={Uri.EscapeDataString(filePath)}&weekType={weekType}", null);

        response.EnsureSuccessStatusCode();
    }

   
  
    public async Task<List<ComparisonResult>> CompareWeeksInMemoryAsync(LogWeekType week1, LogWeekType week2)
    {
        return await _httpClient.GetFromJsonAsync<List<ComparisonResult>>(
            $"api/log/compare/result?week1={week1}&week2={week2}");
    }
    
    public async Task<List<LogWeekType>> GetAvailableWeekTypesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<LogWeekType>>("api/log/available-weeks");
    }
    public async Task<List<AlarmlogClient>> GetLogsByWeekAsync(LogWeekType week)
    {
        return await _httpClient.GetFromJsonAsync<List<AlarmlogClient>>($"api/log/logs-by-week?week={week}");
    }
    
    
    public async Task<List<string>> GetDatabasesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<string>>("api/database/list");
    }

    public async Task CreateDatabaseAsync(string dbName)
    {
        var response = await _httpClient.PostAsync($"api/database/create?dbName={Uri.EscapeDataString(dbName)}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task SelectDatabaseAsync(string dbName)
    {
        var response = await _httpClient.PostAsync($"api/database/select?dbName={Uri.EscapeDataString(dbName)}", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteDatabaseAsync(string dbName)
    {
        var response = await _httpClient.DeleteAsync($"api/database/delete?dbName={Uri.EscapeDataString(dbName)}");
        response.EnsureSuccessStatusCode();
    }
    
    public async Task<List<ComparisonResult>> CompareByDateRangeAsync(DateTimeRangeComparisonRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/log/compare/by-daterange", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ComparisonResult>>();
    }
    
    public async Task<(DateTime Min, DateTime Max)> GetMinMaxGenerationTimeAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<MinMaxGenerationTimeDto>("api/log/min-max-generationtime");
        return (result.Min, result.Max);
    }
    
    
    
}