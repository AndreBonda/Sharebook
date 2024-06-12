using FluentAssertions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.UnitTests.Shared.Primitives;

[TestFixture]
public class ValueObjectTests
{
    private class FakeValueObject : ValueObject
    {
        public required string Text { get; set; }
        public int Value { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
            yield return Value;
        }
    }

    [TestCase]
    public void Equals_ReturnFalse_IfDifferentValueObjectTypeIsPassedAsParam()
    {
        FakeValueObject vo = new()
        {
            Text = "text",
            Value = 10
        };

        vo.Equals(new { }).Should().BeFalse();
    }

    [TestCase]
    public void Equals_ReturnFalse_IfNullIsPassedAsParam()
    {
        FakeValueObject vo = new()
        {
            Text = "text",
            Value = 10
        };

        vo.Equals(null).Should().BeFalse();
    }

    [TestCase]
    public void Equals_ReturnFalse_IfTextIsNotEqual()
    {
        FakeValueObject vo1 = new()
        {
            Text = "textA",
            Value = 10
        };

        FakeValueObject vo2 = new()
        {
            Text = "textB",
            Value = 10
        };

        vo1.Equals(vo2).Should().BeFalse();
    }

    [TestCase]
    public void Equals_ReturnFalse_IfValueIsNotEqual()
    {
        FakeValueObject vo1 = new()
        {
            Text = "text",
            Value = 10
        };

        FakeValueObject vo2 = new()
        {
            Text = "text",
            Value = 20
        };

        vo1.Equals(vo2).Should().BeFalse();
    }

    [TestCase]
    public void Equals_ReturnTrue_IfTheyHAveSameValueProperties()
    {
        FakeValueObject vo1 = new()
        {
            Text = "text",
            Value = 10
        };

        FakeValueObject vo2 = new()
        {
            Text = "text",
            Value = 10
        };

        vo1.Equals(vo2).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfLeftOperatorIsNull()
    {
        FakeValueObject? vo1 = null;
        FakeValueObject vo2 = new()
        {
            Text = "text",
            Value = 10
        };

        (vo1 == vo2).Should().BeFalse();
        (vo1 != vo2).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfRightOperatorIsNull()
    {
        FakeValueObject vo1 = new()
        {
            Text = "text",
            Value = 10
        };

        FakeValueObject? vo2 = null;

        (vo1 == vo2).Should().BeFalse();
        (vo1 != vo2).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckEquality_IfOperatoresAreNull()
    {
        FakeValueObject? vo1 = null;
        FakeValueObject? vo2 = null;

        (vo1 == vo2).Should().BeTrue();
        (vo1 != vo2).Should().BeFalse();
    }

    [TestCase]
    public void GetHashCode_ReturnsDifferentCode_IfPropertiesAreNotEquals()
    {
        // Arrange
        FakeValueObject vo1 = new()
        {
            Text = "textA",
            Value = 10
        };

        FakeValueObject vo2 = new()
        {
            Text = "textB",
            Value = 20
        };

        // Act & Assert
        vo1.GetHashCode().Should().NotBe(vo2.GetHashCode());
    }

    [TestCase]
    public void GetHashCode_ReturnsSameCode_IfPropertiesAreEquals()
    {
        // Arrange
        FakeValueObject vo1 = new()
        {
            Text = "text",
            Value = 10
        };

        FakeValueObject vo2 = new()
        {
            Text = "text",
            Value = 10
        };

        // Act & Assert
        vo1.GetHashCode().Should().Be(vo2.GetHashCode());
    }
}