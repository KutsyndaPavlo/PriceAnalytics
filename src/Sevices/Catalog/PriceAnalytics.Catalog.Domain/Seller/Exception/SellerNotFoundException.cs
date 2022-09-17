namespace PriceAnalytics.Catalog.Domain
{
    public class SellerNotFoundException : Exception
    {
        public SellerNotFoundException()
        { }

        public SellerNotFoundException(string message)
            : base(message)
        { }

        public SellerNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

