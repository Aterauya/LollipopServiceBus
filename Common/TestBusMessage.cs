using LollipopServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class TestBusMessage : BusMessage
    {
        public string Message { get; set; }
    }
}
