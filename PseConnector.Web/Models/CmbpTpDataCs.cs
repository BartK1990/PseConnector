namespace PseConnector.Web.Models;
public class CmbpTpDataCs
{
    public List<CmbpTpDataCsValue> value { get; set; }
}

public class CmbpTpDataCsValue
{
    public double? afrr_d { get; set; }
    public double? afrr_g { get; set; }
    public double? fcr_d { get; set; }
    public double? fcr_g { get; set; }
    public double? mfrrd_d { get; set; }
    public double? mfrrd_g { get; set; }
    public double? rr_d { get; set; }
    public double? rr_g { get; set; }
    public string onmb { get; set; }
    public string dtime_utc { get; set; }
    public string business_date { get; set; }
}
