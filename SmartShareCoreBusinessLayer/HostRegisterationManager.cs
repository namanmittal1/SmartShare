using SmartShareCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartShareCoreBusinessLayer
{
    public class HostRegisterationManager
    {
        public static void RegisterHostToServer(ChatHost host)
        {
            try
            {
                string fileName = GlobalDataInstances.GetInstance().HostFileLocation;
                String content = String.Concat(host.DisplayName, "|", host.HostName, "|", host.URI);
                List<String> contents = new List<string>();
                contents.Add(content);
                ReadWriteFile(fileName, 1, contents.ToArray());
            }
            catch (Exception e)
            {
            }
        }

        public static ChatHost GenerateHost()
        {
            ChatHost host = GetHost();
            StringBuilder sb = new StringBuilder();
            sb.Append("http://");
            sb.Append(host.IPAdress);
            sb.Append(":1234/");
            sb.Append("ServiceController/");
            host.URI = sb.ToString();

            return host;
        }

        static object syncobj = new object();
        public static string[] ReadWriteFile(String filename, int operation, string[] lines = null, bool isexit = false)
        {
            lock (syncobj)
            {
                string[] data = null;
                try
                {
                    if (operation == 1)
                    {
                        if (isexit)
                        {
                            File.WriteAllLines(filename, lines);
                        }
                        else
                        {
                            File.AppendAllLines(filename, lines);
                        }
                    }
                    else if (operation == 0)
                    {
                        data = File.ReadAllLines(filename);
                    }
                }
                catch (Exception e)
                {
                }
                return data;
            }
        }

        public static void SignoutUser()
        {
            try
            {
                string fileName = GlobalDataInstances.GetInstance().HostFileLocation;
                string item = Dns.GetHostName().Trim();
                string[] lines = ReadWriteFile(fileName, 0).Where(line => !line.Trim().Contains(item)).ToArray();
                ReadWriteFile(fileName, 1, lines, true);
            }
            catch (Exception e)
            {
            }
        }
        
        private static ChatHost GetHost()
        {
            ChatHost host = new ChatHost();

            host.HostName = Dns.GetHostName(); // Retrive the Name of HOST 

            // Get the IP  
            host.IPAdress = Dns.GetHostByName(host.HostName).AddressList[0].ToString();

            return host;
        }
    }
}
