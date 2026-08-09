using Blazored.LocalStorage;
using PseConnector.Web.Models;

namespace PseConnector.Web.Services;

public class RceCacheService(PseHttpClientService _pseHttpClient, ILocalStorageService _localStorage)
{
    private const string CacheKeyPrefix = "rce-pln-cache-";
    private const string Endpoint = "rce-pln";
    private const string QueryParamBusinessDate = "business_date";

    public async Task<List<RcePlnDataCsValue>> GetDataForDateAsync(DateOnly date)
    {
        var cacheKey = CacheKeyPrefix + date.ToString("yyyy-MM-dd");

        var cached = await _localStorage.GetItemAsync<RcePlnCacheEntry>(cacheKey);
        if (cached is not null && cached.BusinessDate == date && cached.Data.Count > 0)
        {
            return cached.Data;
        }

        var data = await FetchFromApiAsync(date);

        await _localStorage.SetItemAsync(cacheKey, new RcePlnCacheEntry
        {
            BusinessDate = date,
            Data = data,
        });

        return data;
    }

    private async Task<List<RcePlnDataCsValue>> FetchFromApiAsync(DateOnly date)
    {
        var url = $"{Endpoint}?$first=1000&$filter={QueryParamBusinessDate}%20eq%20'{date:yyyy-MM-dd}'";
        var result = await _pseHttpClient.GetFromJsonAsync<RcePlnDataCs>(url);
        return result?.value ?? [];
    }
}
