using System;
using System.Globalization;
using System.Windows.Data;
using LostArkTools.Extensions;
using LostArkTools.Misc;

namespace LostArkTools.Converters;

public class ChanceToPercentStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
        value is Chance chance ? $"{chance.AsDouble():0}%" : string.Empty;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
        throw new NotImplementedException();
}