namespace PriceAnalytics.Catalog.API
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }
    }
}
