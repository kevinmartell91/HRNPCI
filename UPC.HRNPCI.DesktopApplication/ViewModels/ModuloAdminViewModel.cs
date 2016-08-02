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

            CerrarSesionCommand = new RelayCommand(CerrarSesion);
            RolLogueado = "Administrador";

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
