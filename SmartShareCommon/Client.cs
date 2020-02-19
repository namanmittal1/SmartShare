using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartShareCommon
{
    public class Client
    {
        public Client(string friendlyName,string ipAddress,string systemName, string clientSharedPath)
        {
            FriendlyName = friendlyName;
            IPAddress = ipAddress;
            SystemName = systemName;
            ClientSharedPath = clientSharedPath;
        }
        public String FriendlyName { get; set; }
        public String IPAddress { get; set; }
        public String SystemName { get; set; }
        public String ClientSharedPath { get; set; }
    }
}
