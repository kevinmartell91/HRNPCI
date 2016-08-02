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

namespace UPC.HRNPCI.DesktopApplication.ViewModels
{
    class ModuloFisioterapeutaViewModel : BaseViewModel
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


        public ModuloFisioterapeutaViewModel()// TODO Enviar el usuario logueado y sacar su rol
        {
            _children = new ObservableCollection<object>();
            
            _children.Add(FisioterapeutaTestAnalisisViewModel.Instance());
            CloseWindowFlag = true;

            CerrarSesionCommand = new RelayCommand(CerrarSesion);
            RolLogueado = "Fisioterapeuta";


        }

        private void CerrarSesion(object parameter)
        {
            //parameter para saber que usuauio se esta desloguado LOG
            //if (parameter == null) return;
            MessageBox.Show("¿Está seguro que desea cerrar la sesión?");
            
        }
    }
}
