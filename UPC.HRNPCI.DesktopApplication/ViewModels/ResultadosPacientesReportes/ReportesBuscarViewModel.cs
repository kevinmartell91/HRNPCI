using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using UPC.HRNPCI.DesktopApplication;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes;
using UPC.HRNPCI.Model.ResultadosReportesPaciente;
using UPC.HRNPCI.Model.PacienteModel;
using System.IO;


namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ReportesBuscarViewModel: BaseViewModel
    {
        ReportesBusinessObject businessObject = null;
       


        public RelayCommand BuscarReporteCommand { get; set; }

       
        private DateTime? _dtInicio;
        public DateTime? dtInicio { 
            get { 
                return _dtInicio; 
            } 
            set { 
                _dtInicio = value; 
                OnPropertyChanged("dtInicio");
            }
        }

        private DateTime? _dtFin;
        public DateTime? dtFin { get { return _dtFin; } set { _dtFin = value; OnPropertyChanged("dtFin"); } }

        private DateTime? _dtInicioDisplay;
        public DateTime? dtInicioDisplay { get { return _dtInicioDisplay; } set { _dtInicioDisplay = value; OnPropertyChanged("dtInicioDisplay"); } }


        #region Declaraciones Pacientes Combobox
       
        Dictionary<int, string> lstPacientes = new Dictionary<int, string>();
        public IDictionary<int, string> LstPacientes
        {
            get { return this.lstPacientes; }
        }

        private KeyValuePair<int, string>? selectedKeyUno = null;
        public KeyValuePair<int, string>? SelectedKeyUno
        {
            get { return this.selectedKeyUno; }
            set
            {
                this.selectedKeyUno = value;
                this.OnPropertyChanged("SelectedKeyUno");
                this.OnPropertyChanged("SelectedValueUno");
            }
        }
        public string SelectedValueUno
        {
            get
            {
                if (null == this.SelectedKeyUno)
                {
                    return string.Empty;
                }

                return this.lstPacientes[this.SelectedKeyUno.Value.Key];
            }
            set
            {
                this.lstPacientes[this.SelectedKeyUno.Value.Key] = value;
                this.OnPropertyChanged("SelectedValueUno");
            }
        }



        Dictionary<int, string> lstReportes = new Dictionary<int, string>();
        public IDictionary<int, string> LstReportes
        {
            get { return this.lstReportes; }
        }

        private KeyValuePair<int, string>? selectedKeyDos = null;
        public KeyValuePair<int, string>? SelectedKeyDos
        {
            get { return this.selectedKeyDos; }
            set
            {
                this.selectedKeyDos = value;
                this.OnPropertyChanged("SelectedKeyDos");
            }
        }
        public string SelectedValueDos
        {
            get
            {
                if (null == this.SelectedKeyDos)
                {
                    return string.Empty;
                }

                return this.lstPacientes[this.SelectedKeyDos.Value.Key];
            }
            set
            {
                this.lstPacientes[this.SelectedKeyDos.Value.Key] = value;
                this.OnPropertyChanged("SelectedValueDos");
            }
        }

        public void InicializarFisioterapeutaComboBox()
        {
            var collection = PacienteDL.ObtenerPacientes();
            lstPacientes.Add(-1, "-");
            foreach (var item in collection)
            {
                lstPacientes.Add(item.iCodigoPaciente, item.vNombresPaciente + " " + item.vApellidosPaciente);
            }


            lstReportes.Add(-1, "-");
            lstReportes.Add(1, "Reporte Completo");
            lstReportes.Add(2,"Reporte Cruzado");


            selectedKeyUno = new KeyValuePair<int, string>(-1, "-");
            selectedKeyDos = new KeyValuePair<int, string>(-1, "-");

        }
      
        #endregion


        public ReportesBuscarViewModel()
        {
            InicializarFisioterapeutaComboBox();
            BuscarReporteCommand = new RelayCommand(BuscarRepote);
            businessObject = new ReportesBusinessObject();
            dtInicioDisplay = new DateTime(2015, 5, 01);
           
        }


        private void BuscarRepote(object parameter)
        {
            int iCodigoPaciente = selectedKeyUno.Value.Key;
            int iCodigoReporte = selectedKeyDos.Value.Key;
            DateTime dt = new DateTime();
            if (dtInicio == null || dtFin == null)
            {
                //dtInicio = dt;
                //dtFin = dt;
                MessageBox.Show("Seleccione, al menos ,el rango de fechas para realizar un búsqueda");
                return;

            }
               
            ObservableCollection<ReportesCRViewModel> cltnBusqueReporte = businessObject.ListarBusquedaReporteCRViewModel(iCodigoPaciente, iCodigoReporte, (DateTime)dtInicio, (DateTime)dtFin);
            ListarResultadosReportesViewModel.instance.ActualizarListaReportesBusqueda(cltnBusqueReporte);

            //dt = new DateTime();

            //if (dtInicio == dt)
            //    dtInicio = null;

            //if (dtFin == dt)
            //    dtFin = null;

            //dtInicioDisplay = new DateTime(2015, 05, 01);


            //try
            //{
            //     ObservableCollection<ReportesCRViewModel> cltnBusqueReporte = businessObject.ListarBusquedaReporteCRViewModel(iCodigoPaciente, iCodigoReporte, (DateTime)dtInicio, (DateTime)dtFin);
            //ListarResultadosReportesViewModel.instance.ActualizarListaReportesBusqueda(cltnBusqueReporte);
            //}
            //catch (Exception)
            //{
                
            //     MessageBox.Show("Ingrese datos porfavor.");
            //}
        }
    }
}
