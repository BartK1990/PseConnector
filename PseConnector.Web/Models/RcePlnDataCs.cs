namespace PseConnector.Web.Models;

public class RcePlnDataCs
{
    public List<RcePlnDataCsValue> value { get; set; }
}

public class RcePlnDataCsValue
{
    public string doba { get; set; }
    public double rce_pln { get; set; }
    public string udtczas { get; set; }
    public string udtczas_oreb { get; set; }
    public string business_date { get; set; }
    public string source_datetime { get; set; }
}

