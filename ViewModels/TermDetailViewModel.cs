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
    public ObservableCollection<Course> Courses { get; } = new();

    [ObservableProperty] private bool isBusy;

    public TermDetailViewModel(DatabaseService db) => _db = db;

    public void SetTerm(Term t) => Term = t;

    [RelayCommand]
    public async Task LoadAsync()
    {
        if (IsBusy || Term.Id == 0) return;
        IsBusy = true;
        try
        {
            Courses.Clear();
            foreach (var c in await _db.GetCoursesForTermAsync(Term.Id))
                Courses.Add(c);
            OnPropertyChanged(nameof(CourseCountText));
        }
        finally { IsBusy = false; }
    }

    public string CourseCountText => $"{Courses.Count}/6 Courses";

    [RelayCommand]
    public async Task AddCourseAsync()
    {
        var newCourse = new Course
        {
            TermId = Term.Id,
            Title = "",
            StartDate = Term.StartDate,
            EndDate   = Term.EndDate
        };
        await Shell.Current.GoToAsync(nameof(Pages.CourseEditPage), true,
            new() { { "Course", newCourse }, { "IsNew", true } });
    }

    [RelayCommand]
    public async Task EditTermAsync()
    {
        await Shell.Current.GoToAsync(nameof(Pages.TermEditPage), true,
            new() { { "Term", new Term { Id = Term.Id, Title = Term.Title, StartDate = Term.StartDate, EndDate = Term.EndDate } },
                    { "IsNew", false } });
    }

    [RelayCommand]
    public async Task EditCourseAsync(Course? course)
    {
        if (course is null) return;
        await Shell.Current.GoToAsync(nameof(Pages.CourseEditPage), true,
            new() { { "Course", course }, { "IsNew", false } });
    }
}
