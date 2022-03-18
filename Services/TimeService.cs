using System;
using System.Linq;
using FluentScheduler;
using LostArkTools.Extensions;
using LostArkTools.Services.Base;
using StyletIoC;

namespace LostArkTools.Services;

public class TimeService
{
    private static readonly IReadOnlyDictionary<string, TimeZoneInfo> Servers = new Dictionary<string, TimeZoneInfo>
    {
        {"NA West", TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")},
        {"NA East", TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")},
        {"South America", TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")},
        {"EU Central/West", TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time")}
    };
    
    public Action WeeklyReset { get; set; } = () => { };
    public Action DailyReset { get; set; } = () => { };
    public Action SecondsTick { get; set; } = () => { };

    private DateTime Today => DateTime.UtcNow.Date;

    private DateTime Now => DateTime.UtcNow;

    public DateTime ServerTime =>
        DateTime.UtcNow + _currentTimezone.BaseUtcOffset;

    public string ServerTimeString =>
        $"Server Time ({_currentServer}): {ServerTime:HH:mm}";

    public DateTime ResetToday =>
        Today.AddHours(10);

    public DateTime NextDailyReset =>
        ServerTime >= ResetToday ? ResetToday.AddDays(1) : ResetToday;

    public DateTime LastDailyReset =>
        ServerTime < ResetToday ? ResetToday.AddDays(-1) : ResetToday;

    public TimeSpan UntilNextDailyReset =>
        NextDailyReset - Now;

    public DateTime NextWeeklyReset =>
        ResetToday.AddDays(((int)DayOfWeek.Thursday - (int)Today.DayOfWeek + 7) % 7);

    public TimeSpan UntilNextWeeklyReset =>
        NextWeeklyReset - Now;

    public bool IsWeeklyResetDay =>
        ServerTime.DayOfWeek == DayOfWeek.Thursday;
    
    private TimeZoneInfo _currentTimezone = Servers["EU Central/West"];
    private string _currentServer = "EU Central/West";

    public TimeService(IContainer container)
    {
        JobManager.UseUtcTime();
        JobManager.Initialize();
        JobManager.AddJob(() => SecondsTick(), s => s.ToRunEvery(1).Seconds());
        JobManager.AddJob(() => DailyReset(), s => s.ToRunEvery(1).Days().At(10,00));
        JobManager.AddJob(() => WeeklyReset(), s => s.ToRunEvery(1).Weeks().On(DayOfWeek.Thursday).At(10,00));
    }

    public IEnumerable<string> GetRegions() => Servers.Select(x => x.Key);

    public void SetTimezone(string serverName)
    {
        _currentTimezone = Servers[serverName];
        _currentServer = serverName;
    }

    public bool HasResetPassedSinceLastLaunch(DateTime lastOpened) =>
        DateTime.UtcNow >= LastDailyReset && lastOpened < LastDailyReset;
}