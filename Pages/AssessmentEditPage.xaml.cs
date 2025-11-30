using c971_mobile_application_development_using_c_sharp.Helpers;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.ViewModels;

namespace c971_mobile_application_development_using_c_sharp.Pages;

[QueryProperty(nameof(Assessment), "Assessment")]
[QueryProperty(nameof(IsNew), "IsNew")]
public partial class AssessmentEditPage : ContentPage
{
    public AssessmentEditPage() => InitializeComponent();

    public Assessment? Assessment { get; set; }
    public bool IsNew { get; set; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var vm = ServiceHelper.GetService<AssessmentEditViewModel>();
        BindingContext = vm;
        vm.SetAssessment(Assessment ?? new Assessment());
        vm.Initialize(IsNew);
    }

    private async void OnCancel(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("..");
}
