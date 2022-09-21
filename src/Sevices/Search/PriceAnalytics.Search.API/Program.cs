using PriceAnalytics.Search.IntegrationEvents;
using PriceAnalytics.Search.Repository;
using Microsoft.AspNetCore.OData;
using PriceAnalytics.Search.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddSingleton<IRepository<ProductProposal>>(
    InitializeCosmosClientInstanceAsync(builder.Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());

var app = builder.Build();
app.ConfigureEventBus();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();


static async Task<IRepository<ProductProposal>> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
{
    string databaseName = Environment.GetEnvironmentVariable("AZURE_COSMOS_DB_NAME");
    string containerName = Environment.GetEnvironmentVariable("AZURE_COSMOS_DB_CONTAINER_NAME");
    string account = Environment.GetEnvironmentVariable("AZURE_COSMOS_DB_ACCOUNT");
    string key = Environment.GetEnvironmentVariable("AZURE_COSMOS_DB_KEY"); ;
    Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
    IRepository<ProductProposal> cosmosDbService = new Repository<ProductProposal>(client, databaseName, containerName);
    Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

    return cosmosDbService;
}