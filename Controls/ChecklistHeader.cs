using System;
using System.Windows;
using System.Windows.Controls;

namespace LostArkTools.Controls;

[TemplatePart(Name = PART_AddButton, Type = typeof(Button))]
[TemplatePart(Name = PART_Header, Type = typeof(Label))]
public class ChecklistHeader : Control
{
    private const string PART_AddButton = nameof(PART_AddButton);
    private const string PART_Header = nameof(PART_Header);
    
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        "Header", typeof(string), typeof(ChecklistHeader), new PropertyMetadata(default(string)));
    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty TotalTasksProperty = DependencyProperty.Register(
        "TotalTasks", typeof(double), typeof(ChecklistHeader), new PropertyMetadata(default(double)));

    public double TotalTasks
    {
        get => (double)GetValue(TotalTasksProperty);
        set => SetValue(TotalTasksProperty, value);
    }

    public static readonly DependencyProperty TasksCompletedProperty = DependencyProperty.Register(
        "TasksCompleted", typeof(double), typeof(ChecklistHeader), new PropertyMetadata(default(double)));

    public double TasksCompleted
    {
        get => (double)GetValue(TasksCompletedProperty);
        set => SetValue(TasksCompletedProperty, value);
    }
    
    public static readonly RoutedEvent OnAddClickedEvent =
        EventManager.RegisterRoutedEvent("OnAddClicked", RoutingStrategy.Bubble, typeof(EventHandler), typeof(ChecklistHeader));
    public event RoutedEventHandler OnAddClicked
    {
        add => AddHandler(OnAddClickedEvent, value);
        remove => RemoveHandler(OnAddClickedEvent, value);
    }

    private Button _addButton;
    
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _addButton = GetTemplateChild(PART_AddButton) as Button;
        _addButton.Click += (o, e) => RaiseEvent(new RoutedEventArgs(OnAddClickedEvent));
    }

    static ChecklistHeader()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ChecklistHeader), new FrameworkPropertyMetadata(typeof(ChecklistHeader)));
    }
}