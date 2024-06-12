using FluentAssertions;
using NSubstitute;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.ValueObjects;

namespace ShareBook.UnitTests.Shared.ValueObjects;

[TestFixture]
public class PasswordTests
{
    private IHashingProvider _hashingProvider = Substitute.For<IHashingProvider>();

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("aA*1")]
    [TestCase("AA*1")]
    [TestCase("aa*1")]
    [TestCase("aa*A")]
    [TestCase("aaAA")]
    public void Constructor_InvalidPassword_ThrowsArgumentException(string invalidPassword)
    {
        var act = () => new Password(invalidPassword, _hashingProvider);

        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Constructor_NullHashingProvider_ThrowsArgumentException()
    {
        var act = () => new Password("AAbb11**", null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Constructor_ValidInputs_SetHashedPassword()
    {
        // Arrange
        _hashingProvider
            .Hash(Arg.Any<string>())
            .Returns("Hashed_password");


        // Act
        var password = new Password("AAbb11**", _hashingProvider);

        // Assert
        password.PasswordHash.Should().Be("Hashed_password");
    }
}