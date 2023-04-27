using ShareBook.Domain.Shared;

namespace ShareBook.Domain.Books;

public class Book : Entity<Guid>
{
    public string Owner { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Pages { get; private set; }
    public IEnumerable<string> Labels
    {
        get { return _labels.AsEnumerable(); }
    }
    private List<string> _labels { get; set; }
    public bool SharedByOwner { get; private set; }

    protected Book(
        Guid id,
        string owner,
        string title,
        string author,
        int pages,
        bool sharedByOwner,
        IEnumerable<string> labels) : base(id, DateTime.UtcNow)
    {
        Owner = owner;
        Title = title;
        Author = author;
        Pages = pages;
        SharedByOwner = sharedByOwner;
        SetupLabels(labels);

        Validate();
    }

    public void Update(string owner, string title, string author, int pages, bool sharedByOwner, IEnumerable<string> labels)
    {
        if(owner != Owner)
            throw new ArgumentException("User is not the owner of this book");

        Title = title;
        Author = author;
        Pages = Pages;
        SharedByOwner = sharedByOwner;
        SetupLabels(labels);

        Validate();
    }

    private void SetupLabels(IEnumerable<string> labels)
    {
        _labels = new List<string>();

        foreach(var label in labels)
            AddLabel(label);
    }

    private void AddLabel(string label)
    {
        if (string.IsNullOrWhiteSpace(label))
            throw new ArgumentNullException(nameof(label));

        if (!_labels.Contains(label))
            _labels.Add(label);
    }

    // TODO: move validation outside
    private void Validate() {
        if (Id == Guid.Empty) throw new ArgumentException(nameof(Id));
        if (string.IsNullOrWhiteSpace(Owner)) throw new ArgumentNullException(nameof(Owner));
        if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentNullException(nameof(Title));
        if (string.IsNullOrWhiteSpace(Author)) throw new ArgumentNullException(nameof(Author));
        if (Pages <= 0) throw new ArgumentOutOfRangeException(nameof(Pages));
    }

    public static Book New(
        Guid id,
        string owner,
        string title,
        string author,
        int pages,
        bool sharedByOwner,
        IEnumerable<string> labels = null)
    {
        return new Book(id, owner, title, author, pages, sharedByOwner, labels ?? Enumerable.Empty<string>());
    }
}