using Timeforing.Model;

namespace Timeforing.Services;

using System.Text;

public class CsvExportService
{
    public byte[] ExportToCsv(IEnumerable<TimeEntry> entries)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Employee,Date,Hours,Project,Description");

        foreach (var e in entries)
        {
            var line = $"{e.Employee},{e.Date:yyyy-MM-dd},{e.Hours},{Escape(e.Project)},{Escape(e.Description)}";
            sb.AppendLine(line);
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private string Escape(string value)
    {
        if (value.Contains(",") || value.Contains("\""))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }
        return value;
    }
}
