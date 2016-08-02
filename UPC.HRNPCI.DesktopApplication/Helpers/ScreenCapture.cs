using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using UPC.HRNPCI.DesktopApplication.ViewModels.RutasAlmacenamiento;

namespace UPC.HRNPCI.DesktopApplication.Helpers
{
    public class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static Image CaptureDesktop()
        {
            return CaptureWindow(GetDesktopWindow(),2);
        }

        public static Bitmap CaptureActiveWindow(int mode)
        {
            return CaptureWindow(GetForegroundWindow(),mode);
        }

        public static Bitmap CaptureWindow(IntPtr handle,int mode)
        {

            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

            if (mode == 0)
                bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            else if (mode == 2) // reporte una grafica  (cruce de graficas de rodillas)
                //bounds = new Rectangle(rect.Left+10, rect.Top+ 195, rect.Right - rect.Left - 25, rect.Bottom - rect.Top - 233);
                bounds = new Rectangle(rect.Left+10, rect.Top+ 35, rect.Right - rect.Left - 25, rect.Bottom - rect.Top - 75);
            else if (mode == 1) // reporte 2 graficas   (graficas de rodillas por separado)
                //bounds = new Rectangle(rect.Left + 10, rect.Top + 195, rect.Right - rect.Left - 25, rect.Bottom - rect.Top - 380);
            bounds = new Rectangle(rect.Left + 10, rect.Top + 35, rect.Right - rect.Left - 25, rect.Bottom - rect.Top - 180);
            else if (mode == 3) // Foto de angulos
                //bounds = new Rectangle(rect.Left + 10, rect.Top + 195, rect.Right - rect.Left - 25, rect.Bottom - rect.Top - 380);
                bounds = new Rectangle(rect.Left + 80, rect.Top + 150, (rect.Right - rect.Left)/2 -55, rect.Bottom - rect.Top - 240);

          
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
            }

            return result;

            //var rect = new Rect();
            //GetWindowRect(handle, ref rect);
            //var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

            //var result = new Bitmap(bounds.Width, bounds.Height);

            //using (var graphics = Graphics.FromImage(result))
            //{
            //    graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            //}

            //return result;
        }

        public static void TomarFoto(int mode)
        {
            //System.Drawing.Image image = ScreenCapture.CaptureDesktop();
            System.Drawing.Image image = ScreenCapture.CaptureActiveWindow(mode);
            System.Drawing.Bitmap dImg = new System.Drawing.Bitmap(image);
            MemoryStream ms = new MemoryStream();
            dImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Windows.Media.Imaging.BitmapImage bImg = new System.Windows.Media.Imaging.BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(ms.ToArray());
            bImg.EndInit();
            

            if (bImg != null)
            {
                // create a png bitmap encoder which knows how to save a .png file
                BitmapEncoder encoder = new PngBitmapEncoder();

                // create frame from the writable bitmap and add to encoder
                encoder.Frames.Add(BitmapFrame.Create(bImg));

                string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

                //string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                string myPhotos = RutasAlmacenamientoStatic.strRutaFotos;

                if (myPhotos == null)
                {
                    MessageBox.Show("La ruta de almacenamiento aún no ha sido establecida, favor de comunicarlo al administrador del sistema");
                    return;
                }
                

                if (  myPhotos != "" )
                {

                    string path = System.IO.Path.Combine(myPhotos, "HRNPCI_foto-" + time + ".png");

                    // write the new file to disk
                    try
                    {
                        // FileStream is IDisposable
                        using (FileStream fs = new FileStream(path, FileMode.Create))
                        {
                            encoder.Save(fs);
                            //this.StatusText = string.Format(CultureInfo.CurrentCulture, Properties.Resources.SavedScreenshotStatusTextFormat, path);
                        }
                    }

                    catch (IOException ex)
                    {
                        //this.StatusText = string.Format(CultureInfo.CurrentCulture, Properties.Resources.FailedScreenshotStatusTextFormat, path);
                        MessageBox.Show("Error al guardar la foto");
                    }
                    MessageBox.Show("Se almacenó la captura de imagen en la ruta establecida por el administrador");
                }
                else 
                {
                    MessageBox.Show("La ruta de almacenamiento aún no ha sido establecida, favor de comunicarlo al administrador del sistema");
                }
            }
        }

        public static string TomarFotoObtenerRuta(int mode)
        {
            //System.Drawing.Image image = ScreenCapture.CaptureDesktop();
            System.Drawing.Image image = ScreenCapture.CaptureActiveWindow(mode);
            System.Drawing.Bitmap dImg = new System.Drawing.Bitmap(image);
            MemoryStream ms = new MemoryStream();
            dImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Windows.Media.Imaging.BitmapImage bImg = new System.Windows.Media.Imaging.BitmapImage();
            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(ms.ToArray());
            bImg.EndInit();


            if (bImg != null)
            {
                // create a png bitmap encoder which knows how to save a .png file
                BitmapEncoder encoder = new PngBitmapEncoder();

                // create frame from the writable bitmap and add to encoder
                encoder.Frames.Add(BitmapFrame.Create(bImg));

                string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

                //string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                string myPhotos = RutasAlmacenamientoStatic.strRutaFotos;

                string path = System.IO.Path.Combine(myPhotos, "KinectScreenshot-Infrared-" + time + ".png");

                // write the new file to disk
                try
                {
                    // FileStream is IDisposable
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        encoder.Save(fs);
                        return path;
                        //this.StatusText = string.Format(CultureInfo.CurrentCulture, Properties.Resources.SavedScreenshotStatusTextFormat, path);
                    }
                }

                catch (IOException ex)
                {
                    //this.StatusText = string.Format(CultureInfo.CurrentCulture, Properties.Resources.FailedScreenshotStatusTextFormat, path);
                    //MessageBox.Show("Error al guardar la foto");
                    return "ERROR";
                }
                
            }
            return "";
        }
    }
}
