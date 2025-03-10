using System.Net.Http.Json;

namespace PseConnector.Web;

public class PseHttpClientService(HttpClient _httpClient)
{
    public Task<string> GetStringAsync(string url)
    {
        return _httpClient.GetStringAsync(url);
    }

    public Task<T?> GetFromJsonAsync<T>(string url)
    {
        return _httpClient.GetFromJsonAsync<T>(url);
    }
}