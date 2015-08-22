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
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete
{
    class PacienteActualizarViewModel : BaseViewModel, IDataErrorInfo
    {
         #region Declaraciones

        PacienteBusinessObject businessObject { get; set; }

        public RelayCommand ActualizarCommand { get; set; }
        public Paciente PacienteActualizar { get; set; }

        private int _iCodigo;
        private string _strNombres;
        private string _strApellidos;
        private char _chrGenero;
        private string _strDNI;
        private string _strFecNacimiento;
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
        public string strFecNacimiento            { get { return _strFecNacimiento           ; } set { _strFecNacimiento            = value; OnPropertyChanged("strFecNacimiento"); } }
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
        public string strUrlFotoPaciente { get { return _strUrlFotoPaciente; } set { _strUrlFotoPaciente = value; OnPropertyChanged("strUrlFotoPaciente"); } }



        #endregion

        #region Metodos

        public PacienteActualizarViewModel()
        {
            ActualizarCommand = new RelayCommand(ActualizarPaciente);
        }

        public void SetPacienteActualizar(Paciente pPaciente)
        {
            if (pPaciente != null)
            {
                PacienteActualizar = pPaciente;
                Paciente paciente = PacienteActualizar;
             iCodigo                                     = paciente.iCodigoPaciente                                 ;
             strNombres                                  = paciente.vNombresPaciente                                ;
             strApellidos                                = paciente.vApellidosPaciente                              ;
             chrGenero                                   = Convert.ToChar(paciente.cGeneroPaciente)                 ;
             strDNI                                      = paciente.cDNIPaciente                                    ;
             strFecNacimiento                             = Convert.ToDateTime(paciente.daFecNacPaciente).ToString("yyyy/MM/dd");
             strFisioAsignadoUno                         = paciente.iCodigoFisioterapeutaUno.ToString()                        ;
             strFisioAsignadoDos                         = paciente.iCodigoFisioterapeutaDos.ToString()                        ;
             strDiagnosticoMedico                        = paciente.vDiagnosticoMedicoPaciente                      ;
             iNivel                                      = Convert.ToInt32(paciente.iNivelPaciente)                 ;
             iPorcentajeNivel                            = Convert.ToInt32(paciente.iPorcentajeNivelPaciente)       ;
             strNomApeMedNeuroReferencia                 = paciente.vNomApeMedNeuroReferencia                        ;
             strCelMedNeuroReferencia                    = paciente.vCelMedNeuroReferencia                       ;
             strParentescoApod                           = paciente.vParentescoApoderado                          ;
             strNombresApod                              = paciente.vNombresApoderado                               ;
             strApellidosApod                            = paciente.vApellidoApoderado                              ;
             strCelularApod                              = paciente.vCelularApoderado                               ;
             strTelefonoApod                             = paciente.vTelefonoApoderado                              ;
             strEdadCronologica                          = paciente.vEdadCronologicaPaciente                            ;
             iBorrrado                                   = Convert.ToInt32(paciente.iFlagBorrradoPaciente)                                ;
             iFisioAsigPaciente                          = Convert.ToInt32(paciente.iFlagFisioAsigPaciente)                           ;
             strUrlFotoPaciente                          = paciente.vUrlFotoPaciente                                ;
                
                
            }

        }

        private void ActualizarPaciente(object parameter)
        {
            try
            {
               Paciente paciente = new Paciente();
               paciente.iCodigoPaciente           =  iCodigo                                    ;
               paciente.vNombresPaciente          =  strNombres                                 ;
               paciente.vApellidosPaciente        =  strApellidos                               ;
               paciente.cGeneroPaciente           =  chrGenero                                  ;
               paciente.cDNIPaciente              =  strDNI                                     ;
               paciente.daFecNacPaciente          =  DateTime.ParseExact(strFecNacimiento,"yyyy/mm/dd",null)                       ;
               if (strFisioAsignadoUno != "")
                    paciente.iCodigoFisioterapeutaUno  =  Convert.ToInt32(strFisioAsignadoUno)       ;
               if (strFisioAsignadoDos != "")
                    paciente.iCodigoFisioterapeutaDos  =  Convert.ToInt32(strFisioAsignadoDos)       ;
               paciente.vDiagnosticoMedicoPaciente=  strDiagnosticoMedico                       ;
               paciente.iNivelPaciente            =  iNivel                                     ;
               paciente.iPorcentajeNivelPaciente  =  iPorcentajeNivel                           ;
               paciente.vNomApeMedNeuroReferencia =  strNomApeMedNeuroReferencia                ;
               paciente.vCelMedNeuroReferencia    =  strCelMedNeuroReferencia                   ;
               paciente.vParentescoApoderado      =  strParentescoApod                          ;
               paciente.vNombresApoderado         =  strNombresApod                             ;
               paciente.vApellidoApoderado        =  strApellidosApod                           ;
               paciente.vCelularApoderado         =  strCelularApod                             ;
               paciente.vTelefonoApoderado        =  strTelefonoApod                            ;
               paciente.vEdadCronologicaPaciente  =  strEdadCronologica                         ;
               paciente.iFlagBorrradoPaciente     =  iBorrrado                                  ;
               paciente.iFlagFisioAsigPaciente    =  iFisioAsigPaciente                         ;
               paciente.vUrlFotoPaciente          =  strUrlFotoPaciente                         ;

                if (PacienteDL.ActualizarPaciente(paciente))
                {

                    ListarPacientesViewModel.Instance().ForzarListaRefresh();
                    MessageBox.Show("Paciente actualizado.");

                    //businessObject = new PacienteBusinessObject();

                    //ObservableCollection<PacienteCRUDViewModel> listaPacientesCRUD = ListarPacientesViewModel.Instance().ocltnPacientesCRUD;
                    //PacienteCRUDViewModel pacienteActualizar = null;
                    //if (listaPacientesCRUD != null)
                    //{
                    //    for (int i = 0; i < listaPacientesCRUD.Count; i++)
                    //    {
                    //        if (listaPacientesCRUD[i].iCodigo == this.iCodigo)
                    //        {
                    //            pacienteActualizar = listaPacientesCRUD[i];
                    //            ListarPacientesViewModel.Instance().ocltnPacientesCRUD[i] = pacienteActualizar;
                    //            ListarPacientesViewModel.Instance().ForzarListaRefresh();
                    //            MessageBox.Show("Paciente actualizado.");
                    //        }
                    //    }
                    //}
                   
                   
                }

            }
            catch (Exception ex)
            {
                throw ex;
                 MessageBox.Show("No actualizó al paciente, intente en unos momentos.");
                 return;
            }
        
        }

      

        public ListarFisioterapeutasViewModel Continer
        {
            get { return ListarFisioterapeutasViewModel.Instance(); }
        }

        //TODO Ipmplemtar el métod de lee y que luego cargue la foto en el nuevo deestino

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (columnName == "strNombresApod")
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
