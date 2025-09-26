using c971_mobile_application_development_using_c_sharp.Services;
using c971_mobile_application_development_using_c_sharp.Helpers;

namespace c971_mobile_application_development_using_c_sharp;

public partial class App : Application
{
    private readonly DatabaseService _db;

    public App(DatabaseService db)
    {
        InitializeComponent();
        _db = db;

        MainPage = new AppShell();
    }

    protected override async void OnStart()
    {
        base.OnStart();

        await _db.InitAsync();
        await _db.EnsureDemoDataAsync();

        var notifier = ServiceHelper.GetService<NotificationService>();
        await notifier.RequestPermissionAsync();
    }
}
