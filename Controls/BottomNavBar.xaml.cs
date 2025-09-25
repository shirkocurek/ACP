using System.Windows.Input;

namespace c971_mobile_application_development_using_c_sharp.Controls;

public partial class BottomNavBar : ContentView
{
    public BottomNavBar()
    {
        InitializeComponent();
    }

    public ICommand NavigateTermsCommand => new Command(async () =>
        await Shell.Current.GoToAsync("//TermsPage"));

    public ICommand NavigateCoursesCommand => new Command(async () =>
        await Shell.Current.GoToAsync("//CoursesPage"));
}
