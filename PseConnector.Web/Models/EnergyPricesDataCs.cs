namespace PseConnector.Web.Models;

public class EnergyPricesDataCs
{
    public List<EnergyPricesDataCsValue> value { get; set; }
}

public class EnergyPricesDataCsValue
{
    public double? cor_cost { get; set; }
    public double? ceb_pp_cost { get; set; }
    public double? ceb_sr_cost { get; set; }
    public double? cen_cost { get; set; }
    public double? csdac_pln { get; set; }
    public double? sk_cost { get; set; }
    public double? balance { get; set; }
    public double? balance_power { get; set; }
    public double? sk_cost_power { get; set; }
    public string dtime_utc { get; set; }
    public string business_date { get; set; }
}
