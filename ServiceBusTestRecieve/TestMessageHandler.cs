using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LollipopServiceBus.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace ServiceBusTestRecieve
{
    public class TestMessageHandler : IMessageHandler
    {
        private readonly IBusClient _busClient;

        private readonly IConfiguration _config;

        public TestMessageHandler(IBusClient busClient, IConfiguration config)
        {
            _busClient = busClient;
            _config = config;
        }


        public Task Handle(Message message, CancellationToken token)
        {
            Console.WriteLine(message.Body);
            Console.ReadKey();
            return Task.CompletedTask;
        }
    }
}
