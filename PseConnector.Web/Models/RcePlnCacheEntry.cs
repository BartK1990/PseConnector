namespace PseConnector.Web.Models;

public class RcePlnCacheEntry
{
    public DateOnly BusinessDate { get; set; }
    public List<RcePlnDataCsValue> Data { get; set; } = [];
}
