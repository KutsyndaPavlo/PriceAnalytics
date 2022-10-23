using PriceAnalytics.Infrustructure.EventBus;
using PriceAnalytics.Infrustructure.EventBus.Abstractions;
using PriceAnalytics.Infrustructure.EventBusRabbitMQ;
using PriceAnalytics.Search.IntegrationEvents.EventHandling;
using RabbitMQ.Client;

namespace PriceAnalytics.Search.IntegrationEvents
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
