using System.Text;

namespace ManyToManyAutoInclude;

public class Library
{
    public Library()
    {
    }

    public Library(int id, string name, List<Book> books)
    {
        Id = id;
        Name = name;
        Books = books;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public List<Book> Books { get; set; } = new();

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Library Name: {Name} - Books: ");

        foreach (var book in Books)
        {
            stringBuilder.Append($"{book.Name} ");
        }

        return stringBuilder.ToString();
    }
}
