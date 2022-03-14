using System;
using System.Windows;
using System.Windows.Controls;

namespace LostArkChecklist.Controls;

public partial class ChecklistHeaderControl : Grid
{
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        "Header", typeof(string), typeof(ChecklistHeaderControl), new PropertyMetadata(default(string)));
    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
    
    public static readonly RoutedEvent OnAddClickedEvent =
        EventManager.RegisterRoutedEvent("OnAddClicked", RoutingStrategy.Bubble, typeof(EventHandler), typeof(ChecklistHeaderControl));
    public event RoutedEventHandler OnAddClicked
    {
        add => AddHandler(OnAddClickedEvent, value);
        remove => RemoveHandler(OnAddClickedEvent, value);
    }

    public ChecklistHeaderControl()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        RaiseEvent(new RoutedEventArgs(OnAddClickedEvent));
    }
}