using System.Windows;
using System.Windows.Controls;

namespace LostArkChecklist.Misc;

public class ClickOpensContextMenuBehaviour
{
    private static readonly DependencyProperty ClickOpensContextMenuProperty =
        DependencyProperty.RegisterAttached(
            "Enabled", typeof(bool), typeof(ClickOpensContextMenuBehaviour),
            new PropertyMetadata(HandlePropertyChanged)
        );

    public static bool GetEnabled(DependencyObject obj)
    {
        return (bool)obj.GetValue(ClickOpensContextMenuProperty);
    }

    public static void SetEnabled(DependencyObject obj, bool value)
    {
        obj.SetValue(ClickOpensContextMenuProperty, value);
    }

    private static void HandlePropertyChanged(
        DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        switch (obj)
        {
            case Button button:
                button.Click -= ExecuteClick;
                button.Click += ExecuteClick;
                break;
        }
    }

    private static void ExecuteClick(object sender, RoutedEventArgs args)
    {
        var obj = sender as DependencyObject;
        var enabled = (bool)obj.GetValue(ClickOpensContextMenuProperty);
        if (!enabled) return;
        if (sender is not Button { ContextMenu: { } } button) return;
        button.ContextMenu.PlacementTarget = button;
        button.ContextMenu.IsOpen = true;
    }
}