namespace CollegeScheduler.Core;

public static class HoneypotGuard
{
    public static bool IsClean(string? honeypotField) =>
        string.IsNullOrWhiteSpace(honeypotField);
}
