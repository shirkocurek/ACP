using SQLite;

namespace c971_mobile_application_development_using_c_sharp.Models;

public partial class Course
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }

    [Indexed] public int TermId { get; set; }

    [MaxLength(120), NotNull] public string Title { get; set; } = "";

    public DateTime StartDate { get; set; }
    public DateTime EndDate   { get; set; }
    public string Status { get; set; } = "Planned";
    public string? InstructorName  { get; set; }
    public string? InstructorPhone { get; set; }
    public string? InstructorEmail { get; set; }
    public string? Notes { get; set; }

    // For later (notifications)
    public bool NotifyOnStart { get; set; } = false;
    public bool NotifyOnEnd   { get; set; } = false;
}
