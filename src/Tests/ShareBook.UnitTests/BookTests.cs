using ShareBook.Domain.Books;
using ShareBook.Domain.Books.Exceptions;

namespace ShareBook.UnitTests;

[TestFixture]
public class BookTests
{
    [Test]
    public void New_ThrowsException_IfGuidIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => Book.New(Guid.Empty, "valid_owner", "valid_title", "valid_author", 1, true));
    }

    [TestCase("","valid_title","valid_author",1, true)]
    [TestCase(" ", "valid_title", "valid_author", 1, true)]
    [TestCase(null, "valid_title", "valid_author", 1, true)]
    public void New_ThrowsArgumentNullException_IfOwnerIsNullOrEmptyOrWhiteSpaces(string owner, string title, string author, int pages, bool sharedByUser) {
        Assert.Throws<ArgumentNullException>(() => Book.New(Guid.NewGuid(),owner, title, author, pages, sharedByUser));
    }

    [TestCase("valid_owner", "", "valid_author", 1, true)]
    [TestCase("valid_owner", " ", "valid_author", 1, true)]
    [TestCase("valid_owner", null, "valid_author", 1, true)]
    public void New_ThrowsArgumentNullException_IfTitleIsNullOrEmptyOrWhiteSpaces(string owner, string title, string author, int pages, bool sharedByUser)
    {
        Assert.Throws<ArgumentNullException>(() => Book.New(Guid.NewGuid(),owner, title, author, pages, true));
    }

    [TestCase("valid_owner", "valid_title", "", 1, true)]
    [TestCase("valid_owner", "valid_title", " ", 1, true)]
    [TestCase("valid_owner", "valid_title", null, 1, true)]
    public void New_ThrowsArgumentNullException_IfAuthorIsNullOrEmptyOrWhiteSpaces(string owner, string title, string author, int pages, bool sharedByUser)
    {
        Assert.Throws<ArgumentNullException>(() => Book.New(Guid.NewGuid(),owner, title, author, pages, sharedByUser));
    }

    [TestCase("valid_owner", "valid_title", "valid_author", 0, true)]
    [TestCase("valid_owner", "valid_title", "valid_author", -1, true)]
    public void New_ThrowsArgumentOutOfRangeException_IfPagesAreLessThanOne(string owner, string title, string author, int pages, bool sharedByUser)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Book.New(Guid.NewGuid(),owner, title, author, pages, sharedByUser));
    }

    [Test]
    public void New_CreateNewBook_IfValidInputs() 
    {
        var emptyGuid = Guid.Empty;
        
        var book = Book.New(
            Guid.NewGuid(),
            "valid_owner", 
            "valid_title", 
            "valid_author", 
            1,
            true, 
            new string[] { "label1", "label2" });

        Assert.That(book, Is.Not.Null);
        Assert.That(book.Id, Is.Not.EqualTo(emptyGuid));
        Assert.That(book.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(1).Minutes);
        Assert.That(book.Owner, Is.EqualTo("valid_owner"));
        Assert.That(book.Title, Is.EqualTo("valid_title"));
        Assert.That(book.Author, Is.EqualTo("valid_author"));
        Assert.That(book.Pages, Is.EqualTo(1));
        Assert.IsTrue(book.SharedByOwner);
        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1", "label2" }));

    }

    [Test]
    public void New_CreateNewBook_IgnoringDuplicateLabels()
    {
        var book = Book.New(
            Guid.NewGuid(),
            "valid_owner",
            "valid_title",
            "valid_author",
            1,
            true,
            new string[] { "label1", "label1" });

        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label1" }));
    }

    [Test]
    public void Update_ThrowsUpdateBookUserNotOwnerException_IfCurrentUserIsNotTheBookOwner()
    {
        var book = Book.New(
            Guid.NewGuid(),
            "owner",
            "title",
            "author",
            1,
            true,
            new string[] { "label1" });

        Assert.Throws<UpdateBookUserNotOwnerException>(() => book.Update(
            "not_the_owner",
            "title",
            "author",
            1,
            true,
            new string[] { "label1" }
        ));
    }

    [Test]
    public void Update_UpdateFields_IfValidInputs()
    {
        var book = Book.New(
            Guid.NewGuid(),
            "owner",
            "title",
            "author",
            1,
            true,
            new string[] { "label1" });

         book.Update(
            "owner",
            "title_update",
            "author_update",
            2,
            false,
            new string[] { "label2" });

        Assert.That(book.Title, Is.EqualTo("title_update"));
        Assert.That(book.Author, Is.EqualTo("author_update"));
        Assert.That(book.Pages, Is.EqualTo(2));
        Assert.IsFalse(book.SharedByOwner);
        Assert.That(book.Labels, Is.EquivalentTo(new string[] { "label2" }));
    }
}