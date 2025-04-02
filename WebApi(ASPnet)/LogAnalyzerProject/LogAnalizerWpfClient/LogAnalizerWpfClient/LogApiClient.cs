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

    public async Task CompareWeeksAsync(LogWeekType week1, LogWeekType week2)
    {
        var response = await _httpClient.PostAsync(
            $"api/log/compare?week1={week1}&week2={week2}", null);

        response.EnsureSuccessStatusCode();
    }

    public async Task<List<ComparisonResult>> GetComparisonResultsAsync(LogWeekType week1, LogWeekType week2)
    {
        return await _httpClient.GetFromJsonAsync<List<ComparisonResult>>(
            $"api/log/results?week1={week1}&week2={week2}");
    }
}