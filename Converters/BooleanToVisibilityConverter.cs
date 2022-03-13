using System.Windows;

namespace LostArkChecklist.Converters;

public class BooleanToVisibilityConverter : BooleanConverterBase<Visibility>
{
    public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Hidden)
    {
    }
}