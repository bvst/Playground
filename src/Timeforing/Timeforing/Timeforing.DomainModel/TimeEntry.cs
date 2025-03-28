namespace Timeforing.Model;

public class TimeEntry
{
    public int Id { get; set; }
    public string Employee { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Hours { get; set; }
    public string Project { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}