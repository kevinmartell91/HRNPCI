using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using UPC.HRNPCI.DesktopApplication;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.PacienteModel;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete
{
    class PacienteVerViewModel : BaseViewModel
    {
         #region Declaraciones

        public PacienteB PacienteVer { get; set; }

        private int _iCodigo;
        private string _strNombres;
        private string _strApellidos;
        private char _chrGenero;
        private string _strDNI;
        private string _strFecNacimiento;
        private string _strFisioAsignadoUno;
        private string _strFisioAsignadoDos;
        private string _strDiagnosticoMedico;
        private int _iNivel;
        private int _iPorcentajeNivel;
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
        
        public int iCodigo                        { get { return _iCodigo                    ; } set { _iCodigo                     = value; OnPropertyChanged("iCodigo"); } }
        public string strNombres                  { get { return _strNombres                 ; } set { _strNombres                  = value; OnPropertyChanged("strNombres"); } }
        public string strApellidos                { get { return _strApellidos               ; } set { _strApellidos                = value; OnPropertyChanged("strApellidos"); } }
        public char chrGenero                     { get { return _chrGenero                  ; } set { _chrGenero                   = value; OnPropertyChanged("chrGenero"); } }
        public string strDNI                      { get { return _strDNI                     ; } set { _strDNI                      = value; OnPropertyChanged("strDNI"); } }
        public string  strFecNacimiento           { get { return _strFecNacimiento            ; } set { _strFecNacimiento             = value; OnPropertyChanged("strFecNacimiento"); } }
        public string strFisioAsignadoUno         { get { return _strFisioAsignadoUno        ; } set { _strFisioAsignadoUno         = value; OnPropertyChanged("strFisioAsignadoUno"); } }
        public string strFisioAsignadoDos         { get { return _strFisioAsignadoDos        ; } set { _strFisioAsignadoDos         = value; OnPropertyChanged("strFisioAsignadoDos"); } }
        public string strDiagnosticoMedico        { get { return _strDiagnosticoMedico       ; } set { _strDiagnosticoMedico        = value; OnPropertyChanged("strDiagnosticoMedico"); } }
        public int iNivel                         { get { return _iNivel                      ; } set { _iNivel                       = value; OnPropertyChanged("iNivel"); } }
        public int iPorcentajeNivel               { get { return _iPorcentajeNivel            ; } set { _iPorcentajeNivel             = value; OnPropertyChanged("iPorcentajeNivel"); } }
        public string strNomApeMedNeuroReferencia { get { return _strNomApeMedNeuroReferencia; } set { _strNomApeMedNeuroReferencia = value; OnPropertyChanged("strNomApeMedNeuroReferencia"); } }
        public string strCelMedNeuroReferencia    { get { return _strCelMedNeuroReferencia   ; } set { _strCelMedNeuroReferencia    = value; OnPropertyChanged("strCelMedNeuroReferencia"); } }
        public string strNombresApod              { get { return _strNombresApod             ; } set { _strNombresApod              = value; OnPropertyChanged("strNombresApod"); } }
        public string strApellidosApod            { get { return _strApellidosApod           ; } set { _strApellidosApod            = value; OnPropertyChanged("strApellidosApod          "); } }
        public string strCelularApod              { get { return _strCelularApod             ; } set { _strCelularApod              = value; OnPropertyChanged("strCelularApod"); } }
        public string strTeléfonoApod             { get { return _strTeléfonoApod            ; } set { _strTeléfonoApod             = value; OnPropertyChanged("strTeléfonoApod"); } }
        public string strParentescoApod           { get { return _strParentescoApod          ; } set { _strParentescoApod           = value; OnPropertyChanged("strParentescoApod"); } }
        public int iBorrrado                      { get { return _strBorrrado                ; } set { _strBorrrado                 = value; OnPropertyChanged("iBorrrado"); } }
        public int iFisioAsigPaciente             { get { return _strFisioAsigPaciente       ; } set { _strFisioAsigPaciente        = value; OnPropertyChanged("iFisioAsigPaciente"); } }
        public string strEdadCronologica          { get { return _strEdadCronologica         ; } set { _strEdadCronologica          = value; OnPropertyChanged("strEdadCronologica"); } }
        public string strUrlFotoPaciente          { get { return _strUrlFotoPaciente         ; } set { _strUrlFotoPaciente          = value; OnPropertyChanged("strUrlFotoPaciente"); } }



        #endregion

        #region Metodos

        public PacienteVerViewModel()
        {

        }

        public void SetPacienteVer(PacienteB pPaciente)
        {
            if (pPaciente != null)
            {
                PacienteVer = pPaciente;
                PacienteB paciente = PacienteVer;
              //iCodigo                              = paciente.iCodigoPaciente                                 ;
                strNombres                           = paciente.vNombresPaciente                                ;
                strApellidos                         = paciente.vApellidosPaciente                              ;
                chrGenero                            = Convert.ToChar(paciente.cGeneroPaciente)                 ;
                strDNI                               = paciente.cDNIPaciente                                    ;
                strFecNacimiento                     = Convert.ToDateTime(paciente.daFecNacPaciente).ToString("yyyy/MM/dd");
             // Convert.ToInt32(strFisioAsignadoUno) = paciente.iCodigoFisioterapeutaUno                        ;
             // Convert.ToInt32(strFisioAsignadoDos) = paciente.iCodigoFisioterapeutaDos                        ;
                strDiagnosticoMedico                 = paciente.vDiagnosticoMedicoPaciente                      ;
                iNivel                               = Convert.ToInt32(paciente.iNivelPaciente)                 ;
                iPorcentajeNivel                     = Convert.ToInt32(paciente.iPorcentajeNivelPaciente)       ;
                strEdadCronologica                   = paciente.vEdadCronologicaPaciente                        ;
                strNomApeMedNeuroReferencia          = paciente.vNomApeMedNeuroReferencia                       ;
                strCelMedNeuroReferencia             = paciente.vCelMedNeuroReferencia                          ;
                strNombresApod                       = paciente.vNombresApoderado                               ;
                strApellidosApod                     = paciente.vApellidoApoderado                              ;
                strCelularApod                       = paciente.vCelularApoderado                               ;
                strTeléfonoApod                      = paciente.vTelefonoApoderado                              ;
                strParentescoApod                    = paciente.vParentescoApoderado                            ;
                strUrlFotoPaciente                   = paciente.vUrlFotoPaciente                                ;
             // iBorrrado                            = paciente.iFlagBorrradoPaciente                           ;
             // iFisioAsigPaciente                   = paciente.iFlagFisioAsigPaciente                          ;
                
            }

        }

        

        public ListarFisioterapeutasViewModel Continer
        {
            get { return ListarFisioterapeutasViewModel.Instance(); }
        }


        #endregion
    }
}
