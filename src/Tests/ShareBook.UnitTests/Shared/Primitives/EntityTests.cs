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
        Assert.That(_a, Is.Not.EqualTo(new object{}));
    }

    [TestCase]
    public void Equals_ReturnFalse_IfNullIsPassedAsParam()
    {
        Assert.That(_a, Is.Not.EqualTo(null));
    }

    [TestCase]
    public void Equals_ReturnFalse_IfEntitiesHaveDifferentId()
    {
        Assert.That(_a, Is.Not.EqualTo(_b));
    }

    [TestCase]
    public void Equals_ReturnTrue_IfEntitiesHaveSameId()
    {
        var id = Guid.NewGuid();
        _a = new FakeEntity(id);
        _b = new FakeEntity(id);
        
        Assert.That(_a, Is.EqualTo(_b));
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
    public void GetHashCode_ReturnsDifferentCode_IfIdsAreNotEqual()
    {
        var code1 = _a.GetHashCode();
        var code2 = _b.GetHashCode();

        Assert.That(code1, Is.Not.EqualTo(code2));
    }

    [TestCase]
    public void GetHashCode_ReturnsSameCode_IfIdsAreEqual()
    {
        var id = Guid.NewGuid();
        _a = new FakeEntity(id);
        _b = new FakeEntity(id);

        var code1 = _a.GetHashCode();
        var code2 = _b.GetHashCode();

        Assert.That(code1, Is.EqualTo(code2));
    }
}