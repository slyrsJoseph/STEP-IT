namespace GameStoreProject.Data.Config;
using Microsoft.EntityFrameworkCore;
using GameStoreProject.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WishlistConfig : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.HasKey(w => w.Id);

        builder.HasOne(w => w.User)
            .WithMany(u => u.Wishlists)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.Games)
            .WithMany(g => g.Wishlists)
            .HasForeignKey(w => w.GameId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}