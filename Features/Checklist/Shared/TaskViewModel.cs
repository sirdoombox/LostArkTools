using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using LostArkTools.Models;
using MahApps.Metro.IconPacks;

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
            SetAndNotify(ref _isComplete, value);
            _item.Completed = value;
            OnStatusChanged?.Invoke();
        }
    }

    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set => SetAndNotify(ref _isEditMode, value);
    }
    
    public Action OnStatusChanged { get; set; }
    public Action<bool> OnEditModeChanged { get; set; }
    public Action OnRequestDelete { get; set; }

    private readonly ChecklistItem _item;

    public TaskViewModel(ChecklistItem item)
    {
        _item = item;
        TaskTitle = item.Title;
        TaskNote = item.Note;
        IsComplete = item.Completed;
        OnStatusChanged = () => { };
        OnEditModeChanged = _ => { };
        OnRequestDelete = () => { };
    }

    public void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (IsEditMode) return;
        switch (e.ChangedButton)
        {
            case MouseButton.Left:
                IsComplete = !IsComplete;
                break;
            case MouseButton.Right:
                IsEditMode = true;
                OnEditModeChanged(IsEditMode);
                e.Handled = true;
                break;
        }
    }
    
    public void TextBoxKeyPressed(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        ExitEditMode();
        Window.GetWindow((DependencyObject)sender)?.Focus();
    }

    public void DeleteTask() => OnRequestDelete();

    public void ExitEditMode() => OnEditModeChanged(false);
}