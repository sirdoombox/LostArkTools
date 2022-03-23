using System;
using LostArkTools.Misc;

namespace LostArkTools.Extensions;

public static class EnumExtensions
{
    public static T Up<T>(this T src) where T : struct
    {
        var arr = (T[])Enum.GetValues(src.GetType());
        var j = Array.IndexOf(arr, src) + 1;
        return (arr.Length==j) ? src : arr[j];            
    }
        
    public static T Down<T>(this T src) where T : struct
    {
        var arr = (T[])Enum.GetValues(src.GetType());
        var j = Array.IndexOf(arr, src) - 1;
        return (j <= 0) ? src : arr[j];            
    }
        
    public static T[] All<T>() where T : struct
    {
        return (T[])Enum.GetValues(typeof(T));
    }

    public static double AsDouble(this Chance val) => val switch
    {
        Chance.SeventyFive => .75,
        Chance.SixtyFive => .65,
        Chance.FiftyFive => .55,
        Chance.FortyFive => .45,
        Chance.ThirtyFive => .35,
        Chance.TwentyFive => .25,
        _ => throw new ArgumentOutOfRangeException(nameof(val), val, null)
    };
}