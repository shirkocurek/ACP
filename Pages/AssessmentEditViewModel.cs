using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Services;

namespace c971_mobile_application_development_using_c_sharp.ViewModels;

public partial class AssessmentEditViewModel : ObservableObject
{
    private readonly DatabaseService _db;
    private readonly NotificationService _notify;

    [ObservableProperty] private Assessment assessment = new();
    [ObservableProperty] private bool isNew;
    [ObservableProperty] private string pageTitle = "Edit Assessment";
    [ObservableProperty] private string saveText = "Save Changes";
    [ObservableProperty] private bool showDelete = true;

    public string[] TypeOptions   { get; } = new[] { "Objective", "Performance" };
    public string[] StatusOptions { get; } = new[] { "Planned", "In Progress", "Completed" };

    public AssessmentEditViewModel(DatabaseService db, NotificationService notify)
    {
        _db = db; _notify = notify;
    }

    public void SetAssessment(Assessment a) => Assessment = a;

    public void Initialize(bool isNewFlag)
    {
        IsNew = isNewFlag;
        if (IsNew)
        {
            PageTitle = "Add Assessment";
            SaveText  = "Add Assessment";
            ShowDelete = false;
        }
        else
        {
            PageTitle = "Edit Assessment";
            SaveText  = "Save Changes";
            ShowDelete = true;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Assessment.Title))
        {
            await App.Current.MainPage.DisplayAlert("Required", "Enter an assessment title.", "OK");
            return;
        }
        if (Assessment.EndDate < Assessment.StartDate)
        {
            await App.Current.MainPage.DisplayAlert("Dates", "Due date must be after start date.", "OK");
            return;
        }

        await _db.SaveAssessmentAsync(Assessment);
        await _notify.ScheduleAssessmentNotificationsAsync(Assessment);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (IsNew || Assessment.Id == 0)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        var ok = await App.Current.MainPage.DisplayAlert("Delete", $"Delete “{Assessment.Title}”?", "Delete", "Cancel");
        if (!ok) return;

        await _db.DeleteAssessmentAsync(Assessment);
        await _notify.CancelAssessmentNotificationsAsync(Assessment);
        await Shell.Current.GoToAsync("..");
    }
}
