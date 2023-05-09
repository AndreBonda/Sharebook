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
        Assert.False(_a.Equals(new object {}));
    }

    [TestCase]
    public void Equals_ReturnFalse_IfNullIsPassedAsParam()
    {
        _b = null;
        Assert.False(_a.Equals(_b));
    }

    [TestCase]
    public void Equals_ReturnFalse_IfTextIsNotEqual()
    {
        _b.Text = "different";
        Assert.False(_a.Equals(_b));
    }

    [TestCase]
    public void Equals_ReturnFalse_IfValueIsNotEqual()
    {
        _b.Value = 11;
        Assert.False(_a.Equals(_b));
    }

    [TestCase]
    public void Equals_ReturnTrue_IfTheyHAveSameValueProperties()
    {
        Assert.True(_a.Equals(_b));
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfLeftOperatorIsNull()
    {
        _a = null;
        Assert.False(_a == _b);
        Assert.True(_a != _b);
    }

    [TestCase]
    public void EqualityStaticOperator_CheckInequality_IfRightOperatorIsNull()
    {
        _b = null;
        Assert.False(_a == _b);
        Assert.True(_a != _b);
    }

    [TestCase]
    public void EqualityStaticOperator_CheckEquality_IfOperatoresAreNull()
    {
        _a = null;
        _b = null;
        Assert.True(_a == _b);
        Assert.False(_a != _b);
    }

    [TestCase]
    public void GetHashCode_ReturnsDifferentCode_IfPropertiesAreNotEquals()
    {
        _b.Text = "different";

        var code1 = _a.GetHashCode();
        var code2 = _b.GetHashCode();

        Assert.That(code1, Is.Not.EqualTo(code2));
    }

    [TestCase]
    public void GetHashCode_ReturnsSameCode_IfPropertiesAreEquals()
    {
        var code1 = _a.GetHashCode();
        var code2 = _b.GetHashCode();

        Assert.That(code1, Is.EqualTo(code2));
    }
}