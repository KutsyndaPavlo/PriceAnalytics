namespace PriceAnalytics.ApiGateway.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsActive { get; set; }
    }
}
