using System.Linq;
using System.Windows;
using System.Windows.Input;
using LostArkTools.Extensions;
using LostArkTools.Services;
using LostArkTools.Services.Base;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StyletIoC;

namespace LostArkTools.Features.Checklist.CharacterChecklist;

public class CharacterViewModel : Conductor<CharacterChecklistViewModel>.Collection.OneActive
{
    private bool _isInEditCharacterMode;

    public bool IsInEditCharacterMode
    {
        get => _isInEditCharacterMode;
        set => SetAndNotify(ref _isInEditCharacterMode, value);
    }

    private readonly ChecklistDataService _checklistDataService;

    public CharacterViewModel(IContainer container)
    {
        _checklistDataService = container.GetSpecificImplementation<ChecklistDataService, ILocalStorageService>();
        Items.AddRange(_checklistDataService.GetCharacters().Select(x => new CharacterChecklistViewModel(x)));
    }

    public void EnterEditMode() => IsInEditCharacterMode = true;

    public void AddNewCharacter()
    {
        var newChar = _checklistDataService.AddCharacter();
        var newVm = new CharacterChecklistViewModel(newChar);
        Items.Add(newVm);
        ActiveItem = newVm;
    }

    public async void DeleteCharacter()
    {
        var window = Application.Current.MainWindow as MetroWindow;
        if (await window.ShowMessageAsync("Confirm Character Deletion",
                $"Are you sure you want to delete {ActiveItem.CharacterName}?",
                MessageDialogStyle.AffirmativeAndNegative)
            is MessageDialogResult.Affirmative)
        {
            _checklistDataService.RemoveCharacter(ActiveItem.Character);
            Items.Remove(ActiveItem);
        }
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
        foreach (var character in Items)
            character.DailyReset();
    }

    public void ResetWeekly()
    {
        foreach (var character in Items)
            character.WeeklyReset();
    }
}