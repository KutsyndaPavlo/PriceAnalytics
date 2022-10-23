using PriceAnalytics.Search.Model;

namespace PriceAnalytics.Search.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IOrderedQueryable<ProductProposal>> GetItemsAsync();
        Task<ProductProposal> GetItemAsync(string id);
        Task AddItemAsync(ProductProposal item);
        Task UpdateItemAsync(string id, ProductProposal item);
        Task DeleteItemAsync(string id);
    }
}
