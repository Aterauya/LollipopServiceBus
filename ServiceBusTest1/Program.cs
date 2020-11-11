using System;
using Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using LollipopServiceBus.Extensions;
using LollipopServiceBus.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ServiceBusTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddAzureServiceBus(_configuration["azureServiceBusConnectionString"])
                .AddTransient<IMessageSender, MessageSender>()
                .AddSingleton(_configuration)
                .BuildServiceProvider();

            serviceProvider.GetService<IMessageSender>().SendMessage();
            Console.ReadKey();

        }
    }
}
