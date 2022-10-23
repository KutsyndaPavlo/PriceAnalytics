using Newtonsoft.Json;

namespace PriceAnalytics.Search.Model
{
    public class ProductProposal
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        //[JsonProperty(PropertyName = "name")]
        //public string Name { get; set; }

        //[JsonProperty(PropertyName = "description")]
        //public string Description { get; set; }

        //[JsonProperty(PropertyName = "isComplete")]
        //public bool Completed { get; set; }

        public Guid ProductId { get; set; }

        public Guid SellerId { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string SellerName { get; set; }
    }
}