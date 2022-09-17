using Microsoft.AspNetCore.Mvc;

namespace PriceAnalytics.ApiGateway.Models
{
    public class UpdateProposalRequest
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public DateTime LastModified { get; set; }

        public float Price { get; set; }

        public string Currency { get; set; }
    }
}
