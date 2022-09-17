using Microsoft.AspNetCore.Mvc;

namespace PriceAnalytics.ApiGateway.Models
{
    public class CreateProposalRequest
    {
        public Guid SellerId { get; set; }

        public Guid ProductId { get; set; }

        public float Price { get; set; }

        public string Currency { get; set; }

        public string? Description { get; set; }
    }
}
