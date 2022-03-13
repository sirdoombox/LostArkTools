using System;

namespace LostArkChecklist.Misc;

public static class Time
{
    private static DateTime Today => DateTime.UtcNow.Date;
    
    public static DateTime ServerTime =>
        DateTime.UtcNow.AddHours(1);

    public static DateTime ResetToday =>
        Today.AddHours(11);

    public static DateTime NextDailyReset =>
        ServerTime >= ResetToday ? ResetToday.AddDays(1) : ResetToday;

    public static TimeSpan UntilNextDailyReset =>
        NextDailyReset - ServerTime;
    
    public static DateTime NextWeeklyReset =>
        ResetToday.AddDays(((int)DayOfWeek.Thursday - (int)Today.DayOfWeek + 7) % 7);

    public static TimeSpan UntilNextWeeklyReset =>
        NextWeeklyReset - ServerTime;
}