using c971_mobile_application_development_using_c_sharp.ViewModels;

namespace c971_mobile_application_development_using_c_sharp.Pages;

public partial class ReportPage : ContentPage
{
    private readonly ReportViewModel _vm;
    public ReportPage(ReportViewModel vm)
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
