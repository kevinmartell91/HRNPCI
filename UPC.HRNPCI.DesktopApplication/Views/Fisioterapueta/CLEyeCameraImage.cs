//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// This file is part of CL-EyeMulticam SDK
//
// WPF C# CLEyeMulticamWPFTest Sample Application
//
// It allows the use of multiple CL-Eye cameras in your own applications
//
// For updates and file downloads go to: http://codelaboratories.com
//
// Copyright 2008-2012 (c) Code Laboratories, Inc. All rights reserved.
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Controls;


using Emgu.CV.CvEnum;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV;

using System.IO;

namespace UPC.HRNPCI.DesktopApplication.Views.Fisioterapueta
{
    public class CLEyeCameraImage : Image, IDisposable
    {
        public CLEyeCameraDevice Device { get; private set; }

        public CLEyeCameraImage()
        {
            Device = new CLEyeCameraDevice();
            Device.BitmapReady += OnBitmapReady;
        }

        ~CLEyeCameraImage()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        void OnBitmapReady(object sender, EventArgs e)
        {
            Source = Device.BitmapSource;

            //System.Windows.Media.ImageSource imageSourceEmgu = testEmgu(Device.BitmapSource);
            //if (imageSourceEmgu != null)
            //    Source = imageSourceEmgu;

        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (Device != null)
                {
                    Device.Stop();
                    Device.Dispose();
                    Device = null;
                }
            }
            // free native resources if there are any.
        }

        #region [ Dependency Properties ]
        public float Framerate
        {
            get { return (float)GetValue(FramerateProperty); }
            set { SetValue(FramerateProperty, value); }
        }
        public static readonly DependencyProperty FramerateProperty =
            DependencyProperty.Register("Framerate", typeof(float), typeof(CLEyeCameraImage),
            new UIPropertyMetadata((float)15, (PropertyChangedCallback)delegate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
            {
                CLEyeCameraImage typedSender = sender as CLEyeCameraImage;
                typedSender.Device.Framerate = (float)e.NewValue;
            }));

        public CLEyeCameraColorMode ColorMode
        {
            get { return (CLEyeCameraColorMode)GetValue(ColorModeProperty); }
            set { SetValue(ColorModeProperty, value); }
        }
        public static readonly DependencyProperty ColorModeProperty =
            DependencyProperty.Register("ColorMode", typeof(CLEyeCameraColorMode), typeof(CLEyeCameraImage),
            new UIPropertyMetadata(default(CLEyeCameraColorMode), (PropertyChangedCallback)delegate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
            {
                CLEyeCameraImage typedSender = sender as CLEyeCameraImage;
                typedSender.Device.ColorMode = (CLEyeCameraColorMode)e.NewValue;
            }));

        public CLEyeCameraResolution Resolution
        {
            get { return (CLEyeCameraResolution)GetValue(ResolutionProperty); }
            set { SetValue(ResolutionProperty, value); }
        }
        public static readonly DependencyProperty ResolutionProperty =
            DependencyProperty.Register("Resolution", typeof(CLEyeCameraResolution), typeof(CLEyeCameraImage),
            new UIPropertyMetadata(default(CLEyeCameraResolution), (PropertyChangedCallback)delegate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
            {
                CLEyeCameraImage typedSender = sender as CLEyeCameraImage;
                typedSender.Device.Resolution = (CLEyeCameraResolution)e.NewValue;
            }));
        #endregion

        #region Methods added

        public System.Windows.Media.ImageSource testEmgu(System.Windows.Interop.InteropBitmap interopBitmap)
        {

            Image<Bgr, Byte> img = new Image<Bgr, byte>(new System.Drawing.Bitmap(ToBitmap(interopBitmap)));

            //Image<Bgr, Byte> img = new Image<Bgr, byte>(new System.Drawing.Bitmap((System.Drawing.Bitmap)interopBitmap));

            return ToBitmapSource(img);
        }



        public System.Drawing.Bitmap ToBitmap(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                // from System.Media.BitmapImage to System.Drawing.Bitmap
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
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
            using (System.Drawing.Bitmap source = image.Bitmap)
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
    }
}
