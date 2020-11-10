using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LollipopServiceBus.Interfaces
{
    interface IBusClient
    {
        /// <summary>
        /// Sends the message to the given topic
        /// </summary>
        /// <param name="busMessage">The message to send to the bus</param>
        /// <param name="topicName">The name of the topic to send the message</param>
        /// <returns></returns>
        Task SendMessage(BusMessage busMessage, string topicName);

        /// <summary>
        /// Adds a new topic to the client
        /// </summary>
        /// <param name="connectionString">The azure service bus connection string</param>
        /// <param name="topicName">The topic to send the message to</param>
        void AddTopic( string topicName);

        /// <summary>
        /// Adds a new subscription to the client
        /// </summary>
        /// <param name="connectionString">The azure service bus connection string</param>
        /// <param name="topicName">The topic to read messages from</param>
        /// <param name="subscriptionName">The subscription to connect to</param>
        void AddSubscription(string topicName, string subscriptionName, IMessageHandler messageHandler);
    }
}
