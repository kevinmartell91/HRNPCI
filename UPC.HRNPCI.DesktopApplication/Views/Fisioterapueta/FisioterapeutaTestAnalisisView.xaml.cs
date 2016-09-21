using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Drawing.Imaging;

using System.IO;
using Microsoft.Kinect;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Drawing;


using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Runtime.InteropServices;

using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;
using UPC.HRNPCI.DesktopApplication.ViewModels.RutasAlmacenamiento;
//using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes.Helpers;

namespace UPC.HRNPCI.DesktopApplication.Views.Fisioterapueta
{
    /// <summary>
    /// Interaction logic for FisioterapeutaTestAnalisisView.xaml
    /// </summary>
    public partial class FisioterapeutaTestAnalisisView : UserControl, INotifyPropertyChanged
    {

        #region Commun Declarations

        Image<Bgr, Byte> imgEmguFormat;
        Bitmap bmpFrame;
        int _iEstadosAnalisisMarcha;
        string _strEstadosAnalisisMarcha;
        double xm, ym;
        

        DispatcherTimer timer = new DispatcherTimer();
        bool blnLimpiarGrafica;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public string strEstadosAnalisisMarcha
        {
            get{return _strEstadosAnalisisMarcha;}
        }

      
            

	    #endregion

        #region Declarations CLEYE demo - commented

        int numCameras = 0;

        #endregion

        #region Declarations Kinect v1.0

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>
        private WriteableBitmap colorBitmap;

        /// <summary>
        /// Intermediate storage for the color data received from the camera
        /// </summary>
        private byte[] colorPixels;

        #endregion

        #region Declarations Kinect v2 integration

        /// <summary>
        /// Maximum value (as a float) that can be returned by the InfraredFrame
        /// </summary>
        private const float InfraredSourceValueMaximum = (float)ushort.MaxValue;

        /// <summary>
        /// The value by which the infrared source data will be scaled
        /// </summary>
        private const float InfraredSourceScale = 0.75f;

        /// <summary>
        /// Smallest value to display when the infrared data is normalized
        /// </summary>
        private const float InfraredOutputValueMinimum = 0.01f;

        /// <summary>
        /// Largest value to display when the infrared data is normalized
        /// </summary>
        private const float InfraredOutputValueMaximum = 1.0f;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Reader for infrared frames
        /// </summary>
        private InfraredFrameReader infraredFrameReader = null;

        /// <summary>
        /// Description (width, height, etc) of the infrared frame data
        /// </summary>
        private FrameDescription infraredFrameDescription = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap infraredBitmap = null;

        /// <summary>
        /// Current status text to display
        /// </summary>
        //private string statusText = null;

        #endregion

        #region Declarations Capture Angles demo

        List<double> angles;

        Image<Bgr, byte> original_Frame;
        Image<Gray, byte> umbral_frame;

        int blobCount;
        int countFrames;


        double markerInitialContact;
        List<List<System.Drawing.PointF>> markersHistory;
        int posContactoInicial;
        int posBalanceoFinal;

        #endregion

        #region Declaration Bezier curves

        /// <summary>
        /// Curve Functions Names
        /// </summary>
        public enum CurveNames
        {
            /// <summary>
            /// Sinus curve, <see cref="FisioterapeutaTestAnalisisView.Sinus"/>
            /// </summary>
            Sinus,
            /// <summary>
            /// Runge curve, <see cref="FisioterapeutaTestAnalisisView.Runge"/>
            /// </summary>
            Runge,
            /// <summary>
            /// Arc curve, <see cref="FisioterapeutaTestAnalisisView.Arc"/>
            /// </summary>
            Arc,
        }

        #endregion
        
        public FisioterapeutaTestAnalisisView()
        {
            InitializeComponent();

            #region Commun Initializations

            _iEstadosAnalisisMarcha = 0;
            timer.Interval = new TimeSpan( 200);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            blnLimpiarGrafica = false;

            markerInitialContact = 0;


            

            #endregion

            #region Initialization of CLEYE demo - commented

            //this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            ////capture PS3 frame by this manual event
            //this.KeyDown += FisioterapeutaTestAnalisisView_KeyDown;

            /////
            //markers = new List<System.Drawing.Point>();
            //angles = new List<double>();
            //countFrames = 0;


            #endregion

            #region Initialization of Kinect v1.0

            //// Look through all sensors and start the first connected one.
            //// This requires that a Kinect is connected at the time of app startup.
            //// To make your app robust against plug/unplug, 
            //// it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
            //foreach (var potentialSensor in KinectSensor.KinectSensors)
            //{
            //    if (potentialSensor.Status == KinectStatus.Connected)
            //    {
            //        this.sensor = potentialSensor;
            //        break;
            //    }
            //}

            //if (null != this.sensor)
            //{
            //    // Turn on the color stream to receive color frames
            //    this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

            //    // Allocate space to put the pixels we'll receive
            //    this.colorPixels = new byte[this.sensor.ColorStream.FramePixelDataLength];

            //    // This is the bitmap we'll display on-screen
            //    this.colorBitmap = new WriteableBitmap(this.sensor.ColorStream.FrameWidth, this.sensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Gray16, null);

            //    // Set the image we display to point to the bitmap where we'll put the image data
            //    this.IImageFormat.Source = this.colorBitmap;

            //    // Add an event handler to be called whenever there is new color frame data
            //    this.sensor.ColorFrameReady += this.SensorColorFrameReady;

            //    // Start the sensor!
            //    try
            //    {
            //        this.sensor.Start();
            //    }
            //    catch (IOException)
            //    {
            //        this.sensor = null;
            //    }
            //}

           
            #endregion

            #region Initialization of Kinect v2.0

            // get the kinectSensor object
            this.kinectSensor = KinectSensor.GetDefault();

            // open the reader for the depth frames
            this.infraredFrameReader = this.kinectSensor.InfraredFrameSource.OpenReader();

            // wire handler for frame arrival
            this.infraredFrameReader.FrameArrived += this.Reader_InfraredFrameArrived;

            // get FrameDescription from InfraredFrameSource
            this.infraredFrameDescription = this.kinectSensor.InfraredFrameSource.FrameDescription;

            // create the bitmap to display
            this.infraredBitmap = new WriteableBitmap(this.infraredFrameDescription.Width, this.infraredFrameDescription.Height, 96.0, 96.0, PixelFormats.Gray32Float, null);

            // set IsAvailableChanged event notifier
            //this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // open the sensor
            this.kinectSensor.Open();

            // set the status text
            //this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
            //                                                : Properties.Resources.NoSensorStatusText;

            // use the window object as the view model in this simple example
            //this.DataContext = this;

            // initialize the components (controls) of the window
            //this.InitializeComponent();


            #endregion

            #region Initialization of Capture Angles demo

            original_Frame = null;
            umbral_frame = null;

            //markers = new List<System.Drawing.Point>();
            angles = new List<double>();
            
            countFrames = 0;
            markersHistory = new List<List<System.Drawing.PointF>>();

            #endregion
        }

        #region Commun Methods

        void timer_Tick(object sender, EventArgs e)
        {


            switch (_iEstadosAnalisisMarcha)
            {
                case 0: // can take pictures in this case
                    btnEstadosAnalisis.Content = "Capturar";
                    btnFoto.IsEnabled = true;
                    btnGrabarGrafica.IsEnabled = false;
                    break;

                case 1: // Gait analysis begin, for getting only angles List
                    btnEstadosAnalisis.Content = "Detener";
                    btnFoto.IsEnabled = false;
                    btnGrabarGrafica.IsEnabled = false;
                    break;

                case 2:
                    btnEstadosAnalisis.Content = "Limpiar";
                    btnFoto.IsEnabled = true;
                    btnGrabarGrafica.IsEnabled = true;
                    //Make grafic, call it just once
                    if (angles.Count != 0)
                        PointCount = angles.Count;
                    break;

                case 3:
                    if (!blnLimpiarGrafica)
                    // Guarda e base de datos, para hacerlo pasarla la lista de anguloa a la calse estatica y en el view model a BD
                    {

                        blnLimpiarGrafica = false;
                    }

                    // lima todo para un nuevo analisis
                    angles.Clear();
                    PointCount = 2;
                    //emptyGraphic();
                    _iEstadosAnalisisMarcha = 0;
                    break;
               
            }
        }

        private void GaitAnalysisStates()
        {
            switch (_iEstadosAnalisisMarcha)
            {
                case 0: // can take pictures in this case
                    IImageFormat.Source = ToBitmapSource(imgEmguFormat);
                    GaitAnalysis(imgEmguFormat,false);
                    break;

                case 1: // Gait analysis begin, for getting only angles List
                    GaitAnalysis(imgEmguFormat,true);
                    break;

                case 2:
                    //Make grafic, call it just once
                    IImageFormat.Source = ToBitmapSource(imgEmguFormat);
                    break;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region CLEYE demo - Cemeras instaciation and start threads - commented

            //// Query for number of connected cameras
            //numCameras = CLEyeCameraDevice.CameraCount;
            ////MyOwnEvent += MainWindow_MyOwnEvent;
            //if (numCameras == 0)
            //{
            //    MessageBox.Show("Could not find any PS3Eye cameras!");
            //    return;
            //}
            ////output.Items.Add(string.Format("Found {0} CLEyeCamera devices", numCameras));
            //// Show camera's UUIDs
            ////for (int i = 0; i < numCameras; i++)
            ////{
            ////    output.Items.Add(string.Format("CLEyeCamera #{0} UUID: {1}", i + 1, CLEyeCameraDevice.CameraUUID(i)));
            ////}
            //// Create cameras, set some parameters and start capture
            //if (numCameras >= 1)
            //{
            //    cameraImage1.Device.Create(CLEyeCameraDevice.CameraUUID(0));
            //    cameraImage1.Device.Zoom = -50;
            //    cameraImage1.Device.Start();
            //    //ps3 = true;

            //}
            ////if (numCameras == 2)
            ////{
            ////    cameraImage2.Device.Create(CLEyeCameraDevice.CameraUUID(1));
            ////    cameraImage2.Device.Rotation = 200;
            ////    cameraImage2.Device.Start();
            ////}

            #endregion
        }

        private void btnEstadosAnalisis_Click(object sender, RoutedEventArgs e)
        {

            if (_iEstadosAnalisisMarcha == 0) { _iEstadosAnalisisMarcha = 1; return; }
            if (_iEstadosAnalisisMarcha == 1) { _iEstadosAnalisisMarcha = 2; return; }
            if (_iEstadosAnalisisMarcha == 2)
            {
                blnLimpiarGrafica = true;
                tbxResults.Clear();
                _iEstadosAnalisisMarcha = 3;
            }

        }

        private void btnGrabarGrafica_Click(object sender, RoutedEventArgs e)
        {
            if (angles != null || angles.Count == 0)
                FisioterapeutaStatic.setAngles(angles, 1);



            if (_iEstadosAnalisisMarcha == 2 && FisioterapeutaStatic.blnGuardar) // selcione todo los combos  static 
            //if (true)
            {
                //MessageBox.Show("Guardo en base de datos y se guardo en la clase estatica");
                FisioterapeutaStatic.setAngles(angles, 1);
                angles.Clear();// listo para u nuevo analisis la grafica deberia seguir mostrandose
               

            }
            _iEstadosAnalisisMarcha = 3;
            // y Grabar  grafica en la clase estatica que sirve de puente
            //y en el view model golpear directamente a la BD con los angulos de la grafica
        }

        private void btnFoto_Click(object sender, RoutedEventArgs e)
        {
            ScreenCapture.TomarFoto(3);
           // GaitAnalysis(imgEmguFormat, false);

           
        }

      
        #endregion

        #region Events for accesing CLEYE Thread - commmented


        //private void FisioterapeutaTestAnalisisView_KeyDown(object sender, KeyEventArgs e)
        //{
        //    #region My demo Capture Angles

        //    imgEmguFormat = new Image<Bgr, byte>(new Bitmap(ToBitmap(cameraImage1.Device.BitmapSource)));
        //    GaitAnalysis(imgEmguFormat);

        //    #endregion
        //}

        //private void My_Timer_Tick(object sender, EventArgs e)
        //{
        //    //frames.Source = ToBitmapSource( _capture.QueryFrame());
        //}

        //private void btnOn_Click(object sender, RoutedEventArgs e)
        //{
        //    Capture captute = new Capture(@"C:\Users\kevin\Documents\IR_View_test.avi");
        //    Image<Bgr, byte> recordFrame = captute.QueryFrame();
        //    IImageFormat.Source = ToBitmapSource(recordFrame);
        //}

        //private void btnOff_Click(object sender, RoutedEventArgs e)
        //{
        //    #region CYELE Demo

        //    //if (numCameras >= 1)
        //    //{
        //    //    cameraImage1.Device.Stop();
        //    //    cameraImage1.Device.Destroy();
        //    //}
        //    ////if (numCameras == 2)
        //    ////{
        //    ////    cameraImage2.Device.Stop();
        //    ////    cameraImage2.Device.Destroy();
        //    ////}
        //    #endregion
        //}

        #endregion

        #region Methods of kinect v1.0

        ///// <summary>
        ///// Event handler for Kinect sensor's ColorFrameReady event
        ///// </summary>
        ///// <param name="sender">object sending the event</param>
        ///// <param name="e">event arguments</param>
        //private void SensorColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        //{
        //    using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
        //    {
        //        if (colorFrame != null)
        //        {
        //            //// Copy the pixel data from the image to a temporary array
        //            //colorFrame.CopyPixelDataTo(this.colorPixels);

        //            //// Write the pixel data into our bitmap
        //            //this.colorBitmap.WritePixels(
        //            //    new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
        //            //    this.colorPixels,
        //            //    this.colorBitmap.PixelWidth * colorFrame.BytesPerPixel,
        //            //    0);

        //            bmpFrame = ColorImageFrameToBitmap(colorFrame);
        //            imgEmguFormat = new Image<Bgr, byte>(new Bitmap(bmpFrame));
                    
        //            switch (_iEstadosAnalisisMarcha)
        //            {
        //                case 0 : // can take pictures in this case
        //                    IImageFormat.Source = ToBitmapSource(imgEmguFormat);
        //                    break;

        //                case 1 : // Gait analysis begin, for getting only angles List
        //                    GaitAnalysis(imgEmguFormat);
                            
        //                    break;

        //                case 2 :
        //                    //Make grafic, call it just once
        //                    IImageFormat.Source = ToBitmapSource(imgEmguFormat);
        //                    break;
        //            }
        //        }
        //    }
        //}

        //private static Bitmap ColorImageFrameToBitmap(ColorImageFrame colorFrame)
        //{
        //    byte[] pixelBuffer = new byte[colorFrame.PixelDataLength];
        //    colorFrame.CopyPixelDataTo(pixelBuffer);

        //    Bitmap bitmapFrame = new Bitmap(colorFrame.Width, colorFrame.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
        //    BitmapData bitmapData = bitmapFrame.LockBits(new System.Drawing.Rectangle(0, 0, colorFrame.Width, colorFrame.Height), ImageLockMode.WriteOnly, bitmapFrame.PixelFormat);

        //    IntPtr intPointer = bitmapData.Scan0;
        //    Marshal.Copy(pixelBuffer, 0, intPointer, colorFrame.PixelDataLength);

        //    bitmapFrame.UnlockBits(bitmapData);

        //    return bitmapFrame;
        //}
        #endregion

        #region Methods Kinect v2.0

        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return this.infraredBitmap;
            }
        }

        /// <summary>
        /// Gets or sets the current status text to display
        /// </summary>
        public string statusText;
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;

                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                    }
                }
            }
        }

        /// <summary>
        /// Handles the infrared frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_InfraredFrameArrived(object sender, InfraredFrameArrivedEventArgs e)
        {
            // InfraredFrame is IDisposable
            using (InfraredFrame infraredFrame = e.FrameReference.AcquireFrame())
            {
                if (infraredFrame != null)
                {

                    //Process the IR frame from kinect which is more beutiful than the default image is lighter
                    imgEmguFormat = new Image<Bgr, Byte>(new Bitmap(ToBitmap(ToBitmapSource(infraredFrame))));

                    GaitAnalysisStates();


                    // the fastest way to process the infrared frame data is to directly access 
                    // the underlying buffer
                    //using (Microsoft.Kinect.KinectBuffer infraredBuffer = infraredFrame.LockImageBuffer())
                    //{
                    //    // verify data and write the new infrared frame data to the display bitmap
                    //    if (((this.infraredFrameDescription.Width * this.infraredFrameDescription.Height) == (infraredBuffer.Size / this.infraredFrameDescription.BytesPerPixel)) &&
                    //        (this.infraredFrameDescription.Width == this.infraredBitmap.PixelWidth) && (this.infraredFrameDescription.Height == this.infraredBitmap.PixelHeight))
                    //    {
                    //        this.ProcessInfraredFrameData(infraredBuffer.UnderlyingBuffer, infraredBuffer.Size);
                    //    }
                    //}
                }
            }
        }

        /// <summary>
        /// Directly accesses the underlying image buffer of the InfraredFrame to 
        /// create a displayable bitmap.
        /// This function requires the /unsafe compiler option as we make use of direct
        /// access to the native memory pointed to by the infraredFrameData pointer.
        /// </summary>
        /// <param name="infraredFrameData">Pointer to the InfraredFrame image data</param>
        /// <param name="infraredFrameDataSize">Size of the InfraredFrame image data</param>
        private unsafe void ProcessInfraredFrameData(IntPtr infraredFrameData, uint infraredFrameDataSize)
        {
            // infrared frame data is a 16 bit value
            ushort* frameData = (ushort*)infraredFrameData;

            // lock the target bitmap
            this.infraredBitmap.Lock();

            // get the pointer to the bitmap's back buffer
            float* backBuffer = (float*)this.infraredBitmap.BackBuffer;

            // process the infrared data
            for (int i = 0; i < (int)(infraredFrameDataSize / this.infraredFrameDescription.BytesPerPixel); ++i)
            {
                // since we are displaying the image as a normalized grey scale image, we need to convert from
                // the ushort data (as provided by the InfraredFrame) to a value from [InfraredOutputValueMinimum, InfraredOutputValueMaximum]
                backBuffer[i] = Math.Min(InfraredOutputValueMaximum, (((float)frameData[i] / InfraredSourceValueMaximum * InfraredSourceScale) * (1.0f - InfraredOutputValueMinimum)) + InfraredOutputValueMinimum);
            }


            // mark the entire bitmap as needing to be drawn
            this.infraredBitmap.AddDirtyRect(new Int32Rect(0, 0, this.infraredBitmap.PixelWidth, this.infraredBitmap.PixelHeight));

            // unlock the bitmap
            this.infraredBitmap.Unlock();


            // Lo hace lento 
            ////Process the IR frame from kinect which is more beutiful than the default image is lighter
            //Bitmap bitmap = new Bitmap(BitmapImage2Bitmap());
            //Image<Bgr, Byte> KinectIrFrame = new Image<Bgr, Byte>(bitmap);
            //GaitAnalysis(KinectIrFrame);





        }

        /// <summary>
        /// Handles the event which the sensor becomes unavailable (E.g. paused, closed, unplugged).
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        //private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        //{
        //    // set the status text
        //    this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
        //                                                    : Properties.Resources.SensorNotAvailableStatusText;
        //}


        public Bitmap BitmapImage2Bitmap()
        {

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(this.infraredBitmap));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                // return bitmap; <– leads to problems, stream is closed/closing …
                return new Bitmap(bitmap);
            }
        }

        public BitmapSource ToBitmapSource(InfraredFrame frame)
        {
            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;
            System.Windows.Media.PixelFormat format = PixelFormats.Bgr32;

            ushort[] frameData = new ushort[width * height];
            byte[] pixels = new byte[width * height * (format.BitsPerPixel + 7) / 8];

            frame.CopyFrameDataToArray(frameData);

            int colorIndex = 0;
            for (int infraredIndex = 0; infraredIndex < frameData.Length; infraredIndex++)
            {
                ushort ir = frameData[infraredIndex];

                byte intensity = (byte)(ir >> 7);

                pixels[colorIndex++] = (byte)(intensity / 1); // Blue
                pixels[colorIndex++] = (byte)(intensity / 1); // Green   
                pixels[colorIndex++] = (byte)(intensity / 1); // Red

                colorIndex++;
            }

            int stride = width * format.BitsPerPixel / 8;

            return BitmapSource.Create(width, height, 96, 96, format, null, pixels, stride);
        }

        #endregion

        #region Methods of Capture Angles demo - Gait Analysis process

        private void GaitAnalysis(Image<Bgr, byte> image, bool captureAngles)
        {
            Contour<System.Drawing.Point> contours = null;
            List<System.Drawing.PointF> markers = null ;

            MCvScalar markerColor;

            original_Frame = image.Copy();
            umbral_frame = null;

            umbral_frame = image.Convert<Gray, Byte>();
            umbral_frame = umbral_frame.ThresholdBinary(new Gray((int)sliderMin.Value), new Gray(255));
            //umbral_frame._SmoothGaussian(7 );
            //umbral_frame.SmoothBlur(1,1);


            using (MemStorage stor = new MemStorage())
            {
                //Find contours with no holes try CV_RETR_EXTERNAL to find holes
                contours = umbral_frame.FindContours(
                 Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                 Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL,
                 stor);

                markers = new List<System.Drawing.PointF>();
                blobCount = 0;
                for (int i = 0; contours != null; contours = contours.HNext)
                {
                    i++;
                    float markerPosX = contours.GetMinAreaRect().center.X;
                    float markerPosY = contours.GetMinAreaRect().center.Y;


                    //if ((contours.Area > Math.Pow(sliderMinSize.Value, 2)) && (contours.Area < Math.Pow(sliderMaxSize.Value, 2)))

                    if ((contours.Area > Math.Pow(sliderMinSize.Value, 2)) && (contours.Area < Math.Pow(sliderMaxSize.Value, 2))
                            && 40 < markerPosY && markerPosY < original_Frame.Height - 60
                            && 50 < markerPosX && markerPosX < original_Frame.Width - 50)

                     
                    //if ((contours.Area > Math.Pow(sliderMinSize.Value, 2)) && (contours.Area < Math.Pow(sliderMaxSize.Value, 2)))

                    //if ((contours.Area > Math.Pow(sliderMinSize.Value, 2)) && (contours.Area < Math.Pow(sliderMaxSize.Value, 2))
                    //     && 70 < markerPosY && markerPosY < ym
                    //     && xm < markerPosX && markerPosX < original_Frame.Width - 50)

                    {
                        MCvBox2D box = contours.GetMinAreaRect();
                        //openCVImg.Draw(box, new Bgr(System.Drawing.Color.Red), 2);
                        //umbral_frame.Draw(box, new Gray(88), 2);
                        blobCount++; 
                        markerColor = new MCvScalar(0, 0, 255);

                        //if (blobCount == 3) // Marcador del tobillo
                        //{
                        //    double actualMarkerInitialContact = box.center.Y;
                        //    if (actualMarkerInitialContact > markerInitialContact)
                        //    {
                        //        markerInitialContact = actualMarkerInitialContact;
                        //        markerColor = new MCvScalar(255, 0, 0);
                        //    }
                        //}
                        
                      
                        
                        CvInvoke.cvCircle(original_Frame,
                                  new System.Drawing.Point((int)box.center.X, (int)box.center.Y),
                                  2,
                                  markerColor,
                                  -1,
                                  LINE_TYPE.CV_AA,
                                  0);

                        System.Drawing.PointF pointF = new System.Drawing.PointF(box.center.X, box.center.Y);

                        //CircleF c = new CircleF(pointF, 20);
                        //original_Frame.Draw(c,
                        //             new Bgr(System.Drawing.Color.Orange),
                        //             3);


                        markers.Add(pointF);

                    }
                }

            }


            if (markers.Count == 3)
            {
                DrawLines(markers, original_Frame,captureAngles);
                markersHistory.Add(markers);
            }

            //DrawLinesHoriontal(markers, original_Frame, captureAngles);

            IImageFormat.Source = ToBitmapSource(original_Frame);
            IImage_umbral.Source = ToBitmapSource(umbral_frame);
            txtBlobCount.Text = blobCount.ToString();


        }

        private void DrawLines(List<System.Drawing.PointF> markersF, Image<Bgr, byte> outImg1, bool captureAngles)
        {



            LineSegment2D line1 = new LineSegment2D(new System.Drawing.Point((int)markersF[0].X, (int)markersF[0].Y),
                                                    new System.Drawing.Point((int)markersF[1].X, (int)markersF[1].Y));
            LineSegment2D line2 = new LineSegment2D(new System.Drawing.Point((int)markersF[1].X, (int)markersF[1].Y),
                                                    new System.Drawing.Point((int)markersF[2].X, (int)markersF[2].Y));

            outImg1.Draw(line1, new Bgr(System.Drawing.Color.Red), 1);
            outImg1.Draw(line2, new Bgr(System.Drawing.Color.Red), 1);
            double angleEmgu = line1.GetExteriorAngleDegree(line2);
            double angle = findAngle(markersF);
            // double angle = angleEmgu;

            MCvFont f = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0);
            outImg1.Draw(((int)angle).ToString(), ref f, new System.Drawing.Point((int)markersF[1].X, (int)markersF[1].Y), new Bgr(121, 116, 40));

            if(captureAngles)
                angles.Add(angle);

            //========================== activar un BOOL para empezar a capturar lo angulos y desactivarlo al presionarlo nuevamente 
            //========================== luego guardar la lista de angulos y mostrarlo en la grafica

            //tbxResults.AppendText("line.AddPoint(" + (countFrames * 1.0).ToString() + "," + Math.Abs(angle).ToString() + ");");
            tbxResults.AppendText(Math.Abs(angle).ToString() + ",");
            tbxResults.AppendText(Environment.NewLine);

            /*
            ListBoxItem item = new ListBoxItem();
            item.Content = "AddPoint(" + (countFrames * 1.0).ToString() + "," + angle.ToString() + ");";
            listResults.Items.Add(item);
            */
            countFrames++;
        }

        private void DrawLinesHoriontal(List<System.Drawing.PointF> markersF, Image<Bgr, byte> outImg1, bool captureAngles)
        {


            for (int i = 0; i < markersF.Count; i++)
            {
                LineSegment2D line1 = new LineSegment2D(new System.Drawing.Point(0, (int)markersF[i].Y),
                                                    new System.Drawing.Point((int)markersF[i].X, (int)markersF[i].Y));

                outImg1.Draw(line1, new Bgr(System.Drawing.Color.Red), 1);
            }
            
          
            countFrames++;
        }

        private double findAngle(List<System.Drawing.PointF> markersF)
        {
              double a = Math.Pow(markersF[1].X-markersF[0].X,2) + Math.Pow(markersF[1].Y-markersF[0].Y,2);
              double b = Math.Pow(markersF[1].X-markersF[2].X,2) + Math.Pow(markersF[1].Y-markersF[2].Y, 2);
              double c = Math.Pow(markersF[2].X-markersF[0].X,2) + Math.Pow(markersF[2].Y-markersF[0].Y,2);
              return Math.Abs (Math.Acos( (a+b-c) / Math.Sqrt(4*a*b) ) * 180/Math.PI - 180);
            }

        #endregion

        #region Methods of Bezier curves

        #region DependencyProperties

        #region Curve
        /// <summary>
        /// Identifies the Curve dependency property.
        /// </summary>
        public static readonly DependencyProperty CurveProperty
            = DependencyProperty.Register("Curve", typeof(CurveNames), typeof(FisioterapeutaTestAnalisisView)
                , new FrameworkPropertyMetadata(CurveNames.Sinus
                    , FrameworkPropertyMetadataOptions.AffectsMeasure
                        | FrameworkPropertyMetadataOptions.AffectsRender)
                , validateCurve);
        /// <summary>
        /// Gets or sets the Curve property.
        /// </summary>
        /// <value>CurveNames value</value>
        public CurveNames Curve
        {
            get { return (CurveNames)GetValue(CurveProperty); }
            set { SetValue(CurveProperty, value); }
        }
        /// <summary>
        /// Validates the proposed Curve property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool validateCurve(object value)
        {
            CurveNames cName = (CurveNames)value;
            foreach (CurveNames item in Enum.GetValues(typeof(CurveNames)))
            {
                if (item == cName)
                    return true;
            }
            return false;
        }
        #endregion Curve

        #region PointCount
        /// <summary>
        /// Identifies the PointCount dependency property.
        /// </summary>
        public static readonly DependencyProperty PointCountProperty
            = DependencyProperty.Register("PointCount", typeof(int), typeof(FisioterapeutaTestAnalisisView)
                , new FrameworkPropertyMetadata(2
                    , FrameworkPropertyMetadataOptions.AffectsMeasure
                        | FrameworkPropertyMetadataOptions.AffectsRender)
                , validatePointCount);
        /// <summary>
        /// Gets or sets the PointCount property.
        /// </summary>
        /// <value>integer > 1</value>
        public int PointCount
        {
            get { return (int)GetValue(PointCountProperty); }
            set { SetValue(PointCountProperty, value); }
        }
        /// <summary>
        /// Validates the proposed PointCount property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool validatePointCount(object value)
        {
            int cnt = (int)value;
            return (cnt > 1 ? true : false);
        }
        #endregion PointCount

        #region ScaleX
        /// <summary>
        /// Identifies the ScaleX dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleXProperty
            = DependencyProperty.Register("ScaleX", typeof(double), typeof(FisioterapeutaTestAnalisisView)
                , new FrameworkPropertyMetadata(100.0
                    , FrameworkPropertyMetadataOptions.AffectsMeasure
                        | FrameworkPropertyMetadataOptions.AffectsRender)
                , validateScaleX);
        /// <summary>
        /// Gets or sets the ScaleX property.
        /// </summary>
        /// <value>double >= 1</value>
        public double ScaleX
        {
            get { return (double)GetValue(ScaleXProperty); }
            set { SetValue(ScaleXProperty, value); }
        }
        /// <summary>
        /// Validates the proposed ScaleX property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool validateScaleX(object value)
        {
            double scale = (double)value;
            return (scale >= 1.0 ? true : false);
        }
        #endregion ScaleX

        #region ScaleY
        /// <summary>
        /// Identifies the ScaleY dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleYProperty
            = DependencyProperty.Register("ScaleY", typeof(double), typeof(FisioterapeutaTestAnalisisView)
                , new FrameworkPropertyMetadata(3.12
                    , FrameworkPropertyMetadataOptions.AffectsMeasure
                        | FrameworkPropertyMetadataOptions.AffectsRender)
                , validateScaleY);
        /// <summary>
        /// Gets or sets the ScaleY property.
        /// </summary>
        /// <value>double >= 1</value>
        public double ScaleY
        {
            get { return (double)GetValue(ScaleYProperty); }
            set { SetValue(ScaleYProperty, value); }
        }
        /// <summary>
        /// Validates the proposed ScaleY property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool validateScaleY(object value)
        {
            double scale = (double)value;
            return (scale >= 1.0 ? true : false);
        }
        #endregion ScaleY
        #endregion DependencyProperties

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            switch (Curve)
            {
                case CurveNames.Sinus:
                    DrawCurve(Sinus);
                    break;
                case CurveNames.Runge:
                    DrawCurve(Runge);
                    break;
                case CurveNames.Arc:
                    DrawCurve(Arc);
                    break;
            }
            base.OnRender(drawingContext);
        }

        /// <summary>
        /// Function points provider
        /// </summary>
        delegate System.Windows.Point[] Function();

        /// <summary>
        /// Draw the Curve.
        /// </summary>
        /// <param name="curve">The curve to draw.</param>
        void DrawCurve(Function curve)
        {
            canvas.Children.Clear();

            System.Windows.Point[] points = curve();
            if (points.Length < 2)
                return;

            const double markerSize = 0.1;
            // Draw Curve points (Black)
            for (int i = 0; i < points.Length; ++i)
            {
                System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle()
                {
                    Stroke = System.Windows.Media.Brushes.Black,
                    Fill = System.Windows.Media.Brushes.Black,
                    Height = markerSize,
                    Width = markerSize
                };
                Canvas.SetLeft(rect, points[i].X - markerSize / 2);
                Canvas.SetTop(rect, points[i].Y - markerSize / 2);
                canvas.Children.Add(rect);
            }

            // Get Bezier Spline Control Points.
            System.Windows.Point[] cp1, cp2;
            BezierSpline.GetCurveControlPoints(points, out cp1, out cp2);

            // Draw curve by Bezier.
            PathSegmentCollection lines = new PathSegmentCollection();
            for (int i = 0; i < cp1.Length; ++i)
            {
                lines.Add(new BezierSegment(cp1[i], cp2[i], points[i + 1], true));
            }
            PathFigure f = new PathFigure(points[0], lines, false);
            PathGeometry g = new PathGeometry(new PathFigure[] { f });
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path() { Stroke = System.Windows.Media.Brushes.Red, StrokeThickness = 1.5, Data = g };
            canvas.Children.Add(path);

            // Draw Bezier control points markers
            for (int i = 0; i < cp1.Length; ++i)
            {
                // First control point (Blue)
                System.Windows.Shapes.Ellipse marker = new System.Windows.Shapes.Ellipse()
                {
                    Stroke = System.Windows.Media.Brushes.Blue,
                    Fill = System.Windows.Media.Brushes.Blue,
                    Height = markerSize / 2,
                    Width = markerSize / 2
                };
                Canvas.SetLeft(marker, cp1[i].X - markerSize / 2);
                Canvas.SetTop(marker, cp1[i].Y - markerSize / 2);
                canvas.Children.Add(marker);

                // Second control point (Green)
                marker = new System.Windows.Shapes.Ellipse()
                {
                    Stroke = System.Windows.Media.Brushes.Green,
                    Fill = System.Windows.Media.Brushes.Green,
                    Height = markerSize / 2,
                    Width = markerSize / 2
                };
                Canvas.SetLeft(marker, cp2[i].X - markerSize / 2);
                Canvas.SetTop(marker, cp2[i].Y - markerSize / 2);
                canvas.Children.Add(marker);
            }

            // Print points
            //Trace.WriteLine(string.Format("Start=({0})", points[0]));
            //for (int i = 0; i < cp1.Length; ++i)
            //{
            //    Trace.WriteLine(string.Format("CP1=({0}) CP2=({1}) Stop=({2})"
            //        , cp1[i], cp2[i], points[i + 1]));
            //}
        }

        #region Curves
        /// <summary>
        /// Sinus points in [0,2PI].
        /// </summary>
        /// <returns></returns>
        System.Windows.Point[] Sinus()
        {
            // Fill point array with scaled in X,Y Sin values in [0, PI].
            //PointCount = 0;
            double x_origin = 53.4;
            double y_origin = 252.19;

            #region angles recorded from kinect v1

            Double[] angles_kinect_v1 = new Double[] {
       1.57107576566613,
2.06509689434351,
3.66418428506466,
5.50293288418031,
5.4477847723619,
5.15709832717024,
5.11266287172452,
5.06621051456069,
4.73576952975133,
4.68577056924535,
4.63408720878075,
5.78239206854893,
4.78254780595794,
4.72142398199244,
5.59895472348194,
5.26178037521827,
5.16147544539038,
3.49615248216569,
3.09183816248203,
2.37990550244281,
0.60904572857245,
2.74987992033371,
6.55080098034177,
12.1873151212086,
15.5970018036631,
21.4884449374227,
27.6943277754093,
34.776910559237,
41.1083222800258,
45.9308686551314,
48.8155967809824,
51.7098369823299,
51.759957700917,
51.3929189271081,
48.0362944473692,
44.7622123524095,
42.2325694233068,
37.6373600567239,
33.3738308814114,
25.7424323843055,
20.2499618013877,
16.5839770632399,
11.1167597617722,
6.97761805251387,
3.03596170424674,
0.859042074240521,
0.0443807696706979,
0.0814689702510274,
1.19592378171547

                };



            #endregion

            #region test of gemetrical distorsion
            double[] angles_geometrical_distorsion_kinectv2 = new double[] {
        8.1226033982932,
        6.98231222848164,
        6.98231222848164,
        6.98231222848164,
        5.83929184040533,
        7.42454355277428,
        7.42454355277428,
        7.42454355277428,
        7.42454355277428,
        7.42454355277428,
        7.42454355277428,
        7.42454355277428,
        7.42454355277428,
        7.42454355277428,
        7.86629873821161,
        9.00658990802317,
        9.00658990802317,
        9.00658990802317,
        9.00658990802317,
        9.00658990802317,
        9.00658990802317,
        9.00658990802317,
        9.00658990802317,
        7.86629873821161,
        7.86629873821161,
        7.86629873821161,
        7.86629873821161,
        7.86629873821161,
        7.86629873821161,
        7.86629873821161,
        7.86629873821161,
        6.72327835013531,
        6.72327835013531,
        8.30752661923492,
        6.72327835013531,
        6.72327835013531,
        6.72327835013531,
        8.30752661923492,
        8.30752661923492,
        8.30752661923492,
        7.86629873821161,
        8.30752661923492,
        7.86629873821161,
        8.30752661923492,
        8.27021366680302,
        8.27021366680302,
        8.27021366680302,
        9.8884673532057,
        7.12719327872671,
        8.70752662320215,
        8.74817618339415,
        8.74817618339415,
        8.74817618339415,
        8.74817618339415,
        7.60515579531784,
        7.60515579531784,
        9.14422348290533,
        9.14422348290533,
        9.14422348290533,
        9.14422348290533,
        7.56450623512584,
        9.14422348290533,
        9.14422348290533,
        9.14422348290533,
        8.00120309482902,
        8.00120309482902,
        8.00120309482902,
        8.00120309482902,
        9.58025616829573,
        9.58025616829573,
        8.43723578021942,
        8.43723578021942,
        8.48393158626185,
        8.43723578021942,
        8.43723578021942,
        8.43723578021942,
        10.0155750005837,
        8.8725546125074,
        8.8725546125074,
        8.8725546125074,
        8.43723578021942,
        8.43723578021942,
        8.43723578021942,
        7.29238869520925,
        7.29238869520925,
        7.29238869520925,
        7.29238869520925,
        8.8725546125074,
        8.91925041854983,
        8.91925041854983,
        8.91925041854983,
        8.91925041854983,
        8.91925041854983,
        8.91925041854983,
        8.8725546125074,
        8.8725546125074,
        8.8725546125074,
        8.8725546125074,
        8.8725546125074,
        7.72770752749723,
        7.72770752749723,
        7.72770752749723,
        7.72770752749723,
        7.72770752749723,
        7.72770752749723,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        9.30711171508891,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.21610767605663,
        8.21610767605663,
        8.23948412293466,
        8.23948412293466,
        8.23948412293466,
        8.23948412293466,
        8.23948412293466,
        9.84465206286809,
        9.84465206286809,
        9.40765056710923,
        9.84465206286809,
        9.40765056710923,
        9.40765056710923,
        9.40765056710923,
        9.40765056710923,
        9.30711171508891,
        8.21610767605663,
        8.16226463007874,
        8.21610767605663,
        8.16226463007874,
        8.21610767605663,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.16226463007874,
        8.13260784245066,
        8.13260784245066,
        8.13260784245066,
        9.74085902598557,
        9.74085902598557,
        9.74085902598557,
        9.74085902598557,
        9.74085902598557,
        9.74085902598557,
        8.61938838785344,
        8.61938838785344,
        8.61938838785344,
        8.67648561869352,
        8.67648561869352,
        8.67648561869352,
        8.6437385282044,
        8.6437385282044,
        9.11261130676986,
        9.11261130676986,
        9.11261130676986,
        9.08923485989182,
        8.67648561869352,
        8.73446017604406,
        8.73446017604406,
        8.73446017604406,
        8.73446017604406,
        8.73446017604406,
        8.67648561869352,
        8.67648561869352,
        7.45024906865618,
        7.45024906865618,
        7.45024906865618,
        7.45024906865618,
        9.6123300372438,
        9.17386796512637,
        9.11261130676986,
        8.67648561869352,
        9.11261130676986,
        8.67648561869352,
        7.50734629949626,
        7.50734629949626,
        7.50734629949626,
        7.50734629949626,
        9.11261130676986,
        7.50734629949626,
        9.11261130676986,
        9.11261130676986,
        9.11261130676986,
        7.9434719875726,
        9.11261130676986,
        7.9434719875726,
        7.9434719875726,
        7.9434719875726,
        7.9434719875726,
        7.88313939479484,
        7.9434719875726,
        7.88313939479484,
        7.9434719875726,
        9.54781209387098,
        9.54781209387098,
        9.54781209387098,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        9.91530190630851,
        9.91530190630851,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        8.31512599073606,
        8.37867277467373,
        8.37867277467373,
        7.20953345547647,
        7.20953345547647,
        7.20953345547647,
        8.81290210210308,
        8.81290210210308,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.37867277467373,
        8.74616258711125,
        8.74616258711125,
        7.20953345547647,
        7.57702326791399,
        7.20953345547647,
        7.20953345547647,
        7.57702326791399,
        7.20953345547647,
        7.57702326791399,
        9.17620348301187,
        8.74616258711125,
        8.81290210210308,
        9.17620348301187,
        8.00706416381461,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.74616258711125,
        8.81290210210308,
        8.81290210210308,
        7.64376278290582,
        7.64376278290582,
        7.64376278290582,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        7.18518331512551,
        7.18518331512551,
        7.18518331512551,
        7.18518331512551,
        7.18518331512551,
        7.18518331512551,
        7.18518331512551,
        8.74616258711125,
        8.81290210210308,
        7.55267312756304,
        7.61941264255487,
        7.61941264255487,
        7.55267312756304,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        7.55267312756304,
        7.55267312756304,
        7.55267312756304,
        7.55267312756304,
        7.55267312756304,
        7.55267312756304,
        7.55267312756304,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        8.07697370072717,
        8.07697370072717,
        8.05262356037622,
        6.42695807171427,
        6.42695807171427,
        7.61941264255487,
        7.61941264255487,
        7.61941264255487,
        6.42695807171427,
        6.42695807171427,
        8.05262356037622,
        8.05262356037622,
        8.05262356037622,
        6.42695807171427,
        8.05262356037622,
        8.05262356037622,
        8.05262356037622,
        8.05262356037622,
        6.86016898953562,
        6.9311418544383,
        6.9797801214553,
        6.9311418544383,
        6.9311418544383,
        6.9311418544383,
        6.9311418544383,
        6.9311418544383,
        8.45938469120008,
        8.58328535950788,
        8.58328535950788,
        8.58328535950788,
        7.34095417885069,
        7.34095417885069,
        7.34095417885069,
        6.9311418544383,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        6.9311418544383,
        8.48477048267429,
        8.91580900688437,
        7.29231591183369,
        7.29231591183369,
        7.72335443604378,
        7.41511891533333,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        7.36648064831633,
        6.17609030370779,
        6.17609030370779,
        6.17609030370779,
        7.67265033305353,
        6.6102956738059,
        6.6102956738059,
        6.6102956738059,
        7.80068601841443,
        7.92783517312351,
        7.92783517312351,
        6.32419234732438,
        6.32419234732438,
        6.25138966928565,
        6.32419234732438
        };
            #endregion

            #region angles from kinect v2
            double[] angles_kinectv2 = new double[] {
        
        5.93204773272603,
        5.80407672216646,
        7.38598200672507,
        9.87141752947275,
        11.278036601575,
        14.702214936247,
        17.1596081755653,
        19.8779467172774,
        20.9631452797333,
        22.4867079850999,
        24.72046946744,
        24.9994290291999,
        24.1435661464819,
        24.7375946740425,
        24.6252189351891,
        23.3914742458806,
        23.0280009147423,
        22.2909410369897,
        21.8636415959842,
        20.3365558743327,
        19.0915599826464,
        18.7265406811554,
        18.7587613756385,
        18.0645070804329,
        18.0715721615614,
        16.8729034731733,
        15.6721270843507,
        14.1457954402568,
        13.3445081261938,
        12.5435341238264,
        11.726286316711,
        11.3356000365997,
        11.655047048288,
        11.0193403833461,
        10.7297862006532,
        11.5684674178852,
        9.94407120048239,
        9.23488711873271,
        9.62739354528713,
        9.89116391742479,
        10.2498090585682,
        11.749829790018,
        12.3434676462626,
        13.6542818347247,
        15.5569274705488,
        17.1624751832405,
        19.0096700462219,
        21.5732890146974,
        24.3372478587667,
        27.8769388123262,
        30.0092537044637,
        33.8567646702647,
        38.1402398542594,
        42.9218706451413,
        48.632329921058,
        53.1301026104424,
        58.6085923281639,
        61.5380469671359,
        64.132270874533,
        65.7336087583811,
        66.2648646540462,
        65.4301929545789,
        64.0151915641327,
        62.4866116724257,
        59.0031721542887,
        56.2407171359688,
        52.3443843302673,
        48.6296253300093,
        44.8796311900064,
        40.5597467174601,
        36.5044705915895,
        31.923930267504,
        27.4344830419149,
        23.3738162399394,
        19.2912888437544,
        16.547934084823,
        12.8932077363074,
        9.9597022191451,
        9.12942613859747,
        7.99687152160721,
        6.49022011687187,
        6.03431611723634,
        6.11015215093454,
        6.10724076885789,
        4.60169115133381,




         };
            #endregion

            #region Calibracion
            double[] test = new double[] {
        
       0,15,-15,30,75,45,-15,60,75,-15,15,-15,30,75,45,-15,60,75,-15




         };
            #endregion



            if (angles.Count == 0)
            {
                emptyGraphic();
            }
            else
            {
                if (angles.Count >= 3)
                {
                    angles.RemoveAt(1);
                    angles.RemoveAt(0);
                    PointCount = angles.Count;
                }
                else 
                {
                    angles.Clear();
                }

            }

            #region posible deteccion de  IC and BF
            //    //hallo el primer mayor
            //    double valorPrimerMarcadorSuelo = 0;
            //    int posPrimerMarcadorSuelo = 0;
            //    for (int i = 0; i < markersHistory.Count; i++)
            //    {
            //        if (valorPrimerMarcadorSuelo < markersHistory[i][0].Y)
            //        {
            //            posPrimerMarcadorSuelo = i;
            //            valorPrimerMarcadorSuelo = markersHistory[i][0].Y;
            //        }

            //    }

            //    //hallo el segundo menor
            //    double valorSegundoMarcadorSuelo = 0;
            //    int posSegundoMarcadorSuelo = 0;
            //    for (int i = 0; i < markersHistory.Count; i++)
            //    {
            //        if (valorSegundoMarcadorSuelo < markersHistory[i][0].Y && i != posPrimerMarcadorSuelo)
            //        {
            //            posSegundoMarcadorSuelo = i;
            //            valorSegundoMarcadorSuelo = markersHistory[i][0].Y;
            //        }
            //    }

            //    //hallo el nuevo PointCount
            //    PointCount = Math.Abs(posSegundoMarcadorSuelo - posPrimerMarcadorSuelo) + 1;

            //    //hallo cual es el el contacto inicial y balanceo final

            //    if (posPrimerMarcadorSuelo < posSegundoMarcadorSuelo)
            //    {
            //        posContactoInicial = posPrimerMarcadorSuelo;
            //        posBalanceoFinal = posSegundoMarcadorSuelo + 1;
            //    }
            //    else
            //    {
            //        posBalanceoFinal = posPrimerMarcadorSuelo + 1;
            //        posContactoInicial = posSegundoMarcadorSuelo;
            //    }

            //}

            //System.Windows.Point[] points = new System.Windows.Point[PointCount];
            //double step = 3.766 / (PointCount - 1);

            //for (int i = posContactoInicial; i < posBalanceoFinal; ++i)
            //{
            //    points[i - posContactoInicial] = new System.Windows.Point((ScaleX * i * step) + x_origin, (ScaleY * angles[i] * (-1)) + y_origin);
            //}
            #endregion


            PointCount = angles.Count;

            System.Windows.Point[] points = new System.Windows.Point[PointCount];
            double step = 3.766 / (PointCount - 1);

            for (int i = 0; i < PointCount; ++i)
            {
                points[i] = new System.Windows.Point((ScaleX * i * step) + x_origin, (ScaleY * angles[i] * (-1)) + y_origin);
            }

            
           // FisioterapeutaStatic.anglesKneeAnalysis = angles;


            return points;
        }

        private void emptyGraphic()
        {
            angles.Add(1000.0);
            angles.Add(1000.0);
        }

        /// <summary>
        /// Runge function points in [-1,1].
        /// </summary>
        /// <returns></returns>
        System.Windows.Point[] Runge()
        {
            // Fill point array with scaled in X,Y Runge (1 / (1 + 25 * x * x)) values in [-1, 1].
            System.Windows.Point[] points = new System.Windows.Point[PointCount];
            double step = 2.0 / (PointCount - 1);
            for (int i = 0; i < PointCount; ++i)
            {
                double x = -1 + i * step;
                points[i] = new System.Windows.Point(ScaleX * (x + 1), ScaleY * (1 - 1 / (1 + 25 * x * x)));
            }
            return points;
        }

        /// <summary>
        /// Arc curve points in [0º,270º], radius=1.
        /// </summary>
        /// <returns></returns>
        System.Windows.Point[] Arc()
        {
            // Fill point array with scaled in X,Y Arc values in [0º,270º].
            System.Windows.Point[] points = new System.Windows.Point[PointCount];
            double step = 270.0 / (PointCount - 1);
            for (int i = 0; i < PointCount; ++i)
            {
                double x = Math.PI * (0 + i * step) / 180;
                points[i] = new System.Windows.Point(ScaleX * (1 + Math.Cos(x)), ScaleY * (1 + Math.Sin(x)));
            }
            return points;
        }

        #endregion Curves

        #endregion

        #region Methods added for Kinect v2.0

        public System.Drawing.Bitmap ToBitmap(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (MemoryStream outStream = new System.IO.MemoryStream())
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

        private void IImageFormat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            //xm = e.GetPosition(IImageFormat).X;
            //ym = e.GetPosition(IImageFormat).Y;
            //LineSegment2DF lineH = new LineSegment2DF(new System.Drawing.PointF(0, (float)ym),
            //                                        new System.Drawing.PointF((float)original_Frame.Width, (float)ym));

            //LineSegment2DF lineV = new LineSegment2DF(new System.Drawing.PointF((float)xm, 0),
            //                                        new System.Drawing.PointF((float)xm, (float)original_Frame.Height));

            //original_Frame.Draw(lineH, new Bgr(System.Drawing.Color.Green), 1);
            //original_Frame.Draw(lineV, new Bgr(System.Drawing.Color.Green), 1);
            //IImageFormat.Source = ToBitmapSource(original_Frame);
        }

        private void IImageFormat_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

    }

    #region Bezier Extra class

    /// <summary>
    /// ValidationRule class to validate that a value is a number >= 1.
    /// </summary>
    public class ScaleRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = value as string;
            if (!string.IsNullOrEmpty(stringValue))
            {
                double doubleValue;
                if (double.TryParse(stringValue, out doubleValue))
                {
                    if (doubleValue >= 1)
                        return new ValidationResult(true, null);
                }
            }
            return new ValidationResult(false, "Must be a number greater or equal to 1");
        }
    }
    #endregion

}
