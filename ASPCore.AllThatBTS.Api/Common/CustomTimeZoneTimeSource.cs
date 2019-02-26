using NLog.Time;
using System;
using System.ComponentModel.DataAnnotations;

[TimeSource("CustomTimeZone")]
public class CustomTimeZoneTimeSource : TimeSource
{
    string ZoneName;
    TimeZoneInfo ZoneInfo;

    [Required]
    public string Zone
    {
        get { return ZoneName; }
        set
        {
            ZoneName = value;
            ZoneInfo
                = TimeZoneInfo.FindSystemTimeZoneById(value);
        }
    }

    public override DateTime Time
    {
        get
        {
            return TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow, ZoneInfo);
        }
    }

    public override DateTime FromSystemTime(DateTime systemTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(systemTime, ZoneInfo);
    }
}