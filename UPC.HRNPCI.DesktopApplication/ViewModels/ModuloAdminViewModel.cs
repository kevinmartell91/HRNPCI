using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;
using UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete;
using UPC.HRNPCI.DesktopApplication.ViewModels.AsociarPacienteFisioterapeuta;
using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes;
using UPC.HRNPCI.DesktopApplication.ViewModels.RutasAlmacenamiento;
using UPC.HRNPCI.Model.ConfiguracionModel;


namespace UPC.HRNPCI.DesktopApplication.ViewModels
{
    class ModuloAdminViewModel : BaseViewModel
    {

        string _RolLogueado;
        public string RolLogueado
        {
            get { return _RolLogueado; }
            set { _RolLogueado = value; }
        }

        bool _BlnSavePathNoExist;
        public bool BlnSavePathNoExist
        {
            get { return _BlnSavePathNoExist; }
            set { _BlnSavePathNoExist = value; }
        }

        int _iTabSelected;
        public int iTabSelected
        {
            get { return _iTabSelected; }
            set { _iTabSelected = value; }
        } 

        public event EventHandler CloseWindowEvent;

        ObservableCollection<object> _children;
        public ObservableCollection<object> Children { get { return _children; } }



        public RelayCommand CerrarSesionCommand { get; set; }


        public ModuloAdminViewModel()// TODO Enviar el usuario logueado y sacar su rol
        {
            _children = new ObservableCollection<object>();

            _children.Add(ListarFisioterapeutasViewModel.Instance());
            _children.Add(ListarPacientesViewModel.Instance());
            _children.Add(ListaPacientesAsociacionViewModel.Instance());
            _children.Add(ListarResultadosReportesViewModel.Instance());
            _children.Add(RutasAlmacenamientoViewModel.Instance());



            CloseWindowFlag = true;
            BlnSavePathNoExist = false;
            iTabSelected = 0;

            CerrarSesionCommand = new RelayCommand(CerrarSesion);
            RolLogueado = "Administrador";

            if (RutasConfiguracionDL.ObtenerConfiguraciones().Count == 0)
            {
                // the view does not recognize this variables, then is not possible to see an UI update
                BlnSavePathNoExist = true;
                iTabSelected = 4;
                
                //Manually fixed
                System.Windows.Forms.MessageBox.Show("Aún no ha determinado la rutas de almacenamiento de las fotos y exportaciones de archivos PDF. Por favor dirigirse a la pestaña Rutas de Almacenamiento para determinar las rutas de almacenamiento.", "Advertencia", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            //cargamos las configuranes de base de datos
        }

        private void CerrarSesion(object parameter)
        {
            //parameter para saber que usuauio se esta desloguado LOG
            //if (parameter == null) return;
            MessageBox.Show("¿Está seguro que desea cerrar la sesión?");

        }






    }
}
