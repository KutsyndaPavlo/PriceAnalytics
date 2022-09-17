using Microsoft.Azure.Documents;
using PriceAnalytics.Search.IntegrationEvents;
using System.Linq.Expressions;

namespace PriceAnalytics.Search.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<Document> CreateItemAsync(T item);
        Task DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate);
        Task<Document> UpdateItemAsync(string id, T item);
    }
}
