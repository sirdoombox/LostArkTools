using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LostArkTools.Controls;

public partial class HighlightableTextBlock : TextBlock
{
    public static readonly DependencyProperty IsHighlightedProperty = DependencyProperty.Register(
        "IsHighlighted", typeof(bool), typeof(HighlightableTextBlock), new PropertyMetadata(default(bool)));
    public bool IsHighlighted
    {
        get => (bool)GetValue(IsHighlightedProperty);
        set => SetValue(IsHighlightedProperty, value);
    }

    public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register(
        "HighlightBrush", typeof(Brush), typeof(HighlightableTextBlock), new PropertyMetadata(default(Brush)));
    public Brush HighlightBrush
    {
        get => (Brush)GetValue(HighlightBrushProperty);
        set => SetValue(HighlightBrushProperty, value);
    }

    public static readonly DependencyProperty BoldOnHighlightProperty = DependencyProperty.Register(
        "BoldOnHighlight", typeof(bool), typeof(HighlightableTextBlock), new PropertyMetadata(default(bool)));

    public bool BoldOnHighlight
    {
        get => (bool)GetValue(BoldOnHighlightProperty);
        set => SetValue(BoldOnHighlightProperty, value);
    }
    
    public HighlightableTextBlock()
    {
        InitializeComponent();
    }
}