

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PriceAnalytics.Catalog.Application;
using PriceAnalytics.Catalog.Infrastructure;
using PriceAnalytics.Administration.Services;
using PriceAnalytics.Administration.Services.Services;
using PriceAnalytics.Catalog.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(PriceAnalytics.Administration.Services.MappingProfile.AutoMapperConfig, typeof(PriceAnalytics.Administration.Services.MappingProfile).Assembly);
var hcBuilder = builder.Services.AddHealthChecks();
hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

//hcBuilder
//    .AddSqlServer(
//        builder.Configuration["ConnectionString"],
//        name: "OrderingDB-check",
//        tags: new string[] { "orderingdb" });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProductService>();
app.MapGrpcService<ProposalService>();
app.MapGrpcService<SellerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
//https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-6.0
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecks("/liveness", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("self")
});

app.Run();
