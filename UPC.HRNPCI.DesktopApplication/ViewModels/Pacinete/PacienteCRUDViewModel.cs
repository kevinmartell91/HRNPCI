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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete
{
    class PacienteCRUDViewModel
    {
        #region Declaraciones

        PacienteBusinessObject businessObject = null;

        public Button VerButton;
        public Button ActualizarButton;
        public Button EliminarButton;

        public RelayCommand VerPacienteCommand { get; set; }
        public RelayCommand ActualizarPacienteCommand { get; set; }
        public RelayCommand EliminarPacienteCommand { get; set; }

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
        public int      iFisioAsigPaciente          {get;set;}
        public string   strEdadCronologica          {get;set;}
        public string   strUrlFotoPaciente          {get;set;}

        public string strFisioAsignadoNombreCompletoUno { get; set; }
        public string strFisioAsignadoNombreCompletoDos { get; set; }

        #endregion


        #region Metodos

        public PacienteCRUDViewModel()
        {

            VerButton = new Button();
            //VerButton.Name = "Ver";
            VerButton.Command = VerPacienteCommand;

            Uri resourceUri = new Uri("Imagenes/Resourses/testImg.jpg", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;
            VerButton.Background = brush;


            ActualizarButton = new Button();
            ActualizarButton.Name = "Actualizar";
            ActualizarButton.Command = ActualizarPacienteCommand;

            EliminarButton = new Button();
            EliminarButton.Name = "Eliminar";
            EliminarButton.Command = EliminarPacienteCommand;


            VerPacienteCommand = new RelayCommand(VerPacienteDialog);
            ActualizarPacienteCommand = new RelayCommand(ActualizarPacienteDialog);
            EliminarPacienteCommand = new RelayCommand(EliminarPacienteDialog);

        }

        private void VerPacienteDialog(object parameter)
        {
            PacienteVerViewModel pacienteVerViewModel = new PacienteVerViewModel();

            pacienteVerViewModel.SetPacienteVer(GetPaciente());
            IModalDialog dialog = ServiceProvider.Instance4.Get<IModalDialog>();

            dialog.BindViewModel(pacienteVerViewModel);
            dialog.ShowDialog();
        }

        private void ActualizarPacienteDialog(object parameter)
        {
            PacienteActualizarViewModel pacienteActualizarViewModel = new PacienteActualizarViewModel();
            pacienteActualizarViewModel.SetPacienteActualizar(GetPaciente());
            IModalDialog dialog = ServiceProvider.Instance6.Get<IModalDialog>();

            dialog.BindViewModel(pacienteActualizarViewModel);
            dialog.ShowDialog();
        }

        private void EliminarPacienteDialog(object parameter)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("¿Esta seguro que desea eliminar al paciente " + strNombres + " " + strApellidos  + "?", "Advertencia", MessageBoxButton.YesNo))
                if (PacienteDL.BorrarPaciente(GetPaciente().iCodigoPaciente))
                {
                    businessObject = new PacienteBusinessObject();

                    ObservableCollection<PacienteCRUDViewModel> listaPacientesCRUD = ListarPacientesViewModel.Instance().ocltnPacientesCRUD;
                    if (listaPacientesCRUD != null)
                    {
                        for (int i = 0; i < listaPacientesCRUD.Count; i++)
                        {
                            if (listaPacientesCRUD[i].iCodigo == this.iCodigo)
                            {
                                ListarPacientesViewModel.Instance().ocltnPacientesCRUD.RemoveAt(i);
                                //ListarPacientesViewModel.Instance().ForzarListaRefresh();
                                MessageBox.Show("Se eliminó al fisioterpapeuta " + strNombres + " " + strApellidos + ".", "Advertencia");
                            }
                        }
                    }
                   
                }

            //ListarPacientesViewModel.Instance().refrescarListaFiosioterapeutas();
        }

        public Paciente GetPaciente()
        {
            Paciente paciente = new Paciente();

               paciente.iCodigoPaciente           =  iCodigo                         ;
               paciente.vNombresPaciente =  strNombres;
               paciente.vApellidosPaciente        =  strApellidos               ;
               paciente.cGeneroPaciente           =  chrGenero                  ;
               paciente.cDNIPaciente              =  strDNI                     ;
               //strFecNacimiento = Convert.ToDateTime(paciente.daFecNacPaciente).ToString("yyyy/MM/dd");
               paciente.daFecNacPaciente          =  DateTime.ParseExact(strFecNacimiento,"yyyy/MM/dd",null);
               if(strFisioAsignadoUno != "")
                 paciente.iCodigoFisioterapeutaUno  =  Convert.ToInt32(strFisioAsignadoUno)      ;
               if (strFisioAsignadoDos != "")
                 paciente.iCodigoFisioterapeutaDos  =  Convert.ToInt32(strFisioAsignadoDos)      ;
               paciente.vDiagnosticoMedicoPaciente=  strDiagnosticoMedico       ;
               paciente.iNivelPaciente            =  iNivel                     ;
               paciente.iPorcentajeNivelPaciente  =  iPorcentajeNivel           ;
               paciente.vNomApeMedNeuroReferencia =  strNomApeMedNeuroReferencia;
               paciente.vCelMedNeuroReferencia    =  strCelMedNeuroReferencia   ;
               paciente.vParentescoApoderado      = strParentescoApod           ;
               paciente.vNombresApoderado         =  strNombresApod             ;
               paciente.vApellidoApoderado        =  strApellidosApod           ;
               paciente.vCelularApoderado         =  strCelularApod             ;
               paciente.vTelefonoApoderado        =  strTeléfonoApod            ;
               paciente.vEdadCronologicaPaciente  = strEdadCronologica          ;
               paciente.iFlagBorrradoPaciente     =  iBorrrado                 ;
               paciente.iFlagFisioAsigPaciente    =  iFisioAsigPaciente        ;
               paciente.vUrlFotoPaciente          =  strUrlFotoPaciente;


            return paciente;

        }

        #endregion

    }
}
