using ManyToManyAutoInclude.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ManyToManyAutoInclude;

internal class Program
{
    static void Main(string[] args)
    {
        var app = CreateHostBuilder(args).Build();

        // Recreate database for testing
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        // Create test data
        var books = new List<Book> 
        {
            new(1, "Book1"),
            new(2, "Book2"),
            new(3, "Book3"),
        };

        dbContext.Books.AddRange(books);
        dbContext.SaveChanges();

        dbContext.Libraries.Add(new Library(0, "Library1", books));
        dbContext.SaveChanges();
        dbContext.ChangeTracker.Clear();

        // No related books returned
        var libraryWithAutoInclude = dbContext.Libraries.OrderBy(x => x.Id).First();
        Console.WriteLine("AutoInclude query");
        Console.WriteLine(libraryWithAutoInclude);
        Console.WriteLine();

        // Related books returned
        var libraryWithSpecificInclude = dbContext.Libraries.Include(x => x.Books).OrderBy(x => x.Id).First();
        Console.WriteLine("Specific include query");
        Console.WriteLine(libraryWithSpecificInclude);

        Console.ReadLine();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging(builder =>
                {
                    builder.AddFilter("Microsoft", LogLevel.Warning);
                });

                services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=LocalDatabase.db"));
            });
}
