using CollegeScheduler.Core;
using FluentAssertions;
using Xunit;

public class SearchUtilsTests
{
    private static readonly CourseDto[] Data =
    [
        new(1, "Algorithms", "Planned", "Ada", new(2025,1,1), new(2025,3,1)),
        new(2, "Databases", "In Progress", "Edgar", new(2025,2,1), new(2025,4,1)),
        new(3, "Mobile Dev", "Completed", "Grace", new(2024,9,1), new(2024,12,1)),
    ];

    [Fact]
    public void EmptyQuery_ReturnsAll()
    {
        var result = SearchUtils.FilterCourses(Data, "");
        result.Should().HaveCount(3);
    }

    [Fact]
    public void Query_MatchesTitleOrStatusOrInstructor()
    {
        SearchUtils.FilterCourses(Data, "algo").Should().ContainSingle(c => c.Title == "Algorithms");
        SearchUtils.FilterCourses(Data, "progress").Should().ContainSingle(c => c.Title == "Databases");
        SearchUtils.FilterCourses(Data, "grace").Should().ContainSingle(c => c.Title == "Mobile Dev");
    }

    [Fact]
    public void Query_NoMatches_ReturnsEmpty()
    {
        SearchUtils.FilterCourses(Data, "zzz").Should().BeEmpty();
    }
}
