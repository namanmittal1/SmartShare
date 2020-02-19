using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartShareCommon;
using SmartShareCoreBusinessLayer;
using SmartShareChatService;
using System.ServiceModel;
using System.Threading;
using System.Timers;
using System.Windows;

namespace SmartShare
{
    public class MainWindowViewModel : BaseViewModel
    {
        private object currentView;
        private RelayCommand addButtonCommand;
        private RelayCommand reloadButtonCommand;
        private RelayCommand sendChatCommand;
        private ObservableCollection<Client> clients;
        private ObservableCollection<Chat> chats;
        private ObservableCollection<ChatHost> hosts;
        private Client selectedClient;
        private ChatHost selectedHost;
        private AddClientViewModel addClientViewModel;
        private DropContentViewModel dropContentViewModel;
        private RelayCommand browseSharedDirCommand;
        private String sharedDirectory;
        private String sendText;
        ServiceIntegrationManager servicemanager;
        bool exitThread = false;
        string thisHost = "";
        string[] prevHosts;
        private Visibility isTypingVisible = Visibility.Hidden;
        System.Timers.Timer timer;

        public MainWindowViewModel()
        {
            addClientViewModel = new AddClientViewModel();
            dropContentViewModel = new DropContentViewModel();
            NavigationService.Navigation.RegisterViewModels(typeof(AddClientViewModel), addClientViewModel);
            NavigationService.Navigation.RegisterViewModels(typeof(DropContentViewModel), dropContentViewModel);
            clients = new ObservableCollection<Client>(CoreBusinessManager.GetClients());
            chats = new ObservableCollection<Chat>();
            hosts = new ObservableCollection<ChatHost>();
            //clients = new ObservableCollection<Client>(GlobalDataInstances.GetInstance().Clients);
            AddButtonCommand = new RelayCommand(AddClient, param => this.canAddClient);
            ReloadButtonCommand = new RelayCommand(ReloadClients, param => this.canReloadClient);
            BrowseSharedDirCommand = new RelayCommand(BrowseDirectory, param => this.canReloadClient);
            WindowClosing = new RelayCommand(WindowClosed);
            IsTypingCommand = new RelayCommand(IsClientTyping, param => this.canReloadClient);
            SendChatCommand = new RelayCommand(SendChat, param => this.canReloadClient);
            NavigationService.Messenger.SSEventAggregator.GetEvent<NewClientAdditionEvent>().Subscribe(UpdateClientsList);
            EventContainers.GetInstance().SSMessageRecievedEevent += MessageRecieved;
            EventContainers.GetInstance().SSTypingProgressEvent += ClientIsTyping;
            SharedDirectory = CoreBusinessManager.GetSharedDirectory();
            thisHost = System.Net.Dns.GetHostName();
            servicemanager = ServiceIntegrationManager.GetInstance();

            timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = false;

            PollForHosts();
            
        }
       
        public Visibility IsTypingVisible
        {
            get
            {
                return isTypingVisible;
            }
            set
            {
                isTypingVisible = value;
                OnPropertyChanged("IsTypingVisible");
            }
        }

        public RelayCommand WindowClosing { get; private set; }
        public Object CurrentView
        {
            get
            {
                return currentView;
            }
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }


        public Client SelectedClient
        {
            get
            {
                return selectedClient;
            }
            set
            {
                selectedClient = value;
                OnPropertyChanged();
                GlobalDataInstances.GetInstance().SelectedClient = selectedClient;
            }
        }

        public ChatHost SelectedHost
        {
            get
            {
                return selectedHost;
            }
            set
            {
                selectedHost = value;
                OnPropertyChanged();
                if(selectedHost!=null)
                {
                    servicemanager.CreateProxy(SelectedHost);
                }
            }
        }
        public ObservableCollection<Client> Clients
        {
            get
            {
                return clients;
            }
            set
            {
                clients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChatHost> Hosts
        {
            get
            {
                return hosts;
            }
            set
            {
                hosts = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Chat> Chats
        {
            get
            {
                return chats;
            }
            set
            {
                chats = value;
                OnPropertyChanged();
            }
        }

        public String SendText
        {
            get
            {
                return sendText;
            }
            set
            {
                sendText = value;
                OnPropertyChanged();
            }
        }


        public String SharedDirectory
        {
            get
            {
                return sharedDirectory;
            }
            set
            {
                sharedDirectory = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand BrowseSharedDirCommand
        {
            get
            {
                return browseSharedDirCommand;
            }
            set
            {
                browseSharedDirCommand = value;
            }
        }

        public RelayCommand AddButtonCommand
        {
            get
            {
                return addButtonCommand;
            }
            set
            {
                addButtonCommand = value;
            }
        }

        public RelayCommand SendChatCommand
        {
            get
            {
                return sendChatCommand;
            }
            set
            {
                sendChatCommand = value;
            }
        }

        public RelayCommand ReloadButtonCommand
        {
            get
            {
                return reloadButtonCommand;
            }
            set
            {
                reloadButtonCommand = value;
            }
        }      
 
        private void ReloadClients(object obj)
        {
            Clients.Clear();
            GlobalDataInstances.GetInstance().Clients.Clear();
            List<Client> clients = CoreBusinessManager.GetClients();
            GlobalDataInstances.GetInstance().Clients = clients;
            Clients = null;
            Clients = new ObservableCollection<Client>(GlobalDataInstances.GetInstance().Clients);
        }

        private void AddClient(object obj)
        {            
            NavigationService.Navigation.NavigateTo(typeof(MainWindowViewModel), typeof(AddClientViewModel));
        }

        private void UpdateClientsList(Client client)
        {
            Clients.Add(client);
        }
        
        private void BrowseDirectory(object obj)
        {
            string folderName = String.Empty;
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                    folderName = dialog.SelectedPath;
            }
            if (!string.IsNullOrEmpty(folderName))
            {
                String systemName = CoreBusinessManager.GetSystemName();
                folderName = new DirectoryInfo(folderName).Name;
                SharedDirectory = String.Concat("\\", systemName, "\\", folderName);
                CoreBusinessManager.AddSharedDirectory(SharedDirectory);
            }

        }

        public bool canAddClient 
        {
            get
            {
                return true;
            }
        }

        public bool canReloadClient
        {
            get
            {
                return true;
            }
        }

        public RelayCommand IsTypingCommand { get; private set; }

        public override void Load(System.Windows.FrameworkElement obj)
        {
            base.Load(obj);
            NavigationService.Navigation.NavigateTo(typeof(MainWindowViewModel), typeof(DropContentViewModel));
        }

        private void SendChat(object chat)
        {
            Thread t = new Thread(SendMessage);
            t.Start();

        }

        private void SendMessage()
        {
            servicemanager.proxy?.SendMessage(SendText, thisHost);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Chats.Add(new Chat(SendText, thisHost + ": ", ChatType.Sent));
            });
        }

        private void MessageRecieved(object sender, MessageEventArgs message)
        {
            if (selectedHost == null)
            {
                SelectedHost = Hosts?.FirstOrDefault((x) => x.HostName == message.SenderName);
            }
            Chats.Add(new Chat(message.Message, message.SenderName + ": ", ChatType.Recieved));
        }

        private void PollForHosts()
        {
            Thread t = new Thread(LookForHosts);
            t.Start();
        }
        private void LookForHosts()
        {          
            String fileName = GlobalDataInstances.GetInstance().HostFileLocation;
                       
            while(!exitThread)
            {
                if (!File.Exists(fileName))
                    continue;
                try
                {
                    Thread.Sleep(100);
                    string[] hosts = HostRegisterationManager.ReadWriteFile(fileName, 0);
                    if (hosts?.Count() != prevHosts?.Count())
                    {
                        prevHosts = null;
                        prevHosts = hosts;
                        UpdateCollection();
                    }
                }
                catch (Exception e)
                {
                }
            }
        }

        private void UpdateCollection()
        {
            ChatHost host = new ChatHost();
            foreach (string hostLine in prevHosts)
            {
               if (hostLine.Contains(System.Net.Dns.GetHostName()))
                    continue;
                string[] hostDetails = hostLine.Split('|');
                host.DisplayName = hostDetails[0];
                host.HostName = hostDetails[1];
                host.URI = hostDetails[2];
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                Hosts?.Clear();
                Hosts?.Add(host);
            });
        }

        private void ClientIsTyping(object sender, bool e)
        {
            if (e)
            {
                IsTypingVisible = Visibility.Visible;
                timer.Enabled = true;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IsTypingVisible = Visibility.Hidden;
            timer.Enabled = false;
        }

        private void IsClientTyping(object obj)
        {
            bool val = (bool)obj;
            if (val)
            {
                Task.Factory.StartNew(() =>
                {
                    servicemanager.proxy?.IsClientTyping(val);
                });
            }
        }
        
        private void WindowClosed(object obj)
        {
            exitThread = true;
            HostRegisterationManager.SignoutUser();
            CoreBusinessManager.CloseDBConnection();
        }
    }
}
