using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using c971_mobile_application_development_using_c_sharp.Services;
using c971_mobile_application_development_using_c_sharp.ViewModels;
using c971_mobile_application_development_using_c_sharp.Pages;
using c971_mobile_application_development_using_c_sharp.Helpers; 

namespace c971_mobile_application_development_using_c_sharp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Services
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "wgu_terms.db3");
        builder.Services.AddSingleton(new DatabaseService(dbPath));

        // ViewModels
        builder.Services.AddSingleton<TermsViewModel>();
        builder.Services.AddTransient<TermEditViewModel>();
		builder.Services.AddTransient<TermDetailViewModel>();
		builder.Services.AddTransient<CourseEditViewModel>();

        // Pages
        builder.Services.AddSingleton<TermsPage>();
        builder.Services.AddTransient<TermEditPage>();
		builder.Services.AddTransient<TermDetailPage>();
		builder.Services.AddTransient<CourseEditPage>();


#if DEBUG
		builder.Logging.AddDebug();
#endif
        var app = builder.Build();
		ServiceHelper.Services = app.Services;
		return builder.Build();
	}
}
