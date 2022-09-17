namespace PriceAnalytics.Catalog.Domain.Product
{
    public class ProductCanNotBeDeletedException : Exception
    {
        public ProductCanNotBeDeletedException()
        { }

        public ProductCanNotBeDeletedException(string message)
            : base(message)
        { }

        public ProductCanNotBeDeletedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
