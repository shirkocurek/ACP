using SQLite;
using c971_mobile_application_development_using_c_sharp.Models;

namespace c971_mobile_application_development_using_c_sharp.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _db;
    public DatabaseService(string dbPath) => _db = new(dbPath);

    public async Task InitAsync()
    {
        await _db.CreateTableAsync<Term>();
        await _db.CreateTableAsync<Course>();
        await _db.CreateTableAsync<Assessment>();

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

