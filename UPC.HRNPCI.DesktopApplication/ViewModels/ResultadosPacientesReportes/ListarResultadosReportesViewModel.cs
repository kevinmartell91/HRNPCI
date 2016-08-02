using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

using UPC.HRNPCI.DesktopApplication;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;
using UPC.HRNPCI.Model.FisioterapeutaModel;
using UPC.HRNPCI.Model;
using System.Collections.Specialized;
using System.Windows.Threading;

using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;
using System.Windows.Forms;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ListarResultadosReportesViewModel : BaseViewModel
    {

        #region Declaraciones de listar fisioterapeuta

        public static ListarResultadosReportesViewModel instance = null;

        /// <summary>
        /// Para actuliar en forma manual la data en la vista
        /// </summary>
        DispatcherTimer Timer;

        ResultadosBusinessObject businesobjectResultado;
        ReportesBusinessObject businesobjectReporte;

        public ObservableCollection<ResultadosRViewModel> _listaResultadosPacientes = null;

        public ObservableCollection<ResultadosRViewModel> ListaResultadosPacientes
        {
            get
            {
                //return new ObservableCollection<FisioterapeutaCRUDViewModel>(businesobject.ObtenerFisioterapeutasCRUD());
                return _listaResultadosPacientes;
            }
            set
            {
                _listaResultadosPacientes = value;
                OnPropertyChanged("ListaResultadosPacientes");
            }
        }

        public FisioterapeutaCRUDViewModel _ResultadosPacienteSeleccionado = null;

        public FisioterapeutaCRUDViewModel ResultadosPacienteSeleccionado
        {
            get
            {
                return _ResultadosPacienteSeleccionado;
            }
            set
            {
                _ResultadosPacienteSeleccionado = value;
                OnPropertyChanged("ResultadosPacienteSeleccionado");
            }
        }


        public ObservableCollection<ReportesCRViewModel> _listaReportesPacientes = null;

        public ObservableCollection<ReportesCRViewModel> ListaReportesPacientes
        {
            get
            {
                //return new ObservableCollection<FisioterapeutaCRUDViewModel>(businesobject.ObtenerFisioterapeutasCRUD());
                return _listaReportesPacientes;
            }
            set
            {
                _listaReportesPacientes = value;
                OnPropertyChanged("ListaReportesPacientes");
            }
        }

        public FisioterapeutaCRUDViewModel _reportePacienteSeleccionado = null;

        public FisioterapeutaCRUDViewModel ReportePacienteSeleccionado
        {
            get
            {
                return _reportePacienteSeleccionado;
            }
            set
            {
                _reportePacienteSeleccionado = value;
                OnPropertyChanged("ReportePacienteSeleccionado");
            }
        }

        List<ResultadosRViewModel> lstCheckedResultados = null;

        public RelayCommand BuscarResultadosCommand { get; set; }
        public RelayCommand BuscarReportesCommand { get; set; }
        public RelayCommand GenerarReporteCommand { get; set; }


        public bool _ShowStatusColumn;
        public bool ShowStatusColumn
        {
            get { return _ShowStatusColumn; }
            set
            {
                _ShowStatusColumn = value;
                OnPropertyChanged("ShowStatusColumn");
            }
        }

        public string _BuscarTexto;
        public string BuscarTexto
        {
            get
            {
                return _BuscarTexto;
            }
            set
            {
                _BuscarTexto = value;
                OnPropertyChanged("BuscarTexto");
                if (_BuscarTexto.Trim().Equals(""))
                    FisioterapeutaStatic.TextBusquedaVacio = true;
                else
                    FisioterapeutaStatic.TextBusquedaVacio = false;

            }
        }

        public string _MesajeResultadoBusqueda;
        public string MesajeResultadoBusqueda
        {
            get
            {
                return _MesajeResultadoBusqueda;
            }
            set
            {
                _MesajeResultadoBusqueda = value;
                OnPropertyChanged("MesajeResultadoBusqueda");
            }
        }

        public string _MesajeReporteBusqueda;
        public string MesajeReporteBusqueda
        {
            get
            {
                return _MesajeReporteBusqueda;
            }
            set
            {
                _MesajeReporteBusqueda = value;
                OnPropertyChanged("MesajeReporteBusqueda");
            }
        }

        #endregion


        #region Combo Opciones masivas resultados

        private List<string> _lstOpcionesResultados;
        public List<string> lstOpcionesResultados
        {
            get { return _lstOpcionesResultados; }
            set
            {
                _lstOpcionesResultados = value;
                OnPropertyChanged("lstOpcionesResultados");
            }
        }

        public int _iOpcionResultadoSeleccionado;
        public int iOpcionResultadoSeleccionado
        {
            get { return _iOpcionResultadoSeleccionado; }
            set
            {
                _iOpcionResultadoSeleccionado = value;
                OnPropertyChanged("iOpcionResultadoSeleccionado");

                ////-------------------------------------------------------------------
                if (lstCheckedResultados == null)
                   lstCheckedResultados = new List<ResultadosRViewModel>();
                else
                    lstCheckedResultados.Clear();

                if (_iOpcionResultadoSeleccionado == 1)
                {
                    ListaResultadosPacientes = businesobjectResultado.ListarResultadosRViewModel();
                    ActualizarMensajeBusqueda();
                }
                foreach (var item in ListaResultadosPacientes)
                {
                    
                    if (_iOpcionResultadoSeleccionado == 4)
                    {
                        item.IsChecked = true;
                        item.cbxSeleccion.IsChecked = true;
                        lstCheckedResultados.Add(item);
                    }
                    if (_iOpcionResultadoSeleccionado == 5)
                    {
                        item.IsChecked = false;
                        item.cbxSeleccion.IsChecked = false;
                    }
                }

            }


        }

        

        public void InicializarComboOpcionesResultados()
        {
            _lstOpcionesResultados = new List<string>();
            _lstOpcionesResultados.Add("Seleccion una opción");
            _lstOpcionesResultados.Add("Listar resultados");
            _lstOpcionesResultados.Add("Informe cruzado");
            _lstOpcionesResultados.Add("Informe completo");
            _lstOpcionesResultados.Add("Seleccionar todo");
            _lstOpcionesResultados.Add("Deselecccionar todo");
            _iOpcionResultadoSeleccionado = 0;
        }

        #endregion


        #region Combo Opciones masivas reportes

        private List<string> _lstOpcionesReportes;
        public List<string> lstOpcionesReportes
        {
            get { return _lstOpcionesReportes; }
            set
            {
                _lstOpcionesReportes = value;
                OnPropertyChanged("lstOpcionesReportes");
            }
        }

        public int _iOpcionReportesSeleccionado;
        public int iOpcionReporteSeleccionado
        {
            get { return _iOpcionReportesSeleccionado; }
            set
            {
                _iOpcionReportesSeleccionado = value;
                OnPropertyChanged("iOpcionReporteSeleccionado");
                ////-------------------------------------------------------------------


                if (_iOpcionReportesSeleccionado == 1)
                {
                    //Listo todos
                    ForzarListaReportesRefresh();
                    ActualizarMensajeBusqueda();
                }

                //if (_iOpcionReportesSeleccionado == 2)
                //{
                //    //Exportar pdf masivamente los seleccionados
                //    ExportarReportesMasivoPDF();
                //}

                foreach (var item in ListaReportesPacientes)
                {
                    
                   
                    if (_iOpcionReportesSeleccionado == 2)
                    {
                        item.IsChecked = true;
                        item.cbxSeleccion.IsChecked = true;
                    }

                    if (_iOpcionReportesSeleccionado == 3)
                    {
                        item.IsChecked = false;
                        item.cbxSeleccion.IsChecked = false;
                    }

                }
            }
        }

       
        public void InicializarComboOpcionesReportes()
        {
            _lstOpcionesReportes = new List<string>();
            _lstOpcionesReportes.Add("Seleccione una opción");
            _lstOpcionesReportes.Add("Listar todos");
            //_lstOpcionesReportes.Add("Exportar a PDF");
            _lstOpcionesReportes.Add("Seleccionar todo");
            _lstOpcionesReportes.Add("Deselecccionar todo");
            _iOpcionReportesSeleccionado = 0;
        }

        #endregion
     

        #region Metodos

        public ListarResultadosReportesViewModel()
        {

            InicializarComboOpcionesReportes();
            InicializarComboOpcionesResultados();


            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(1000);
            Timer.Tick += new EventHandler(RefreshUIListaFisioterapuetas);
            //Timer.Start();


            businesobjectResultado = new ResultadosBusinessObject();
            ListaResultadosPacientes = businesobjectResultado.ListarResultadosRViewModel();
            businesobjectReporte = new ReportesBusinessObject();
            ListaReportesPacientes = businesobjectReporte.ListarReportesCRViewModel();
            //if (ListaFisioterapeutas.Count == 0)
            //    --MesajeResultadoBusqueda = "No tiene ningún registro de fisioteapaeutas, agregue uno por favor.";

            ResultadosReportesStatic.ocltnResultadosPacientes = ListaResultadosPacientes;


            BuscarResultadosCommand = new RelayCommand(BuscarResultadosDialog);
            BuscarReportesCommand = new RelayCommand(BuscarReportesDialog);
            GenerarReporteCommand = new RelayCommand(GenerarReporteAction);

            BuscarTexto = "";
            MesajeReporteBusqueda = "";
            MesajeResultadoBusqueda = "";


            // TEST
            ShowStatusColumn = false;
            FisioterapeutaStatic.Nombre = ShowStatusColumn;


        }

        public void RefreshUIListaFisioterapuetas(object sender, EventArgs e)
        {
            //BuscarCommand.Execute(new Object());
            //RefrescarComboxColumanasSeleccionadas(new Object());

        }

        public static ListarResultadosReportesViewModel Instance()
        {
            if (instance == null)
                instance = new ListarResultadosReportesViewModel();
            return instance;
        }

        public void BuscarResultadosDialog(object parameter)
        {
            ResultadosBuscarViewModel fvm = new ResultadosBuscarViewModel();
            IModalDialog dialog = ServiceProvider.Instance11.Get<IModalDialog>();

            dialog.BindViewModel(fvm);
            dialog.ShowDialog();
        }

        public void BuscarReportesDialog(object parameter)
        {

            ReportesBuscarViewModel fvm = new ReportesBuscarViewModel();
            IModalDialog dialog = ServiceProvider.Instance12.Get<IModalDialog>();

            dialog.BindViewModel(fvm);
            dialog.ShowDialog();


        }

        public void GenerarReporteAction(object parameter)
        {
            if (_iOpcionResultadoSeleccionado == 0)
            {
                System.Windows.Forms.MessageBox.Show("No ha seleccionado ninguna opción. Seleccione una por favor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (lstCheckedResultados != null)
                {
                    AgruparPacientesSeleccionados();
                    if (blnResultdosMismoPaciente())//Validar que sea de un mismo paciente)
                    {
                        switch (_iOpcionResultadoSeleccionado)
                        {
                            case 2:
                                //reporte cruzado => izquiedo  + derecho en una sola grafica en un PDF => solo de dos selecionados (pies distintos)
                                GenrerarReporteCruzado();
                                iOpcionResultadoSeleccionado = 0;
                                break;
                            case 3:
                                //reporte completo => izquiedo  + derecho en dos graficas respectivamente en un PDF => solo dos selccionados (pies distintos)
                                GenerarReporteCompleto();
                                iOpcionResultadoSeleccionado = 0;
                                break;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Los resultados seleccionados no corresponden a un mismo paciente, por favor verificar e intentar nuevamente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No ha seleccionado ningún resultado de paciente, para generar algún tipo de reporte es necesario que seleccione dos resultados de un mismo paciente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }

        }

        private void AgruparPacientesSeleccionados()
        {
            foreach (var item in ListaResultadosPacientes)
            {
                if (_iOpcionResultadoSeleccionado == 2 ||
                        _iOpcionResultadoSeleccionado == 3)
                {
                    if (item.IsChecked)
                        lstCheckedResultados.Add(item);
                }
            }
        }
        private void ExportarReportesMasivoPDF()
        {
            try
            {

                // for % export per each
                System.Windows.Forms.MessageBox.Show("Se exportaron en formato PDF los reportes seleccionados.");
            }
            catch (Exception)
            {
                
                throw;
            }
        }
     
        private void GenrerarReporteCruzado()
        {
            if (blnEsReporteCruzadoRodilla())
            {
                if (GuardarReporte())
                {
                    System.Windows.Forms.MessageBox.Show("Se generó exitosamente el informe cruzado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.None);
                    ForzarListaReportesRefresh();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Error a conexión de base de datos. Comuníquese con el administrador del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Para generar el informe cruzado es necesario seleccionar dos resultado de una misma rodilla : izquierdo o derecho de un mismo paciente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void GenerarReporteCompleto()
        {

            if (blnEsReporteCompletoRodilla())
            {
                if (GuardarReporte())
                {
                    System.Windows.Forms.MessageBox.Show("Se generó exitosamente el informe completo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.None);
                    ForzarListaReportesRefresh();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Error a conexión de base de datos. Comuníquese con el administrador del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Para generar el informe completo es necesario seleccionar dos resultado de rodillas distintas: izquierdo y derecho de un mismo paciente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private bool blnEsReporteCruzadoRodilla()
        {
            if (lstCheckedResultados.Count == 2)
            {
                return (lstCheckedResultados[0].iCodigoUnidad == lstCheckedResultados[1].iCodigoUnidad && lstCheckedResultados[0].iCodigoLateralidad == lstCheckedResultados[1].iCodigoLateralidad) ? true : false;
            }
            return false;
        }
    
        private bool GuardarReporte()
        {
            int codTemAyuda = _iOpcionResultadoSeleccionado;
            if (_iOpcionResultadoSeleccionado == 3)
                codTemAyuda = 1;

            string dt1 = DateTime.Now.ToShortTimeString();
            string dt2 = DateTime.Now.ToShortDateString();
            string dt3 = DateTime.Now.ToLongTimeString();
            string dt4 = DateTime.Now.ToLongDateString();
            DateTime dt5 = DateTime.Now.ToLocalTime();
            double dt6 = DateTime.Now.ToOADate();

            if (businesobjectReporte.GuardarReporte(codTemAyuda,
                                                 lstCheckedResultados[0].iCodigoPaciente,
                                                 dt5,
                                                 lstCheckedResultados[0].iCodigoResultado,
                                                 lstCheckedResultados[1].iCodigoResultado,
                                                 -1, -1, -1, -1, -1, -1))
                return true;
            else
                return false;

        }

        private bool blnEsReporteCompletoRodilla()
        {
            if (lstCheckedResultados.Count == 2)
            {
                return (lstCheckedResultados[0].iCodigoUnidad == lstCheckedResultados[1].iCodigoUnidad && lstCheckedResultados[0].iCodigoLateralidad != lstCheckedResultados[1].iCodigoLateralidad) ? true : false;
            }
            return false;
        }

        private bool blnResultdosMismoPaciente()
        {
            int iCodigoPaciente = 0;
            bool blnSelected = false;
            foreach (var item in ListaResultadosPacientes)
            {
                if (item.isChecked == true)
                {
                    if (!blnSelected)
                    {
                        iCodigoPaciente = item.iCodigoPaciente;
                        blnSelected = true;
                    }
                    else 
                    {
                        if (iCodigoPaciente != item.iCodigoPaciente)
                            return false;
                    }
                }
            }
            return true;
        }
       
        public void ForzarListaResultasdosRefresh()
        {
            ListaResultadosPacientes = businesobjectResultado.ListarResultadosRViewModel();
            ActualizarMensajeBusqueda();
        }

        public void ForzarListaReportesRefresh()
        {
            ListaReportesPacientes = businesobjectReporte.ListarReportesCRViewModel();
            ActualizarMensajeBusqueda();
        }

        public void ActualizarListaReportesBusqueda(ObservableCollection<ReportesCRViewModel> cltnBusqueReporte)
        {
            ListaReportesPacientes = cltnBusqueReporte;
            ActualizarMensajeBusqueda();
        }
     
        public void ActualizarListaResultadosBusqueda(ObservableCollection<ResultadosRViewModel> cltnBusqueResultado)
        {
            ListaResultadosPacientes = cltnBusqueResultado;
            ActualizarMensajeBusqueda();
        }

        public void ActualizarMensajeBusqueda()
        {
            if (ListaReportesPacientes.Count == 0)
                MesajeReporteBusqueda = "No se encontraron reportes de búsqueda.";
            else
                MesajeReporteBusqueda = "";

            if (ListaResultadosPacientes.Count == 0)
                MesajeResultadoBusqueda = "No se encontraron resultados de búsqueda.";
            else
                MesajeResultadoBusqueda = "";

        }

        #endregion
    }
}
