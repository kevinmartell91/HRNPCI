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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.PacienteModel;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.AsociarPacienteFisioterapeuta
{
    class AsociacionCU
    {
         #region Declaraciones

        public RelayCommand AccionAsignacionCommand { get; set; }
        public Button btnAccion;
        public string strNombreBoton { get; set; }

        DispatcherTimer Timer;
        public int _iFisioAsigPaciente;

        public int      iCodigo                     {get;set;}
        public string   strNombres                  {get;set;}
        public string   strApellidos                {get;set;}
        public char     chrGenero                   {get;set;}
        public string   strDNI                      {get;set;}
        public string   strFecNacimiento             {get;set;}        
        public string   strFisioAsignadoUno         {get;set;}
        public string   strFisioAsignadoDos         {get;set;}
        public string   strDiagnosticoMedico        {get;set;}
        public int      iNivel                      {get;set;}
        public int      iPorcentajeNivel            {get;set;} 
        public string   strNomApeMedNeuroReferencia {get;set;}
        public string   strCelMedNeuroReferencia    {get;set;}
        public string   strNombresApod              {get;set;}
        public string   strApellidosApod            {get;set;}
        public string   strCelularApod              {get;set;}
        public string   strTeléfonoApod             {get;set;}        
        public string   strParentescoApod           {get;set;}
        public int      iBorrrado                   {get;set;}
        public int iFisioAsigPaciente
        {
            get
            {
                //if (_iFisioAsigPaciente == 0)
                //    strNombreBoton = "Asignar";
                //else strNombreBoton = "Actualizar";
                //return _iFisioAsigPaciente;

                if (_iFisioAsigPaciente == 0)
                    strNombreBoton = @"..\..\Resources\imagenes\add_icon.jpg";
                else strNombreBoton = @"..\..\Resources\imagenes\edit_icon.jpg";
                return _iFisioAsigPaciente;
            }
            set
            {
                _iFisioAsigPaciente = value;
            }
        }
        public string   strEdadCronologica          {get;set;}
        public string   strUrlFotoPaciente          {get;set;}

        public string strFisioAsignadoNombreCompletoUno { get; set; }
        public string strFisioAsignadoNombreCompletoDos { get; set; }

        #endregion


        #region Metodos

        public AsociacionCU()
        {

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(8000);
            Timer.Tick += new EventHandler(RefreshBotonTexto);
            Timer.Start();

            btnAccion = new Button();
           
            AccionAsignacionCommand = new RelayCommand(AccionAsignacion);
            btnAccion.Command = AccionAsignacionCommand;

        }

        void RefreshBotonTexto(object sender, EventArgs e)
        {
         
        }

        private void AccionAsignacion(object parameter)
        {


            AsociarPacienteFisioterapeutaViewModel asociacionVM = new AsociarPacienteFisioterapeutaViewModel();

            if (iFisioAsigPaciente == 0) // crear asignacion
            {
                asociacionVM.Mode = Mode.Add;
                asociacionVM.strTipoAction = "Asignar";
                asociacionVM.strNombreVentana = "Asignar fisioterapeuta(s)";

                asociacionVM.SelectedKeyUno = new KeyValuePair<int, string>(-1, "-");
                asociacionVM.SelectedKeyDos = new KeyValuePair<int, string>(-1, "-");

            
            }
            else //actulializar asignacion
            {
                asociacionVM.Mode = Mode.Edit;
                asociacionVM.strTipoAction = "Asignar";
                asociacionVM.strNombreVentana = "Actualizar Asignación";

                string strNombreCompletoFisioterapueta01;
                string strNombreCompletoFisioterapueta02;

                if (strFisioAsignadoUno != null && strFisioAsignadoUno != "")
                {
                    strNombreCompletoFisioterapueta01 = PacienteDL.ObtenerNombresCompletosFisioterapeutasAsignado(Convert.ToInt32(strFisioAsignadoUno));
                    if (strNombreCompletoFisioterapueta01.Equals(""))
                        asociacionVM.SelectedKeyUno = new KeyValuePair<int, string>(-1, "-");
                    asociacionVM.SelectedKeyUno = new KeyValuePair<int, string>(Convert.ToInt32(strFisioAsignadoUno), strNombreCompletoFisioterapueta01);

                }
                else
                {
                    asociacionVM.SelectedKeyUno = new KeyValuePair<int, string>(-1, "-");
                }


                if (strFisioAsignadoDos != null && strFisioAsignadoDos != "")
                {
                    strNombreCompletoFisioterapueta02 = PacienteDL.ObtenerNombresCompletosFisioterapeutasAsignado(Convert.ToInt32(strFisioAsignadoDos));
                    if (strNombreCompletoFisioterapueta02.Equals(""))
                        asociacionVM.SelectedKeyDos = new KeyValuePair<int, string>(-1, "-");
                    asociacionVM.SelectedKeyDos = new KeyValuePair<int, string>(Convert.ToInt32(strFisioAsignadoDos), strNombreCompletoFisioterapueta02);
                }
                else
                {
                    asociacionVM.SelectedKeyDos = new KeyValuePair<int, string>(-1, "-");
                }

            }
            asociacionVM.iCodigoPaciente = iCodigo;
            asociacionVM.strNombrePaciente = strNombres;
            asociacionVM.strApellidoPaciente = strApellidos;
            IModalDialog dialog = ServiceProvider.Instance7.Get<IModalDialog>();

            dialog.BindViewModel(asociacionVM);
            dialog.ShowDialog();



        }
    
        //private void VerPacienteDialog(object parameter)
        //{
        //    PacienteVerViewModel pacienteVerViewModel = new PacienteVerViewModel();

        //    pacienteVerViewModel.SetPacienteVer(GetPaciente());
        //    IModalDialog dialog = ServiceProvider.Instance4.Get<IModalDialog>();

        //    dialog.BindViewModel(pacienteVerViewModel);
        //    dialog.ShowDialog();
        //}

        //private void ActualizarPacienteDialog(object parameter)
        //{
        //    PacienteActualizarViewModel pacienteActualizarViewModel = new PacienteActualizarViewModel();
        //    pacienteActualizarViewModel.SetPacienteActualizar(GetPaciente());
        //    IModalDialog dialog = ServiceProvider.Instance6.Get<IModalDialog>();

        //    dialog.BindViewModel(pacienteActualizarViewModel);
        //    dialog.ShowDialog();
        //}

        //private void EliminarPacienteDialog(object parameter)
        //{
        //    if (MessageBoxResult.Yes == MessageBox.Show("¿Esta seguro que desea eliminar al paciente seleccionado?", "Advertencia", MessageBoxButton.YesNo))
        //        if (PacienteDL.BorrarPaciente(GetPaciente().iCodigoPaciente))
        //            MessageBox.Show("Se elimino al fisioterpapeuta " + GetPaciente().vNombresPaciente + ".");

        //    //ListarPacientesViewModel.Instance().refrescarListaFiosioterapeutas();
        //}

        public PacienteB GetPaciente()
        {
            PacienteB paciente = new PacienteB();

               paciente.iCodigoPaciente           =  iCodigo                         ;
               paciente.vNombresPaciente =  strNombres;
               paciente.vApellidosPaciente        =  strApellidos               ;
               paciente.cGeneroPaciente           =  Convert.ToString(chrGenero)                  ;
               paciente.cDNIPaciente              =  strDNI                     ;
               paciente.daFecNacPaciente          = DateTime.ParseExact(strFecNacimiento,"yyyy/MM/dd",null);
               //paciente.iCodigoFisioterapeutaUno  =  Convert.ToInt32(strFisioAsignadoUno)      ;
               //paciente.iCodigoFisioterapeutaDos  =  Convert.ToInt32(strFisioAsignadoDos)      ;
               paciente.vDiagnosticoMedicoPaciente=  strDiagnosticoMedico       ;
               paciente.iNivelPaciente            =  iNivel                     ;
               paciente.iPorcentajeNivelPaciente  =  iPorcentajeNivel           ;
               paciente.vNomApeMedNeuroReferencia =  strNomApeMedNeuroReferencia;
               paciente.vCelMedNeuroReferencia    =  strCelMedNeuroReferencia   ;
               paciente.vNombresApoderado         =  strNombresApod             ;
               paciente.vApellidoApoderado        =  strApellidosApod           ;
               paciente.vCelularApoderado         =  strCelularApod             ;
               paciente.vTelefonoApoderado        =  strTeléfonoApod            ;
               paciente.vParentescoApoderado      = strParentescoApod           ;
               paciente.vUrlFotoPaciente          = strUrlFotoPaciente          ;
               //paciente.iFlagBorrradoPaciente     =  iBorrrado                 ;
               paciente.iFlagFisioAsigPaciente    =  iFisioAsigPaciente        ;
            return paciente;

        }

        #endregion
    }
}
