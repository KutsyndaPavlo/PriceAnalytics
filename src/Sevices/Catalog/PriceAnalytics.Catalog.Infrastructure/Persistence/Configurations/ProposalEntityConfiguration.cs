using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Proposal;

namespace PriceAnalytics.Catalog.Infrastructure.Persistence.Configurations
{
    public class ProposalEntityConfiguration : IEntityTypeConfiguration<ProposalEntity>
    {
        public void Configure(EntityTypeBuilder<ProposalEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                .HasColumnName("Id");

            builder.Property(t => t.Description)
                .HasColumnName("Description");

            //builder.Property(t => t.IsCompleted)
            //    .HasColumnName("IsCompleted")
            //    .IsRequired();

            //builder.Property(t => t.Gender)
            //    .HasColumnName("Gender")
            //    .HasColumnType("int")
            //    .IsRequired();

            //builder.HasOne(s => s.Seller)
            //    .WithMany(s => s.Prices)
            //    .HasForeignKey(s => s.SellerId)
            //    .IsRequired();

            builder.OwnsOne<MoneyValue>("Price", mv =>
            {
                mv.Property(p => p.Currency).HasColumnName("Currency");
                mv.Property(p => p.Price).HasColumnName("Price");
            });

           // modelBuilder.Entity<Blog>()
           //.HasOne(b => b.BlogImage)
           //.WithOne(i => i.Blog)
           //.HasForeignKey<BlogImage>(b => b.BlogForeignKey);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}
