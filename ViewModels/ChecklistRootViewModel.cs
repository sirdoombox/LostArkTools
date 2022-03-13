using System;
using FluentScheduler;
using LostArkChecklist.Misc;

namespace LostArkChecklist.ViewModels;

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
        ServerTime = Time.ServerTime;
        TimeUntilDailyReset = Time.UntilNextDailyReset;
        TimeUntilWeeklyReset = Time.UntilNextWeeklyReset;
    }
}