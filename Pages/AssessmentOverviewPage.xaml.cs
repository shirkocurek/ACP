using c971_mobile_application_development_using_c_sharp.Helpers;
using c971_mobile_application_development_using_c_sharp.ViewModels;

namespace c971_mobile_application_development_using_c_sharp.Pages;

[QueryProperty(nameof(Course), "Course")]
public partial class AssessmentOverviewPage : ContentPage
{
    public AssessmentOverviewPage()
    {
        InitializeComponent();
    }

    public Models.Course? Course { get; set; }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = ServiceHelper.GetService<AssessmentOverviewViewModel>();
        BindingContext = vm;
        if (Course != null)
            vm.SetCourse(Course);
        await vm.LoadAsync();
    }
}
