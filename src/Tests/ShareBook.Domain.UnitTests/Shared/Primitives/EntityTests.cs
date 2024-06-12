using FluentAssertions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.UnitTests.Shared.Primitives;

[TestFixture]
public class EntityTests
{
    private class FakeEntity(Guid id) : Entity<Guid>(id);

    [TestCase]
    public void Equals_ReturnFalse_IfDifferentEntityTypeIsPassedAsParam()
    {
        FakeEntity e = new(Guid.NewGuid());

        e.Should().NotBe(new { });
    }

    [TestCase]
    public void Equals_ReturnFalse_IfNullIsPassedAsParam()
    {
        FakeEntity e = new(Guid.NewGuid());

        e.Should().NotBe(null);
    }

    [TestCase]
    public void Equals_ReturnFalse_IfEntitiesHaveDifferentId()
    {
        FakeEntity e1 = new(Guid.NewGuid());
        FakeEntity e2 = new(Guid.NewGuid());

        e1.Should().NotBe(e2);
    }

    [TestCase]
    public void Equals_ReturnTrue_IfEntitiesHaveSameId()
    {
        // Arrange
        var id = Guid.NewGuid();
        FakeEntity e1 = new (id);
        FakeEntity e2 = new (id);

        // Act & Assert
        e1.Should().Be(e2);
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfLeftOperatorIsNull()
    {
        // Arrange
        FakeEntity? e1 = null;
        FakeEntity e2 = new (Guid.NewGuid());

        // Act & Assert
        (e1 != e2).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfRightOperatorIsNull()
    {
        // Arrange
        FakeEntity e1 = new (Guid.NewGuid());
        FakeEntity? e2 = null;

        // Act & Assert
        (e1 == e2).Should().BeFalse();
        (e1 != e2).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckEquality_IfOperatorsAreNull()
    {
        // Arrange
        FakeEntity? e1 = null;
        FakeEntity? e2 = null;

        // Act & Assert
        (e1 == e2).Should().BeTrue();
        (e1 != e2).Should().BeFalse();
    }

    [TestCase]
    public void GetHashCode_ReturnsDifferentCode_IfIdsAreNotEqual()
    {
        // Arrange
        FakeEntity e1 = new(Guid.NewGuid());
        FakeEntity e2 = new(Guid.NewGuid());

        // Act & Assert
        e1.GetHashCode().Should().NotBe(e2.GetHashCode());
    }

    [TestCase]
    public void GetHashCode_ReturnsSameCode_IfIdsAreEqual()
    {
        // Arramge
        var id = Guid.NewGuid();
        // Arrange
        FakeEntity e1 = new(id);
        FakeEntity e2 = new(id);

        // Act & Assert
        e1.GetHashCode().Should().Be(e2.GetHashCode());
    }
}