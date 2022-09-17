namespace PriceAnalytics.Catalog.Domain
{
    public class SellerConcurrentUpdateException : Exception
    {
        public SellerConcurrentUpdateException()
        { }

        public SellerConcurrentUpdateException(string message)
            : base(message)
        { }

        public SellerConcurrentUpdateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
