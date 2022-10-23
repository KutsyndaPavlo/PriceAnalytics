using Microsoft.AspNetCore.Mvc;

namespace PriceAnalytics.ApiGateway.Models
{
    public class CreateSellerRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
