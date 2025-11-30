using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Services;

namespace c971_mobile_application_development_using_c_sharp.ViewModels;

public partial class AssessmentOverviewViewModel : ObservableObject
{
    private readonly DatabaseService _db;
    private readonly NotificationService _notify;

    [ObservableProperty] private Course course = new();
    [ObservableProperty] private Assessment? objective;
    [ObservableProperty] private Assessment? performance;

    public bool HasObjective => Objective != null;
    public bool HasPerformance => Performance != null;

    public AssessmentOverviewViewModel(DatabaseService db, NotificationService notify)
    {
        _db = db; _notify = notify;
    }

    public void SetCourse(Course c) => Course = c;

    public async Task LoadAsync()
    {
        var pair = await _db.GetPairForCourseAsync(Course.Id);
        Objective = pair.Objective;
        Performance = pair.Performance;
        OnPropertyChanged(nameof(HasObjective));
        OnPropertyChanged(nameof(HasPerformance));
    }

    [RelayCommand]
    private async Task EditObjectiveAsync() =>
        await EditTypeAsync("Objective", Objective);

    [RelayCommand]
    private async Task EditPerformanceAsync() =>
        await EditTypeAsync("Performance", Performance);

    private async Task EditTypeAsync(string type, Assessment? existing)
    {
        var model = existing ?? new Assessment
        {
            CourseId = Course.Id,
            Type = type,
            Title = $"{type} Assessment"
        };

        await Shell.Current.GoToAsync(nameof(Pages.AssessmentEditPage), true, new()
        {
            { "Assessment", model },
            { "IsNew", existing == null }
        });
    }

    [RelayCommand]
    private async Task DeleteObjectiveAsync() => await DeleteAsync(Objective);

    [RelayCommand]
    private async Task DeletePerformanceAsync() => await DeleteAsync(Performance);

    private async Task DeleteAsync(Assessment? a)
    {
        if (a == null) return;
        var ok = await App.Current.MainPage.DisplayAlert("Delete", $"Delete “{a.Title}”?", "Delete", "Cancel");
        if (!ok) return;

        await _db.DeleteAssessmentAsync(a);
        await _notify.CancelAssessmentNotificationsAsync(a);
        await LoadAsync();
    }
}
