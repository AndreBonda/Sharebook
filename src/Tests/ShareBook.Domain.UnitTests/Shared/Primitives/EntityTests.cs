using FluentAssertions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.UnitTests.Shared.Primitives;

[TestFixture]
public class EntityTests
{
    private class FakeEntity : Entity<Guid>
    {
        public FakeEntity(Guid id) : base(id)
        {
        }
    }

    private FakeEntity _a;
    private FakeEntity _b;

    [SetUp]
    public void SetUp() {
        _a = new FakeEntity(Guid.NewGuid());
        _b = new FakeEntity(Guid.NewGuid());
    }

    [TestCase]
    public void Equals_ReturnFalse_IfDifferentEntityTypeIsPassedAsParam()
    {
        _a.Should().NotBe(new { });
    }

    [TestCase]
    public void Equals_ReturnFalse_IfNullIsPassedAsParam()
    {
        _a.Should().NotBe(null);
    }

    [TestCase]
    public void Equals_ReturnFalse_IfEntitiesHaveDifferentId()
    {
        _a.Should().NotBe(_b);
    }

    [TestCase]
    public void Equals_ReturnTrue_IfEntitiesHaveSameId()
    {
        // Arrange
        var id = Guid.NewGuid();
        _a = new FakeEntity(id);
        _b = new FakeEntity(id);

        // Act & Assert
        _a.Should().Be(_b);
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfLeftOperatorIsNull()
    {
        // Arrange
        _a = null;

        // Act & Assert
        (_a != _b).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfRightOperatorIsNull()
    {
        // Arrange
        _b = null;

        // Act & Assert
        (_a == _b).Should().BeFalse();
        (_a != _b).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckEquality_IfOperatoresAreNull()
    {
        // Arrange
        _a = null;
        _b = null;

        // Act & Assert
        (_a == _b).Should().BeTrue();
        (_a != _b).Should().BeFalse();
    }

    [TestCase]
    public void GetHashCode_ReturnsDifferentCode_IfIdsAreNotEqual()
    {
        // Arrange & Act
        var code1 = _a.GetHashCode();
        var code2 = _b.GetHashCode();

        // Assert
        code1.Should().NotBe(code2);
    }

    [TestCase]
    public void GetHashCode_ReturnsSameCode_IfIdsAreEqual()
    {
        // Arramge
        var id = Guid.NewGuid();
        _a = new FakeEntity(id);
        _b = new FakeEntity(id);

        // Act
        var code1 = _a.GetHashCode();
        var code2 = _b.GetHashCode();

        // Assert
        code1.Should().Be(code2);
    }
}