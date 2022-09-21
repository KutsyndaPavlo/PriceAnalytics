// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


//static async Task<IRepository<ProductProposal>> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
//{
//    string databaseName = Environment.GetEnvironmentVariable("AZURE_COSMOS_DB_NAME");
//    string containerName = Environment.GetEnvironmentVariable("AZURE_COSMOS_DB_CONTAINER_NAME");
//    string account = Environment.GetEnvironmentVariable("AZURE_COSMOS_DB_ACCOUNT");
//    string key = Environment.GetEnvironmentVariable("AZURE_COSMOS_DB_KEY"); ;
//    Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
//    IRepository<ProductProposal> cosmosDbService = new Repository<ProductProposal>(client, databaseName, containerName);
//    Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
//    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

//    return cosmosDbService;
//}
