using System.Windows;

namespace LostArkChecklist.Converters;

public class InverseBooleanToVisibilityConverter : BooleanConverterBase<Visibility>
{
    public InverseBooleanToVisibilityConverter() : base(Visibility.Hidden, Visibility.Visible)
    {
    }
}