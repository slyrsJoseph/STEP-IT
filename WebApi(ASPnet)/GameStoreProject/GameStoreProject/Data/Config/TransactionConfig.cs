namespace GameStoreProject.Data.Config;
using Microsoft.EntityFrameworkCore;
using GameStoreProject.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.PaymentMethod).HasMaxLength(50);
        builder.Property(t => t.Status).HasMaxLength(20);
        builder.Property(t => t.CreatedAt).IsRequired();

        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Order)
            .WithMany()
            .HasForeignKey(t => t.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}