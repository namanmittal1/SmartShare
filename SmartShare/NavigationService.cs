using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;
using SmartShareCommon;

namespace SmartShare
{
    public class NavigationService
    {
        public static class Navigation
        {
            public static BaseViewModel CurrentViewModel;

            private static Dictionary<Type, BaseViewModel> viewModelsDictionary = new Dictionary<Type, BaseViewModel>();
            public static MainWindowViewModel MainWindowViewModel;

            public static void RegisterViewModels(Type type, BaseViewModel viewModel)
            {
                if (typeof(MainWindowViewModel) == type)
                    MainWindowViewModel = viewModel as MainWindowViewModel;

                viewModelsDictionary.Add(type,viewModel);
            }
            public static void NavigateTo(Type baseViewModel, Type currentViewModel)
            {
                BaseViewModel tempViewModel=null;
                if (baseViewModel != null && currentViewModel != null)
                {
                    if (baseViewModel == typeof(MainWindowViewModel))
                    {
                        viewModelsDictionary.TryGetValue(currentViewModel, out tempViewModel);
                        MainWindowViewModel.CurrentView = tempViewModel;
                        CurrentViewModel = tempViewModel;
                    }
                }
            }
        }

        public static class Messenger
        {
            private static EventAggregator eventAggregator;
            private static NewClientAdditionEvent newClientAdditionEvent;

            public static EventAggregator SSEventAggregator
            {
                get
                {
                    return eventAggregator;
                }
            }
            static Messenger()
            {
                eventAggregator = new EventAggregator();
                newClientAdditionEvent = eventAggregator.GetEvent<NewClientAdditionEvent>();
            } 
            

        }
    }
}
