using FluentAssertions;
using ShareBook.Domain.Shared.ValueObjects;

namespace ShareBook.UnitTests.Shared.ValueObjects;

[TestFixture]
public class EmailTests
{
    [Test]
    public void Constructor_NullInput_ThrowsArgumentException()
    {
        var act = () => new Email(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("wrong_email")]
    public void Constructor_InvalidInput_ThrowsArgumentException(string invalidInput)
    {
        var act = () => new Email(invalidInput);

        act.Should().Throw<ArgumentException>();
    }


    [Test]
    public void Constructor_ValidInput_SetValue()
    {
        var email = new Email("valid_email@gmail.com");

        email.Value.Should().Be("valid_email@gmail.com");
    }
}