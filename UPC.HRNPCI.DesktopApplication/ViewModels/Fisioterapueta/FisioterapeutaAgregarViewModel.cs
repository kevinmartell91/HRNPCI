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
using UPC.HRNPCI.DesktopApplication.ViewModels.AsociarPacienteFisioterapeuta;
using System.ComponentModel;


using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta
{
    class FisioterapeutaAgregarViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Declaraciones
        FisioterapeutaBusinessObject businesssObject = null;

        public RelayCommand AgregarCommand { get; set; }
        public RelayCommand ObtenerUrlFotoCommand { get; set; }

        public int iFiosioterapeutaID { get; set; }
        string strExtension { get; set; }


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

        


        #region Combo genero

        private List<string> _lstGeneros;
        public List<string> lstGeneros
        {
            get { return _lstGeneros; }
            set
            {
                _lstGeneros = value;
                OnPropertyChanged("lstGeneros");
            }
        }

        public int _iGeneroSeleccionado;
        public int iGeneroSeleccionado
        {
            get { return _iGeneroSeleccionado; }
            set
            {
                _iGeneroSeleccionado = value;
                OnPropertyChanged("iGeneroSeleccionado");
                if (iGeneroSeleccionado == 0) { Sexo = '-'; }
                else if (iGeneroSeleccionado == 1) { Sexo = 'M'; }
                else { Sexo = 'F'; }
            }
        }

        public void InicializarComboGenero()
        {
            _lstGeneros = new List<string>();
            _lstGeneros.Add("-");
            _lstGeneros.Add("Masculino");
            _lstGeneros.Add("Femenino");

        }

        #endregion
        
        #endregion


        #region Metodos
        
        public FisioterapeutaAgregarViewModel()
        {
            InicializarComboGenero();
            AgregarCommand = new RelayCommand(AgregarFisioterapueta);
            ObtenerUrlFotoCommand = new RelayCommand(ObtenerURLfotoFisioterapeuta);
            Rol = FisioterapeutaStatic.Rol;
            Sexo = '-';
            businesssObject = new FisioterapeutaBusinessObject();
            
        }

        private void AgregarFisioterapueta(object parameter)
        {
            try
            {
                FisioterapeutaB f = new FisioterapeutaB();
                f.vNombresFisioterapeuta = Nombre;
                f.vApellidosFisioterapeuta = Apellidos;
                f.vCelularFisioterapeuta = Celular;
                f.vTelefonoFisioterapeuta = Telefono;
                f.vCentLabFisioterapeuta = CentLaboral;
                f.vEmailFisioterapeuta = Email;
                f.vNumCTMPFisioterapeuta = NCTMP;
                f.vNumNDTAFisioterapeuta = NNDTA;
                f.vRolFisioterapeuta = Rol;
                char[] delimiterChars = {'@'};
                string[] strEmail = Email.Split(delimiterChars);
                f.vUsuarioFiosioterapeuta = strEmail[0];

                Random r = new Random();
                f.vContrasenaFisioterapeuta = r.Next(1000,10000).ToString();
                f.cGenero = Sexo.ToString();
                f.vUrlFotoFosioterapeuta = UrlFoto;
                f.iFlagBorradoFisioterapeuta= 0;

                GuardarImagenCargada();

                if (FisioterapeutaDL.GuardarFisiotaerapeuta(f))
                {
                    businesssObject = new FisioterapeutaBusinessObject();
                    ListarFisioterapeutasViewModel.Instance().ListaFisioterapeutas.Add(businesssObject.ObtenerFisioterapeutaCRUD(f));
                    
                    MessageBox.Show("El fisioterapuesta ha sido registrado.");
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
           
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
                string strNombreFoto = "\\" + Apellidos + "_" + Nombre;
                string strDestino = strFolderDestino + strNombreFoto + "." + strExtension;
                FileInfo fileInfo = new FileInfo(strDestino);
                if (!fileInfo.Exists && strExtension != null)
                    File.Copy(UrlFoto, strDestino);
                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
       

        public ListarFisioterapeutasViewModel Continer
        {
            get { return ListarFisioterapeutasViewModel.Instance(); }
        }

        #endregion


        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (columnName == "Nombre")
                {

                    if (Nombre == null)  //must have an order description
                        return " ";
                    else if (Nombre.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in Nombre)
                    {

                        if (!Char.IsLetter(c) &&  c != ' ')
                           return "Solo caracteres alfabéticos";
                    }
                    
                }
                else if (columnName == "Apellidos")
                {

                    if (Apellidos == null) 
                        return " ";
                    else if (Apellidos.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in Apellidos)
                    {
                        if (!Char.IsLetter(c) && c != ' ')
                            return "Solo caracteres alfabéticos";
                    }

                }
                else if (columnName == "Sexo")
                {
                    if (Sexo.Equals('-'))
                        return " ";
                    //if(Sexo != 'F' && Sexo != 'M')    
                    //    return "Es un campo obligatorio";
                   
                }
                else if (columnName == "Celular")
                {
                    if (Celular == null)  
                        return " ";
                    else if (Celular.Trim() == string.Empty)
                        return "Es un campo obligatorio";
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
                    else if (Telefono.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in Telefono)
                    {
                        if (!Char.IsDigit(c))
                            return "Solo se permiten números";
                    }
                }
                else if (columnName == "CentLaboral")
                {

                    if (CentLaboral == null)  //must have an order description
                        return " ";
                    else if (CentLaboral.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in CentLaboral)
                    {
                        if (!Char.IsLetterOrDigit(c) && c != ' ')
                            return "Solo caracteres alfanuméricos";
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
                        if(! (Regex.Replace(Email, expresion, string.Empty).Length == 0))
                            return "Email invalido";
                    }
                    else
                    {
                        return "Email invalido";
                    }


                }
                else if (columnName == "NCTMP")
                {
                    if (NCTMP == null)
                        return " ";
                    foreach (char c in NCTMP)
                    {
                        if (!Char.IsDigit(c))
                            return "Solo se permiten números";
                    }
                }
                else if (columnName == "NNDTA")
                {
                    if (NNDTA == null)  
                        return " ";
                    foreach (char c in NNDTA)
                    {
                        if (!Char.IsDigit(c))
                            return "Solo se permiten números";
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
