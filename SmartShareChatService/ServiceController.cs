using SmartShareCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartShareChatService
{
    public class ServiceController : ISmartShareService
    {
        
        private ServiceIntegrationManager ssIntegrationManager = null;

        public int Init(ServiceIntegrationManager manager)
        {
            ssIntegrationManager = manager;
            return 1;
        }

        public void IsClientTyping(bool isTyping)
        {
            EventContainers.GetInstance().RaiseSSTypingProgressEvent(this, isTyping);
        }

        public String Ping()
        {
            return System.Net.Dns.GetHostName();
        }
        
        public bool SendMessage(String message, String sender)
        {
            MessageEventArgs args = new MessageEventArgs();
            args.Message = message;
            args.SenderName = sender;
            EventContainers.GetInstance().RaiseSSMessageRecievedEevent(this, args);
            return true;
        }
    }
}
