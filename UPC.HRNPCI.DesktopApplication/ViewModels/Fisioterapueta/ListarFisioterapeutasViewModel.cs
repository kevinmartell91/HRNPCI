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


namespace UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta
{
    class ListarFisioterapeutasViewModel : BaseViewModel
    {
        public string ImagenVer { get; set; }

        #region Declaraciones de listar fisioterapeuta
        
        public static ListarFisioterapeutasViewModel instance = null;

        /// <summary>
        /// Para actuliar en forma manual la data en la vista
        /// </summary>
        DispatcherTimer Timer;

        FisioterapeutaBusinessObject businesobject;

        public ObservableCollection<FisioterapeutaCRUDViewModel> _listaFisioterapeutas = null;

        public ObservableCollection<FisioterapeutaCRUDViewModel> ListaFisioterapeutas
        {
            get
            {
                //return new ObservableCollection<FisioterapeutaCRUDViewModel>(businesobject.ObtenerFisioterapeutasCRUD());
                return _listaFisioterapeutas;
            }
            set
            {
                _listaFisioterapeutas = value;
                OnPropertyChanged("ListaFisioterapeutas");
            }
        }

        public FisioterapeutaCRUDViewModel _FisioterapeutaSeleccionado = null;

        public FisioterapeutaCRUDViewModel FisioterapeutaSeleccionado
        {
            get
            {
                return _FisioterapeutaSeleccionado;
            }
            set
            {
                _FisioterapeutaSeleccionado = value;
                OnPropertyChanged("FisioterapeutaSeleccionado");
            }
        }

        public RelayCommand AgreagarFiosioterapeutaCommand { get; set; }
        public RelayCommand RefrescarComboxColumanasSeleccionadasCommand { get; set; }
        public RelayCommand BuscarCommand { get; set; }


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

        #endregion


        #region Declaraciones de Combox Filtros

        public List<bool> ListacolumnasSeleccionadas { get; set; }

        private ObservableCollection<string> _itemsCampos;
        public ObservableCollection<string> ItemsCampos
        {
            get { return _itemsCampos; }
            set
            {
                _itemsCampos = value;
                OnPropertyChanged("ItemsCampos");
            }
        }

        public string _campoSeleccionado;
        public string CampoSeleccionado
        {
            get { return _campoSeleccionado; }
            set
            {
                _campoSeleccionado = value;
                OnPropertyChanged("CampoSeleccionado");
            }
        }

        #endregion


        #region Declaraciones MultiCombobox Columnas

        private Dictionary<string, object> _items;
        
        public Dictionary<string, object> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;

            }
        }

        private Dictionary<string, object> _selectedItems;

        public Dictionary<string, object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
            }
        }
        
        public void InicializarMultiComboBox()
        {
            Items = new Dictionary<string, object>();
            SelectedItems = new Dictionary<string, object>();

            ListacolumnasSeleccionadas = new List<bool>();
            _itemsCampos = new ObservableCollection<string>();

            FisioterapeutaColumnas columnas = FisioterapeutaDL.ObtenerColumnasFisioterapeuta();
            FisioterapeutaStatic.FisioterapuestasColumnas = columnas;

            for (int i = 0; i < columnas.ListaColumnasFisioterapeuta.Count; i++)
            {
                FisioterapeutaColumnas aux = columnas.ListaColumnasFisioterapeuta[i];
                Items.Add(aux.NombreColumna, aux.idColumna.ToString());

                ListacolumnasSeleccionadas.Add(true);
                if (i == 1 || i == 2 || i == 7 || i == 4 || i == 8 || i == 9)
                {
                    SelectedItems.Add(aux.NombreColumna, aux.idColumna.ToString());
                    ListacolumnasSeleccionadas[i] = false;
                }
               

            }

            foreach (var item in _selectedItems)
            { 
                ItemsCampos.Add(item.Key); 
            }
            
            FisioterapeutaStatic.ListaColumnasSeleccionadas = this.ListacolumnasSeleccionadas;

           


           
            //MC.ItemsSource = Items;
            //MC.SelectedItems = SelectedItems;
        }

        #endregion


        #region Metodos

        public ListarFisioterapeutasViewModel()
        {

            InicializarMultiComboBox();

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(1000);
            Timer.Tick += new EventHandler(RefreshUIListaFisioterapuetas);
            //Timer.Start();

            
            businesobject = new FisioterapeutaBusinessObject();
            ListaFisioterapeutas = new ObservableCollection<FisioterapeutaCRUDViewModel>(businesobject.ObtenerFisioterapeutasCRUD());
            //if (ListaFisioterapeutas.Count == 0)
            //    --MesajeResultadoBusqueda = "No tiene ningún registro de fisioteapaeutas, agregue uno por favor.";
            
            
            
            AgreagarFiosioterapeutaCommand = new RelayCommand(AgregarFisioterapeutaDialog);
            RefrescarComboxColumanasSeleccionadasCommand = new RelayCommand(RefrescarComboxColumanasSeleccionadas);
            BuscarCommand = new RelayCommand(BuscarPorFiltro);

            BuscarTexto = "";
            MesajeResultadoBusqueda = "";

  
            // TEST
            ShowStatusColumn = false;
            FisioterapeutaStatic.Nombre = ShowStatusColumn;


        }

        public void BuscarPorFiltro(object parameter)
        {
            if (FisioterapeutaStatic.TextBusquedaVacio)
            {
                ListaFisioterapeutas = new ObservableCollection<FisioterapeutaCRUDViewModel>(businesobject.ObtenerFisioterapeutasCRUD());
                if (ListaFisioterapeutas.Count > 0)
                    MesajeResultadoBusqueda = "";
            }
            else
            {
                int idColumnSeleccionado = FisioterapeutaStatic.FisioterapuestasColumnas.ObtenerIdColumna(CampoSeleccionado);
                ObservableCollection<FisioterapeutaCRUDViewModel> resultadosBusqueda = new ObservableCollection<FisioterapeutaCRUDViewModel>(businesobject.ObtenerFisioterapeutasBuscadosCRUD(idColumnSeleccionado, BuscarTexto));
                if (resultadosBusqueda.Count > 0)
                    MesajeResultadoBusqueda = "";
                else
                    MesajeResultadoBusqueda = "No se han encontrado coincidencias.";
                
                ListaFisioterapeutas = resultadosBusqueda;
                
            }
                
        }
        
        public void RefreshUIListaFisioterapuetas(object sender, EventArgs e)
        {
            //BuscarCommand.Execute(new Object());
            //RefrescarComboxColumanasSeleccionadas(new Object());

        }
     
        public static ListarFisioterapeutasViewModel Instance()
        {
            if (instance == null)
                instance = new ListarFisioterapeutasViewModel();
            return instance;
        }

        private void AgregarFisioterapeutaDialog(object parameter)
        {
            if (FisioterapeutaStatic.kstrRutaFoto == "")
            {
                MessageBox.Show("Aún no ha determinado la ruta de almacenamiento de las fotos. Antes de realizar este proceso dirigase a la pestaña de Rutas de Almacenamiento", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            FisioterapeutaAgregarViewModel fvm = new FisioterapeutaAgregarViewModel();
            IModalDialog dialog = ServiceProvider.Instance2.Get<IModalDialog>();

            dialog.BindViewModel(fvm);
            dialog.ShowDialog();
        }

        public void RefrescarComboxColumanasSeleccionadas(object parameter)
        {
            //Limpiamos todo para refrescar las nuevas seleciones
            //BuscarTexto = "";
            ItemsCampos.Clear();
            this.ListacolumnasSeleccionadas.Clear();
            foreach (var col in _items)
            {
                ListacolumnasSeleccionadas.Add(true);
            }

            // seteamos los nuevos valores al ItemsCampos

            foreach (var item in _selectedItems)
            {
                ItemsCampos.Add(item.Key);
                ListacolumnasSeleccionadas[Convert.ToInt32((item.Value).ToString())] = false;
                //BuscarTexto += item.Key;  
            }

            //Actualizamos la clase estatica para que communique a la vista
            if (!ShowStatusColumn)
                ShowStatusColumn = true;
            else
                ShowStatusColumn = false;

            FisioterapeutaStatic.ListaColumnasSeleccionadas = this.ListacolumnasSeleccionadas;



        }

        public void ForzarListaRefresh()
        {
            ListaFisioterapeutas = new ObservableCollection<FisioterapeutaCRUDViewModel>(businesobject.ObtenerFisioterapeutasCRUD());

        }

        #endregion


       
    }
}



