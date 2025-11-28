using CollegeScheduler.Core;
using FluentAssertions;
using Xunit;

public class ValidatorsTests
{
    [Fact]
    public void Clean_TrimsWhitespace()
    {
        Validators.Clean("  hi  ").Should().Be("hi");
    }

    [Theory]
    [InlineData("A", 2, false)]
    [InlineData("AB", 2, true)]
    [InlineData("  AB  ", 2, true)]
    public void HasMinLen_Works(string input, int min, bool expected)
    {
        Validators.HasMinLen(input, min).Should().Be(expected);
    }

    [Theory]
    [InlineData("alice@example.com", true)]
    [InlineData("bad@", false)]
    [InlineData("", false)]
    public void IsValidEmail_Works(string email, bool expected)
    {
        Validators.IsValidEmail(email).Should().Be(expected);
    }

    [Theory]
    [InlineData("404-555-1212", true)]
    [InlineData("12345", false)]
    [InlineData("", false)]
    public void IsValidPhone_Works(string phone, bool expected)
    {
        Validators.IsValidPhone(phone).Should().Be(expected);
    }
}
