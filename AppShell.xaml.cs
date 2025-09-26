namespace c971_mobile_application_development_using_c_sharp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Pages.TermEditPage), typeof(Pages.TermEditPage));
		Routing.RegisterRoute(nameof(Pages.TermDetailPage), typeof(Pages.TermDetailPage));
		Routing.RegisterRoute(nameof(Pages.CourseDetailPage), typeof(Pages.CourseDetailPage));
		Routing.RegisterRoute(nameof(Pages.CourseEditPage), typeof(Pages.CourseEditPage));
		Routing.RegisterRoute(nameof(Pages.AssessmentOverviewPage), typeof(Pages.AssessmentOverviewPage));
        Routing.RegisterRoute(nameof(Pages.AssessmentEditPage),     typeof(Pages.AssessmentEditPage));
	}
}
