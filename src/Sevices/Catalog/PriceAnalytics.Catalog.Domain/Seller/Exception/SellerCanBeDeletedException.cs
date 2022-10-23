using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceAnalytics.Catalog.Domain.Seller
{
    public class SellerCanBeDeletedException : Exception
    {
        public SellerCanBeDeletedException()
        { }

        public SellerCanBeDeletedException(string message)
            : base(message)
        { }

        public SellerCanBeDeletedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
