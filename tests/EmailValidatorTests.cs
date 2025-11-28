using CollegeScheduler.Core;
using FluentAssertions;
using Xunit;

namespace CollegeScheduler.Tests;

public class EmailValidatorTests
{
    [Theory]
    [InlineData("alice@example.com", true)]
    [InlineData("bad@", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("bob.smith@domain.co.uk", true)]
    public void IsValidEmail_Works(string? input, bool expected)
    {
        Validators.IsValidEmail(input).Should().Be(expected);
    }

    [Fact]
    public void Clean_TrimsWhitespace()
    {
        Validators.Clean("  hello  ").Should().Be("hello");
    }
}
