using Microsoft.Azure.Documents;
using System.Linq.Expressions;

namespace PriceAnalytics.Search.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IOrderedQueryable<Item>> GetItemsAsync();
        Task<Item> GetItemAsync(string id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(string id, Item item);
        Task DeleteItemAsync(string id);
    }
}
