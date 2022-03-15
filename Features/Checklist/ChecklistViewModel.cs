using LostArkChecklist.Features.Checklist.CharacterChecklist;
using LostArkChecklist.Features.Checklist.RosterChecklist;
using LostArkChecklist.Features.Shared;
using LostArkChecklist.Services;
using MahApps.Metro.IconPacks;

namespace LostArkChecklist.Features.Checklist;

public class ChecklistViewModel : FeatureScreenBase
{
    public RosterChecklistViewModel RosterChecklistVm { get; }
    public CharacterViewModel CharacterVm { get; }

    public ChecklistViewModel(RosterChecklistViewModel rosterChecklistVm, CharacterViewModel characterVm, TimeService timeService) 
        : base("Checklist", PackIconBoxIconsKind.SolidCalendarCheck, int.MinValue)
    {
        timeService.DailyReset += DailyReset;
        timeService.WeeklyReset += WeeklyReset;
        RosterChecklistVm = rosterChecklistVm;
        CharacterVm = characterVm;
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