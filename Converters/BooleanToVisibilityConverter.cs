using System.Windows;

namespace LostArkTools.Converters;

public class BooleanToVisibilityConverter : BooleanConverterBase<Visibility>
{
    public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Hidden)
    {
    }
}