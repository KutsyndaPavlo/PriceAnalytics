using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceAnalytics.Catalog.Application.Common.Model
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; set; }

        public Page Page { get; set; }
    }
}
