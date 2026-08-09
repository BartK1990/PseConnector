using System.Globalization;

namespace PseConnector.Web;

// PSE reports timestamps (dtime_utc) in UTC; PSE data is always displayed in
// Polish local time. Offset is computed from the fixed EU DST rule (last
// Sunday of March/October, 01:00 UTC) rather than TimeZoneInfo, since Blazor
// WebAssembly's bundled ICU/timezone data isn't guaranteed across environments.
public static class PolandTime
{
    public static DateTime ParseUtc(string dtimeUtc) =>
        DateTime.Parse(dtimeUtc, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

    public static DateTime ToLocal(DateTime utc) => utc + GetUtcOffset(utc);

    public static DateTime ToLocal(string utcString) => ToLocal(ParseUtc(utcString));

    public static string ToLocalLabel(string utcString) =>
        ToLocal(utcString).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

    private static TimeSpan GetUtcOffset(DateTime utc)
    {
        var dstStart = LastSundayOfMonthUtc(utc.Year, 3).AddHours(1);
        var dstEnd = LastSundayOfMonthUtc(utc.Year, 10).AddHours(1);
        return utc >= dstStart && utc < dstEnd ? TimeSpan.FromHours(2) : TimeSpan.FromHours(1);
    }

    private static DateTime LastSundayOfMonthUtc(int year, int month)
    {
        var lastDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        return lastDay.AddDays(-(int)lastDay.DayOfWeek);
    }
}
