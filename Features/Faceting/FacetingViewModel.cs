using System;
using System.Linq;
using System.Threading.Tasks;
using LostArkTools.Extensions;
using LostArkTools.Features.Shared;
using LostArkTools.Misc;
using LostArkTools.Models.Faceting;
using LostArkTools.Services;
using MahApps.Metro.IconPacks;

namespace LostArkTools.Features.Faceting;

public class FacetingViewModel : FeatureScreenBase
{
    public List<RowViewModel> RowViews { get; } = new();
    public List<Chance> Chances { get; } = new();
    public List<byte> SlotChoices { get; } = new();

    private Chance _selectedChance;

    public Chance SelectedChance
    {
        get => _selectedChance;
        set => SetAndNotify(ref _selectedChance, value);
    }

    private byte _selectedSlots;

    public byte SelectedSlots
    {
        get => _selectedSlots;
        set
        {
            foreach (var row in RowViews)
                row.UpdateSlotCount(value);
            SetAndNotify(ref _selectedSlots, value);
        }
    }

    public WeightViewModel FirstWeight { get; } = new(1.2, 0.0);
    public WeightViewModel SecondWeight { get; } = new(1.0, 0.0);
    public WeightViewModel NegativeWeight { get; } = new(-1.0, 0.0);

    private readonly FacetingSimulatorService _faceting;
    private readonly GameState _gameState;
    private readonly Scoring _scoring;

    public FacetingViewModel(FacetingSimulatorService faceting)
        : base("Faceting", PackIconBoxIconsKind.SolidDiamond, 2)
    {
        _faceting = faceting;
        _scoring = new Scoring();
        SlotChoices.AddRange(Enumerable.Range(1, 20).Select(i => (byte)i));
        SelectedSlots = 8;
        RowViews.AddRange(Enumerable.Range(0, 3).Select(_ => new RowViewModel(this)));
        Chances.AddRange(EnumExtensions.All<Chance>());
        SelectedChance = Chances.Last();
        _gameState = new GameState
        {
            CurrChance = SelectedChance,
            NumSlots = SelectedSlots,
            Rows = RowViews.Select(x => x.RowState.ToList()).ToArray()
        };
    }

    protected override async void OnInitialActivate()
    {
        base.OnInitialActivate();
        await RebuildOptimal();
    }

    private async Task RebuildOptimal()
    {
        _scoring.Success[0] = FirstWeight.Success;
        _scoring.Success[1] = SecondWeight.Success;
        _scoring.Success[2] = NegativeWeight.Success;
        _scoring.Fail[0] = FirstWeight.Failure;
        _scoring.Fail[1] = SecondWeight.Failure;
        _scoring.Fail[2] = NegativeWeight.Failure;
        await _faceting.BuildOptimalLookup(_scoring, SelectedSlots);
    }

    protected override async void OnPropertyChanged(string propertyName)
    {
        base.OnPropertyChanged(propertyName);
        await UpdateGameState();
    }

    public async Task UpdateGameState()
    {
        if (_faceting is null || _gameState is null) return;
        if (!_faceting.IsBuilt) await RebuildOptimal();
        _gameState.Rows = RowViews.Select(x => x.RowState.ToList()).ToArray();
        _gameState.CurrChance = SelectedChance;
        _gameState.NumSlots = SelectedSlots;
        var res = await _faceting.SortedChoices(_gameState);
        var suggested = res.First();
        foreach (var row in RowViews)
            row.IsNextSuggested = false;
        RowViews[suggested.Index].IsNextSuggested = true;
    }

    public async void Reset()
    {
        foreach(var row in RowViews)
            row.Reset();
        SelectedChance = Chances.Last();
        await UpdateGameState();
    }
}