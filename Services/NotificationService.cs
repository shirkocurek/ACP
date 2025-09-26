using Plugin.LocalNotification;
using c971_mobile_application_development_using_c_sharp.Models;

namespace c971_mobile_application_development_using_c_sharp.Services;

public class NotificationService
{
    public async Task RequestPermissionAsync()
    {
        await LocalNotificationCenter.Current.RequestNotificationPermission();
    }

    private int CourseStartId(int courseId) => 100000 + courseId;
    private int CourseEndId(int courseId)   => 200000 + courseId;

    public async Task ScheduleCourseNotificationsAsync(Course course)
    {
        await CancelCourseNotificationsAsync(course);

        if (course.NotifyOnStart)
        {
            var when = AtNineAM(course.StartDate);
            if (when > DateTime.Now)
            {
                await LocalNotificationCenter.Current.Show(new NotificationRequest
                {
                    NotificationId = CourseStartId(course.Id),
                    Title = $"Course starts: {course.Title}",
                    Description = $"Starts {course.StartDate:d}",
                    Schedule = new NotificationRequestSchedule { NotifyTime = when }
                });
            }
        }

        if (course.NotifyOnEnd)
        {
            var when = AtNineAM(course.EndDate);
            if (when > DateTime.Now)
            {
                await LocalNotificationCenter.Current.Show(new NotificationRequest
                {
                    NotificationId = CourseEndId(course.Id),
                    Title = $"Course ends: {course.Title}",
                    Description = $"Ends {course.EndDate:d}",
                    Schedule = new NotificationRequestSchedule { NotifyTime = when }
                });
            }
        }
    }

    public Task CancelCourseNotificationsAsync(Course course)
    {
        LocalNotificationCenter.Current.Cancel(CourseStartId(course.Id));
        LocalNotificationCenter.Current.Cancel(CourseEndId(course.Id));
        return Task.CompletedTask;
    }

    private static DateTime AtNineAM(DateTime date)
    {
        var local = date.Date.AddHours(9);
        return DateTime.SpecifyKind(local, DateTimeKind.Local);
    }
}
