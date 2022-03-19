using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using LostArkTools.Features.Shared;
using LostArkTools.Models;
using LostArkTools.Services;
using MahApps.Metro.IconPacks;

namespace LostArkTools.Features.Engravings;

public class EngravingsViewModel : FeatureScreenBase
{
    public BindableCollection<string> Engravings { get; } = new();
    public BindableCollection<string> Stats { get; } = new();
    public BindableCollection<BuildViewModel> Builds { get; } = new();

    private bool _filteringEnabled;
    public bool FilteringEnabled
    {
        get => _filteringEnabled;
        set => SetAndNotify(ref _filteringEnabled, value);
    }
    
    private string _selectedEngravingOne;
    public string SelectedEngravingOne
    {
        get => _selectedEngravingOne;
        set => SetAndNotify(ref _selectedEngravingOne, value);
    }

    private string _selectedEngravingTwo;
    public string SelectedEngravingTwo
    {
        get => _selectedEngravingTwo;
        set => SetAndNotify(ref _selectedEngravingTwo, value);
    }
    
    private bool _includeStat;
    public bool IncludeStat
    {
        get => _includeStat;
        set => SetAndNotify(ref _includeStat, value);
    }

    private string _selectedStat;
    public string SelectedStat
    {
        get => _selectedStat;
        set => SetAndNotify(ref _selectedStat, value);
    }
    
    private readonly ICollectionView _buildsView;
    
    public EngravingsViewModel(ResourceService resources) : base("Engravings", PackIconBoxIconsKind.SolidSearch, 4)
    {
        Engravings.AddRange(resources.LoadJsonResourceAs<List<string>>("Engravings"));
        
        Stats.AddRange(resources.LoadJsonResourceAs<List<string>>("Stats"));
        
        var builds = resources.LoadJsonResourceAs<List<Build>>("Builds");
        Builds.AddRange(builds.Select(x => new BuildViewModel(x)));

        _buildsView = CollectionViewSource.GetDefaultView(Builds);
        _buildsView.Filter = FilterBuilds;
        _buildsView.SortDescriptions.Clear();
        _buildsView.SortDescriptions.Add(new SortDescription("HighlightedWeight", ListSortDirection.Descending));
        
        SelectedEngravingOne = Engravings.First();
        SelectedEngravingTwo = Engravings.Skip(1).First();

        SelectedStat = Stats.First();
        
        _buildsView.Refresh();
    }

    private bool FilterBuilds(object obj)
    {
        var build = (BuildViewModel)obj;
        
        // Reset highlights.
        foreach (var value in build.Engravings)
            value.IsHighlight = false;
        build.Primary.IsHighlight = build.Secondary.IsHighlight = false;

        if (!FilteringEnabled) return true;
        
        if (IncludeStat && !ProcessStat(build)) return false;
        
        var highlighted = build.Engravings.Where(x => x.Value == SelectedEngravingOne || x.Value == _selectedEngravingTwo);
        if (highlighted.Any())
        {
            foreach (var value in build.Engravings)
                value.IsHighlight = highlighted.Any(x => x == value);
            return true;
        }
        return false;
    }

    private bool ProcessStat(BuildViewModel build)
    {
        build.Primary.IsHighlight = build.Primary.Value == SelectedStat;
        build.Secondary.IsHighlight = build.Secondary.Value == SelectedStat;
        return build.Primary.IsHighlight || build.Secondary.IsHighlight;
    }

    protected override bool SetAndNotify<T>(ref T field, T value, string propertyName = "")
    {
        var res = base.SetAndNotify(ref field, value, propertyName);
        _buildsView?.Refresh();
        return res;
    }
}