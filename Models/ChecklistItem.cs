namespace LostArkChecklist.Models;

public class ChecklistItem
{
    public string Title { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }

    public ChecklistItem()
    {
        Title = "New Checklist Item";
        Note = string.Empty;
    }

    public ChecklistItem(string title, string note)
    {
        Title = title;
        Note = note;
    }
}