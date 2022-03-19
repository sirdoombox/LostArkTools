using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LostArkTools.Controls;

[TemplatePart(Name = "PART_Border", Type = typeof(Border))]
[TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
public class HighlightableBorderedText : Control
{
    public static readonly DependencyProperty IsHighlightedProperty = DependencyProperty.Register(
        "IsHighlighted", typeof(bool), typeof(HighlightableBorderedText), new PropertyMetadata(default(bool)));
    public bool IsHighlighted
    {
        get => (bool)GetValue(IsHighlightedProperty);
        set => SetValue(IsHighlightedProperty, value);
    }

    public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register(
        "HighlightBrush", typeof(Brush), typeof(HighlightableBorderedText), new PropertyMetadata(default(Brush)));
    public Brush HighlightBrush
    {
        get => (Brush)GetValue(HighlightBrushProperty);
        set => SetValue(HighlightBrushProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        "Text", typeof(string), typeof(HighlightableBorderedText), new PropertyMetadata(default(string)));
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
        "TextAlignment", typeof(TextAlignment), typeof(HighlightableBorderedText), new PropertyMetadata(default(TextAlignment)));
    public TextAlignment TextAlignment
    {
        get => (TextAlignment)GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }

    static HighlightableBorderedText()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(HighlightableBorderedText), new FrameworkPropertyMetadata(typeof(HighlightableBorderedText)));
    }
}