namespace CollegeScheduler.Core;

public static class SearchUtils
{
    public static IEnumerable<CourseDto> FilterCourses(
        IEnumerable<CourseDto> source, string? query)
    {
        query = (query ?? string.Empty).Trim().ToLowerInvariant();
        if (string.IsNullOrEmpty(query)) return source;

        return source.Where(c =>
            (c.Title ?? "").ToLowerInvariant().Contains(query) ||
            (c.Status ?? "").ToLowerInvariant().Contains(query) ||
            (c.InstructorName ?? "").ToLowerInvariant().Contains(query));
    }
}

public record CourseDto(
    int Id,
    string Title,
    string Status,
    string? InstructorName,
    DateTime StartDate,
    DateTime EndDate);
