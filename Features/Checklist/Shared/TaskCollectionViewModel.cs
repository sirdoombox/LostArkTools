using System.Linq;
using LostArkChecklist.Models;

namespace LostArkChecklist.Features.Checklist.Shared;

public class TaskCollectionViewModel : Screen
{
    public BindableCollection<TaskViewModel> Tasks { get; } = new();

    public void Populate(IEnumerable<ChecklistItem> items)
    {
        Tasks.AddRange(items.Select(x => new TaskViewModel(x)));
    }
    
    public void Reset()
    {
        foreach (var task in Tasks)
            task.IsComplete = false;
    }
}