namespace PriceAnalytics.ApiGateway.Models
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }
    }
}
