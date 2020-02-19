using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartShare
{
    public class BaseViewModel :INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public Boolean Initialized { get; set; }
        public virtual void Load(FrameworkElement obj)
        {

        }

        public virtual void Unload(FrameworkElement obj)
        {

        }

       
    }
}
