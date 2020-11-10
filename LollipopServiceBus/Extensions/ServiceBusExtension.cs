using LollipopServiceBus.AzureServiceBus;
using LollipopServiceBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LollipopServiceBus.Extensions
{
    public static class ServiceBusExtension
    {
        /// <summary>
        /// Registers the Azure Service Bus client to the service provider
        /// </summary>
        /// <param name="services">Current service provider</param>
        /// <param name="connectionString">The connection string for the azure service bus</param>
        /// <returns></returns>
        public static IServiceCollection AddAzureServiceBus(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IBusClient, AzureServiceBusClient>(asbc => new AzureServiceBusClient(connectionString));
            return services;
        }
    }

}
