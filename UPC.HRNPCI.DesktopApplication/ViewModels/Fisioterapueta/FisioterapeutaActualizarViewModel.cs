using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

using UPC.HRNPCI.DesktopApplication;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.FisioterapeutaModel;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;
using UPC.HRNPCI.DesktopApplication.Views;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta
{
    class FisioterapeutaActualizarViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Declaraciones

        FisioterapeutaBusinessObject businesssObject = null;

        private int _Codigo;
        private string _Nombre;
        private string _Apellidos;
        private char _Sexo;
        private string _Celular;
        private string _Telefono;
        private string _CentLaboral;
        private string _Email;
        private string _Rol;
        private string _NCTMP;
        private string _NNDTA;
        private string _UrlFoto;
        private int _FlagBorradoFisioterapeuta;


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
        public char Sexo
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
        public int FlagBorradoFisioterapeuta
        {
            get { return _FlagBorradoFisioterapeuta; }
            set
            {
                _FlagBorradoFisioterapeuta = value;
                OnPropertyChanged("FlagBorradoFisioterapeuta");
            }
        }

        public RelayCommand ActualizarCommand { get; set; }

        public RelayCommand ObtenerUrlFotoCommand { get; set; }


        private static FisioterapeutaActualizarViewModel instance = null;

        public Fisioterapeuta fisoterapuetaActual { get; set; }

        public string strExtension { get; set; }

        #endregion


        #region Metodos

        public FisioterapeutaActualizarViewModel()
        {
            ActualizarCommand = new RelayCommand(ActualizarFisioterapueta);
            ObtenerUrlFotoCommand = new RelayCommand(ObtenerURLfotoFisioterapeuta);
            Rol = "Fisioterapueta Asignado";
        }

        public static FisioterapeutaActualizarViewModel Instace()
        {
            if (instance == null)
                instance = new FisioterapeutaActualizarViewModel();
            return instance;
        }

        public void SetFisioterapeutaSelecionado(Fisioterapeuta fa)
        {
            if (fa != null)
            {
                fisoterapuetaActual = fa;
                Fisioterapeuta f = fisoterapuetaActual;
                Codigo = f.iCodigoFisioterapeuta;
                Nombre = f.vNombresFisioterapeuta;
                Apellidos = f.vApellidosFisioterapeuta;
                Celular = f.vCelularFisioterapeuta;
                Telefono = f.vTelefonoFisioterapeuta;
                CentLaboral = f.vCentLabFisioterapeuta;
                Email = f.vEmailFisioterapeuta;
                Rol = f.vRolFisioterapeuta;
                NCTMP = f.vNumCTMPFisioterapeuta;
                NNDTA = f.vNumNDTAFisioterapeuta;
                Sexo = (char)f.cGenero;
                UrlFoto = f.vUrlFotoFosioterapeuta; ;
                FlagBorradoFisioterapeuta = Convert.ToInt32(f.iFlagBorradoFisioterapeuta);

            }

        }

        private void ActualizarFisioterapueta(object parameter)
        {
            try
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
                f.vUsuarioFiosioterapeuta = Nombre;
                f.vContrasenaFisioterapeuta = Nombre + Apellidos;
                f.vUrlFotoFosioterapeuta = UrlFoto;
                f.iFlagBorradoFisioterapeuta = FlagBorradoFisioterapeuta;

                GuardarImagenCargada();
                if (FisioterapeutaDL.ActualizarFisioterapeuta(f))
                {
                    businesssObject = new FisioterapeutaBusinessObject();

                    ObservableCollection<FisioterapeutaCRUDViewModel> ocltnFisioterapeutasCRUD = ListarFisioterapeutasViewModel.Instance().ListaFisioterapeutas;
                    FisioterapeutaCRUDViewModel fisioterapeutaActulizar = null;

                    if (ocltnFisioterapeutasCRUD != null)
                    {
                        for (int i = 0; i < ocltnFisioterapeutasCRUD.Count; i++)
                        {
                            if (ocltnFisioterapeutasCRUD[i].Codigo == this.Codigo)
                            {
                                fisioterapeutaActulizar = ocltnFisioterapeutasCRUD[i];

                                fisioterapeutaActulizar.Email = this.Email;
                                fisioterapeutaActulizar.Telefono = this.Telefono;
                                fisioterapeutaActulizar.Celular = this.Celular;

                                ListarFisioterapeutasViewModel.Instance().ListaFisioterapeutas[i] = fisioterapeutaActulizar;
                                ListarFisioterapeutasViewModel.Instance().ForzarListaRefresh();
                            }

                        }

                        MessageBox.Show("Fisioterauta actualizado");
                    }
                }


            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                //MessageBox.Show("Se guardo con exito");
            }
        }

        public ListarFisioterapeutasViewModel Continer
        {
            get { return ListarFisioterapeutasViewModel.Instance(); }
        }

        public void ObtenerURLfotoFisioterapeuta(object parameter)
        {

            OpenFileDialog ofdImage = new OpenFileDialog();
            ofdImage.Filter = "JPEG Files|*.jpg|Bitmap Files|*.bmp|Gif Files|*.gif";
            ofdImage.DefaultExt = "jpg";
            ofdImage.FilterIndex = 1;
            if (ofdImage.ShowDialog() == true)
            {
                Stream s = ofdImage.OpenFile();
                UrlFoto = ofdImage.FileName;
                strExtension = ofdImage.DefaultExt;

            }

        }

        private void GuardarImagenCargada()
        {
            //string strNombreFoto = DateTime.Now.ToString() + "_" + Apellidos + "_" + Nombre;
            try
            {
                string strFolderDestino = FisioterapeutaStatic.kstrRutaFoto;
                string strNombreFoto = Apellidos + "_" + Nombre;
                string strDestino = strFolderDestino + strNombreFoto + "." + strExtension;
                FileInfo fileInfo = new FileInfo(strDestino);
                if (!fileInfo.Exists)
                    File.Copy(UrlFoto, strDestino);
                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (columnName == "Celular")
                {
                    if (Celular == null)
                        return " ";
                    foreach (char c in Celular)
                    {
                        if (!Char.IsDigit(c))
                            return "Solo se permiten números";
                    }
                }
                else if (columnName == "Telefono")
                {
                    if (Telefono == null)
                        return " ";
                    foreach (char c in Telefono)
                    {
                        if (!Char.IsDigit(c))
                            return "Solo se permiten números";
                    }
                }
                else if (columnName == "Email")
                {

                    if (CentLaboral == null)  //must have an order description
                        return " ";
                    else if (CentLaboral.Trim() == string.Empty)
                        return "Es un campo obligatorio";

                    string expresion;
                    expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                    if (Regex.IsMatch(Email, expresion))
                    {
                        if (!(Regex.Replace(Email, expresion, string.Empty).Length == 0))
                            return "Email invalido";
                    }
                    else
                    {
                        return "Email invalido";
                    }


                }
                return null;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }

        #endregion

    }
}
