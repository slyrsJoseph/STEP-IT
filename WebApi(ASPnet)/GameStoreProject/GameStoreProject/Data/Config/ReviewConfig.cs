namespace GameStoreProject.Data.Config;
using Microsoft.EntityFrameworkCore;
using GameStoreProject.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ReviewConfig : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.Comment).HasMaxLength(500);
        builder.Property(r => r.CreatedAt).IsRequired();
    }
}