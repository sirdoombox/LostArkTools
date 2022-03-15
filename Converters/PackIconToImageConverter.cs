using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MahApps.Metro.IconPacks;

namespace LostArkChecklist.Converters;

/// <summary>
/// Converts a <see cref="PackIcon{TKind}" /> to an DrawingImage.
/// Use the ConverterParameter to pass a Brush.
/// </summary>
public class PackIconToImageConverter : IValueConverter
{
    /// <summary>
    /// Gets or sets the thickness to draw the icon with.
    /// </summary>
    public double Thickness { get; set; } = 0.25;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        GeometryDrawing geoDrawing = new GeometryDrawing();

        geoDrawing.Brush = parameter as Brush ?? Brushes.Black;
        geoDrawing.Pen = new Pen(geoDrawing.Brush, Thickness);
        
        if (value is PackIconBoxIconsKind boxIcon)
        {
            var pathData = string.Empty;
            PackIconBoxIconsDataFactory.DataIndex.Value?.TryGetValue(boxIcon, out pathData);
            geoDrawing.Geometry = Geometry.Parse(pathData);
        }

        var drawingGroup = new DrawingGroup { Children = { geoDrawing }, Transform = new ScaleTransform(1, -1) };

        return new DrawingImage { Drawing = drawingGroup };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}