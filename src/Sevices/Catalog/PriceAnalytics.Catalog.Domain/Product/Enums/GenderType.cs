using PriceAnalytics.Catalog.Domain.Common;

namespace PriceAnalytics.Catalog.Domain.Product.Enums
{
    public class GenderType : Enumeration
    {
        public static GenderType Male = new(1, nameof(Male));
        public static GenderType Female = new(2, nameof(Female));
        public static GenderType All = new(3, nameof(All));

        public GenderType(int id, string name)
            : base(id, name)
        {
        }
    }
}
