namespace PriceAnalytics.Catalog.Application.Common.Model
{
    public class Page
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Count { get; set; }

        public long Total { get; set; }

    }
}
