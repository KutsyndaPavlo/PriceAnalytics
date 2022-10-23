namespace PriceAnalytics.Catalog.Domain
{
    public class ProductConcurrentUpdateException : Exception
    {
        public ProductConcurrentUpdateException()
        { }

        public ProductConcurrentUpdateException(string message)
            : base(message)
        { }

        public ProductConcurrentUpdateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
