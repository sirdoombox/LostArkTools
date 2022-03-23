namespace LostArkTools.Features.Faceting;

public class WeightViewModel : PropertyChangedBase
{
    private double _success;
    public double Success
    {
        get => _success;
        set => SetAndNotify(ref _success, value);
    }
    
    private double _failure;
    public double Failure
    {
        get => _failure;
        set => SetAndNotify(ref _failure, value);
    }

    public WeightViewModel(double success, double failure)
    {
        Success = success;
        Failure = failure;
    }
}