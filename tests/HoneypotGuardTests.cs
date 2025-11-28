using CollegeScheduler.Core;
using FluentAssertions;
using Xunit;

public class HoneypotGuardTests
{
    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData("   ", true)]
    [InlineData("I am a bot", false)]
    public void IsClean_ReturnsExpected(string? trap, bool expected)
    {
        HoneypotGuard.IsClean(trap).Should().Be(expected);
    }
}
