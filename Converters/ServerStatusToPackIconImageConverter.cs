using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using LostArkTools.Models;
using MahApps.Metro.IconPacks;

namespace LostArkTools.Converters;

public class ServerStatusToPackIconImageConverter : IValueConverter
{
    /// <summary>
    /// Gets or sets the thickness to draw the icon with.
    /// </summary>
    public double Thickness { get; set; } = 0.25;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not ServerStatus.Value serverStatus) return null;
        
        var geoDrawing = new GeometryDrawing();
        var icon = PackIconBoxIconsKind.None;

        switch (serverStatus)
        {
            case ServerStatus.Value.Good:
                icon = PackIconBoxIconsKind.RegularCheck;
                geoDrawing.Brush = Brushes.LawnGreen;
                break;
            case ServerStatus.Value.Busy:
                icon = PackIconBoxIconsKind.RegularStopwatch;
                geoDrawing.Brush = Brushes.Yellow;
                break;
            case ServerStatus.Value.Maintenance:
                icon = PackIconBoxIconsKind.RegularWrench;
                geoDrawing.Brush = Brushes.Red;
                break;
        }

        geoDrawing.Pen = new Pen(geoDrawing.Brush, Thickness);
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