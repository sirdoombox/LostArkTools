using LostArkChecklist.Features.Checklist;
using LostArkChecklist.Features.Config;

namespace LostArkChecklist.Features.Root;

public class ChecklistRootViewModel : Conductor<IScreen>
{
    private readonly ChecklistViewModel _checklistVm;
    private readonly ConfigViewModel _configVm;
    
    public ChecklistRootViewModel(ChecklistViewModel checkListVm, ConfigViewModel configVm)
    {
        _checklistVm = checkListVm;
        _configVm = configVm;
        ActiveItem = _checklistVm;
    }

    public void ToggleConfig()
    {
        ActiveItem = ActiveItem == _configVm ? _checklistVm : _configVm;
    }
}