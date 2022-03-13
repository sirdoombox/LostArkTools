using System;
using System.IO;
using LostArkChecklist.Models;
using Newtonsoft.Json;

namespace LostArkChecklist.Services;

public class UserDataService
{
    private readonly string _dataPath;
    public UserData? CurrentData { get; private set; }

    public UserDataService()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _dataPath = Path.Combine(appData, "LostArkChecklist", "UserData.dat");
    }
    
    public void SaveUserData()
    {
        if (CurrentData is null) return;
        File.WriteAllText(_dataPath, JsonConvert.SerializeObject(CurrentData));
    }

    public void LoadUserData()
    {
        if (!File.Exists(_dataPath))
            CurrentData = UserData.Default;
        else
            CurrentData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(_dataPath)) 
                        ?? UserData.Default;
    }
}