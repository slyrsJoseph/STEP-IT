using GameStoreProject.Data.Models;

namespace GameStoreProject.Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class GamesConfig : IEntityTypeConfiguration<Games>
{
    public void Configure(EntityTypeBuilder<Games> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.title).IsRequired().HasMaxLength(100);
        builder.Property(g => g.Description).HasMaxLength(1000);
        builder.Property(g => g.price).HasColumnType("decimal(18,2)");
        builder.Property(g => g.ReleaseDate).IsRequired();

        builder.HasMany(g => g.Wishlists)
            .WithOne(w => w.Games)
            .HasForeignKey(w => w.GameId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}