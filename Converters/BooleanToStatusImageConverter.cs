using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MahApps.Metro.IconPacks;

namespace LostArkTools.Converters;

public class BooleanToStatusImageConverter : IValueConverter
{
    /// <summary>
    /// Gets or sets the thickness to draw the icon with.
    /// </summary>
    public double Thickness { get; set; } = 0.25;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool isTrue) return null;

        var geoDrawing = new GeometryDrawing
        {
            Brush = isTrue ? Brushes.LawnGreen : Brushes.Red
        };
        geoDrawing.Pen = new Pen(geoDrawing.Brush, Thickness);
        var icon = isTrue ? PackIconBoxIconsKind.RegularCheckCircle : PackIconBoxIconsKind.RegularXCircle;
        var pathData = string.Empty;
        PackIconBoxIconsDataFactory.DataIndex.Value?.TryGetValue(icon, out pathData);
        geoDrawing.Geometry = Geometry.Parse(pathData);
        var drawingGroup = new DrawingGroup { Children = { geoDrawing }, Transform = new ScaleTransform(1, -1) };
        return new DrawingImage { Drawing = drawingGroup };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}