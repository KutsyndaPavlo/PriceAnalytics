using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Catalog.Domain.Seller;
using System.Reflection;

namespace PriceAnalytics.Catalog.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDomainEventService domainEventService) : base(options)
        {
            _domainEventService = domainEventService;
            Database.EnsureCreated();
        }

        public DbSet<ProductEntity> Products => Set<ProductEntity>();

        public DbSet<SellerEntity> Sellers => Set<SellerEntity>();

        public DbSet<ProposalEntity> Proposals => Set<ProposalEntity>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var events = ChangeTracker.Entries<BaseEntity>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(events);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            Seed(builder);

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(_connectionString))
        //    {
        //        optionsBuilder.UseSqlServer(
        //            _connectionString,
        //            x => x.MigrationsHistoryTable("__MigrationHistory", "efcore_3_0"));
        //    }
        //}

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SellerEntity>().HasData(
                SellerEntity.CreateNewAndActivate("Silpo", ""),
                SellerEntity.CreateNewAndActivate("Ashan", ""),
                SellerEntity.CreateNewAndActivate("Metro", ""));
        }
    }
}
