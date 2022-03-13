namespace LostArkChecklist.ViewModels;

public class TaskListViewModel : Screen
{
    public BindableCollection<ChecklistItemViewModel> Tasks { get; } = new();

    public void Reset()
    {
        foreach (var task in Tasks)
            task.IsComplete = false;
    }
}