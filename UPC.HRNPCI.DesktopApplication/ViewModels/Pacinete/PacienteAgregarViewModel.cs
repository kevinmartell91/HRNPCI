using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;

using UPC.HRNPCI.DesktopApplication;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.PacienteModel;
using UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete;
using UPC.HRNPCI.DesktopApplication.ViewModels.AsociarPacienteFisioterapeuta;


namespace UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete
{
    class PacienteAgregarViewModel : BaseViewModel , IDataErrorInfo
    {
        #region Declaraciones


        public RelayCommand AgregarCommand { get; set; }
        public RelayCommand CargarFotoCommand { get; set; }

        PacienteBusinessObject busnessObject { get; set; }

        private int _iCodigo;
        public string _strNombres;
        private string _strApellidos;
        private char _chrGenero;
        private string _strDNI;
        private DateTime _daFecNacimiento;
        private string _strFisioAsignadoUno;
        private string _strFisioAsignadoDos;
        private string _strDiagnosticoMedico;
        private int _Nivel;
        private int _PorcentajeNivel;
        private string _strNomApeMedNeuroReferencia;
        private string _strCelMedNeuroReferencia;
        private string _strNombresApod;
        private string _strApellidosApod;
        private string _strCelularApod;
        private string _strTeléfonoApod;
        private string _strParentescoApod;
        private int _strBorrrado;
        private int _strFisioAsigPaciente;
        private string _strEdadCronologica;
        private string _strUrlFotoPaciente;

        private string strExtension{get;set;}



        /*
        public string strCodigo                   { get { return _strCodigo                  ; } set { _strCodigo                   = value; OnPropertyChanged("strCodigo"); } }
        public string strNombres                  { get { return _strNombres                 ; } set { _strNombres                  = value; OnPropertyChanged("strNombres"); } }
        public string strApellidos                { get { return _strApellidos               ; } set { _strApellidos                = value; OnPropertyChanged("strApellidos"); } }
        public string strGénero                   { get { return _strGénero                  ; } set { _strGénero                   = value; OnPropertyChanged("strGénero"); } }
        public string strDNI                      { get { return _strDNI                     ; } set { _strDNI                      = value; OnPropertyChanged("strDNI"); } }
        public string strFecNacimiento            { get { return _strFecNacimiento           ; } set { _strFecNacimiento            = value; OnPropertyChanged("strFecNacimiento"); } }
        public string strFisioAsignadoUno         { get { return _strFisioAsignadoUno        ; } set { _strFisioAsignadoUno         = value; OnPropertyChanged("strFisioAsignadoUno"); } }
        public string strFisioAsignadoDos         { get { return _strFisioAsignadoDos        ; } set { _strFisioAsignadoDos         = value; OnPropertyChanged("strFisioAsignadoDos"); } }
        public string strDiagnosticoMedico        { get { return _strDiagnosticoMedico       ; } set { _strDiagnosticoMedico        = value; OnPropertyChanged("strDiagnosticoMedico"); } }
        public string strNivel                    { get { return _strNivel                   ; } set { _strNivel                    = value; OnPropertyChanged("strNivel"); } }
        public string strPorcentajeNivel          { get { return _strPorcentajeNivel         ; } set { _strPorcentajeNivel          = value; OnPropertyChanged("strPorcentajeNivel"); } }
        public string strNomApeMedNeuroReferencia { get { return _strNomApeMedNeuroReferencia; } set { _strNomApeMedNeuroReferencia = value; OnPropertyChanged("strNomApeMedNeuroReferencia"); } }
        public string strCelMedNeuroReferencia    { get { return _strCelMedNeuroReferencia   ; } set { _strCelMedNeuroReferencia    = value; OnPropertyChanged("strCelMedNeuroReferencia"); } }
        public string strNombresApod              { get { return _strNombresApod             ; } set { _strNombresApod              = value; OnPropertyChanged("strNombresApod"); } }
        public string strApellidosApod            { get { return _strApellidosApod           ; } set { _strApellidosApod            = value; OnPropertyChanged("strApellidosApod          "); } }
        public string strCelularApod              { get { return _strCelularApod             ; } set { _strCelularApod              = value; OnPropertyChanged("strCelularApod"); } }
        public string strTeléfonoApod             { get { return _strTeléfonoApod            ; } set { _strTeléfonoApod             = value; OnPropertyChanged("strTeléfonoApod"); } }
        public string strParentescoApod           { get { return _strParentescoApod          ; } set { _strParentescoApod           = value; OnPropertyChanged("strParentescoApod"); } }
        public string strBorrrado                 { get { return _strBorrrado                ; } set { _strBorrrado                 = value; OnPropertyChanged("strBorrrado"); } }
        public string strFisioAsigPaciente        { get { return _strFisioAsigPaciente       ; } set { _strFisioAsigPaciente        = value; OnPropertyChanged("strFisioAsigPaciente"); } }

         */
        
        public int iCodigo                      { get { return _iCodigo                    ; } set { _iCodigo                     = value; OnPropertyChanged("iCodigo"); } }
        public string strNombres                  { get { return _strNombres                 ; } set { _strNombres                  = value; OnPropertyChanged("strNombres"); } }
        public string strApellidos                { get { return _strApellidos               ; } set { _strApellidos                = value; OnPropertyChanged("strApellidos"); } }
        public char chrGenero                     { get { return _chrGenero                  ; } set { _chrGenero                   = value; OnPropertyChanged("chrGenero"); } }
        public string strDNI                      { get { return _strDNI                     ; } set { _strDNI                      = value; OnPropertyChanged("strDNI"); } }
        public DateTime daFecNacimiento            { get { return _daFecNacimiento           ; } set { _daFecNacimiento            = value; OnPropertyChanged("daFecNacimiento"); } }
        public string strFisioAsignadoUno         { get { return _strFisioAsignadoUno        ; } set { _strFisioAsignadoUno         = value; OnPropertyChanged("strFisioAsignadoUno"); } }
        public string strFisioAsignadoDos         { get { return _strFisioAsignadoDos        ; } set { _strFisioAsignadoDos         = value; OnPropertyChanged("strFisioAsignadoDos"); } }
        public string strDiagnosticoMedico        { get { return _strDiagnosticoMedico       ; } set { _strDiagnosticoMedico        = value; OnPropertyChanged("strDiagnosticoMedico"); } }
        public int iNivel                       { get { return _Nivel                      ; } set { _Nivel                       = value; OnPropertyChanged("iNivel"); } }
        public int iPorcentajeNivel             { get { return _PorcentajeNivel            ; } set { _PorcentajeNivel             = value; OnPropertyChanged("iPorcentajeNivel"); } }
        public string strNomApeMedNeuroReferencia { get { return _strNomApeMedNeuroReferencia; } set { _strNomApeMedNeuroReferencia = value; OnPropertyChanged("strNomApeMedNeuroReferencia"); } }
        public string strCelMedNeuroReferencia    { get { return _strCelMedNeuroReferencia   ; } set { _strCelMedNeuroReferencia    = value; OnPropertyChanged("strCelMedNeuroReferencia"); } }
        public string strNombresApod              { get { return _strNombresApod             ; } set { _strNombresApod              = value; OnPropertyChanged("strNombresApod"); } }
        public string strApellidosApod            { get { return _strApellidosApod           ; } set { _strApellidosApod            = value; OnPropertyChanged("strApellidosApod          "); } }
        public string strCelularApod              { get { return _strCelularApod             ; } set { _strCelularApod              = value; OnPropertyChanged("strCelularApod"); } }
        public string strTelefonoApod             { get { return _strTeléfonoApod            ; } set { _strTeléfonoApod             = value; OnPropertyChanged("strTelefonoApod"); } }
        public string strParentescoApod           { get { return _strParentescoApod          ; } set { _strParentescoApod           = value; OnPropertyChanged("strParentescoApod"); } }
        public int iBorrrado                    { get { return _strBorrrado                ; } set { _strBorrrado                 = value; OnPropertyChanged("iBorrrado"); } }
        public int iFisioAsigPaciente           { get { return _strFisioAsigPaciente       ; } set { _strFisioAsigPaciente        = value; OnPropertyChanged("iFisioAsigPaciente"); } }
        public string strEdadCronologica           { get { return _strEdadCronologica          ; } set { _strEdadCronologica           = value; OnPropertyChanged("strEdadCronologica"); } }
        public string strUrlFotoPaciente           { get { return _strUrlFotoPaciente          ; } set { _strUrlFotoPaciente           = value; OnPropertyChanged("strUrlFotoPaciente"); } }


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
                //-------------------------------------------------------------------
                if (iGeneroSeleccionado == 0) { chrGenero = '-'; }
                else if (iGeneroSeleccionado == 1) { chrGenero = 'M'; }
                else { chrGenero = 'F'; }
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

        #region Combo Niveles

        private List<string> _lstNiveles;
        public List<string> lstNiveles
        {
            get { return _lstNiveles; }
            set
            {
                _lstNiveles = value;
                OnPropertyChanged("lstNiveles");
            }
        }

        public int _iNivelSeleccionado;
        public int iNivelSeleccionado
        {
            get { return _iNivelSeleccionado; }
            set
            {
                _iNivelSeleccionado = value;
                OnPropertyChanged("iNivelSeleccionado");
                iNivel = iNivelSeleccionado;

            }
        }

        public void InicializarComboNivelesPCI()
        {
            _lstNiveles = new List<string>();
            _lstNiveles.Add("-");
            _lstNiveles.Add("I");
            _lstNiveles.Add("II");
            _lstNiveles.Add("III");
            _lstNiveles.Add("IV");
            _lstNiveles.Add("V");

        }

        #endregion

        #endregion

        #region Metodos

        public PacienteAgregarViewModel()
        {
            InicializarComboGenero();
            InicializarComboNivelesPCI();

            AgregarCommand = new RelayCommand(AgregarPaciente);
            CargarFotoCommand = new RelayCommand(ObtenerURLfotoPaciente);


            daFecNacimiento = DateTime.Now;
            strDiagnosticoMedico = "Parálisis Cerebral";
            strUrlFotoPaciente = "";
            chrGenero = '-';
        }

        private void AgregarPaciente(object parameter)
        {

            //CopiarFotoPaciente();

            try
            {
                PacienteB paciente = new PacienteB();
                //f.iCodigoPaciente           =  strCodigo                  ;
                paciente.vNombresPaciente = strNombres;
                paciente.vApellidosPaciente = strApellidos;
                paciente.cGeneroPaciente = Convert.ToString(chrGenero);
                paciente.cDNIPaciente = strDNI;
                paciente.daFecNacPaciente = daFecNacimiento.Date;
                //paciente.iCodigoFisioterapeutaUno  =  Convert.ToInt32(strFisioAsignadoUno)      ;
                //paciente.iCodigoFisioterapeutaDos  =  Convert.ToInt32(strFisioAsignadoDos)      ;
                paciente.vDiagnosticoMedicoPaciente = strDiagnosticoMedico;
                paciente.iNivelPaciente = iNivel;
                paciente.iPorcentajeNivelPaciente = Convert.ToInt32(iPorcentajeNivel);
                paciente.vNomApeMedNeuroReferencia = strNomApeMedNeuroReferencia;
                paciente.vCelMedNeuroReferencia = strCelMedNeuroReferencia;
                paciente.vNombresApoderado = strNombresApod;
                paciente.vApellidoApoderado = strApellidosApod;
                paciente.vCelularApoderado = strCelularApod;
                paciente.vTelefonoApoderado = strTelefonoApod;
                paciente.vParentescoApoderado = strParentescoApod;
                paciente.vEdadCronologicaPaciente = strEdadCronologica;
                paciente.vUrlFotoPaciente = GuardarImagenCargada();

                paciente.iFlagBorrradoPaciente = 0;
                paciente.iFlagFisioAsigPaciente = 0;

                if (PacienteDL.GuardarPaciente(paciente))
                {
                    busnessObject = new PacienteBusinessObject();
                    //ListarPacientesViewModel.Instance().ocltnPacientesCRUD.Add(busnessObject.ObtenerPacienteCRUD(paciente));
                    ListarPacientesViewModel.Instance().ForzarListaRefresh();

                    ListaPacientesAsociacionViewModel.Instance().ForzarListaRefresh();

                    MessageBox.Show("El fisioterapuesta ha sido registrado con éxito.","Mensaje");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                LimpiarCampos();

            }
        }

        public void ObtenerURLfotoPaciente(object parameter)
        {
            try
            {
                OpenFileDialog ofdImage = new OpenFileDialog();
                ofdImage.Filter = "JPEG Files|*.jpg|Bitmap Files|*.bmp|Gif Files|*.gif";
                ofdImage.DefaultExt = "jpg";
                ofdImage.FilterIndex = 1;
                if (ofdImage.ShowDialog() == true)
                {
                    Stream s = ofdImage.OpenFile();
                    strUrlFotoPaciente = ofdImage.FileName;
                    strExtension = ofdImage.DefaultExt;

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

        private string GuardarImagenCargada()
        {
            //string strNombreFoto = DateTime.Now.ToString() + "_" + Apellidos + "_" + Nombre;
            try
            {
                string strFolderDestino = PacienteStatic.kstrRutaFoto;
                string strNombreFoto = strApellidos + "_" + strNombres;
                string strDestino = strFolderDestino + strNombreFoto + "." + strExtension;
                FileInfo fileInfo = new FileInfo(strDestino);
                if (!fileInfo.Exists && strExtension != null)
                    File.Copy(strUrlFotoPaciente, strDestino);
                return strDestino;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void LimpiarCampos()
        {
           //strCodigo                  = "";
           strNombres                 = "";
           strApellidos               = "";
           chrGenero                  = '-';
           strDNI                     = "";
           //daFecNacimiento           = Convert.ToDateTime("2015/15/15");
           //strFisioAsignadoUno        = "";
           //strFisioAsignadoDos        = "";
           strDiagnosticoMedico       = "";
           //iNivel                     = -1;
           iPorcentajeNivel           = -1;
           strNomApeMedNeuroReferencia= "";
           strCelMedNeuroReferencia   = "";
           strNombresApod             = "";
           strApellidosApod           = "";
           strCelularApod             = "";
           strTelefonoApod            = "";
           strParentescoApod          = "";
           strEdadCronologica         = "";
           //strBorrrado                = "";
           //strFisioAsigPaciente       = "";

        }


        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (columnName == "strNombres")
                {

                    if (strNombres == null)  //must have an order description
                        return " ";
                    else if (strNombres.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strNombres)
                    {

                        if (!Char.IsLetter(c) && c != ' ')
                            return "Solo caracteres alfabéticos";
                    }

                }
                else if (columnName == "strApellidos")
                {

                    if (strApellidos == null)
                        return " ";
                    else if (strApellidos.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strApellidos)
                    {
                        if (!Char.IsLetter(c) && c != ' ')
                            return "Solo caracteres alfabéticos";
                    }

                }
                else if (columnName == "chrGenero")
                {
                    if (chrGenero.Equals('-'))
                        return " ";
                    //if(Sexo != 'F' && Sexo != 'M')    
                    //    return "Es un campo obligatorio";

                }
                else if (columnName == "strDNI")
                {
                    if (strDNI == null)
                        return " ";
                    else if (strDNI.Trim() == string.Empty)
                        return "Es un campo obligatorio";

                    foreach (char c in strDNI)
                    {
                        if (!Char.IsDigit(c))
                            return "Solo se permiten números";
                    }
                    if (strDNI.Length < 8)
                        return "Dni invalido";

                }
                else if (columnName == "strNombresApod")
                {

                    if (strNombresApod == null)
                        return " ";
                    else if (strNombresApod.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strNombresApod)
                    {
                        if (!Char.IsLetter(c) && c != ' ')
                            return "Solo caracteres alfabéticos";
                    }

                }
                else if (columnName == "strApellidosApod")
                {

                    if (strApellidosApod == null)
                        return " ";
                    else if (strApellidosApod.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strApellidosApod)
                    {
                        if (!Char.IsLetter(c) && c != ' ')
                            return "Solo caracteres alfabéticos";
                    }

                }
                else if (columnName == "strParentescoApod")
                {

                    if (strParentescoApod == null)
                        return " ";
                    else if (strParentescoApod.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strParentescoApod)
                    {
                        if (!Char.IsLetter(c) && c != ' ')
                            return "Solo caracteres alfabéticos";
                    }

                }
                else if (columnName == "strCelularApod")
                {
                    if (strCelularApod == null)
                        return " ";
                    else if (strCelularApod.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strCelularApod)
                    {
                        if (!Char.IsDigit(c))
                            return "Solo se permiten números";
                    }
                }
                else if (columnName == "strTelefonoApod")
                {
                    if (strTelefonoApod == null)
                        return " ";
                    else if (strTelefonoApod.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strTelefonoApod)
                    {
                        if (!Char.IsDigit(c))
                            return "Solo se permiten números";
                    }
                }
                else if (columnName == "iNivel")
                {
                    if (iNivel == 0)
                        return " ";
                    
                }
                else if (columnName == "iPorcentajeNivel")
                {
                    if (iPorcentajeNivel.ToString() == null)  //must have an order description
                        return " ";
                    else if (iPorcentajeNivel.ToString ().Trim() == string.Empty)
                        return "Es un campo obligatorio 0";
                    else if(iPorcentajeNivel < 0 || iPorcentajeNivel > 100)
                        return "Esta fuera del rango permitido";

                }
                else if (columnName == "strEdadCronologica")
                {

                    if (strEdadCronologica == null)  //must have an order description
                        return " ";
                    else if (strEdadCronologica.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strEdadCronologica)
                    {
                        if (!Char.IsLetterOrDigit(c) && c != '-')
                            return "Solo caracteres alfanuméricos";
                    }

                }
                else if (columnName == "strNomApeMedNeuroReferencia")
                {
                    if (strNomApeMedNeuroReferencia == null)
                        return " ";
                    else if (strNomApeMedNeuroReferencia.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strNomApeMedNeuroReferencia)
                    {
                        if (!Char.IsLetter(c) && c != ' ')
                            return "Solo caracteres alfabéticos";
                    }
                }
                else if (columnName == "strCelMedNeuroReferencia")
                {
                    if (strCelMedNeuroReferencia == null)
                        return " ";
                    else if (strCelMedNeuroReferencia.Trim() == string.Empty)
                        return "Es un campo obligatorio";
                    foreach (char c in strCelMedNeuroReferencia)
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

