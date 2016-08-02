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

namespace UPC.HRNPCI.DesktopApplication.Views.ResultadosReportesPacientes
{
    /// <summary>
    /// Interaction logic for ResultadosBuscarView.xaml
    /// </summary>
    public partial class ResultadosBuscarView : Window
    {
        public ResultadosBuscarView()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
