using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Services;

namespace c971_mobile_application_development_using_c_sharp.ViewModels;

public partial class TermDetailViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty] private Term term = new();
    [ObservableProperty] private bool isBusy;

    public ObservableCollection<CourseSummary> CourseSummaries { get; } = new();

    public TermDetailViewModel(DatabaseService db) => _db = db;

    public void SetTerm(Term t) => Term = t;

    [RelayCommand]
    public async Task LoadAsync()
    {
        if (IsBusy || Term.Id == 0) return;
        IsBusy = true;
        try
        {
            CourseSummaries.Clear();
            var courses = await _db.GetCoursesForTermAsync(Term.Id);
            foreach (var c in courses)
            {
                var (o, p) = await _db.GetAssessmentCountsAsync(c.Id);
                CourseSummaries.Add(new CourseSummary(c, o, p));
            }
        }
        finally { IsBusy = false; }
    }

    [RelayCommand] public async Task EditTermAsync() =>
        await Shell.Current.GoToAsync(nameof(Pages.TermEditPage), true,
            new() { { "Term", new Term { Id=Term.Id, Title=Term.Title, StartDate=Term.StartDate, EndDate=Term.EndDate } }, { "IsNew", false } });

    [RelayCommand]
    public async Task AddCourseAsync()
    {
        var newCourse = new Course { TermId = Term.Id, StartDate = Term.StartDate, EndDate = Term.EndDate };
        await Shell.Current.GoToAsync(nameof(Pages.CourseEditPage), true, new() { { "Course", newCourse }, { "IsNew", true } });
    }

    [RelayCommand]
    public async Task ViewCourseAsync(CourseSummary? row)
    {
        if (row is null) return;
        await Shell.Current.GoToAsync(nameof(Pages.CourseDetailPage), true, new() { { "Course", row.Course } });
    }

    [RelayCommand]
    public async Task EditCourseAsync(CourseSummary? row)
    {
        if (row is null) return;
        await Shell.Current.GoToAsync(nameof(Pages.CourseEditPage), true, new() { { "Course", row.Course }, { "IsNew", false } });
    }
}

public partial class CourseSummary : ObservableObject
{
    public Course Course { get; }
    public CourseSummary(Course c, int objectiveCount, int performanceCount)
    { Course = c; ObjectiveCount = objectiveCount; PerformanceCount = performanceCount; }

    public string Title => Course.Title;
    public DateTime StartDate => Course.StartDate;
    public DateTime EndDate   => Course.EndDate;
    public string Status => Course.Status;
    public string Instructor => Course.InstructorName ?? "";
    public int ObjectiveCount { get; }
    public int PerformanceCount { get; }
    public string AssessmentSummary => $"{ObjectiveCount} Objective, {PerformanceCount} Performance";
}

