using System.Linq;
using SQLite;
using c971_mobile_application_development_using_c_sharp.Models;

namespace c971_mobile_application_development_using_c_sharp.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _db;
    public DatabaseService(string dbPath) => _db = new(dbPath);

    public Task<Assessment?> GetAssessmentAsync(int id) =>
        _db.Table<Assessment>()
           .Where(a => a.Id == id)
           .FirstOrDefaultAsync();

    public async Task<(Assessment? Objective, Assessment? Performance)> GetPairForCourseAsync(int courseId)
    {
        var list = await GetAssessmentsForCourseAsync(courseId);
        return (list.FirstOrDefault(a => a.Type == "Objective"),
                list.FirstOrDefault(a => a.Type == "Performance"));
    }

    public async Task InitAsync()
    {
        await _db.CreateTableAsync<Term>();
        await _db.CreateTableAsync<Course>();
        await _db.CreateTableAsync<Assessment>();

        await EnsureColumnAsync("Assessment", "Type", "TEXT NOT NULL DEFAULT 'Objective'");
        await EnsureColumnAsync("Assessment", "Title", "TEXT NOT NULL DEFAULT ''");
        await EnsureColumnAsync("Assessment", "Status", "TEXT NOT NULL DEFAULT 'Planned'");

        await EnsureColumnAsync("Course", "NotifyOnStart", "INTEGER NOT NULL DEFAULT 0");
        await EnsureColumnAsync("Course", "NotifyOnEnd", "INTEGER NOT NULL DEFAULT 0");
        await EnsureColumnAsync("Assessment", "Status", "TEXT NOT NULL DEFAULT 'Planned'");
        await EnsureColumnAsync("Assessment", "NotifyOnStart", "INTEGER NOT NULL DEFAULT 0");
        await EnsureColumnAsync("Assessment", "NotifyOnEnd", "INTEGER NOT NULL DEFAULT 0");
        await EnsureColumnAsync("Assessment", "Type", "TEXT NOT NULL DEFAULT 'Objective'");
        await EnsureColumnAsync("Assessment", "Title", "TEXT NOT NULL DEFAULT ''");
    }

    // Added to satisfy MauiProgram call, delegates to your existing seeder.
    public Task EnsureDemoDataAsync() => SeedDemoAsync();

    public async Task SeedDemoAsync()
    {
        // Only seed if DB is empty (no terms yet)
        var termCount = await _db.Table<Term>().CountAsync();
        if (termCount > 0) return;

        // 1) Term
        var term = new Term
        {
            Title = "Fall Term 2025",
            StartDate = new DateTime(2025, 9, 1),
            EndDate = new DateTime(2026, 1, 31)
        };
        await SaveTermAsync(term);

        // 2) Course under that term (includes the required instructor info)
        var course = new Course
        {
            TermId = term.Id,
            Title = "Mobile App Dev",
            StartDate = new DateTime(2025, 9, 1),
            EndDate = new DateTime(2026, 1, 31),
            Status = "In Progress",

            InstructorName = "Anika Patel",
            InstructorPhone = "555-123-4567",
            InstructorEmail = "anika.patel@strimeuniversity.edu",

            Notes =
                "Key Topics:\n- .NET MAUI\n- SQLite (sqlite-net-pcl)\n- Local notifications",
        };
        await SaveCourseAsync(course);

        // 3) Two assessments for this course (Objective + Performance)
        var objective = new Assessment
        {
            CourseId = course.Id,
            Type = "Objective",
            Title = "Objective Assessment",
            StartDate = new DateTime(2025, 9, 25),
            EndDate = new DateTime(2025, 10, 2),
            Status = "Planned",
            // NotifyOnStart = false,
            // NotifyOnEnd   = false
        };
        await SaveAssessmentAsync(objective);

        var performance = new Assessment
        {
            CourseId = course.Id,
            Type = "Performance",
            Title = "Performance Assessment",
            StartDate = new DateTime(2025, 9, 25),
            EndDate = new DateTime(2025, 10, 20),
            Status = "Planned",
            // NotifyOnStart = false,
            // NotifyOnEnd   = false
        };
        await SaveAssessmentAsync(performance);
    }

    private async Task EnsureColumnAsync(string table, string column, string columnDDL)
    {
        var found = await _db.ExecuteScalarAsync<int>(
            $"SELECT COUNT(*) FROM pragma_table_info('{table}') WHERE name = ?", column);
        if (found == 0)
        {
            await _db.ExecuteAsync($"ALTER TABLE {table} ADD COLUMN {column} {columnDDL};");
        }
    }

    public Task<List<Assessment>> GetAssessmentsForCourseAsync(int courseId) =>
        _db.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync();

    public async Task<(int objective, int performance)> GetAssessmentCountsAsync(int courseId)
    {
        var all = await GetAssessmentsForCourseAsync(courseId);
        return (all.Count(a => a.Type == "Objective"),
                all.Count(a => a.Type == "Performance"));
    }

    public Task<int> SaveAssessmentAsync(Assessment a) =>
        a.Id == 0 ? _db.InsertAsync(a) : _db.UpdateAsync(a);

    public Task<int> DeleteAssessmentAsync(Assessment a) =>
        _db.DeleteAsync(a);

    public Task<List<Course>> GetAllCoursesAsync() =>
        _db.Table<Course>().OrderBy(c => c.StartDate).ToListAsync();

    // Courses
    public Task<List<Course>> GetCoursesForTermAsync(int termId) =>
        _db.Table<Course>().Where(c => c.TermId == termId).OrderBy(c => c.StartDate).ToListAsync();

    public Task<int> GetCourseCountForTermAsync(int termId) =>
        _db.Table<Course>().Where(c => c.TermId == termId).CountAsync();

    public async Task<int> SaveCourseAsync(Course c)
    {
        if (c.Id == 0)
        {
            var count = await GetCourseCountForTermAsync(c.TermId);
            if (count >= 6)
                throw new InvalidOperationException("This term already has 6 courses.");
            return await _db.InsertAsync(c);
        }
        return await _db.UpdateAsync(c);
    }

    public Task<int> DeleteCourseAsync(Course c) => _db.DeleteAsync(c);

    public Task<List<Term>> GetTermsAsync()
        => _db.Table<Term>().OrderBy(t => t.StartDate).ToListAsync();

    public Task<int> SaveTermAsync(Term t)
        => t.Id == 0 ? _db.InsertAsync(t) : _db.UpdateAsync(t);

    public Task<int> DeleteTermAsync(Term t) => _db.DeleteAsync(t);
}


