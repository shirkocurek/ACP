using SQLite;

namespace c971_mobile_application_development_using_c_sharp.Models;

public class Term
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [MaxLength(100), NotNull]  public string Title { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate   { get; set; }
}
