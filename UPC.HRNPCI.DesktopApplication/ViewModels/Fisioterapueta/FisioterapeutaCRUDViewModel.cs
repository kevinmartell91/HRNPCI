using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UPC.HRNPCI.DesktopApplication;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.FisioterapeutaModel;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta
{
    class FisioterapeutaCRUDViewModel 
    {
        #region Declaraciones

        FisioterapeutaBusinessObject businesssObject = null;

        public int    Codigo{get;set;}
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string CentLaboral { get; set; }
        public string Email { get; set; }
        public string NCTMP { get; set; }
        public string NNDTA { get; set; }
        public int Borrado { get; set; }
        public string Contrasena { get; set; }
        public string Usuario { get; set; }
        public char Sexo{get;set;}
        public string UrlFoto { get; set; }

        /*
        private int _Codigo;
        private string _Nombre;
        private string _Apellidos;
        private string _Sexo;
        private string _Celular;
        private string _Telefono;
        private string _CentLaboral;
        private string _Email;
        private string _Rol;
        private string _NCTMP;
        private string _NNDTA;
        */

        public Button VerButton;
        public Button ActualizarButton;
        public Button EliminarButton;


        public RelayCommand VerFisioterapuetaCommand { get; set; }
        public RelayCommand ActualizarFisioterapuetaCommand { get; set; }
        public RelayCommand EliminarFisioterapuetaCommand { get; set; }

        public string ImagenVer { get; set; }

        
        /*
        public int Codigo
        {
            get { return _Codigo; }
            set
            {
                _Codigo = value;
                OnPropertyChanged("Codigo");
            }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set
            {
                _Nombre = value;
                OnPropertyChanged("Nombre");
            }
        }
        public string Apellidos
        {
            get { return _Apellidos; }
            set
            {
                _Apellidos = value;
                OnPropertyChanged("Apellidos");
            }
        }
        public string Sexo
        {
            get { return _Sexo; }
            set
            {
                _Sexo = value;
                OnPropertyChanged("Sexo");
            }
        }
        public string Celular
        {
            get { return _Celular; }
            set
            {
                _Celular = value;
                OnPropertyChanged("Celular");
            }
        }
        public string Telefono
        {
            get { return _Telefono; }
            set
            {
                _Telefono = value;
                OnPropertyChanged("Telefono");
            }
        }
        public string CentLaboral
        {
            get { return _CentLaboral; }
            set
            {
                _CentLaboral = value;
                OnPropertyChanged("CentLaboral");
            }
        }
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Rol
        {
            get { return _Rol; }
            set
            {
                _Rol = value;
                OnPropertyChanged("Rol");
            }
        }
        public string NCTPM
        {
            get { return _NCTMP; }
            set
            {
                _NCTMP = value;
                OnPropertyChanged("NCTPM");
            }
        }
        public string NNDTA
        {
            get { return _NNDTA; }
            set
            {
                _NNDTA = value;
                OnPropertyChanged("NNDTA");
            }
        }
        */
        #endregion


        #region Metodos

        public FisioterapeutaCRUDViewModel()
        {
            
            VerButton = new Button();
            VerButton.Command = VerFisioterapuetaCommand;
            

            ActualizarButton = new Button();
            ActualizarButton.Command = ActualizarFisioterapuetaCommand;

            EliminarButton = new Button();
            EliminarButton.Command = EliminarFisioterapuetaCommand;


            VerFisioterapuetaCommand = new RelayCommand(VerFisioterapeutaDialog);
            ActualizarFisioterapuetaCommand = new RelayCommand(ActualizarFisioterapeutaDialog);
            EliminarFisioterapuetaCommand = new RelayCommand(EliminarFisioterapeutaDialog);

        }

        private void VerFisioterapeutaDialog(object parameter)
        {
            FisioterapeutaVerViewModel fvm = new FisioterapeutaVerViewModel();

            fvm.SetFisioterapeutaSelecionado(fiosoterapeutaBean());
            IModalDialog dialog = ServiceProvider.Instance1.Get<IModalDialog>();

            dialog.BindViewModel(fvm);
            dialog.ShowDialog();
        }
       
        private void ActualizarFisioterapeutaDialog(object parameter)
        {
            FisioterapeutaActualizarViewModel fvm = new FisioterapeutaActualizarViewModel();
            fvm.SetFisioterapeutaSelecionado(fiosoterapeutaBean());
            IModalDialog dialog = ServiceProvider.Instance3.Get<IModalDialog>();

            dialog.BindViewModel(fvm);
            dialog.ShowDialog();
        }

        private void EliminarFisioterapeutaDialog(object parameter)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("¿Esta seguro que desea eliminar al paciente " + Nombre + " " + Apellidos + "?", "Advertencia", MessageBoxButton.YesNo))
                if (FisioterapeutaDL.BorrarFisioterapeuta(fiosoterapeutaBean().iCodigoFisioterapeuta))
                {
                    businesssObject = new FisioterapeutaBusinessObject();

                    ObservableCollection<FisioterapeutaCRUDViewModel> ocltnFisioterapeutasCRUD = ListarFisioterapeutasViewModel.Instance().ListaFisioterapeutas;
                    FisioterapeutaCRUDViewModel fisioterapeutaEliminar = null;

                    if (ocltnFisioterapeutasCRUD != null)
                    {
                        for (int i = 0; i < ocltnFisioterapeutasCRUD.Count; i++)
                        {
                            if (ocltnFisioterapeutasCRUD[i].Codigo == this.Codigo)
                            {
                                fisioterapeutaEliminar = ocltnFisioterapeutasCRUD[i];
                                ListarFisioterapeutasViewModel.Instance().ListaFisioterapeutas.RemoveAt(i);
                            }

                        }

                        MessageBox.Show("Se eliminó al fisioterpapeuta " + Nombre + " " + Apellidos + ".","Mensaje");
                    }

                }
                else 
                {
                    MessageBox.Show("No se puede eliminar al fisioterapeuta seleccionado, ya que esta asignado a pacientes. Realice la desasignación y podrá eliminarlo al fisioterapeuta " + fiosoterapeutaBean().vNombresFisioterapeuta + ".");

                }



            //ListarFisioterapeutasViewModel.Instance().refrescarListaFiosioterapeutas();
        }

        public Fisioterapeuta fiosoterapeutaBean()
        {
            Fisioterapeuta f = new Fisioterapeuta();
            f.iCodigoFisioterapeuta = Codigo;
            f.vNombresFisioterapeuta = Nombre;
            f.vApellidosFisioterapeuta = Apellidos;
            f.vCelularFisioterapeuta = Celular;
            f.vTelefonoFisioterapeuta = Telefono;
            f.vCentLabFisioterapeuta = CentLaboral;
            f.vEmailFisioterapeuta = Email;
            f.vNumCTMPFisioterapeuta = NCTMP;
            f.vNumNDTAFisioterapeuta = NNDTA;
            f.vRolFisioterapeuta = Rol;
            f.cGenero = Sexo;
            f.vUrlFotoFosioterapeuta = UrlFoto;
            f.iFlagBorradoFisioterapeuta = Borrado;
            f.vContrasenaFisioterapeuta = Contrasena;
            f.vUsuarioFiosioterapeuta = Usuario;
            return f;

        }

        #endregion
    }
}
