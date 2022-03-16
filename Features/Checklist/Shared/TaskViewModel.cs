using System;
using System.Windows;
using System.Windows.Input;
using LostArkTools.Models;

namespace LostArkTools.Features.Checklist.Shared;

public class TaskViewModel : PropertyChangedBase
{
    private string _taskTitle = "New Task";
    public string TaskTitle
    {
        get => _taskTitle;
        set
        {
            _item.Title = value;
            SetAndNotify(ref _taskTitle, value);
        }
    }

    private string _taskNote = string.Empty;
    public string TaskNote
    {
        get => _taskNote;
        set
        {
            _item.Note = value;
            SetAndNotify(ref _taskNote, value);
        }
    }

    private bool _isComplete;
    public bool IsComplete
    {
        get => _isComplete;
        set
        {
            _item.Completed = value;
            SetAndNotify(ref _isComplete, value);
        }
    }
    
    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set => SetAndNotify(ref _isEditMode, value);
    }
    
    public Action<bool> OnEditModeChanged { get; set; }
    public Action OnRequestDelete { get; set; }

    private readonly ChecklistItem _item;

    public TaskViewModel(ChecklistItem item)
    {
        _item = item;
        TaskTitle = item.Title;
        TaskNote = item.Note;
        IsComplete = item.Completed;
        OnEditModeChanged = _ => { };
        OnRequestDelete = () => { };
    }

    public void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount < 2 || IsEditMode) return;
        IsEditMode = true;
        OnEditModeChanged.Invoke(IsEditMode);
    }
    
    public void TextBoxKeyPressed(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        ExitEditMode();
        Window.GetWindow((DependencyObject)sender)?.Focus();
    }

    public void DeleteTask() => OnRequestDelete.Invoke();

    public void ExitEditMode() => OnEditModeChanged.Invoke(false);
}