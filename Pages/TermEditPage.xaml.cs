using c971_mobile_application_development_using_c_sharp.ViewModels;
using c971_mobile_application_development_using_c_sharp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace c971_mobile_application_development_using_c_sharp.Pages;

// Receive query params on the PAGE, then forward to the VM
[QueryProperty(nameof(Term),  "Term")]
[QueryProperty(nameof(IsNew), "IsNew")]
public partial class TermEditPage : ContentPage
{
    private readonly TermEditViewModel _vm;

    public TermEditPage(TermEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    public Term Term
    {
        set { _vm.Term = value ?? new Term(); }
    }

    public bool IsNew
    {
        set { _vm.IsNew = value; }
    }

    private async void OnCancel(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("..");
}
