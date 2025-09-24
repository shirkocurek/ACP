using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Services;

namespace c971_mobile_application_development_using_c_sharp.ViewModels;

public partial class TermsViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    public ObservableCollection<Term> Terms { get; } = new();

    [ObservableProperty] private bool isBusy;

    public TermsViewModel(DatabaseService db) => _db = db;

    [RelayCommand]
    public async Task LoadAsync()
    {
        if (IsBusy) return; IsBusy = true;
        try {
            Terms.Clear();
            foreach (var t in await _db.GetTermsAsync()) Terms.Add(t);
        } finally { IsBusy = false; }
    }

    [RelayCommand]
    public async Task AddAsync()
    {
        var newTerm = new Term {
            StartDate = DateTime.Today,
            EndDate   = DateTime.Today.AddMonths(6)
        };
        await Shell.Current.GoToAsync(nameof(Pages.TermEditPage), true,
            new Dictionary<string, object>{{"Term", newTerm}, { "IsNew", true }});
    }

    [RelayCommand]
    public async Task EditAsync(Term? term)
    {
        if (term is null) return;
        await Shell.Current.GoToAsync(nameof(Pages.TermEditPage), true,
            new Dictionary<string, object>{{"Term", new Term{
                Id = term.Id, Title = term.Title, StartDate = term.StartDate, EndDate = term.EndDate }}, { "IsNew", false }});
    }

    [RelayCommand]
    public async Task ViewAsync(Term? term)
    {
    if (term is null) return;
    await Shell.Current.GoToAsync(nameof(Pages.TermDetailPage), true,
        new Dictionary<string, object>
        {
            { "Term", term }
        });
    }

}
