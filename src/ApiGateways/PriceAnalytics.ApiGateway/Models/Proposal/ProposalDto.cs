namespace PriceAnalytics.ApiGateway.Models
{
    public class ProposalDto
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public float Price { get; set; }

        public string Currency { get; set; }

        public bool IsActive { get; set; }

        public Guid SellerId { get; set; }

        public Guid ProductId { get; set; }
    }
}
