namespace c971_mobile_application_development_using_c_sharp.Models
{
    public partial class Term : ISummarizable  //Inheritance
    {
        // Encapsulation
        public void SetDates(DateTime start, DateTime end)
        {
            if (end < start) throw new ArgumentException("EndDate must be on or after StartDate.");
            StartDate = start;
            EndDate   = end;
        }

        // Polymorphism
        public string GetSummary() => $"Term: {Title} | {StartDate:d} → {EndDate:d}";
    }
}
