namespace PseConnector.Web.Components;

public class PseChartData
{
    public List<string> Labels { get; set; } = [];
    public List<PseChartSeries> Series { get; set; } = [];
}

public class PseChartSeries
{
    public string Label { get; set; } = "";
    public List<double?> Data { get; set; } = [];
    public string Color { get; set; } = "";
    public string AxisId { get; set; } = "y";
    public double[]? BorderDash { get; set; }
    public double BorderWidth { get; set; } = 2;
    public double PointRadius { get; set; } = 1;
    public double PointHoverRadius { get; set; } = 3;

    /// <summary>Per-point marker colors, e.g. to highlight the highest/lowest sample. Overrides <see cref="Color"/> for points.</summary>
    public List<string>? PointColors { get; set; }

    /// <summary>Per-point marker radii, paired with <see cref="PointColors"/>. Overrides <see cref="PointRadius"/>.</summary>
    public List<double>? PointRadii { get; set; }

    /// <summary>Per-point hover radii, paired with <see cref="PointColors"/>. Overrides <see cref="PointHoverRadius"/>.</summary>
    public List<double>? PointHoverRadii { get; set; }
}
