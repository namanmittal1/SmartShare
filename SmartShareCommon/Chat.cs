using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShareCommon
{
    public enum ChatType
    {
        Sent,
        Recieved
    }
    public class Chat
    {
        public Chat(String chatText, String senderName, ChatType type)
        {
            ChatText = chatText;
            SenderName = senderName;
            Type = type;
        }
        public String ChatText { get; set; }
        public String SenderName { get; set; }
        public ChatType Type { get; set; }
    }
}
