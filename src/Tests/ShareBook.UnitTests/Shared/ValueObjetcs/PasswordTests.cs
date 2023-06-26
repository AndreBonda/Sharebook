using Moq;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.ValueObjects;

namespace ShareBook.UnitTests.Shared.ValueObjects;

[TestFixture]
public class PasswordTests
{
    private Mock<IHashingProvider> _hashingProvider = new();

    [Test]
    public void Constructor_NullPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(null, _hashingProvider.Object));
    }

    [Test]
    public void Constructor_EmptyPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(string.Empty, _hashingProvider.Object));
    }

    [Test]
    public void Constructor_WhiteSpacesPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(" ", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_ShortPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("aA*1", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NoLowerCasePassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("AA*1", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NoUpperCasePassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("aa*1", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NoNumericPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("aa*A", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NoSpecialCharacterPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("aaAA", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NullHashingProvider_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentNullException>(() => new Password("AAbb11**", null));
    }

    [Test]
    public void Constructor_PasswordHashNull_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(null, _hashingProvider.Object));
    }

    [Test]
    public void Constructor_PasswordHashEmpty_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(string.Empty, _hashingProvider.Object));
    }

    [Test]
    public void Constructor_ValidInputs_SetHashedPassword()
    {
        // Arrange
        _hashingProvider
            .Setup(p => p.Hash(It.IsAny<string>()))
            .Returns("Hashed_password");
        
        // Act
        var password = new Password("AAbb11**", _hashingProvider.Object);

        // Assert
        Assert.That(password.PasswordHash, Is.EqualTo("Hashed_password"));
    }
}