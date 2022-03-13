using System.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace LostArkChecklist.ViewModels;

public class CharacterViewModel : Conductor<CharacterChecklistViewModel>.Collection.OneActive
{
    private bool _isInEditCharacterMode;

    public bool IsInEditCharacterMode
    {
        get => _isInEditCharacterMode;
        set => SetAndNotify(ref _isInEditCharacterMode, value);
    }

    protected override void OnViewLoaded()
    {
        AddNewCharacter();
    }

    public void EnterEditMode() => IsInEditCharacterMode = true;

    public void AddNewCharacter()
    {
        var newChar = new CharacterChecklistViewModel();
        Items.Add(newChar);
        ActiveItem = newChar;
    }

    public async void DeleteCharacter()
    {
        var window = Application.Current.MainWindow as MetroWindow;
        if (await window.ShowMessageAsync("Confirm Character Deletion",
                $"Are you sure you want to delete {ActiveItem.CharacterName}?", 
                MessageDialogStyle.AffirmativeAndNegative)
            is MessageDialogResult.Affirmative)
            Items.Remove(ActiveItem);
        else
        {
            IsInEditCharacterMode = false;
            return;
        }
        if (Items.Count <= 0)
            AddNewCharacter();
        else
            ActiveItem = Items.First();
        IsInEditCharacterMode = false;
    }

    public void CharacterNameKeyPressed(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        IsInEditCharacterMode = false;
        Window.GetWindow((DependencyObject)sender)?.Focus();
    }

    public void ResetDaily()
    {
    }

    public void ResetWeekly()
    {
    }
}