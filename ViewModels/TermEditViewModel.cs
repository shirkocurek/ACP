using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Services;

namespace c971_mobile_application_development_using_c_sharp.ViewModels;

public partial class TermEditViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    [ObservableProperty] private Term term = new();

    // Mode flag from navigation
    [ObservableProperty] private bool isNew;

    // UI text bound in XAML
    [ObservableProperty] private string pageTitle = "Edit Term";
    [ObservableProperty] private string saveText  = "Save Changes";
    [ObservableProperty] private bool showDelete  = true;

    public TermEditViewModel(DatabaseService db) => _db = db;

    partial void OnIsNewChanged(bool value)
    {
        if (value)
        {
            PageTitle = "Add Term";
            SaveText  = "Add Term";
            ShowDelete = false;
            // Sensible defaults for new term
            if (Term is not null)
            {
                if (Term.StartDate == default) Term.StartDate = DateTime.Today;
                if (Term.EndDate   == default) Term.EndDate   = DateTime.Today.AddMonths(6);
            }
        }
        else
        {
            PageTitle = "Edit Term";
            SaveText  = "Save Changes";
            ShowDelete = true;
        }
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Term.Title))
        {
            await App.Current.MainPage.DisplayAlert("Required", "Please enter a term title.", "OK");
            return;
        }
        if (Term.EndDate < Term.StartDate)
        {
            await App.Current.MainPage.DisplayAlert("Dates", "End date cannot be before start date.", "OK");
            return;
        }
        await _db.SaveTermAsync(Term);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task DeleteAsync()
    {
        if (Term.Id == 0) { await Shell.Current.GoToAsync(".."); return; }
        var ok = await App.Current.MainPage.DisplayAlert("Delete", $"Delete '{Term.Title}'?", "Yes", "No");
        if (ok)
        {
            await _db.DeleteTermAsync(Term);
            await Shell.Current.GoToAsync("..");
        }
    }
}

