namespace ManyToManyAutoInclude;

public class Book
{
    public Book(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }
}
