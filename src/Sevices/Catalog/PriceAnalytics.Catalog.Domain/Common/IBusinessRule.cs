using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceAnalytics.Catalog.Domain.Common
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}
