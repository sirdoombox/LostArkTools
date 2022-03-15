using System.Linq;
using LostArkTools.Models;

namespace LostArkTools.Features.Checklist.Shared;

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