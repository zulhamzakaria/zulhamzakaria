// Act as a generic properties for publishing messages to Azure
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.MessageBus
{
    public class BaseMessage
    {
        public int MessageId { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}
