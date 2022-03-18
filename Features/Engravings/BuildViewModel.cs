using System.Linq;
using LostArkTools.Models;

namespace LostArkTools.Features.Engravings;

public class BuildViewModel : PropertyChangedBase
{
    public string BuildName { get; }
    public HighlightableStringViewModel Primary { get; }
    public HighlightableStringViewModel Secondary { get; }
    public BindableCollection<HighlightableStringViewModel> Engravings { get; } = new();

    public int HighlightedWeight => Engravings.Select(x => x.Weight).Sum()
                                    + Primary.Weight
                                    + Secondary.Weight;

    public BuildViewModel(Build build)
    {
        BuildName = build.Name;
        Primary = new(build.PrimaryStat,11);
        Secondary = new(build.SecondaryStat,10);
        var i = 10;
        foreach (var value in build.Engravings)
        {
            Engravings.Add(new(value, i--));
        }
    }
}