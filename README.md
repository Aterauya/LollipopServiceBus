# LollipopServiceBus

## What is it?
Lollipop service bus is a library that helps connect to a service bus which broadcasts messages to one or many subscribers.
At the minute the only implementaton allows for Azure Service Bus as a message bus.
## How to use
Install the LollipopServiceBus NuGet package from your nuget feed or download it from [here](https://www.nuget.org/packages/LollipopServiceBus/1.0.0).
### Connect to your service bus
In the configure services method call the AddAzureServiceBus method with the connection string for your service bus instance.<br>
##### Startup.cs
```csharp
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAzureServiceBus("Azure-Service-Bus-Connection-String");
        }
```


### Throw a message on the service bus
Wherever you want to send a message inject the bus client and send a message using the SendMessage method on the bus client.
<br>
##### AzureServiceBusTestController.cs
```csharp
        private readonly IBusClient _busClient;

        public AzureServiceBusTestController(IBusClient busClient)
        {
            busClient = _busClient;
        }

        // GET
        public void Index()
        {
            //TestBusMessage Extends BusMessage
            var busMessage = new TestBusMessage
            {
                Message = "Your-test-bus-message"
            };
            _busClient.SendMessage(busMessage, "Your-topic-name");
        }
```
### Listen for messages thrown on the bus
To listen for messages you need to add a subcription to the Azure Service Bus client, for that you need a topic name, subscription name and a message handler.
##### Startup.cs
```csharp
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.GetService<IBusClient>().AddSubscription("Your-topic-name", "Your-subscription-name", MessageHandler);
        }
```

### Handle messages thrown on the bus
Create a message handler that extends IMessageHandler.
#### TestMessageHandler
```csharp
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
            var data = JsonConvert.DeserializeObject<TestBusMessage>(Encoding.UTF8.GetString(message.Body));
            Console.WriteLine(data.Message);
            Console.ReadKey();
            return Task.CompletedTask;
        }
    }

```
