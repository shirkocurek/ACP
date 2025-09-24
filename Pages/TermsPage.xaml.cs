using c971_mobile_application_development_using_c_sharp.ViewModels;

namespace c971_mobile_application_development_using_c_sharp.Pages;

public partial class TermsPage : ContentPage
{
    private readonly TermsViewModel _vm;
    public TermsPage(TermsViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }
}
