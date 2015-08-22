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

namespace UPC.HRNPCI.DesktopApplication.Views.Pacinete
{
    /// <summary>
    /// Interaction logic for PacienteVerView.xaml
    /// </summary>
    public partial class PacienteVerView : Window
    {
        public PacienteVerView()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
