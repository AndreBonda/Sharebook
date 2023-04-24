using ShareBook.Domain.Books;

namespace ShareBook.UnitTests;

[TestFixture]
public class BookTests
{
    [TestCase("","valid_title","valid_author",1)]
    [TestCase(" ", "valid_title", "valid_author", 1)]
    [TestCase(null, "valid_title", "valid_author", 1)]
    public void New_ThrowsArgumentNullException_IfOwnerIsNullOrEmptyOrWhiteSpaces(string owner, string title, string author, int pages) {
        Assert.Throws<ArgumentNullException>(() => Book.New(owner, title, author, pages));
    }

    [TestCase("valid_owner", "", "valid_author", 1)]
    [TestCase("valid_owner", " ", "valid_author", 1)]
    [TestCase("valid_owner", null, "valid_author", 1)]
    public void New_ThrowsArgumentNullException_IfTitleIsNullOrEmptyOrWhiteSpaces(string owner, string title, string author, int pages)
    {
        Assert.Throws<ArgumentNullException>(() => Book.New(owner, title, author, pages));
    }

    [TestCase("valid_owner", "valid_title", "", 1)]
    [TestCase("valid_owner", "valid_title", " ", 1)]
    [TestCase("valid_owner", "valid_title", null, 1)]
    public void New_ThrowsArgumentNullException_IfAuthorIsNullOrEmptyOrWhiteSpaces(string owner, string title, string author, int pages)
    {
        Assert.Throws<ArgumentNullException>(() => Book.New(owner, title, author, pages));
    }

    [TestCase("valid_owner", "valid_title", "valid_author", 0)]
    [TestCase("valid_owner", "valid_title", "valid_author", -1)]
    public void New_ThrowsArgumentOutOfRangeException_IfPagesAreLessThanOne(string owner, string title, string author, int pages)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Book.New(owner, title, author, pages));
    }

    [Test]
    public void New_CreateNewBook_IfValidInputs() 
    {
        var before = DateTime.UtcNow;
        var emptyGuid = Guid.Empty;
        
        var book = Book.New("valid_owner", 
            "valid_title", 
            "valid_author", 
            1, 
            new string[] { "label1", "label2" });

        Assert.That(book, Is.Not.Null);
        Assert.That(book.Id, Is.Not.EqualTo(emptyGuid));
        Assert.That(book.CreatedAt, Is.GreaterThan(before));
        Assert.That(book.Owner, Is.EqualTo("valid_owner"));
        Assert.That(book.Title, Is.EqualTo("valid_title"));
        Assert.That(book.Author, Is.EqualTo("valid_author"));
        Assert.That(book.Pages, Is.EqualTo(1));
        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1", "label2" }));
    }

    [Test]
    public void New_CreateNewBook_RemoveingDuplicaeLabels()
    {
        var book = Book.New("valid_owner",
            "valid_title",
            "valid_author",
            1,
            new string[] { "label1", "label1" });

        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1" }));
    }

    [TestCase("")]
    [TestCase(null)]
    public void AddLabel_ThrowsArgumentNullExceptions_IfLabelIsNullOrWhiteSpaces(string label)
    {
        var book = Book.New("valid_owner",
            "valid_title",
            "valid_author",
            1,
            new string[] { "label1", "label2" });

        Assert.Throws<ArgumentNullException>(() => book.AddLabel(label));
    }

    [Test]
    public void AddLabel_IfValidInput()
    {
        var book = Book.New("valid_owner",
            "valid_title",
            "valid_author",
            1,
            new string[] { "label1" });

        book.AddLabel("label2");

        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1", "label2" }));
    }

    [Test]
    public void AddLabel_DoesNotAdd_IfLabelAlreadyExists()
    {
        var book = Book.New("valid_owner",
            "valid_title",
            "valid_author",
            1,
            new string[] { "label1" });

        book.AddLabel("label1");

        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1" }));
    }

    [Test]
    public void RemoveLabel_DoesNotRemove_IfLabelIsNotContained()
    {
        var book = Book.New("valid_owner",
            "valid_title",
            "valid_author",
            1,
            new string[] { "label1" });

        book.RemoveLabel("label2");

        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1" }));
    }

    [Test]
    public void RemoveLabel_IfValidInput()
    {
        var book = Book.New("valid_owner",
            "valid_title",
            "valid_author",
            1,
            new string[] { "label1", "label2" });

        book.RemoveLabel("label2");

        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1" }));
    }
}