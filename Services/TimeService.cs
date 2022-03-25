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

    private static DateTime Today => DateTime.UtcNow.Date;

    public static DateTime Now => DateTime.UtcNow;

    public DateTime ServerNow =>
        DateTime.UtcNow + _currentTimezone.BaseUtcOffset;

    public string ServerTimeString =>
        $"Server Time ({_currentServer}): {ServerNow:HH:mm}";

    public DateTime ResetToday =>
        Today.AddHours(10);

    public DateTime NextDailyReset =>
        Now > ResetToday ? ResetToday.AddDays(1) : ResetToday;

    public DateTime LastDailyReset =>
        Now < ResetToday ? ResetToday.AddDays(-1) : ResetToday;

    public TimeSpan UntilNextDailyReset =>
        NextDailyReset - Now;

    public DateTime NextWeeklyReset =>
        ResetToday.AddDays(((int)DayOfWeek.Thursday - (int)Today.DayOfWeek + 7) % 7);

    public TimeSpan UntilNextWeeklyReset =>
        NextWeeklyReset - Now;

    public bool IsWeeklyResetDay =>
        Now.DayOfWeek == DayOfWeek.Thursday;
    
    private TimeZoneInfo _currentTimezone = Servers["EU Central/West"];
    private string _currentServer = "EU Central/West";

    public TimeService(IContainer container)
    {
        JobManager.UseUtcTime();
        JobManager.Initialize();
        JobManager.AddJob(() => SecondsTick(), s => s.ToRunEvery(1).Seconds());
        JobManager.AddJob(() =>
        {
            DailyReset();
            if (IsWeeklyResetDay)
                WeeklyReset();
        }, s => s.ToRunEvery(1).Days().At(10,00));
    }

    public IEnumerable<string> GetRegions() => Servers.Select(x => x.Key);

    public void SetTimezone(string serverName)
    {
        _currentTimezone = Servers[serverName];
        _currentServer = serverName;
    }

    public bool HasResetPassedSinceLastLaunch(DateTime lastOpened) =>
        Now > LastDailyReset && lastOpened < LastDailyReset;
}