using LollipopServiceBus.AzureServiceBus;
using Microsoft.Extensions.DependencyInjection;
using LollipopServiceBus.Interfaces;

namespace LollipopServiceBus.Extensions
{
    public static class ServiceBusExtension
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services)
        {
            services.AddTransient<IBusClient, AzureServiceBusClient>();
            return services;
        }
    }

}
