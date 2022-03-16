using LostArkTools.Services;

namespace LostArkTools.Models;

public class AppSettings
{
    public string Theme { get; set; } = string.Empty;
    public string Accent { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    
    public static AppSettings Default => new()
    {
        Theme = "Dark",
        Accent = "Orange",
        Region = "EU Central/West"
    };
}