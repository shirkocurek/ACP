using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Services;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace c971_mobile_application_development_using_c_sharp.ViewModels;

public partial class CourseDetailViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty] private Course course = new();
    public ObservableCollection<Assessment> Assessments { get; } = new();

    public CourseDetailViewModel(DatabaseService db) => _db = db;

    public void SetCourse(Course c) => Course = c;

    public async Task LoadAsync()
    {
        Assessments.Clear();
        foreach (var a in await _db.GetAssessmentsForCourseAsync(Course.Id))
            Assessments.Add(a);
        OnPropertyChanged(nameof(Objective));
        OnPropertyChanged(nameof(Performance));
    }

    public Assessment? Objective => Assessments.FirstOrDefault(a => a.Type == "Objective");
    public Assessment? Performance => Assessments.FirstOrDefault(a => a.Type == "Performance");

    [RelayCommand]
    public async Task EditCourseAsync() =>
        await Shell.Current.GoToAsync(nameof(Pages.CourseEditPage), true,
            new() { { "Course", Course }, { "IsNew", false } });

    [RelayCommand]
    public async Task ShareNotesAsync()
    {
        if (string.IsNullOrWhiteSpace(Course.Notes)) return;

            await Share.RequestAsync(new ShareTextRequest
            {
                Title = $"Notes: {Course.Title}",
                Text  = Course.Notes
            });
    }
}
