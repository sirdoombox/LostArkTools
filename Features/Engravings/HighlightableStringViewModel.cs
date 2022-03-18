namespace LostArkTools.Features.Engravings;

public class HighlightableStringViewModel : PropertyChangedBase
{
    private string _value = string.Empty;
    public string Value
    {
        get => _value;
        set => SetAndNotify(ref _value, value);
    }

    private bool _isHighlight;
    public bool IsHighlight
    {
        get => _isHighlight;
        set => SetAndNotify(ref _isHighlight, value);
    }

    public int WeightValue { get; set; } = 1;

    public int Weight => IsHighlight ? WeightValue : 0;
    
    public HighlightableStringViewModel(string value, int weight)
    {
        Value = value;
        WeightValue = weight;
    }
}