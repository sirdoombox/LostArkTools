using LostArkTools.Misc;

namespace LostArkTools.Features.Faceting;

public class RollStateViewModel : PropertyChangedBase
{
    private RollState _state;
    public RollState State
    {
        get => _state;
        set => SetAndNotify(ref _state, value);
    }

    public RollStateViewModel()
    {
        State = RollState.Unrolled;
    }
}