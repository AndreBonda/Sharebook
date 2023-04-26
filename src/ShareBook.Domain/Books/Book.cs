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

    protected Book(
        Guid id,
        string owner,
        string title,
        string author,
        int pages,
        IEnumerable<string> labels) : base(id, DateTime.UtcNow)
    {
        if(id == Guid.Empty) throw new ArgumentException(nameof(id));
        if (string.IsNullOrWhiteSpace(owner)) throw new ArgumentNullException(nameof(owner));
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentNullException(nameof(author));
        if (pages <= 0) throw new ArgumentOutOfRangeException(nameof(pages));

        Owner = owner;
        Title = title;
        Author = author;
        Pages = pages;
        _labels = labels != null ?
            labels.Distinct().ToList() :
            new List<string>();
    }

    public void AddLabel(string label)
    {
        if (string.IsNullOrWhiteSpace(label))
            throw new ArgumentNullException(nameof(label));

        if (!_labels.Contains(label))
            _labels.Add(label);
    }

    public void RemoveLabel(string label)
    {
        if (_labels.Contains(label))
            _labels.Remove(label);
    }

    public static Book New(
        Guid id,
        string owner,
        string title,
        string author,
        int pages,
        IEnumerable<string> labels = null)
    {
        return new Book(id, owner, title, author, pages, labels);
    }
}