using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using c971_mobile_application_development_using_c_sharp.Models;
using c971_mobile_application_development_using_c_sharp.Services;
using System.Text;
using System.Reflection;

namespace c971_mobile_application_development_using_c_sharp.ViewModels;

public partial class ReportViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    public ObservableCollection<ReportRow> Rows { get; } = new();

    [ObservableProperty] private DateTime fromDate = DateTime.Today.AddMonths(-1);
    [ObservableProperty] private DateTime toDate = DateTime.Today.AddMonths(3);

    public string ReportTitle => "Academic Overview Report";
    public string GeneratedAt => $"Generated: {DateTime.Now:G}";

    public ReportViewModel(DatabaseService db) => _db = db;

    [RelayCommand]
    public async Task LoadAsync()
    {
        Rows.Clear();
        var list = await _db.GetReportRowsAsync(FromDate, ToDate);
        foreach (var r in list) Rows.Add(r);
        OnPropertyChanged(nameof(GeneratedAt));
    }


    [RelayCommand]
    public async Task ExportCsvAsync()
    {
        if (Rows.Count == 0)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
                await Application.Current!.MainPage!.DisplayAlert(
                    "Export CSV", "No report rows to export. Generate a report first.", "OK"));
            return;
        }

        var props = typeof(ReportRow)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead)
            .ToArray();

        var sb = new StringBuilder();

        sb.AppendLine(string.Join(",", props.Select(p => Csv(p.Name))));

        foreach (var row in Rows)
        {
            var cells = props.Select(p =>
            {
                var val = p.GetValue(row, null);
                if (val == null) return "";

                return val switch
                {
                    DateTime dt => Csv(dt.ToString("u")),
                    DateTimeOffset dto => Csv(dto.ToString("u")),
                    _ => Csv(val.ToString()!)
                };
            });

            sb.AppendLine(string.Join(",", cells));
        }

        var fileName = $"CollegeScheduler_Report_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

        await Share.RequestAsync(new ShareFileRequest
        {
            Title = "Export Report CSV",
            File = new ShareFile(filePath)
        });

        // local helper
        static string Csv(string? raw)
        {
            if (string.IsNullOrEmpty(raw)) return "";
            var needsQuotes = raw.Contains(',') || raw.Contains('"') || raw.Contains('\n') || raw.Contains('\r');
            var cleaned = raw.Replace("\"", "\"\"");
            return needsQuotes ? $"\"{cleaned}\"" : cleaned;
        }
    }

}
