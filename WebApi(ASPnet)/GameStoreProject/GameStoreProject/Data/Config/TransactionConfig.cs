namespace GameStoreProject.Data.Config;
using Microsoft.EntityFrameworkCore;
using GameStoreProject.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict); // ✅ Убираем каскадное удаление

        builder.HasOne(t => t.Order)
            .WithOne(o => o.Transaction)
            .HasForeignKey<Transaction>(t => t.OrderId)
            .OnDelete(DeleteBehavior.Restrict); // ✅ Убираем каскадное удаление

        builder.Property(t => t.PaymentMethod).IsRequired().HasMaxLength(50);
        builder.Property(t => t.Status).IsRequired().HasMaxLength(50);
    }
}
