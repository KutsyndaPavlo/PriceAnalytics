namespace PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById
{
    public class SellerDto
    { 
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsActive { get; set; }
    }
}
