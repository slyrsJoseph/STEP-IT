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
            .OnDelete(DeleteBehavior.Cascade); // Если пользователь удаляется, его вишлист тоже

        builder.HasOne(w => w.Game)
            .WithMany(g => g.Wishlists)
            .HasForeignKey(w => w.GameId)
            .OnDelete(DeleteBehavior.Cascade); // Если игра удаляется, удаляются и записи в вишлисте

        // Добавляем уникальный индекс, чтобы один и тот же пользователь не мог добавить одну игру дважды
        builder.HasIndex(w => new { w.UserId, w.GameId }).IsUnique();
    }
}
