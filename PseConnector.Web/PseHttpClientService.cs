using System.Net.Http.Json;

namespace PseConnector.Web;

public class PseHttpClientService(HttpClient _httpClient)
{
    public Task<string> GetStringAsync(string url)
    {
        return _httpClient.GetStringAsync(url);
    }
}