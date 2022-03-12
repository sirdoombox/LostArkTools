namespace LostArkChecklist.ViewModels;

public class TaskListViewModel : Screen
{
    private static readonly string[] _weeklyDefaults =
    {
        "Abyss Dungeon 1",
        "Abyss Dungeon 2",
        "Una Weekly 1",
        "Una Weekly 2",
        "Una Weekly 3",
        "Merchant Ship Exchange",
        "Bloodstone Exchange",
        "Chaos Exchange"
    };

    private static readonly string[] _dailyDefaults =
    {
        "Chaos 1",
        "Chaos 2",
        "Guardian 1",
        "Guardian 2",
        "Una Daily 1",
        "Una Daily 2",
        "Una Daily 3",
        "Guild Support"
    };

    public BindableCollection<TaskViewModel> Tasks { get; } = new();

    public void Reset()
    {
        foreach (var task in Tasks)
            task.IsComplete = false;
    }

    public void DefaultWeekly()
    {
        Tasks.Add(new TaskViewModel { TaskTitle = "Abyss Dungeons" });
    }
}