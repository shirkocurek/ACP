using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Services;

namespace c971_mobile_application_development_using_c_sharp.ViewModels;

public partial class CoursesViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    public ObservableCollection<Course> Courses { get; } = new();
    private List<Course> _allCourses = new();

    [ObservableProperty] private bool isBusy;
    [ObservableProperty] private string? searchText;

    public CoursesViewModel(DatabaseService db) => _db = db;

    [RelayCommand]
    public async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            _allCourses = await _db.GetAllCoursesAsync();
            ApplyFilter();
        }
        finally
        {
            IsBusy = false;
        }
    }

    partial void OnSearchTextChanged(string? value) => ApplyFilter();

    private void ApplyFilter()
    {
        var query = (SearchText ?? string.Empty).Trim();
        IEnumerable<Course> filtered = _allCourses;

        if (!string.IsNullOrEmpty(query))
        {
            var q = query.ToLowerInvariant();
            filtered = _allCourses.Where(c =>
                (c.Title ?? string.Empty).ToLowerInvariant().Contains(q)
                || (c.Status ?? string.Empty).ToLowerInvariant().Contains(q)
                || (c.InstructorName ?? string.Empty).ToLowerInvariant().Contains(q)
                || (c.InstructorEmail ?? string.Empty).ToLowerInvariant().Contains(q)
                || c.StartDate.ToString("d").ToLowerInvariant().Contains(q)
                || c.EndDate.ToString("d").ToLowerInvariant().Contains(q));
        }

        Courses.Clear();
        foreach (var c in filtered.OrderBy(c => c.StartDate))
            Courses.Add(c);
    }

    [RelayCommand]
    public async Task ViewAsync(Course? c)
    {
        if (c is null) return;
        await Shell.Current.GoToAsync(nameof(Pages.CourseDetailPage), true, new() { { "Course", c } });
    }
}

