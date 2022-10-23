namespace PriceAnalytics.Catalog.Domain
{
    public class ProposalNotFoundException : Exception
    {
        public ProposalNotFoundException()
        { }

        public ProposalNotFoundException(string message)
            : base(message)
        { }

        public ProposalNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

