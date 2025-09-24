using c971_mobile_application_development_using_c_sharp.Services;

namespace c971_mobile_application_development_using_c_sharp;

public partial class App : Application
{
    private readonly DatabaseService _db;

    public App(DatabaseService db)
    {
        InitializeComponent();

        _db = db;
        _ = _db.InitAsync();

        MainPage = new AppShell();
    }
}

