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
using System.Windows.Shapes;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;

namespace UPC.HRNPCI.DesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class ModuloAdminView : Window
    {
        public ModuloAdminView()
        {
            InitializeComponent();
        }

        private void MenuItem_Checked_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("¿Está seguro que desea cerrar la sesión?", "Cerrar sessión", MessageBoxButton.YesNo))
            {
                
                this.Close();
            }
        }
    }
}
