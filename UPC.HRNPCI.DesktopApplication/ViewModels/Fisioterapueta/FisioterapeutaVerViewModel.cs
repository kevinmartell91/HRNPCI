using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

using System.Collections.ObjectModel;
using UPC.HRNPCI.DesktopApplication;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;
using UPC.HRNPCI.Model;



namespace UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta
{
    class FisioterapeutaVerViewModel : BaseViewModel
    {
        #region Declaraciones

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
        private string _UrlFoto;

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
        public string NCTMP
        {
            get { return _NCTMP; }
            set
            {
                _NCTMP = value;
                OnPropertyChanged("NCTMP");
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
        public string UrlFoto
         {
            get { return _UrlFoto; }
            set
            {
                _UrlFoto = value;
                OnPropertyChanged("UrlFoto");
            }
        }


        public RelayCommand testCommand { get; set; }

        public Fisioterapeuta fisoterapuetaActual { get; set; }

        public int iFiosioterapeutaID { get; set; }

        #endregion

        #region Metodos

        public FisioterapeutaVerViewModel()
        {
            testCommand = new RelayCommand(verFisioterapueta);
        }


        public void SetFisioterapeutaSelecionado(Fisioterapeuta fa)
        {
            if (fa != null)
            {
                fisoterapuetaActual = fa;
                Fisioterapeuta f = fisoterapuetaActual;
                Nombre = f.vNombresFisioterapeuta;
                Apellidos = f.vApellidosFisioterapeuta;
                Celular = f.vCelularFisioterapeuta;
                Telefono = f.vTelefonoFisioterapeuta;
                CentLaboral = f.vCentLabFisioterapeuta;
                Email = f.vEmailFisioterapeuta;
                Rol = f.vRolFisioterapeuta;
                NCTMP = f.vNumCTMPFisioterapeuta;
                NNDTA = f.vNumNDTAFisioterapeuta;
                Sexo = f.cGenero.ToString();
                UrlFoto = f.vUrlFotoFosioterapeuta;
            }

        }

        private void MostraVerDialog(object parameter)
        {
            IModalDialog dialog = ServiceProvider.Instance1.Get<IModalDialog>();
            dialog.BindViewModel(this);
            dialog.ShowDialog();
        }

        private void verFisioterapueta(object parameter)
        {
            //Fisioterapeuta f = fisoterapuetaActual;
            //Nombre = f.vNombresFisioterapeuta;
            //Apellidos = f.vApellidosFisioterapeuta;
            //Celular = f.cCelularFisioterapeuta;
            //Telefono = f.vTelefonoFisioterapeuta;
            //CentLaboral = f.vCentLabFisioterapeuta;
            //Email = f.vEmailFisioterapeuta;
            //Rol = f.vRolFisioterapeuta;
            //NCTMP = f.vNumCTMPFisioterapeuta;
            //NNDTA = f.vNumNDTAFisioterapeuta;

        }

        public ListarFisioterapeutasViewModel Continer
        {
            get { return ListarFisioterapeutasViewModel.Instance(); }
        }

        #endregion


    }
}