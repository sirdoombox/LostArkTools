using System;
using System.IO;
using LostArkTools.Models;
using Newtonsoft.Json;

namespace LostArkTools.Services.Base;

public abstract class LocalStorageServiceBase<T> : ILocalStorageService
{
    private static readonly string DataDir;
    
    protected T? Data { get; set; }
    
    private readonly string _dataPath;
    

    static LocalStorageServiceBase()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        DataDir = Path.Combine(appData, "LostArkTools");
        Directory.CreateDirectory(DataDir);
    }
    
    public LocalStorageServiceBase(string fileName)
    {
        _dataPath = Path.Combine(DataDir, $"{fileName}.dat");
    }

    public void Save()
    {
        File.WriteAllText(_dataPath, JsonConvert.SerializeObject(Data));
    }
    
    public void Load()
    {
        if (!File.Exists(_dataPath))
        {
            OnLoadFailed();
            return;
        }
        var rawDat = File.ReadAllText(_dataPath);
        Data = JsonConvert.DeserializeObject<T>(rawDat);
    }

    protected virtual void OnLoadFailed()
    {
        
    }
}