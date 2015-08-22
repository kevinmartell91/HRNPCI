using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.FisioterapeutaModel;

using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;

using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication.ViewModels;
using System.Windows.Controls;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;



namespace UPC.HRNPCI.DesktopApplication.ViewModels
{
    class LoginViewModel : BaseViewModel
    {


         public event EventHandler CloseWindowEvent;


        string _UsuarioMensajeError;
        public string UsuarioMensajeError
        {
            get { return _UsuarioMensajeError; }
            set
            {
                _UsuarioMensajeError = value;
                OnPropertyChanged("UsuarioMensajeError");
            }
        }

        string _ContranaMensajeError;
        public string ContrasenaMensajeError
        {
            get { return _ContranaMensajeError; }
            set
            {
                _ContranaMensajeError = value;
                OnPropertyChanged("ContrasenaMensajeError");
            }
        }
                
        string _Usuario;
        public string Usuario
        {
            get { return _Usuario;}
            set
            {
                _Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        public object SelectedPerson { get; set; }

        public RelayCommand AceptarCommand { get; set; }
       


        public LoginViewModel()
        {
            
            AceptarCommand = new RelayCommand(ValidarUsuario);
            Usuario = "";
            CloseWindowFlag = false;

        }

       
        private void ValidarUsuario(object parameter)
        {
            var passwordBox = parameter as PasswordBox;

            //if (passwordBox.Password == "" && Usuario == "")
            //{
            //    UsuarioMensajeError = "Debe ingresar un usuario ";
            //    ContrasenaMensajeError = "Debe ingresar una contraseña";
            //    return;
            //}
            //if (Usuario == "")
            //{
            //    UsuarioMensajeError = "Debe ingresar un usuario";
            //    ContrasenaMensajeError = "";

            //    return;
            //}
            //if (passwordBox.Password == "")
            //{
            //    ContrasenaMensajeError = "Debe ingresar una contraseña";
            //    UsuarioMensajeError = "";
            //    return;
            //}

            int iStatusLogin = FisioterapeutaDL.ValidarUsuario(Usuario, passwordBox.Password);
            switch (iStatusLogin)
            {
                //case -1:
                //    ContrasenaMensajeError = "Usuario y/o contraseña incorrectas";
                //    break;
                //case 0:
                //    //login fisoterapueta
                //    MessageBox.Show("Simulación de ingreso a módulofisioterapeuta", " Rol Fisioterapeuta");
                //    FisioterapeutaStatic.kblnLoginExitoso = true;
                //    break;

                case 1:

                    ModuloAdminViewModel mav = new ModuloAdminViewModel();
                    IModalDialog dialog = ServiceProvider.Instance.Get<IModalDialog>();
                    dialog.BindViewModel(mav);
                    dialog.ShowDialog();
                    FisioterapeutaStatic.kblnLoginExitoso = true;

                    break;
            }

            CloseWindowFlag = true;

        }

       
    }
}
