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
using UPC.HRNPCI.Model.ConfiguracionModel;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.DesktopApplication.ViewModels.RutasAlmacenamiento;
using Microsoft.Win32;
using System.IO;
using UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;

namespace UPC.HRNPCI.DesktopApplication.Views.RutasAlmacenamiento
{
    /// <summary>
    /// Interaction logic for RutasAlmacenamientosView.xaml
    /// </summary>
    public partial class RutasAlmacenamientosView : UserControl
    {
        public RutasAlmacenamientosView()
        {
            InitializeComponent();
            txtRutaFotos.Text = RutasAlmacenamientoStatic.strRutaFotos ;
            txtRutaReportes.Text = RutasAlmacenamientoStatic.strRutaReportes;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtRutaReportes.Text.Trim() == "" || txtRutaFotos.Text.Trim() == "")
            {
                MessageBox.Show("Debe especificar las rutas de almacenamiento","Advertencia",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return;
            }

            //gruardar rutas de Fotos
            ConfiguracionB conf = new ConfiguracionB();
            conf.iCodigoConfiguracion = 1;
            conf.vParametro = "RutaFotos";
            conf.vRutaEstatica = txtRutaFotos.Text;
            conf.dtFechaCreacion = DateTime.Now;
            conf.dtFechaActualizacion = DateTime.Now;
            bool blnfotos = false;

            //if (RutasAlmacenamientoStatic.strRutaFotos == "")
           // {
            if (RutasConfiguracionDL.GetRutaFotos() != null)//update
                    blnfotos = RutasConfiguracionDL.GuardarConfiguracion(conf, 1);
                else //create
                    blnfotos = RutasConfiguracionDL.GuardarConfiguracion(conf, 0);

            //}
            //else
                //blnfotos = RutasConfiguracionDL.GuardarConfiguracion(conf, 1);

            //gruardar rutas reportes
            ConfiguracionB conf1 = new ConfiguracionB();
            conf1.iCodigoConfiguracion = 2;
            conf1.vParametro = "RutaReportes";
            conf1.vRutaEstatica = txtRutaReportes.Text;
            conf1.dtFechaCreacion = DateTime.Now;
            conf1.dtFechaActualizacion = DateTime.Now;
            bool blnRportes = false;

            //if (RutasAlmacenamientoStatic.strRutaReportes == "")
            //{
                if (RutasConfiguracionDL.GetRutaReportes() != null) //update
                    blnRportes = RutasConfiguracionDL.GuardarConfiguracion(conf1, 1);
                else //create
                    blnRportes = RutasConfiguracionDL.GuardarConfiguracion(conf1, 0);
            //}
            //else
                //blnRportes = RutasConfiguracionDL.GuardarConfiguracion(conf1, 1);

            if (blnfotos == blnRportes == true)
            {
                RutasAlmacenamientoStatic.strRutaFotos = txtRutaFotos.Text;
                RutasAlmacenamientoStatic.strRutaReportes =  txtRutaReportes.Text ;

                PacienteStatic.kstrRutaFoto = RutasAlmacenamientoStatic.strRutaFotos;
                FisioterapeutaStatic.kstrRutaFoto = RutasAlmacenamientoStatic.strRutaFotos;

                MessageBox.Show("Se guardaron satisfactoriamente las configuraciones");
            }
            else
            {
                MessageBox.Show("Le faltó determinar un ruta de almacenamiento. Seleccionela por favor.");
            }

        }

        private void btnRutasFotos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtRutaFotos.Text = dlg.SelectedPath;
                    RutasAlmacenamientoStatic.strRutaFotos = dlg.SelectedPath;
                }
                else
                {
                    // This prevents a crash when you close out of the window with nothing
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnRutasResultadosReportes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtRutaReportes.Text = dlg.SelectedPath;
                    RutasAlmacenamientoStatic.strRutaReportes = dlg.SelectedPath;

                }
                else
                {
                    // This prevents a crash when you close out of the window with nothing
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
