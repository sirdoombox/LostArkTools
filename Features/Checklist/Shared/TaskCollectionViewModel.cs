using System.Linq;
using LostArkTools.Models;

namespace LostArkTools.Features.Checklist.Shared;

public class TaskCollectionViewModel : Screen
{
    public BindableCollection<TaskViewModel> Tasks { get; } = new();

    public int TaskCount => Tasks.Count;
    public int CompletedCount => Tasks.Count(x => x.IsComplete);

    private TaskViewModel _currentEditedTask;
    private List<ChecklistItem> _items;
    
    public void Populate(List<ChecklistItem> items)
    {
        _items = items;
        foreach (var task in items)
            AddNewTask(task);
    }

    public void AddNewTask(ChecklistItem task)
    {
        var newTask = new TaskViewModel(task);
        newTask.OnEditModeChanged += isEditMode =>
        {
            if (_currentEditedTask != null)
                _currentEditedTask.IsEditMode = false;
            _currentEditedTask = isEditMode ? newTask : null;
        };
        newTask.OnRequestDelete += () =>
        {
            _items.Remove(task);
            _currentEditedTask.IsEditMode = false;
            Tasks.Remove(_currentEditedTask);
            _currentEditedTask = null;
        };
        newTask.OnStatusChanged += () =>
        {
            NotifyOfPropertyChange(nameof(TaskCount));
            NotifyOfPropertyChange(nameof(CompletedCount));
        };
        Tasks.Add(newTask);
    }
    
    public void Reset()
    {
        foreach (var task in Tasks)
            task.IsComplete = false;
    }
}