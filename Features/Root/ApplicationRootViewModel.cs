using System;
using LostArkChecklist.Features.Checklist;
using LostArkChecklist.Features.Config;
using LostArkChecklist.Features.ServerStatus;
using LostArkChecklist.Features.Shared;
using LostArkChecklist.Services;

namespace LostArkChecklist.Features.Root;

public class ApplicationRootViewModel : Conductor<FeatureScreenBase>.Collection.OneActive
{
    private string _serverTime = string.Empty;
    public string ServerTime
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

    public ApplicationRootViewModel(
        ChecklistViewModel checkListVm, 
        ServerStatusViewModel serverVm,
        ConfigViewModel configVm,
        TimeService timeService)
    {
        timeService.SecondsTick += () => SecondsTick(timeService);
        Items.Add(checkListVm);
        Items.Add(serverVm);
        Items.Add(configVm);
        ActiveItem = checkListVm;
    }

    private void SecondsTick(TimeService ts)
    {
        ServerTime = ts.ServerTimeString;
        TimeUntilWeeklyReset = ts.UntilNextWeeklyReset;
        TimeUntilDailyReset = ts.UntilNextDailyReset;
    }

    public void SwitchView(FeatureScreenBase switchTo)
    {
        ActiveItem = switchTo;
    }
}