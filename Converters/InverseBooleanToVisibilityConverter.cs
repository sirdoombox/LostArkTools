using System.Windows;

namespace LostArkTools.Converters;

public class InverseBooleanToVisibilityConverter : BooleanConverterBase<Visibility>
{
    public InverseBooleanToVisibilityConverter() : base(Visibility.Hidden, Visibility.Visible)
    {
    }
}