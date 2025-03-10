namespace PseConnector.Web.Models;
public class CmbpTpDataCs
{
    public List<CmbpTpDataCsValue> value { get; set; }
}

public class CmbpTpDataCsValue
{
    public double? cmbp { get; set; }
    public string doba { get; set; }
    public string onmb { get; set; }
    public string udtczas { get; set; }
    public string typ_rezerwy { get; set; }
    public string business_date { get; set; }
    public string source_datetime { get; set; }
}
