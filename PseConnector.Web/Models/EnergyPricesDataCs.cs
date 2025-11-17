namespace PseConnector.Web.Models;

public class EnergyPricesDataCs
{
    public List<EnergyPricesDataCsValue> value { get; set; }
}

public class EnergyPricesDataCsValue
{
    public double? price { get; set; }
    public string dtime_utc { get; set; }
    public string business_date { get; set; }
}
