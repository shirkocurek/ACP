using SQLite;

namespace c971_mobile_application_development_using_c_sharp.Models;

public class Assessment
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }

    [Indexed] public int CourseId { get; set; }

    [MaxLength(120), NotNull] public string Title { get; set; } = "";

    [NotNull] public string Type { get; set; } = "Objective";

    public DateTime StartDate { get; set; }
    public DateTime EndDate   { get; set; }

    public bool NotifyStart { get; set; }
    public bool NotifyEnd   { get; set; }
}
