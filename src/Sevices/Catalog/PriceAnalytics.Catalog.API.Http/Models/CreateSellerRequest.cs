using Microsoft.AspNetCore.Mvc;

namespace PriceAnalytics.Catalog.API.Models
{
    public class CreateSellerRequest
    {
        public string Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }
    }
}
