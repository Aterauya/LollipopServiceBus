using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace LollipopServiceBus.Interfaces
{
    public interface IMessageHandler
    {
        Task Handle(Message message, CancellationToken token);
    }
}
