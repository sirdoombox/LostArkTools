using System;
using System.Linq;
using System.Windows.Threading;
using Stylet;

namespace LostArkChecklist.ViewModels;

public class ChecklistRootViewModel : Conductor<IScreen>.Collection.OneActive
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

    public ChecklistRootViewModel(DispatcherTimer timer, RosterChecklistViewModel rosterChecklistVm)
    {
        RosterChecklistVm = rosterChecklistVm;
        timer.Tick += TimerOnTick;
    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        ServerTime = DateTime.UtcNow;
        TimeUntilDailyReset = 
    }

    protected override void OnInitialActivate()
    {
        for (int i = 0; i < 10; i++)
        {
            Items.Add(new CharacterChecklistViewModel
            {
                DisplayName = $"Ass {i}"
            });
            ActiveItem = Items.First();
        }
    }
}