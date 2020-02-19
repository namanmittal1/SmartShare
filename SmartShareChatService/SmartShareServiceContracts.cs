using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartShareChatService
{
    [ServiceContract()]
    public interface ISmartShareService
    {
        [OperationContract]
        String Ping();       

        [OperationContract]
        bool SendMessage(String message, String sender);

        [OperationContract]
        void IsClientTyping(bool isTyping);

    }
}
