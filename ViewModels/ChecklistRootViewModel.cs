using System;
using System.Linq;
using FluentScheduler;

namespace LostArkChecklist.ViewModels;

// TODO: Rework this to have the Character Checklist as a completely separate standalone View/ViewModel
public class ChecklistRootViewModel : Screen
{
    private DateTime _serverTime;
    public DateTime ServerTime
    {
        get => _serverTime;
        set => SetAndNotify(ref _serverTime, value);
    }
    
    private TimeSpan _timeUntilWeeklyReset;
    public TimeSpan TimeUntilWeeklyReset
    {
        get => _timeUntilWeeklyReset;
        set => SetAndNotify(ref _timeUntilWeeklyReset, value);
    }
    
    private TimeSpan _timeUntilDailyReset;
    public TimeSpan TimeUntilDailyReset
    {
        get => _timeUntilDailyReset;
        set => SetAndNotify(ref _timeUntilDailyReset, value);
    }
    
    public RosterChecklistViewModel RosterChecklistVm { get; }
    public CharacterViewModel CharacterVm { get; }

    public ChecklistRootViewModel(RosterChecklistViewModel rosterChecklistVm, CharacterViewModel characterVm)
    {
        RosterChecklistVm = rosterChecklistVm;
        CharacterVm = characterVm;
        JobManager.AddJob(SecondTick, s => s.ToRunEvery(1).Seconds());
        JobManager.AddJob(DailyReset, s => s.ToRunEvery(1).Days().At(11,00));
        JobManager.AddJob(WeeklyReset, s => s.ToRunEvery(1).Weeks().On(DayOfWeek.Thursday).At(11,00));
    }

    private void DailyReset()
    {
        RosterChecklistVm.ResetDaily();
        CharacterVm.ResetDaily();
    }

    private void WeeklyReset()
    {
        RosterChecklistVm.ResetWeekly();
        CharacterVm.ResetWeekly();
    }
    
    private void SecondTick()
    {
        ServerTime = DateTime.UtcNow.AddHours(1);
        var today = DateTime.Today;
        var dailyReset = today.AddHours(11);
        var thursdayReset = dailyReset.AddDays(((int)DayOfWeek.Thursday - (int)today.DayOfWeek + 7) % 7);
        if (ServerTime > dailyReset)
            dailyReset = dailyReset.AddDays(1);
        TimeUntilDailyReset = dailyReset - ServerTime;
        TimeUntilWeeklyReset = thursdayReset - ServerTime;
    }
}