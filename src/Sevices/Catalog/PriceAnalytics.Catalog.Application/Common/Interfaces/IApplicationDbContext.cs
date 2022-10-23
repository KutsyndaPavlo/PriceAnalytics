using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Catalog.Domain.Seller;

namespace PriceAnalytics.Catalog.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<SellerEntity> Sellers { get; }

        DbSet<ProductEntity> Products { get; }

        DbSet<ProposalEntity> Proposals { get; }        

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
