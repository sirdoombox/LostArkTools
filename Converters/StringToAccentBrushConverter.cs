using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using ControlzEx.Theming;

namespace LostArkTools.Converters;

public class StringToAccentBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is string accentName ? ThemeManager.Current.Themes.First(x => x.ColorScheme == accentName).ShowcaseBrush : Brushes.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}