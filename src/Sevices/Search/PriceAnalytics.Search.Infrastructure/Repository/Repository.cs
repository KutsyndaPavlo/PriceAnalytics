using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using PriceAnalytics.Search.Model;

namespace PriceAnalytics.Search.Repository
{   
    public class Repository<T> : IRepository<T> where T : class
    {

        private Container _container;

        public Repository(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(ProductProposal item)
        {
            await this._container.CreateItemAsync<ProductProposal>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<ProductProposal>(id, new PartitionKey(id));
        }

        public async Task<ProductProposal> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<ProductProposal> response = await this._container.ReadItemAsync<ProductProposal>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IOrderedQueryable<ProductProposal>> GetItemsAsync()
        {
            //var query = this._container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));
            //List<Item> results = new List<Item>();
            //while (query.HasMoreResults)
            //{
            //    var response = await query.ReadNextAsync();

            //    results.AddRange(response.ToList());
            //}

            //return results;

            return _container.GetItemLinqQueryable<ProductProposal>(allowSynchronousQueryExecution: true);
        }

        public async Task UpdateItemAsync(string id, ProductProposal item)
        {
            await this._container.UpsertItemAsync<ProductProposal>(item, new PartitionKey(id));
        }
    }
}
