namespace CollegeScheduler.Core;

public static class Validators
{
    public static string Clean(string? s) => (s ?? string.Empty).Trim();

    public static bool HasMinLen(string? s, int min) =>
        Clean(s).Length >= min;

    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        try { _ = new System.Net.Mail.MailAddress(email); return true; }
        catch { return false; }
    }

    public static bool IsValidPhone(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return false;
        var digits = new string(phone.Where(char.IsDigit).ToArray());
        return digits.Length >= 10;
    }
}
