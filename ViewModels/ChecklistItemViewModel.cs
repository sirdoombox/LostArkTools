namespace LostArkChecklist.ViewModels;

public class ChecklistItemViewModel : PropertyChangedBase
{
    private string _taskTitle = "New Task";
    public string TaskTitle
    {
        get => _taskTitle;
        set => SetAndNotify(ref _taskTitle, value);
    }
    
    private bool _isComplete;
    public bool IsComplete
    {
        get => _isComplete;
        set => SetAndNotify(ref _isComplete, value);
    }
}