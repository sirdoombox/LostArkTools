using System;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;
using LostArkTools.Misc;

namespace LostArkTools.Converters;

public class RollStateToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not RollState state) throw new ArgumentException("Incorrect converter.");
        return state switch
        {
            RollState.Unrolled => Brushes.Gray,
            RollState.Success => Brushes.Blue,
            RollState.Failure => Brushes.Red,
            _ => throw new Exception("Out Of Range.")
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
        throw new NotImplementedException();
}