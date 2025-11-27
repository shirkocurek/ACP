using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Services;
using System.Net.Mail;

namespace c971_mobile_application_development_using_c_sharp.ViewModels;

public partial class CourseEditViewModel : ObservableObject
{
    private readonly DatabaseService _db;
    private readonly NotificationService _notify;

    [ObservableProperty] private Course course = new();
    [ObservableProperty] private bool isNew;

    // honeypot field to catch bots
    [ObservableProperty] private string? honeypot;
    [ObservableProperty] private string pageTitle = "Edit Course";
    [ObservableProperty] private string saveText = "Save Changes";
    [ObservableProperty] private bool showDelete = true;

    public List<string> StatusOptions { get; } = new()
        { "Planned", "In Progress", "Completed", "Dropped" };

    public CourseEditViewModel(DatabaseService db, NotificationService notify)
    {
        _db = db;
        _notify = notify;
    }

    public void Initialize(bool isNewFlag)
    {
        IsNew = isNewFlag;
        if (IsNew)
        {
            PageTitle = "Add Course";
            SaveText = "Add Course";
            ShowDelete = false;

            if (string.IsNullOrWhiteSpace(Course.Status))
                Course.Status = "Planned";
            if (Course.StartDate == default) Course.StartDate = DateTime.Today;
            if (Course.EndDate == default) Course.EndDate = DateTime.Today.AddMonths(1);
        }
        else
        {
            PageTitle = "Edit Course";
            SaveText = "Save Changes";
            ShowDelete = true;
        }
    }

    [RelayCommand]
    public async Task SaveAsync()
    {

        // Honeypot check and alert
        if (!string.IsNullOrWhiteSpace(Honeypot))
        {
            await Alert("Error", "Something went wrong. Please try again.");
            return;
        }

        // validation
        if (string.IsNullOrWhiteSpace(Course.Title))
        { await Alert("Required", "Enter a course title."); return; }

        if (Course.EndDate < Course.StartDate)
        { await Alert("Dates", "End date cannot be before start date."); return; }

        if (string.IsNullOrWhiteSpace(Course.InstructorName) ||
            string.IsNullOrWhiteSpace(Course.InstructorPhone) ||
            string.IsNullOrWhiteSpace(Course.InstructorEmail))
        { await Alert("Instructor", "Name, phone, and email are required."); return; }

        if (!IsValidEmail(Course.InstructorEmail!))
        { await Alert("Email", "Enter a valid instructor email address."); return; }

        try
        {
            await _db.SaveCourseAsync(Course);
            await _notify.ScheduleCourseNotificationsAsync(Course);
            await Shell.Current.GoToAsync("..");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("6 courses"))
        {
            await Alert("Limit Reached", "This term already has 6 courses.");
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (IsNew || Course.Id == 0)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        var confirm = await App.Current.MainPage.DisplayAlert(
            "Delete course", $"Delete “{Course.Title}”?", "Delete", "Cancel");
        if (!confirm) return;

        await _db.DeleteCourseAsync(Course);
        await _notify.CancelCourseNotificationsAsync(Course);
        await Shell.Current.GoToAsync("..");
    }

    private static Task Alert(string title, string msg) =>
        App.Current.MainPage.DisplayAlert(title, msg, "OK");

    private static bool IsValidEmail(string email)
    {
        try { _ = new MailAddress(email); return true; }
        catch { return false; }
    }
}
