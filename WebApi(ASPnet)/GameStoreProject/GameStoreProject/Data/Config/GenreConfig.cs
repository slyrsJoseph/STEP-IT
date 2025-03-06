using GameStoreProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStoreProject.Data.Config;

public class GenreConfig : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(gn => gn.Id);
        builder.Property(gn => gn.Name).IsRequired().HasMaxLength(100);
    }
}