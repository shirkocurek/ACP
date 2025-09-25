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

    public CoursesViewModel(DatabaseService db) => _db = db;

    public async Task LoadAsync()
    {
        Courses.Clear();
        foreach (var c in await _db.GetAllCoursesAsync())
            Courses.Add(c);
    }

    [RelayCommand]
    public async Task ViewAsync(Course? c)
    {
        if (c is null) return;
        await Shell.Current.GoToAsync(nameof(Pages.CourseDetailPage), true, new() { { "Course", c } });
    }
}
