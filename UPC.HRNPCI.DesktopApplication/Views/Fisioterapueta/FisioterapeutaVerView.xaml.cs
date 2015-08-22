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

namespace UPC.HRNPCI.DesktopApplication.Views.Fisioterapueta
{
    /// <summary>
    /// Interaction logic for FisioterapeutaVerView.xaml
    /// </summary>
    public partial class FisioterapeutaVerView : Window
    {
        public FisioterapeutaVerView()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
