using SmartShareCommon;
using SmartShareCoreBusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SmartShareChatService
{
    public class ServiceIntegrationManager
    {
        volatile static ServiceIntegrationManager ss_manager = new ServiceIntegrationManager();
        ServiceHost host = null;
        private ServiceController serviceImplementation;
        const int Timeout = 5000;
        public ISmartShareService proxy = null;

        public ServiceIntegrationManager()
        {
            InitService();
            //CreateProxy();
        }

        public static ServiceIntegrationManager GetInstance()
        {
            return ss_manager;
        }

        private void InitService()
        {
            serviceImplementation = new ServiceController();
            serviceImplementation.Init(this);

            ChatHost host = HostService();
            if(host!=null)
            {
                HostRegisterationManager.RegisterHostToServer(host);
            }
            //  InitTimer();
        }

        private ChatHost HostService()
        {
            try
            {
                ChatHost chatHost = HostRegisterationManager.GenerateHost();
                var httpAddress = new Uri(chatHost.URI);

                ServiceHost host = new ServiceHost(typeof(ServiceController), httpAddress);
                //Add a service endpoint
                host.AddServiceEndpoint(typeof(ISmartShareService), new BasicHttpBinding(), "ChatService");
                //Enable metadata exchange
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);

                if (host != null && host.State == CommunicationState.Created)
                {
                    host.Open();
                    return chatHost;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                host.Abort();
                host = null;
                return null;
            }
        }

      

        public void CreateProxy(ChatHost host)
        {
            try
            {
                Uri baseAddress = new Uri(host.URI + "ChatService");

                BasicHttpBinding bind = new BasicHttpBinding();

                EndpointAddress endPoint = new EndpointAddress(baseAddress);

                ChannelFactory<ISmartShareService> factory = new ChannelFactory<ISmartShareService>(bind, endPoint);

                proxy = factory.CreateChannel();
            }
            catch (Exception exc)
            {
                //   Marshal.ThrowExceptionForHR(Marshal.GetHRForException(exc));
            }
        }
    }
}
