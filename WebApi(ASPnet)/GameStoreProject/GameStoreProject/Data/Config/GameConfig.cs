using GameStoreProject.Data.Models;

namespace GameStoreProject.Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class GameConfig : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.title).IsRequired().HasMaxLength(150);
        builder.Property(g => g.Description).HasMaxLength(1000);
        builder.Property(g => g.price).HasPrecision(18, 2);
        builder.HasOne(g => g.Developer)
            .WithMany(d => d.Games)
            .HasForeignKey(g => g.DeveloperId);
        builder.HasOne(g => g.Genre)
            .WithMany(gn => gn.Games)
            .HasForeignKey(g => g.GenreId);
    }
}

