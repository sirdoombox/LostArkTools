namespace LostArkChecklist.ViewModels;

public class CharacterChecklistViewModel : Screen
{
    private string _characterName = "New Character";
    public string CharacterName
    {
        get => _characterName;
        set => SetAndNotify(ref _characterName, value);
    }
}