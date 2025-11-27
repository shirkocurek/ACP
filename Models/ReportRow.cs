namespace c971_mobile_application_development_using_c_sharp.Models;

public class ReportRow
{
    public string Kind { get; set; } = "";
    public string Title { get; set; } = "";
    public string Parent { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = "";
}
