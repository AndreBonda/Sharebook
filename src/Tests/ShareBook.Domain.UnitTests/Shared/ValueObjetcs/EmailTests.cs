using Moq;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.ValueObjects;

namespace ShareBook.UnitTests.Shared.ValueObjects;

[TestFixture]
public class EmailTests
{
    [Test]
    public void Constructor_NullInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentNullException>(() => new Email(null));
    }

    [Test]
    public void Constructor_EmptyInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Email(string.Empty));
    }

    [Test]
    public void Constructor_WhiteSpacesInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Email(" "));
    }

    [Test]
    public void Constructor_WrongInputFormat_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Email("wrong_email"));
    }

    [Test]
    public void Constructor_ValidInput_SetValue()
    {
        var email = new Email("valid_email@gmail.com");

        Assert.That(email.Value, Is.EqualTo("valid_email@gmail.com"));
    }
}