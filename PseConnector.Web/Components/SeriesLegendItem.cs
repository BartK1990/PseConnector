namespace PseConnector.Web.Components;

public class SeriesLegendItem
{
    public string Abbreviation { get; set; } = "";
    public string? FullName { get; set; }
    public string Description { get; set; } = "";

    /// <summary>One swatch per color; e.g. two colors when the item summarizes a pair of chart series (like a _D/_G direction pair).</summary>
    public List<string> Colors { get; set; } = [];
}
