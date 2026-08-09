namespace PseConnector.Web.Models;

public class EbRozlDataCs
{
    public List<EbRozlDataCsValue> value { get; set; }
}

public class EbRozlDataCsValue
{
    public double eb_d_pp { get; set; }
    public double eb_w_pp { get; set; }
    public double eb_afrrd { get; set; }
    public double eb_afrrg { get; set; }
    public string dtime_utc { get; set; }
}
