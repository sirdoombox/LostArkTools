using System;
using System.Linq;
using FluentScheduler;

namespace LostArkChecklist.Services;

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

    public DateTime ServerTime =>
        DateTime.UtcNow + _currentTimezone.BaseUtcOffset;

    public string ServerTimeString =>
        $"Server Time ({_currentServer}): {ServerTime:HH:mm}";

    public DateTime ResetToday =>
        Today.AddHours(10);

    public DateTime NextDailyReset =>
        ServerTime >= ResetToday ? ResetToday.AddDays(1) : ResetToday;

    public TimeSpan UntilNextDailyReset =>
        NextDailyReset - ServerTime;

    public DateTime NextWeeklyReset =>
        ResetToday.AddDays(((int)DayOfWeek.Thursday - (int)Today.DayOfWeek + 7) % 7);

    public TimeSpan UntilNextWeeklyReset =>
        NextWeeklyReset - ServerTime;
    
    private TimeZoneInfo _currentTimezone = Servers["EU Central/West"];
    private string _currentServer = "EU Central/West";

    public TimeService()
    {
        JobManager.UseUtcTime();
        JobManager.Initialize();
        JobManager.AddJob(() => SecondsTick(), s => s.ToRunEvery(1).Seconds());
        JobManager.AddJob(() => DailyReset(), s => s.ToRunEvery(1).Days().At(10,00));
        JobManager.AddJob(() => WeeklyReset(), s => s.ToRunEvery(1).Weeks().On(DayOfWeek.Thursday).At(10,00));
    }

    public IEnumerable<string> GetTimezones() => Servers.Select(x => x.Key);

    public void SetTimezone(string serverName)
    {
        _currentTimezone = Servers[serverName];
        _currentServer = serverName;
    }
}