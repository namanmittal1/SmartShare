using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartShareCommon;
using Microsoft.Practices.Prism.PubSubEvents;

namespace SmartShareCommon
{
    public class NewClientAdditionEvent : PubSubEvent<Client>
    {
    }
    
}
