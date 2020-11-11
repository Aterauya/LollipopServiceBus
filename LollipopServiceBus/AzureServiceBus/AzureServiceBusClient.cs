using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LollipopServiceBus.Interfaces;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace LollipopServiceBus.AzureServiceBus
{
    public class AzureServiceBusClient : IBusClient
    {
        private readonly List<ITopicClient> _topicClients;
        private readonly List<ISubscriptionClient> _subscriptionClients;
        private readonly string _serviceBusConnectionString;

        public AzureServiceBusClient(string connectionString)
        {
            _topicClients = new List<ITopicClient>();
            _subscriptionClients = new List<ISubscriptionClient>();

            _serviceBusConnectionString = connectionString;
        }


        /// <inheritdoc />
        public async Task SendMessage(BusMessage busMessage, string topicName)
        {
            var data = JsonConvert.SerializeObject(busMessage);

            var message = new Message
            {
                Body = Encoding.UTF8.GetBytes(data)
            };

            await GetTopicClient(topicName).SendAsync(message);
        }

        /// <inheritdoc />
        public void AddTopic( string topicName)
        {
            var topicClient = new TopicClient(_serviceBusConnectionString, topicName);
            _topicClients.Add(topicClient);
            
        }

        /// <inheritdoc />
        public void AddSubscription(string topicName, string subscriptionName, IMessageHandler messageHandler)
        {
            var subscriptionClient = new SubscriptionClient(_serviceBusConnectionString, topicName, subscriptionName);
            _subscriptionClients.Add(subscriptionClient);
            RegisterMessageHandler(messageHandler, subscriptionClient);
        }

        private ITopicClient GetTopicClient(string topicName)
        {
            return _topicClients.Find(c => c.TopicName.Equals(topicName));
        }

        private void RegisterMessageHandler(IMessageHandler messageHandler, SubscriptionClient subClient)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionRecievedHandler)
            {
                // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = true
            };

            subClient.RegisterMessageHandler(messageHandler.Handle, messageHandlerOptions);
        }

        private Task ExceptionRecievedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }
    }
}
