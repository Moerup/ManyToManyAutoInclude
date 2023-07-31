using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManyToManyAutoInclude.Data;

internal class LibraryConfig : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        // This does NOT include books automatically like expected
        builder.Navigation(x => x.Books).AutoInclude();

        // Unidirectional many to many config => https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many#unidirectional-many-to-many
        builder.HasMany(x => x.Books).WithMany();
    }
}
