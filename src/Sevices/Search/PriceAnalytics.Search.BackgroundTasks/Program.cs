using Autofac;
using PriceAnalytics.Infrustructure.EventBus;
using PriceAnalytics.Infrustructure.EventBus.Abstractions;
using PriceAnalytics.Infrustructure.EventBusRabbitMQ;
using PriceAnalytics.Search.IntegrationEvents;
using PriceAnalytics.Search.IntegrationEvents.EventHandling;
using PriceAnalytics.Search.Model;
using PriceAnalytics.Search.Repository;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IRepository<ProductProposal>>(
    InitializeCosmosClientInstanceAsync().GetAwaiter().GetResult());
builder.Services.AddTransient<ProposalCreatedIntegrationEventHandler>();
RegisterEventBus(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
ConfigureEventBus(app);

app.Run();

static void RegisterEventBus(IServiceCollection services)
{
    services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
    {
        var subscriptionClientName = "searchApi";//Configuration["SubscriptionClientName"];
        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
        ILifetimeScope iLifetimeScope = null;//sp.GetRequiredService<ILifetimeScope>();
        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

        var retryCount = 5;
        //if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
        //{
        //    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
        //}

        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
    });

    services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
    services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
    {
        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

        var factory = new ConnectionFactory()
        {
            HostName = Environment.GetEnvironmentVariable("EventBusConnection"),
            DispatchConsumersAsync = true
        };

        //if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
        //{
        factory.UserName = Environment.GetEnvironmentVariable("EventBusUserName");// Configuration["EventBusUserName"];
                                                                                  //}

        //if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
        //{
        factory.Password = Environment.GetEnvironmentVariable("EventBusPassword");// Configuration["EventBusPassword"];
                                                                                  //}

        var retryCount = 5;
        //if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
        //{
        //    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
        //}

        return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
    });
}

static void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

    eventBus.Subscribe<ProposalCreatedIntegrationEvent, ProposalCreatedIntegrationEventHandler>();
}

static async Task<IRepository<ProductProposal>> InitializeCosmosClientInstanceAsync()
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
