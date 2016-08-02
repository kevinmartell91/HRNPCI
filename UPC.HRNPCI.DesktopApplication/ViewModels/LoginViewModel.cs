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
using UPC.HRNPCI.DesktopApplication.ViewModels.RutasAlmacenamiento;
using UPC.HRNPCI.Model.ConfiguracionModel;
using UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete;



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

            try
            {
                if (RutasConfiguracionDL.GetRutaFotos() != null)
                    RutasAlmacenamientoStatic.strRutaFotos = RutasConfiguracionDL.GetRutaFotos().vRutaEstatica;
                if (RutasConfiguracionDL.GetRutaReportes() != null)
                    RutasAlmacenamientoStatic.strRutaReportes = RutasConfiguracionDL.GetRutaReportes().vRutaEstatica;

                PacienteStatic.kstrRutaFoto = RutasAlmacenamientoStatic.strRutaFotos;
                FisioterapeutaStatic.kstrRutaFoto = RutasAlmacenamientoStatic.strRutaFotos;

                


                int iStatusLogin = FisioterapeutaDL.ValidarUsuario(Usuario, passwordBox.Password);
                switch (iStatusLogin)
                {

                    case 0:

                        FisioterapeutaB f = FisioterapeutaDL.ObtenerFisioterapeutaLoguedo(Usuario, passwordBox.Password);
                        FisioterapeutaStatic.FisioterapeutaLogueado = f;

                        //login fisoterapueta
                        ModuloFisioterapeutaViewModel mfv = new ModuloFisioterapeutaViewModel();

                        IModalDialog dialogModuloFisioterapeuta = ServiceProvider.Instance8.Get<IModalDialog>();
                        dialogModuloFisioterapeuta.BindViewModel(mfv);


                        //FisioterapeutaTestAnalisisViewModel ftm = mfv.Children.GetType();
                       
                        
                        dialogModuloFisioterapeuta.ShowDialog();

                        FisioterapeutaStatic.kblnLoginExitoso = true;


                        // FisioterapeutaAgregarViewModel fvm = new FisioterapeutaAgregarViewModel();
                        //IModalDialog dialog = ServiceProvider.Instance2.Get<IModalDialog>();

                        //dialog.BindViewModel(fvm);
                        //dialog.ShowDialog();

                        break;

                    case 1:

                        ModuloAdminViewModel mav = new ModuloAdminViewModel();
                        IModalDialog dialogModuloAdmin = ServiceProvider.Instance.Get<IModalDialog>();
                        dialogModuloAdmin.BindViewModel(mav);
                        dialogModuloAdmin.ShowDialog();
                        FisioterapeutaStatic.kblnLoginExitoso = true;

                        break;
                }

                CloseWindowFlag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

       
    }
}
