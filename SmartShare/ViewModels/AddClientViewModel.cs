using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartShareCommon;
using SmartShareCoreBusinessLayer;

namespace SmartShare
{
    public class AddClientViewModel :BaseViewModel
    {
        private String ipAddress;
        private String compName;
        private String friendlyName;
        private String folderName;
        private RelayCommand addButtonCommand;
        private RelayCommand cancelButtonCommand;

        public AddClientViewModel()
        {
            AddButtonCommand = new RelayCommand(AddNewClient, param => canAddClient);
            CancelButtonCommand = new RelayCommand(CancelAddition, param => canAddClient);
        }

        public String CompName
        {
            get
            {
                return compName;
            }
            set
            {
                compName = value;
                OnPropertyChanged();
            }
        }

        public String FriendlyName
        {
            get
            {
                return friendlyName;
            }
            set
            {
                friendlyName = value;
                OnPropertyChanged();
            }
        }
        public String IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
                OnPropertyChanged();
            }
        }

        public String FolderName
        {
            get
            {
                return folderName;
            }
            set
            {
                folderName = value;
                OnPropertyChanged();
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

        public RelayCommand CancelButtonCommand
        {
            get
            {
                return cancelButtonCommand;
            }
            set
            {
                cancelButtonCommand = value;
            }
        }

        private void CancelAddition(object obj)
        {
            NavigationService.Navigation.NavigateTo(typeof(MainWindowViewModel), typeof(DropContentViewModel));
        }

        private void AddNewClient(object obj)
        {
            Client newClient=new Client(FriendlyName, IpAddress, CompName, FolderName);
            GlobalDataInstances.GetInstance().Clients.Add(newClient);
            CoreBusinessManager.AddClient(newClient);
            NavigationService.Messenger.SSEventAggregator.GetEvent<NewClientAdditionEvent>().Publish(newClient);
            NavigationService.Navigation.NavigateTo(typeof(MainWindowViewModel), typeof(DropContentViewModel));
        }

        public bool canAddClient
        {
            get
            {
                return true;
            }
        }
    }
}
