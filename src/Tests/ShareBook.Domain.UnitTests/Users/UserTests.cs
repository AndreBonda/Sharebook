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

    [Test]
    public void Validation_ThrowsArgumentException_IfGuidIsEmpty() {
        var act = () => new User(Guid.Empty, new Email("valid_email@email.com"), new Password("ValidPassword*!123", _hashingProvider));

        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Constructor_CreateNewUser_IfValidInputs() {
        // Arrange & Act
        var user = new User(Guid.NewGuid(), new Email("valid_email@email.com"), new Password("ValidPassword*!123", _hashingProvider));

        // Assert
        user.Email.Value.Should().Be("valid_email@email.com");
    }
}