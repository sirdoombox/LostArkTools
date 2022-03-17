using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LostArkTools.Converters;

public class IsLastItemInContainerConverter : IValueConverter
{
    public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        var item = (DependencyObject)value;
        var ic = ItemsControl.ItemsControlFromItemContainer(item);
        var index = ic.ItemContainerGenerator.IndexFromContainer(item);
        return index == ic.Items.Count - 1;
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}