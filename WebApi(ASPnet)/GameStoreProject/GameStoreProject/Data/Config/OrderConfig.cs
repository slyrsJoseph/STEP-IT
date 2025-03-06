namespace GameStoreProject.Data.Config;
using Microsoft.EntityFrameworkCore;
using GameStoreProject.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfig: IEntityTypeConfiguration<Order> 
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");
        builder.Property(o => o.CreatedAt).IsRequired();

        builder.HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}