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
using UPC.HRNPCI.Model.PacienteModel;
using UPC.HRNPCI.Model;
using System.Collections.Specialized;
using System.Windows.Threading;
using UPC.HRNPCI.DesktopApplication.ViewModels.AsociarPacienteFisioterapeuta;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete
{
    class ListarPacientesViewModel : BaseViewModel
    {
        #region Declaraciones de listar Paciente

        public static ListarPacientesViewModel instance = null;

        /// <summary>
        /// Para actuliar en forma manual la data en la vista
        /// </summary>
        DispatcherTimer Timer;

        PacienteBusinessObject businesobject;

        public ObservableCollection<PacienteCRUDViewModel> _ocltnPacientesCRUD = null;

        public ObservableCollection<PacienteCRUDViewModel> ocltnPacientesCRUD
        {
            get
            {
                return _ocltnPacientesCRUD;
            }
            set
            {
                _ocltnPacientesCRUD = value;
                OnPropertyChanged("ocltnPacientesCRUD");
            }
        }

        public PacienteCRUDViewModel _PacienteSeleccionado = null;

        public PacienteCRUDViewModel PacienteSeleccionado
        {
            get
            {
                return _PacienteSeleccionado;
            }
            set
            {
                _PacienteSeleccionado = value;
                OnPropertyChanged("PacienteSeleccionado");
            }
        }

        public RelayCommand AgreagarPacienteCommand { get; set; }
        public RelayCommand RefrescarComboxColumanasSeleccionadasCommand { get; set; }
        public RelayCommand BuscarCommand { get; set; }


        public bool _blnShowStatusColumn;
        public bool blnShowStatusColumn
        {
            get { return _blnShowStatusColumn; }
            set
            {
                _blnShowStatusColumn = value;
                OnPropertyChanged("blnShowStatusColumn");
            }
        }

        public string _strBuscarTexto;
        public string strBuscarTexto
        {
            get
            {
                return _strBuscarTexto;
            }
            set
            {
                _strBuscarTexto = value;
                OnPropertyChanged("strBuscarTexto");
                if (_strBuscarTexto.Trim().Equals(""))
                    PacienteStatic.blnTextBusquedaVacio = true;
                else
                    PacienteStatic.blnTextBusquedaVacio = false;

            }
        }

        public string _strMesajeResultadoBusqueda;
        public string strMesajeResultadoBusqueda
        {
            get
            {
                return _strMesajeResultadoBusqueda;
            }
            set
            {
                _strMesajeResultadoBusqueda = value;
                OnPropertyChanged("strMesajeResultadoBusqueda");
            }
        }

        #endregion


        #region Declaraciones de Combox Filtros

        public List<bool> lstBlnColumnasSeleccionadas { get; set; }

        private ObservableCollection<string> _ocltnItemsCampos;
        public ObservableCollection<string> ocltnItemsCampos
        {
            get { return _ocltnItemsCampos; }
            set
            {
                _ocltnItemsCampos = value;
                OnPropertyChanged("ocltnItemsCampos");
            }
        }

        public string _strCampoSeleccionado;
        public string strCampoSeleccionado
        {
            get { return _strCampoSeleccionado; }
            set
            {
                _strCampoSeleccionado = value;
                OnPropertyChanged("strCampoSeleccionado");
            }
        }

        #endregion

        #region Declaraciones MultiCombobox Columnas

        private Dictionary<string, object> _dicItems;

        public Dictionary<string, object> dicItems
        {
            get
            {
                return _dicItems;
            }
            set
            {
                _dicItems = value;

            }
        }

        private Dictionary<string, object> _dicSelectedItems;

        public Dictionary<string, object> dicSelectedItems
        {
            get
            {
                return _dicSelectedItems;
            }
            set
            {
                _dicSelectedItems = value;
            }
        }

        public void InicializarMultiComboBox()
        {
            dicItems = new Dictionary<string, object>();
            dicSelectedItems = new Dictionary<string, object>();

            lstBlnColumnasSeleccionadas = new List<bool>();
            _ocltnItemsCampos = new ObservableCollection<string>();

            PacienteColumnas pacienteColumnasDL = PacienteDL.ObtenerColumnasPaciente();
            PacienteStatic.PacienteColumnas = pacienteColumnasDL;

            for (int i = 0; i < pacienteColumnasDL.ocltnColumnasPaciente.Count; i++)
            {
                PacienteColumnas pacienteColumna = pacienteColumnasDL.ocltnColumnasPaciente[i];
                dicItems.Add(pacienteColumna.strNombreColumna, pacienteColumna.iIdColumna.ToString());
                
                //hacemos crecer esta lista a medida que vamos recorriendo paciente columna
                lstBlnColumnasSeleccionadas.Add(true);
                if (!pacienteColumna.blnVisible)
                    dicSelectedItems.Add(pacienteColumna.strNombreColumna, pacienteColumna.iIdColumna.ToString());

                lstBlnColumnasSeleccionadas[i] = pacienteColumna.blnVisible;
            }

            foreach (var item in _dicSelectedItems)
            {
                ocltnItemsCampos.Add(item.Key);
            }

            PacienteStatic.lstblnColumnasSeleccionadas = this.lstBlnColumnasSeleccionadas;
        }

        #endregion

        #region Fisoterapuetas Nombres

        public Dictionary<string, object> dicFisioterapeustas { get; set; }

        public void RefrescarFisioterapuestasNombres()
        {
            FisioterapeutaNombresCompletos collection = PacienteDL.ObtenerFisioterapeutasNombresCompletos();
            foreach (FisioterapeutaNombresCompletos item in collection.ocltnFisioterapeutasNombresCompletos)
            {
                dicFisioterapeustas.Add(item.iCodigo.ToString(), item.strNombres + " " + item.strApellidos);
            }
        }

        public void CargarNombresFisioterapeutas()
        {
            for (int i = 0; i < ocltnPacientesCRUD.Count; i++)
            {
                if (ocltnPacientesCRUD[i].strFisioAsignadoUno != null && ocltnPacientesCRUD[i].strFisioAsignadoUno != "")
                    ocltnPacientesCRUD[i].strFisioAsignadoNombreCompletoUno = dicFisioterapeustas[ocltnPacientesCRUD[i].strFisioAsignadoUno].ToString();
                if (ocltnPacientesCRUD[i].strFisioAsignadoDos != null && ocltnPacientesCRUD[i].strFisioAsignadoDos != "")
                    ocltnPacientesCRUD[i].strFisioAsignadoNombreCompletoDos = dicFisioterapeustas[ocltnPacientesCRUD[i].strFisioAsignadoDos].ToString();
            }
        }

        public void InicializarFisioterapetuasNombres()
        {
            dicFisioterapeustas = new Dictionary<string, object>();
            RefrescarFisioterapuestasNombres();
            CargarNombresFisioterapeutas();
        }

        #endregion

        #region Metodos

        public ListarPacientesViewModel()  //TODO Tengo dos constructores Ese serala respueta aque no pueda acceder al singleton
        {

            InicializarMultiComboBox();

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(1000);
            Timer.Tick += new EventHandler(RefreshUIListaFisioterapuetas);
            //Timer.Start();


            businesobject = new PacienteBusinessObject();
            ocltnPacientesCRUD = new ObservableCollection<PacienteCRUDViewModel>(businesobject.ObtenerPacientesCRUD());
            InicializarFisioterapetuasNombres();

            AgreagarPacienteCommand = new RelayCommand(AgregarPacienteDialog);
            RefrescarComboxColumanasSeleccionadasCommand = new RelayCommand(RefrescarComboxColumanasSeleccionadas);
            BuscarCommand = new RelayCommand(BuscarPorFiltro);

            strBuscarTexto = "";
            strMesajeResultadoBusqueda = "";

            // TEST
            PacienteStatic.blnNombre = blnShowStatusColumn;
            blnShowStatusColumn = false;



        }

        public void BuscarPorFiltro(object parameter)
        {

            ObservableCollection<PacienteCRUDViewModel> ocltnResultadosBusqueda;
            if (PacienteStatic.blnTextBusquedaVacio)
            {
                ocltnPacientesCRUD = new ObservableCollection<PacienteCRUDViewModel>(businesobject.ObtenerPacientesCRUD());
                if (ocltnPacientesCRUD.Count > 0)
                    strMesajeResultadoBusqueda = "";
               

            }
            else
            {
                int idColumnSeleccionado = PacienteStatic.PacienteColumnas.ObtenerIdColumna(strCampoSeleccionado);
                ocltnResultadosBusqueda = new ObservableCollection<PacienteCRUDViewModel>(businesobject.ObtenerPacientesBuscadosCRUD(idColumnSeleccionado, strBuscarTexto));
                if (ocltnResultadosBusqueda.Count > 0)
                    strMesajeResultadoBusqueda = "";
                else
                    strMesajeResultadoBusqueda = "No se han encontrado coincidencias.";

                ocltnPacientesCRUD = ocltnResultadosBusqueda;

            }
            InicializarFisioterapetuasNombres();

        }
        
        public void RefreshUIListaFisioterapuetas(object sender, EventArgs e)
        {
            //BuscarCommand.Execute(new Object());
            //RefrescarComboxColumanasSeleccionadas(new Object());

        }


        /// <summary>
        /// Verificar si el singleton que se instancia es el mismo para ser invocado desde otras clases
        /// Por el momento no funciona
        /// </summary>
        /// <returns></returns>
        public static ListarPacientesViewModel Instance()
        {
            if (instance == null)
                instance = new ListarPacientesViewModel();
            return instance;
        }

        private void AgregarPacienteDialog(object parameter)
        {
            if (PacienteStatic.kstrRutaFoto == "")
            {
                MessageBox.Show("Aún no ha determinado la ruta de almacenamiento de las fotos. Antes de realizar este proceso dirigase a la pestaña de Rutas de Almacenamiento", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            PacienteAgregarViewModel pacienteAgregarViewModel = new PacienteAgregarViewModel();
            IModalDialog dialog = ServiceProvider.Instance5.Get<IModalDialog>();

            dialog.BindViewModel(pacienteAgregarViewModel);
            dialog.ShowDialog();
        }

        public void RefrescarComboxColumanasSeleccionadas(object parameter)
        {
            //Limpiamos todo para refrescar las nuevas seleciones
            //BuscarTexto = "";
            ocltnItemsCampos.Clear();
            this.lstBlnColumnasSeleccionadas.Clear();
            foreach (var item in _dicItems)
            {
                lstBlnColumnasSeleccionadas.Add(true);
            }

            // seteamos los nuevos valores al ItemsCampos

            foreach (var selectedItem in _dicSelectedItems)
            {
                ocltnItemsCampos.Add(selectedItem.Key);
                lstBlnColumnasSeleccionadas[Convert.ToInt32((selectedItem.Value).ToString())] = false;
            }

            //Actualizamos la clase estatica para que communique a la vista
            if (!blnShowStatusColumn)
                blnShowStatusColumn = true;
            else
                blnShowStatusColumn = false;

            PacienteStatic.lstblnColumnasSeleccionadas = this.lstBlnColumnasSeleccionadas;



        }

         public void ForzarListaRefresh()
        {
            ocltnPacientesCRUD = new ObservableCollection<PacienteCRUDViewModel>(businesobject.ObtenerPacientesCRUD());
            InicializarFisioterapetuasNombres();
        }
        #endregion
    }
}
