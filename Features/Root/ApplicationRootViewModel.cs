using System;
using LostArkChecklist.Features.Checklist;
using LostArkChecklist.Features.Config;
using LostArkChecklist.Services;

namespace LostArkChecklist.Features.Root;

public class ApplicationRootViewModel : Conductor<IScreen>
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
    
    private readonly ChecklistViewModel _checklistVm;
    private readonly ConfigViewModel _configVm;
    private readonly TimeService _timeService;

    public ApplicationRootViewModel(ChecklistViewModel checkListVm, ConfigViewModel configVm, TimeService timeService)
    {
        _checklistVm = checkListVm;
        _configVm = configVm;
        _timeService = timeService;
        timeService.SecondsTick += SecondsTick;
        ActiveItem = _checklistVm;
    }

    private void SecondsTick()
    {
        ServerTime = _timeService.ServerTimeString;
        TimeUntilWeeklyReset = _timeService.UntilNextWeeklyReset;
        TimeUntilDailyReset = _timeService.UntilNextDailyReset;
    }

    public void ToggleConfig()
    {
        ActiveItem = ActiveItem == _configVm ? _checklistVm : _configVm;
    }
}