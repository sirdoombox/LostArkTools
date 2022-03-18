using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace LostArkTools.Services;

public class ResourceService
{
    public T LoadJsonResourceAs<T>(string resourceName)
    {
        var resourceData = GetResourceAsString($"{resourceName}.json");
        return JsonConvert.DeserializeObject<T>(resourceData) 
               ?? throw new Exception($"Resource {resourceName} does not exist or cannot be deserialised.");
    }

    private static string GetResourceAsString(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var fullResName = assembly.GetManifestResourceNames()
            .Single(str => str.EndsWith(resourceName));
        using var stream = assembly.GetManifestResourceStream(fullResName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}