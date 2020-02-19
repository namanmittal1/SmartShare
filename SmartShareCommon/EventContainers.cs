using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShareCommon
{
    public class MessageEventArgs : EventArgs
    {
        public String Message { get; set; }
        public String  SenderName { get; set; }
    }

    public class HostEventArgs : EventArgs
    {
        public ChatHost Host { get; set; }
    }


    public class EventContainers
    {
        volatile static EventContainers eventContainer = new EventContainers();

        private EventContainers()
        {

        }

        public static EventContainers GetInstance()
        {
            return eventContainer;
        }

        public EventHandler<MessageEventArgs> SSMessageRecievedEevent;
        public EventHandler<bool> SSTypingProgressEvent;

        public void RaiseSSTypingProgressEvent(object sender, bool value)
        {
            if (SSTypingProgressEvent != null)
            {
                SSTypingProgressEvent(sender, value);
            }
        }
        public void RaiseSSMessageRecievedEevent(object sender, MessageEventArgs args)
        {
            if(SSMessageRecievedEevent!=null)
            {
                SSMessageRecievedEevent(sender, args);
            }
        }
    }
}
