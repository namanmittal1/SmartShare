using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartShare
{
    /// <summary>
    /// Interaction logic for DropContent.xaml
    /// </summary>
    public partial class DropContent : UserControl
    {
        public DropContent()
        {
            InitializeComponent();
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (NavigationService.Navigation.CurrentViewModel is DropContentViewModel)
                {
                    (NavigationService.Navigation.CurrentViewModel as DropContentViewModel).FileDropCommand.Execute(e.Data);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}
