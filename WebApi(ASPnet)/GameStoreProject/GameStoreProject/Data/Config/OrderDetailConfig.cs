namespace GameStoreProject.Data.Config;
using Microsoft.EntityFrameworkCore;
using GameStoreProject.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(od => od.Id);
        builder.Property(od => od.PriceAtPurchase).HasColumnType("decimal(18,2)");

        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(od => od.Games)
            .WithMany(g => g.OrderDetails)
            .HasForeignKey(od => od.GameId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}