using FluentAssertions;
using ShareBook.Domain.Shared.Primitives;

namespace ShareBook.UnitTests.Shared.Primitives;

[TestFixture]
public class ValueObjectTests
{
    private class FakeValueObject : ValueObject
    {
        public string Text { get; set; }
        public int Value { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
            yield return Value;
        }
    }

    private FakeValueObject _a;
    private FakeValueObject _b;

    [SetUp]
    public void SetUp() {
        _a = new FakeValueObject
        {
            Text = "text",
            Value = 10
        };
        _b = new FakeValueObject
        {
            Text = "text",
            Value = 10
        };
    }

    [TestCase]
    public void Equals_ReturnFalse_IfDifferentValueObjectTypeIsPassedAsParam()
    {
        _a.Equals(new { }).Should().BeFalse();
    }

    [TestCase]
    public void Equals_ReturnFalse_IfNullIsPassedAsParam()
    {
        _a.Equals(null).Should().BeFalse();
    }

    [TestCase]
    public void Equals_ReturnFalse_IfTextIsNotEqual()
    {
        _b.Text = "different";
        _a.Equals(_b).Should().BeFalse();
    }

    [TestCase]
    public void Equals_ReturnFalse_IfValueIsNotEqual()
    {
        _b.Value = 11;
        _a.Equals(_b).Should().BeFalse();
    }

    [TestCase]
    public void Equals_ReturnTrue_IfTheyHAveSameValueProperties()
    {
        _a.Equals(_b).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfLeftOperatorIsNull()
    {
        (null == _b).Should().BeFalse();
        (null != _b).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfRightOperatorIsNull()
    {
        (_a == null).Should().BeFalse();
        (_a != null).Should().BeTrue();
    }

    [TestCase]
    public void EqualityStaticOperator_CheckEquality_IfOperatoresAreNull()
    {
        _a = null;
        _b = null;

        (_a == _b).Should().BeTrue();
        (_a != _b).Should().BeFalse();
    }

    [TestCase]
    public void GetHashCode_ReturnsDifferentCode_IfPropertiesAreNotEquals()
    {
        // Arrange
        _b.Text = "different";

        // Act
        var code1 = _a.GetHashCode();
        var code2 = _b.GetHashCode();

        // Assert
        code1.Should().NotBe(code2);
    }

    [TestCase]
    public void GetHashCode_ReturnsSameCode_IfPropertiesAreEquals()
    {
        // Arrange & Act
        var code1 = _a.GetHashCode();
        var code2 = _b.GetHashCode();

        // Assert
        code1.Should().Be(code2);
    }
}