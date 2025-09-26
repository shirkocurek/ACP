using c971_mobile_application_development_using_c_sharp.ViewModels;

namespace c971_mobile_application_development_using_c_sharp.Pages;
using c971_mobile_application_development_using_c_sharp.Services;
using c971_mobile_application_development_using_c_sharp.Helpers;


public partial class TermsPage : ContentPage
{
    public TermsPage() : this(ServiceHelper.GetService<TermsViewModel>()) {}
    private readonly TermsViewModel _vm;
    public TermsPage(TermsViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
         base.OnAppearing();

    // Ask once; subsequent calls are no-ops if already granted
    var notifier = Helpers.ServiceHelper.GetService<NotificationService>();
    await notifier.RequestPermissionAsync();

    if (BindingContext is ViewModels.TermsViewModel vm)
        await vm.LoadAsync();
    }
}
