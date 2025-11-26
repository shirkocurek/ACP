using SQLite;

namespace c971_mobile_application_development_using_c_sharp.Models;

public partial class Assessment
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int CourseId { get; set; }

    // "Objective" or "Performance"
    public string Type { get; set; } = "Objective";

    public string Title { get; set; } = string.Empty;

    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate   { get; set; } = DateTime.Today.AddDays(7);

    public string Status { get; set; } = "Planned";

    public bool NotifyOnStart { get; set; } = false;
    public bool NotifyOnEnd   { get; set; } = false;
}
