using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceAnalytics.Catalog.Domain.Common
{
    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTime.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
