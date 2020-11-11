using Common.Interfaces;
using LollipopServiceBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common;
using LollipopServiceBus;
using Microsoft.Extensions.Configuration;


namespace ServiceBusTest1
{
    public class MessageSender : IMessageSender
    {
        private readonly IBusClient _busClient;
        private readonly IConfiguration _configuration;

        public MessageSender(IBusClient busClient, IConfiguration configuration)
        {
            _busClient = busClient;
            _configuration = configuration;
            _busClient.AddTopic(_configuration["testTopic"]);
        }

        public Task SendMessage()
        {
            var message = new TestBusMessage
            {
                Message = "Hello cunts"
            };
            Console.WriteLine("Sending Message" + message.Message + " to " + _configuration["testTopic"]);
            return _busClient.SendMessage(message,  _configuration["testTopic"]);
        }
    }
}
