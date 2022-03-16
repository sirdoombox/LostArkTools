using LostArkTools.Extensions;
using LostArkTools.Features.Checklist.CharacterChecklist;
using LostArkTools.Features.Checklist.RosterChecklist;
using LostArkTools.Features.Shared;
using LostArkTools.Services;
using MahApps.Metro.IconPacks;
using StyletIoC;

namespace LostArkTools.Features.Checklist;

public class ChecklistViewModel : FeatureScreenBase
{
    public RosterChecklistViewModel RosterChecklistVm { get; }
    public CharacterViewModel CharacterVm { get; }

    public ChecklistViewModel(RosterChecklistViewModel rosterChecklistVm, CharacterViewModel characterVm, IContainer container) 
        : base("Checklist", PackIconBoxIconsKind.SolidCalendarCheck, int.MinValue)
    {
        var timeService = container.Get<TimeService>();
        timeService.DailyReset += DailyReset;
        timeService.WeeklyReset += WeeklyReset;
        RosterChecklistVm = rosterChecklistVm;
        CharacterVm = characterVm;
        var checkListService = container.GetStorageService<ChecklistDataService>();
        if (!timeService.HasResetPassedSinceLastLaunch(checkListService.GetLastOpened())) return;
        if(timeService.IsWeeklyResetDay) 
            WeeklyReset();
        DailyReset();
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
}