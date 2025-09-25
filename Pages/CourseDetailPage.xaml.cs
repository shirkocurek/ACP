using c971_mobile_application_development_using_c_sharp.ViewModels;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Helpers;

namespace c971_mobile_application_development_using_c_sharp.Pages;

[QueryProperty(nameof(Course), "Course")]
public partial class CourseDetailPage : ContentPage
{
    public CourseDetailPage() : this(ServiceHelper.GetService<CourseDetailViewModel>()) {}

    private readonly CourseDetailViewModel _vm;
    public CourseDetailPage(CourseDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    public Course Course { set => _vm.SetCourse(value); }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }
}
