using System.Linq;
using LostArkChecklist.Models;

namespace LostArkChecklist.Features.Checklist.Shared;

public class ChecklistCollectionViewModel : Screen
{
    public BindableCollection<ChecklistItemViewModel> Tasks { get; } = new();

    public void Populate(IEnumerable<ChecklistItem> items)
    {
        Tasks.AddRange(items.Select(x => new ChecklistItemViewModel(x)));
    }
    
    public void Reset()
    {
        foreach (var task in Tasks)
            task.IsComplete = false;
    }
}