using c971_mobile_application_development_using_c_sharp.ViewModels;
using c971_mobile_application_development_using_c_sharp.Models;

namespace c971_mobile_application_development_using_c_sharp.Pages;

[QueryProperty(nameof(Course), "Course")]
[QueryProperty(nameof(IsNew),  "IsNew")]
public partial class CourseEditPage : ContentPage
{
    private readonly CourseEditViewModel _vm;
    public CourseEditPage(CourseEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    public Course Course { set { _vm.Course = value ?? new Course(); } }
    public bool IsNew { set { _vm.Initialize(value); } }

    private async void OnCancel(object? sender, EventArgs e) =>
        await Shell.Current.GoToAsync("..");
}
