namespace CollegeScheduler.Core;

public static class ReportBuilder
{
    public static Report Build(string title, IEnumerable<ReportRow> rows)
    {
        return new Report(
            Title: string.IsNullOrWhiteSpace(title) ? "Academic Report" : title.Trim(),
            GeneratedAtUtc: DateTime.UtcNow,
            Rows: rows.ToList()
        );
    }
}

public record Report(string Title, DateTime GeneratedAtUtc, List<ReportRow> Rows);

public record ReportRow(
    string TermTitle,
    string CourseTitle,
    string Status,
    DateTime StartDate,
    DateTime EndDate);
