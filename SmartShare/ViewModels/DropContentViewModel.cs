using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SmartShareCoreBusinessLayer;

namespace SmartShare
{
    
    public class DropContentViewModel : BaseViewModel
    {
        private ICommand dropCommand;

        public ICommand FileDropCommand
        {
            get 
            { 
                return dropCommand; 
            }
            set
            {
                dropCommand = value;
            }
        }

        public DropContentViewModel()
        {
            FileDropCommand = new RelayCommand(HandleDroppedFile);
        }

        public void HandleDroppedFile(object obj)
        {
            if (obj == null)
                return;

            IDataObject ido = obj as IDataObject;
            if (null == ido) return;

            // Get all the possible format
            string[] formats = ido.GetFormats();

            object file = ido.GetData(formats[5]);
            // Do what you need here based on the format passed in.
            // You will probably have a few options and you need to
            // decide an order of preference.

            CoreBusinessManager.ShareFiles(file);            
        }

    }
}
