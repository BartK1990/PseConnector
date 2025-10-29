namespace PseConnector.Web.Models;

public class RcePlnDataCs
{
    public List<RcePlnDataCsValue> value { get; set; }
}

public class RcePlnDataCsValue
{
    public double rce_pln { get; set; }
    public string dtime_utc { get; set; }
}

