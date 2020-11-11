using System;
using System.Collections.Generic;
using System.Text;
using LollipopServiceBus.Extensions;
using LollipopServiceBus.Interfaces;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBusTestRecieve
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddAzureServiceBus(_configuration["azureServiceBusConnectionString"])
                .AddTransient<IMessageHandler, TestMessageHandler>()
                .AddSingleton(_configuration)
                .BuildServiceProvider();

            serviceProvider.GetService<IBusClient>()
                .AddSubscription(_configuration["testTopic"], _configuration["testSubscription"], 
                    serviceProvider.GetService<IMessageHandler>());

            Console.ReadKey();
        }
    }
}
