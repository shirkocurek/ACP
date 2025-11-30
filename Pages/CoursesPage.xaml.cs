using c971_mobile_application_development_using_c_sharp.ViewModels;
using c971_mobile_application_development_using_c_sharp.Helpers;

namespace c971_mobile_application_development_using_c_sharp.Pages;


public partial class CoursesPage : ContentPage
{
    public CoursesPage() : this(ServiceHelper.GetService<CoursesViewModel>()) {}

    private readonly CoursesViewModel _vm;
    public CoursesPage(CoursesViewModel vm)
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
