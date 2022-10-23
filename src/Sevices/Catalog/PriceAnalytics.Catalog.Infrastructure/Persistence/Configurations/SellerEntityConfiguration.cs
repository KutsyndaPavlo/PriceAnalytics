using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceAnalytics.Catalog.Domain.Seller;

namespace PriceAnalytics.Catalog.Infrastructure.Persistence.Configurations
{
    public class SellerEntityConfiguration : IEntityTypeConfiguration<SellerEntity>
    {
        public void Configure(EntityTypeBuilder<SellerEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("Id");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Name");

            builder.Property(t => t.Description)
                .HasColumnName("Description");

            //builder.Property(t => t.IsCompleted)
            //    .HasColumnName("IsCompleted")
            //    .IsRequired();

            //builder.Property(t => t.Gender)
            //    .HasColumnName("Gender")
            //    .HasColumnType("int")
            //    .IsRequired();

            builder.Ignore(e => e.DomainEvents);
        }
    }
}
