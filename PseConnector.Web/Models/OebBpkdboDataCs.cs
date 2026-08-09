namespace PseConnector.Web.Models;

public class OebBpkdboDataCs
{
    public List<OebBpkdboDataCsValue> value { get; set; }
}

public class OebBpkdboDataCsValue
{
    public double ofp { get; set; }
    public string activ_direction { get; set; }
    public string dtime_utc { get; set; }
}
