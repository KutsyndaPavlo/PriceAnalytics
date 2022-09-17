namespace PriceAnalytics.ApiGateway.Models
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; set; }

        public Page Page { get; set; }
    }

    public class Page
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Count { get; set; }

        public long Total { get; set; }

    }
}
