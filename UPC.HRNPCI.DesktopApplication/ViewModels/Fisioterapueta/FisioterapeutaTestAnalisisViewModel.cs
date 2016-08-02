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
using UPC.HRNPCI.Model.ResultadosReportesPaciente;
using UPC.HRNPCI.Model;
using System.Collections.Specialized;
using System.Windows.Threading;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.Util;
using Emgu.CV.Structure;
//using Emgu.CV.Cvb;
using Microsoft.Win32;
using System.Windows.Forms;
using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta
{
    class FisioterapeutaTestAnalisisViewModel : BaseViewModel
    {
        #region Declaraciones

        public static FisioterapeutaTestAnalisisViewModel instance = null;

        public static FisioterapeutaB FisioterapeutaLoguedo = null;


        ImageSource imageSource;
        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        private string _strNombresPaciente;
        public string strNombresPaciente { get { return _strNombresPaciente; } set { _strNombresPaciente = value; OnPropertyChanged("strNombresPaciente"); } }

        private string _strNombreFisioterapeuta;
        public string strNombreFisioterapeuta { get { return _strNombreFisioterapeuta; } set { _strNombreFisioterapeuta = value; OnPropertyChanged("strNombreFisioterapeuta"); } }


        public RelayCommand CapturarGraficaCommand { get; set; }
        public RelayCommand GuardarGraficaCommand { get; set; }
        public RelayCommand CargarVideoCommand { get; set; }

        DispatcherTimer My_Time = new DispatcherTimer();
        int FPS = 30;
        Capture _capture;
        int count = 0;

        #endregion


        #region Combo pacientes
        Dictionary<int, string> lstPacientes = new Dictionary<int, string>();
        public IDictionary<int, string> LstPacientes
        {
            get { return this.lstPacientes; }
        }

        public KeyValuePair<int, string>? selectedKeyUno = null;
        public KeyValuePair<int, string>? SelectedKeyUno
        {
            get { return this.selectedKeyUno; }
            set
            {
                this.selectedKeyUno = value;
                this.OnPropertyChanged("SelectedKeyUno");
                this.OnPropertyChanged("SelectedValueUno");
                strNombresPaciente = lstPacientes[this.SelectedKeyUno.Value.Key];
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

        public void InicializarComboPacientes()
        {
            //FisioterapeutaLogueado
            FisioterapeutaLoguedo = FisioterapeutaStatic.FisioterapeutaLogueado;

            var collection = FisioterapeutaDL.ObtenerPacientesDeFisioterapeuta(FisioterapeutaLoguedo.iCodigoFisioterapeuta);
            foreach (var item in collection)
            {
                lstPacientes.Add(item.iCodigoPaciente, item.strNombresApellidos);
            }

        }

        #endregion

        #region combo Lateralidad

        Dictionary<int, string> lstUnidades = new Dictionary<int, string>();
        public IDictionary<int, string> LstUnidades
        {
            get { return this.lstUnidades; }
        }

        public KeyValuePair<int, string>? selectedKeyDos = null;
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

        public void InicializarComboLateralidad()
        {
            var lateralidades = FisioterapeutaDL.ObtenerListaLateralidades();
            foreach (var item in lateralidades)
            {
                lstLateralidad.Add(item.iCodigoLateralidad, item.vNombre);
            }

        }

        #endregion

        #region Combo Unidad

        Dictionary<int, string> lstLateralidad = new Dictionary<int, string>();
        public IDictionary<int, string> LstLateralidad
        {
            get { return this.lstLateralidad; }
        }

        public KeyValuePair<int, string>? selectedKeyTres = null;
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

        public void InicializarComboUnidad()
        {
            var unidades = FisioterapeutaDL.ObtenerListaUnidades();
            foreach (var item in unidades)
            {
                lstUnidades.Add(item.iCodigoUnidad, item.vNombre);
            }
        }

        #endregion

        #region Metodos

        public static FisioterapeutaTestAnalisisViewModel Instance()
        {
            if (instance == null)
                instance = new FisioterapeutaTestAnalisisViewModel();
            return instance;
        }
       
        public FisioterapeutaTestAnalisisViewModel()
        {


            //Commands
            CapturarGraficaCommand = new RelayCommand(CapturarGrafica);
            GuardarGraficaCommand = new RelayCommand(GuardarGrafica);
            CargarVideoCommand = new RelayCommand(CargarVideo);

            //Frame Rate
            My_Time.Interval = TimeSpan.FromMilliseconds(500);
            My_Time.Tick += new EventHandler(My_Timer_Tick);
            My_Time.IsEnabled = true;
            //My_Time.Start();

            //_capture = new Capture("recordings.avi");   // Error this line
            //_capture = new Capture(); 

            ForzarUpdate();

       //strNombreFisioterapeuta = FisioterapeutaLoguedo.vNombresFisioterapeuta + " " + FisioterapeutaLoguedo.vApellidosFisioterapeuta;

        }

        private void CargarVideo(object obj)
        {
            //// Displays an OpenFileDialog so the user can select a Cursor.
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.Filter = "Avi Files|*.avi";
            //openFileDialog1.Title = "Select a Cursor File";

            //// Show the Dialog.
            //// If the user clicked OK in the dialog and
            //// a .CUR file was selected, open it.
            //if (openFileDialog1.ShowDialog() == true)
            //{
            //    // Assign the cursor in the Stream to the Form's Cursor property.
            //    //this.Cursor = new Cursor(openFileDialog1.OpenFile());
            //    _capture = new Capture(openFileDialog1.FileName);   // Error this line
            //    My_Time.IsEnabled = true;


            //}
        }

        private void GuardarGrafica(object obj)
        {

            if (SelectedKeyUno == null)
            {
                System.Windows.Forms.MessageBox.Show("No ha seleccionado al paciente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else FisioterapeutaStatic.blnPaciente = true;

            if (SelectedKeyDos == null)
            {
                System.Windows.Forms.MessageBox.Show("No ha seleccionado la unidad.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else FisioterapeutaStatic.blnUnidad = true;

            if (SelectedKeyTres == null)
            {
                System.Windows.Forms.MessageBox.Show("No ha seleccionado la lateralidad.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else FisioterapeutaStatic.blnLateralidad = true;

            FisioterapeutaStatic.blnGuardar = true;


            List<double> angulos = FisioterapeutaStatic.getAngles(1);

            if ( angulos.Count > 2)
            {
                string strKneeAngles = DoubleToStringAngles(angulos);

                if (1 == ResultadoDL.GuardarResultado(SelectedKeyUno.Value.Key, 1, SelectedKeyTres.Value.Key, SelectedKeyDos.Value.Key, strKneeAngles, DateTime.Now))
                {
                    System.Windows.Forms.MessageBox.Show("Se guardó el reporte exitosamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarResultadosReportesViewModel.Instance().ForzarListaResultasdosRefresh();
                    FisioterapeutaStatic.blnGuardar = false;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Error de conxión a base de datos. Comuníquese con el administrador del sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No ha capturado los datos cuantitativos de los pacientes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 
            }
            
        }

        private string DoubleToStringAngles(List<double> collection)
        {
            StringBuilder strBuiAngels = null;

            if (collection.Count != 0)
            {
                strBuiAngels = new StringBuilder();
                foreach (var item in collection)
                {
                    strBuiAngels.Append(item.ToString());
                    strBuiAngels.Append(",");
                }
                return strBuiAngels.ToString();
            }
            else
            {
                return "";
            }
        }

        private void CapturarGrafica(object obj)
        {
            throw new NotImplementedException();
        }

        private void My_Timer_Tick(object sender, EventArgs e)
        {
            //_capture.QueryFrame();
            //Image<Bgr, byte> img = _capture.RetrieveBgrFrame();
            //ImageSource = ToBitmapSource(img);
            //text = (count++).ToString();
            // 
            ForzarUpdate();

        }

        public void ForzarUpdate()
        {
            LstPacientes.Clear();
            lstLateralidad.Clear();
            LstUnidades.Clear();
            InicializarComboPacientes();
            InicializarComboLateralidad();
            InicializarComboUnidad();
        }

        #region Methods added

        public System.Drawing.Bitmap ToBitmap(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (var outStream = new System.IO.MemoryStream())
            {
                // from System.Media.BitmapImage to System.Drawing.Bitmap
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
                return bitmap;
            }
        }


        [System.Runtime.InteropServices.DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="image">The Emgu CV Image</param>
        /// <returns>The equivalent BitmapSource</returns>
        public BitmapSource ToBitmapSource(IImage image)
        {
            using (Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }


        #endregion

        #endregion
    }
}

