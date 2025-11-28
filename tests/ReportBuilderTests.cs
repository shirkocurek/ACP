using CollegeScheduler.Core;
using FluentAssertions;
using Xunit;

public class ReportBuilderTests
{
    [Fact]
    public void Build_ReportHasTitleTimestampAndRows()
    {
        var rows = new[]
        {
            new ReportRow("Term 1", "Algorithms", "Planned", new(2025,1,1), new(2025,3,1)),
            new ReportRow("Term 1", "Databases", "In Progress", new(2025,2,1), new(2025,4,1)),
        };

        var report = ReportBuilder.Build("Courses Overview", rows);

        report.Title.Should().Be("Courses Overview");
        report.GeneratedAtUtc.Should().BeOnOrAfter(DateTime.UtcNow.AddMinutes(-1));
        report.Rows.Should().HaveCount(2);
        report.Rows[0].TermTitle.Should().Be("Term 1");
        report.Rows[0].CourseTitle.Should().Be("Algorithms");
    }
}
