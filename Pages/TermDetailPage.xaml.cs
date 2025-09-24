using c971_mobile_application_development_using_c_sharp.ViewModels;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Helpers; 

namespace c971_mobile_application_development_using_c_sharp.Pages;

[QueryProperty(nameof(Term), "Term")]
public partial class TermDetailPage : ContentPage
{
    public TermDetailPage() : this(ServiceHelper.GetService<TermDetailViewModel>()) {}

    private readonly TermDetailViewModel _vm;

    public TermDetailPage(TermDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    public Term Term
    {
        set { _vm.SetTerm(value); }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }
}
