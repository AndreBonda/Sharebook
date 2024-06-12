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
    private Email _email = Substitute.For<Email>();
    private Password _password = Substitute.For<Password>();
    private IHashingProvider _hashingProvider =  Substitute.For<IHashingProvider>();

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
    public void Validation_ThrowsArgumentNullException_IfEmailIsNull()
    {
        var act = () => new User(Guid.NewGuid(), null, _password);

        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Validation_ThrowsArgumentNullException_IfPasswordIsNull()
    {
        var act = () => new User(Guid.NewGuid(), _email, null);

        act.Should().Throw<ArgumentNullException>();
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