using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Product.Events;
using PriceAnalytics.Catalog.Domain.Product.Rules;

namespace PriceAnalytics.Catalog.Domain.Product
{
    public class ProductEntity : BaseEntity, IAggregateRoot
    {
        private ProductEntity(string name, string code, string description) : base()
        {
            this.Name = name;
            this.Code = code;
            this.Description = description;
            this.IsActive = true;

            this._domainEvents.Add(new ProductCreatedEvent(this));
        }

        public string Name { get; private set; }

        public string? Code { get; private set; }

        public string? Description { get; private set; }

        public bool IsActive { get; private set; }

        public static ProductEntity CreateNew(string name,
                                              string code,
                                              string description,
                                              IProductService productUniquenessChecker)
        {
            CheckRule(new ProductNameMustBeUniqueRule(productUniquenessChecker, name));

            return new ProductEntity(name, code, description);
        }

        public void UpdateProduct(
            string name,
            string? code,
            string? description,
            IProductService productService)
        {
            CheckRule(new ProductNameMustBeUniqueRule(productService, name));

            this.Name = name;
            this.Code = code;
            this.Description = description;
            this.LastModified = DateTime.UtcNow;

            this._domainEvents.Add(new ProductUpdatedEvent(this));
        }

        public void DeActivate()
        {
            IsActive = false;
            this.LastModified = DateTime.UtcNow;
            this._domainEvents.Add(new ProductDeactivatedEvent(this));
        }

        public bool CanBeDeleted(IProductService productService)
        {
            return !productService.HistoricalProposals(this).Any();
        }

        public IReadOnlyCollection<ProposalEntity?> ActiveProposals(IProductService productService)
        {
            return productService.LatestProposals(this).ToList().AsReadOnly();
        }
    }
}