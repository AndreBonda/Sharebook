using FluentAssertions;
using NSubstitute;
using NSubstitute.ClearExtensions;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shared.ValueObjects;
using ShareBook.Domain.Users;

namespace ShareBook.UnitTests.Users;

[TestFixture]
public class UserTests
{
    private readonly Email _email = Substitute.For<Email>("valid_email@email.com");
    private readonly IHashingProvider _hashingProvider =  Substitute.For<IHashingProvider>();
    private Password _password = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _password = Substitute.For<Password>("ValidPassword*!123", _hashingProvider);
    }

    [SetUp]
    public void SetUp() {
        _email.ClearSubstitute();
        _password.ClearSubstitute();
        _hashingProvider.ClearSubstitute();
    }

    [Test]
    public void Validation_ThrowsArgumentException_IfGuidIsEmpty() {
        var act = () => new User(Guid.Empty, _email, _password);

        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Constructor_CreateNewUser_IfValidInputs() {
        // Arrange
        _email.Value.Returns("valid_email");
        var guid = Guid.NewGuid();

        // Act
        var user = new User(guid, _email, _password);

        // Assert
        user.Email.Value.Should().Be("valid_email");
    }
}