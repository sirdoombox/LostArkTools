using System.Linq;
using LostArkTools.Extensions;
using LostArkTools.Misc;

namespace LostArkTools.Features.Faceting;

public class RowViewModel : Screen
{
    public BindableCollection<RollStateViewModel> RollStates { get; } = new(); 
    public List<bool> RowState { get; } = new();
    
    private bool _isNextSuggested;
    public bool IsNextSuggested
    {
        get => _isNextSuggested;
        set => SetAndNotify(ref _isNextSuggested, value);
    }

    private readonly FacetingViewModel _faceting;
    
    public RowViewModel(FacetingViewModel faceting)
    {
        _faceting = faceting;
        RollStates.AddRange(Enumerable.Range(0,_faceting.SelectedSlots).Select(x => new RollStateViewModel()));
    }

    public void UpdateSlotCount(int count)
    {
        RollStates.Clear();
        RollStates.AddRange(Enumerable.Range(0,_faceting.SelectedSlots).Select(x => new RollStateViewModel()));
        for (var i = 0; i < RowState.Count; i++)
            RollStates[i].State = RowState[i] ? RollState.Success : RollState.Failure;
    }

    public void Reset()
    {
        RowState.Clear();
        for (var i = 0; i < RollStates.Count; i++)
            RollStates[i].State = RollState.Unrolled;
    }

    public void RandomRoll()
    {
        RowState.Add(true);
    }

    public async void Success()
    {
        RowState.Add(true);
        RollStates[RowState.Count - 1].State = RollState.Success;
        _faceting.SelectedChance = _faceting.SelectedChance.Down();
        await _faceting.UpdateGameState();
    }

    public async void Failure()
    {
        RowState.Add(false);
        RollStates[RowState.Count - 1].State = RollState.Failure;
        _faceting.SelectedChance = _faceting.SelectedChance.Up();
        await _faceting.UpdateGameState();
    }
}