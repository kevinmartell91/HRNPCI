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
using UPC.HRNPCI.Model.FisioterapeutaModel;


namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ResultadosBuscarViewModel : BaseViewModel
    {
        ResultadosBusinessObject businessObject = null;

        public RelayCommand BuscarResultadosCommand { get; set; }


        private DateTime? _dtInicio { get; set; }
        private DateTime? _dtFin { get; set; }

        public DateTime? dtInicio { get { return _dtInicio; } set { _dtInicio = value; OnPropertyChanged("dtInicio"); } }
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


        Dictionary<int, string> lstUnidades = new Dictionary<int, string>();
        public IDictionary<int, string> LstUnidades
        {
            get { return this.lstUnidades; }
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

                return this.lstUnidades[this.SelectedKeyDos.Value.Key];
            }
            set
            {
                this.lstUnidades[this.SelectedKeyDos.Value.Key] = value;
                this.OnPropertyChanged("SelectedValueDos");
            }
        }


        Dictionary<int, string> lstLateralidad = new Dictionary<int, string>();
        public IDictionary<int, string> LstLateralidad
        {
            get { return this.lstLateralidad; }
        }

        private KeyValuePair<int, string>? selectedKeyTres = null;
        public KeyValuePair<int, string>? SelectedKeyTres
        {
            get { return this.selectedKeyTres; }
            set
            {
                this.selectedKeyTres = value;
                this.OnPropertyChanged("selectedKeyTres");
            }
        }
        public string SelectedValueTres
        {
            get
            {
                if (null == this.SelectedKeyTres)
                {
                    return string.Empty;
                }

                return this.lstLateralidad[this.SelectedKeyTres.Value.Key];
            }
            set
            {
                this.lstLateralidad[this.SelectedKeyTres.Value.Key] = value;
                this.OnPropertyChanged("SelectedValueTres");
            }
        }




        public void InicializarResultadoBusquedaComboBox()
        {
            var collection = PacienteDL.ObtenerPacientes();
            lstPacientes.Add(-1, "-");
            foreach (var item in collection)
            {
                lstPacientes.Add(item.iCodigoPaciente, item.vNombresPaciente + " " + item.vApellidosPaciente);
            }

            var unidades = FisioterapeutaDL.ObtenerListaUnidades();
            lstUnidades.Add(-1, "-");
            foreach (var item in unidades)
            {
                lstUnidades.Add(item.iCodigoUnidad, item.vNombre);
            }

            var lateralidades = FisioterapeutaDL.ObtenerListaLateralidades();
            lstLateralidad.Add(-1, "-");
            foreach (var item in lateralidades)
            {
                lstLateralidad.Add(item.iCodigoLateralidad, item.vNombre);
            }


            selectedKeyUno = new KeyValuePair<int, string>(-1, "-");
            selectedKeyDos = new KeyValuePair<int, string>(-1, "-");
            selectedKeyTres = new KeyValuePair<int, string>(-1, "-");

        }
      
        #endregion


        public ResultadosBuscarViewModel()
        {
            InicializarResultadoBusquedaComboBox();
            BuscarResultadosCommand = new RelayCommand(BuscarRepote);
            businessObject = new ResultadosBusinessObject();
            dtInicioDisplay = new DateTime(2015, 5, 01);
        }


        private void BuscarRepote(object parameter)
        {
            int iCodigoPaciente = selectedKeyUno.Value.Key;
            int iCodigoUnidad = selectedKeyDos.Value.Key;
            int iCodigoLateralidad = selectedKeyTres.Value.Key;

            DateTime dt = new DateTime();
            if (dtInicio == null || dtFin == null)
            {
                //dtInicio = dt;
                //dtFin = dt;
                MessageBox.Show("Seleccione, al menos ,el rango de fechas para realizar un búsqueda.");
                return;

            }
            ObservableCollection<ResultadosRViewModel> cltnBusqueResultado = businessObject.ListarBusquedaResultadosR(iCodigoPaciente, iCodigoUnidad, iCodigoLateralidad, (DateTime)dtInicio, (DateTime)dtFin);
            ListarResultadosReportesViewModel.instance.ActualizarListaResultadosBusqueda(cltnBusqueResultado);
            //dt = new DateTime();

            //if (dtInicio == dt)
            //    dtInicio = null;

            //if (dtFin == dt)
            //    dtFin = null;

            //dtInicio = DateTime.Now;
            //dtInicioDisplay = new DateTime(2015, 05, 01);

            //try
            //{
            //ObservableCollection<ResultadosRViewModel> cltnBusqueResultado = businessObject.ListarBusquedaResultadosR(iCodigoPaciente, iCodigoUnidad, iCodigoLateralidad, (DateTime)dtInicio, (DateTime)dtFin);
            //ListarResultadosReportesViewModel.instance.ActualizarListaResultadosBusqueda(cltnBusqueResultado);
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Ingrese datos porfavor.");
            //}
        }
    }
}
