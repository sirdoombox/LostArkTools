namespace LostArkTools.Features.Config;

public class ContributionViewModel : PropertyChangedBase
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Url { get; init; }

    public static readonly IReadOnlyList<ContributionViewModel> Libraries = new List<ContributionViewModel>()
    {
        new()
        {
            Name = "AngleSharp",
            Description = "Web Parsing Library",
            Url = "https://github.com/AngleSharp/AngleSharp"
        },
        new()
        {
            Name = "MahApps",
            Description = "UI Theme + Functionality",
            Url = "https://github.com/MahApps/MahApps.Metro"
        },
        new()
        {
            Name = "Stylet",
            Description = "MVVM Library",
            Url = "https://github.com/canton7/Stylet"
        },
        new()
        {
            Name = "FluentScheduler",
            Description = "Timers + Scheduling",
            Url = "https://github.com/fluentscheduler/FluentScheduler"
        }
    };
}