using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShareCommon
{
    public sealed class GlobalDataInstances
    {
        public List<Client> Clients = new List<Client>();

        private static readonly GlobalDataInstances globalDataInstances = new GlobalDataInstances();

        public Client SelectedClient { get; set; }
        public static GlobalDataInstances GetInstance()
        {
            return globalDataInstances;
        }

        public String HostFileLocation { get; set; } = @"..\ChatHosts.txt";
        private GlobalDataInstances()
        {
            //Clients.Add(new Client("Naman", "10.117.2.48", "AAVPM0672P", @"D:/New folder"));
            //Clients.Add(new Client("Diggi", "10.117.2.47", "AAVPM0673P", @"D:/New folder"));
        }
    }
}
