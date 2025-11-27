using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

using c971_mobile_application_development_using_c_sharp.Services;
using c971_mobile_application_development_using_c_sharp.ViewModels;
using c971_mobile_application_development_using_c_sharp.Pages;
using c971_mobile_application_development_using_c_sharp.Helpers;
using Plugin.LocalNotification;

namespace c971_mobile_application_development_using_c_sharp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseLocalNotification()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "wgu_terms.db3");
        builder.Services.AddSingleton(new DatabaseService(dbPath));
        builder.Services.AddSingleton<NotificationService>();

        // ViewModels
        builder.Services.AddSingleton<TermsViewModel>();
        builder.Services.AddTransient<TermEditViewModel>();
        builder.Services.AddTransient<TermDetailViewModel>();
        builder.Services.AddTransient<CourseEditViewModel>();
        builder.Services.AddTransient<CourseDetailViewModel>();
        builder.Services.AddTransient<CoursesViewModel>();
        builder.Services.AddTransient<AssessmentOverviewViewModel>();
        builder.Services.AddTransient<AssessmentEditViewModel>();
        builder.Services.AddTransient<ReportViewModel>();

        // Pages
        builder.Services.AddSingleton<TermsPage>();
        builder.Services.AddTransient<TermEditPage>();
        builder.Services.AddTransient<TermDetailPage>();
        builder.Services.AddTransient<CourseEditPage>();
        builder.Services.AddTransient<CourseDetailPage>();
        builder.Services.AddTransient<CoursesPage>();
        builder.Services.AddTransient<AssessmentOverviewPage>();
        builder.Services.AddTransient<AssessmentEditPage>();
        builder.Services.AddTransient<ReportPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif


        var app = builder.Build();


        ServiceHelper.Initialize(app.Services);

        

        return app;
    }
}
