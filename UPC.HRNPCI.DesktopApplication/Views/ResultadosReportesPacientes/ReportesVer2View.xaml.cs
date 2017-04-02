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
using System.Windows.Shapes;
using System.Globalization;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;
using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes;
//using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes.Helpers;
using UPC.HRNPCI.Model.ResultadosReportesPaciente;

namespace UPC.HRNPCI.DesktopApplication.Views.ResultadosReportesPacientes
{
    /// <summary>
    /// Interaction logic for ReportesVer2View.xaml
    /// </summary>
    public partial class ReportesVer2View : Window
    {

        List<double> lstAnglesUno = null;

        List<double> lstAnglesDos = null;

        #region Declaration Bezier curves

        /// <summary>
        /// Curve Functions Names
        /// </summary>
        public enum CurveNames
        {
            /// <summary>
            /// Sinus curve, <see cref="ReportesVerView.Sinus"/>
            /// </summary>
            Sinus,
            /// <summary>
            /// Runge curve, <see cref="ReportesVerView.Runge"/>
            /// </summary>
            Runge,
            /// <summary>
            /// Arc curve, <see cref="ReportesVerView.Arc"/>
            /// </summary>
            Arc,
        }



        public enum CurveNames2
        {
            /// <summary>
            /// Sinus curve, <see cref="ReportesVerView.Sinus"/>
            /// </summary>
            Sinus2,
            /// <summary>
            /// Runge curve, <see cref="ReportesVerView.Runge"/>
            /// </summary>
            Runge2,
            /// <summary>
            /// Arc curve, <see cref="ReportesVerView.Arc"/>
            /// </summary>
            Arc2,
        }

        #endregion

        public ReportesVer2View()
        {
            InitializeComponent();
            lstAnglesDos = new List<double>();
            lstAnglesUno = new List<double>();
        }

        #region Methods of Bezier curves Canvas 1  RED LINE

        #region DependencyProperties

        #region Curve
        /// <summary>
        /// Identifies the Curve dependency property.
        /// </summary>
        public static readonly DependencyProperty CurveProperty
            = DependencyProperty.Register("CurveReportCrossed", typeof(CurveNames), typeof(ResultadosVerView)
                , new FrameworkPropertyMetadata(CurveNames.Sinus
                    , FrameworkPropertyMetadataOptions.AffectsMeasure
                        | FrameworkPropertyMetadataOptions.AffectsRender)
                , validateCurve);
        /// <summary>
        /// Gets or sets the Curve property.
        /// </summary>
        /// <value>CurveNames value</value>
        public CurveNames CurveReport
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
            = DependencyProperty.Register("PointCountReportCrossed", typeof(int), typeof(ReportesVerView)
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
            = DependencyProperty.Register("ScaleXReportCrossed", typeof(double), typeof(ReportesVerView)
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
            = DependencyProperty.Register("ScaleYReportCrossed", typeof(double), typeof(ReportesVerView)
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
            switch (CurveReport)
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
            switch (CurveReport2)
            {
                case CurveNames2.Sinus2:
                    DrawCurve2(Sinus2);
                    break;
                case CurveNames2.Runge2:
                    DrawCurve2(Runge);
                    break;
                case CurveNames2.Arc2:
                    DrawCurve2(Arc);
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

            #region Grafica 1 - Red

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
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path() { Stroke = System.Windows.Media.Brushes.Red, StrokeThickness = 1.4, Data = g };
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
            #endregion

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

            lstAnglesUno = FisioterapeutaStatic.getAngles(1);
            PointCount = lstAnglesUno.Count;

            System.Windows.Point[] points = new System.Windows.Point[PointCount];
            double step = 3.766 / (PointCount - 1);

            for (int i = 0; i < PointCount; ++i)
            {
                //points[i] = new System.Windows.Point(ScaleX * i * step, ScaleY * (1 - Math.Sin(i * step)));
                points[i] = new System.Windows.Point((ScaleX * i * step) + x_origin, (ScaleY * lstAnglesUno[i] * (-1)) + y_origin);
            }




            return points;
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

        #region Methods of Bezier curves Canvas 2 BLUE LINE

        #region DependencyProperties

        #region Curve
        /// <summary>
        /// Identifies the Curve dependency property.
        /// </summary>
        public static readonly DependencyProperty CurveProperty2
            = DependencyProperty.Register("CurveReport2Crossed", typeof(CurveNames2), typeof(ResultadosVerView)
                , new FrameworkPropertyMetadata(CurveNames2.Sinus2
                    , FrameworkPropertyMetadataOptions.AffectsMeasure
                        | FrameworkPropertyMetadataOptions.AffectsRender)
                , validateCurve2);
        /// <summary>
        /// Gets or sets the Curve property.
        /// </summary>
        /// <value>CurveNames value</value>
        public CurveNames2 CurveReport2
        {
            get { return (CurveNames2)GetValue(CurveProperty2); }
            set { SetValue(CurveProperty2, value); }
        }
        /// <summary>
        /// Validates the proposed Curve property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool validateCurve2(object value)
        {
            CurveNames2 cName = (CurveNames2)value;
            foreach (CurveNames2 item in Enum.GetValues(typeof(CurveNames2)))
            {
                if (item == cName)
                    return true;
            }
            return false;
        }
        #endregion Curve

        #region PointCount2
        /// <summary>
        /// Identifies the PointCount2 dependency property.
        /// </summary>
        public static readonly DependencyProperty PointCountProperty2
            = DependencyProperty.Register("PointCountReport2Crossed", typeof(int), typeof(ReportesVerView)
                , new FrameworkPropertyMetadata(2
                    , FrameworkPropertyMetadataOptions.AffectsMeasure
                        | FrameworkPropertyMetadataOptions.AffectsRender)
                , validatePointCount2);
        /// <summary>
        /// Gets or sets the PointCount2 property.
        /// </summary>
        /// <value>integer > 1</value>
        public int PointCount2
        {
            get { return (int)GetValue(PointCountProperty2); }
            set { SetValue(PointCountProperty2, value); }
        }
        /// <summary>
        /// Validates the proposed PointCount2 property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool validatePointCount2(object value)
        {
            int cnt = (int)value;
            return (cnt > 1 ? true : false);
        }
        #endregion PointCount2

        #region ScaleX2
        /// <summary>
        /// Identifies the ScaleX dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleXProperty2
            = DependencyProperty.Register("ScaleXReport2Crossed", typeof(double), typeof(ReportesVerView)
                , new FrameworkPropertyMetadata(100.0
                    , FrameworkPropertyMetadataOptions.AffectsMeasure
                        | FrameworkPropertyMetadataOptions.AffectsRender)
                , validateScaleX2);
        /// <summary>
        /// Gets or sets the ScaleX property.
        /// </summary>
        /// <value>double >= 1</value>
        public double ScaleX2
        {
            get { return (double)GetValue(ScaleXProperty2); }
            set { SetValue(ScaleXProperty2, value); }
        }
        /// <summary>
        /// Validates the proposed ScaleX property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool validateScaleX2(object value)
        {
            double scale = (double)value;
            return (scale >= 1.0 ? true : false);
        }
        #endregion ScaleX2

        #region ScaleY2
        /// <summary>
        /// Identifies the ScaleY dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleYProperty2
            = DependencyProperty.Register("ScaleYReport2Crossed", typeof(double), typeof(ReportesVerView)
                , new FrameworkPropertyMetadata(3.12
                    , FrameworkPropertyMetadataOptions.AffectsMeasure
                        | FrameworkPropertyMetadataOptions.AffectsRender)
                , validateScaleY2);
        /// <summary>
        /// Gets or sets the ScaleY property.
        /// </summary>
        /// <value>double >= 1</value>
        public double ScaleY2
        {
            get { return (double)GetValue(ScaleYProperty2); }
            set { SetValue(ScaleYProperty2, value); }
        }
        /// <summary>
        /// Validates the proposed ScaleY property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool validateScaleY2(object value)
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
        //protected override void OnRender2(DrawingContext drawingContext)
        //{
        //    switch (CurveReport2)
        //    {
        //        case CurveNames2.Sinus2:
        //            DrawCurve2(Sinus2);
        //            break;
        //        case CurveNames2.Runge2:
        //            DrawCurve2(Runge);
        //            break;
        //        case CurveNames2.Arc2:
        //            DrawCurve2(Arc);
        //            break;
        //    }
        //    base.OnRender(drawingContext);
        //}

        /// <summary>
        /// Function2 points provider
        /// </summary>
        delegate System.Windows.Point[] Function2();

        /// <summary>
        /// Draw the Curve.
        /// </summary>
        /// <param name="curve">The curve to draw.</param>
        void DrawCurve2(Function2 curve)
        {
            canvas2.Children.Clear();

            #region Grafica 2 - Blue

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
                canvas2.Children.Add(rect);
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
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path() { Stroke = System.Windows.Media.Brushes.Blue, StrokeThickness = 1.4, Data = g };
            canvas2.Children.Add(path);

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
                canvas2.Children.Add(marker);

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
                canvas2.Children.Add(marker);
            }

            // Print points
            //Trace.WriteLine(string.Format("Start=({0})", points[0]));
            //for (int i = 0; i < cp1.Length; ++i)
            //{
            //    Trace.WriteLine(string.Format("CP1=({0}) CP2=({1}) Stop=({2})"
            //        , cp1[i], cp2[i], points[i + 1]));
            //}
            #endregion

        }

        #region Curves
        /// <summary>
        /// Sinus2 points in [0,2PI].
        /// </summary>
        /// <returns></returns>
        System.Windows.Point[] Sinus2()
        {
            // Fill point array with scaled in X,Y Sin values in [0, PI].
            //PointCount2 = 0;
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

            lstAnglesDos = FisioterapeutaStatic.getAngles(2);
            PointCount2 = lstAnglesDos.Count;

            System.Windows.Point[] points = new System.Windows.Point[PointCount2];
            double step = 3.766 / (PointCount2 - 1);

            for (int i = 0; i < PointCount2; ++i)
            {
                //points[i] = new System.Windows.Point(ScaleX * i * step, ScaleY * (1 - Math.Sin(i * step)));
                points[i] = new System.Windows.Point((ScaleX * i * step) + x_origin, (ScaleY * lstAnglesDos[i] * (-1)) + y_origin);
            }




            return points;
        }

        /// <summary>
        /// Runge function points in [-1,1].
        /// </summary>
        /// <returns></returns>
        System.Windows.Point[] Runge2()
        {
            // Fill point array with scaled in X,Y Runge (1 / (1 + 25 * x * x)) values in [-1, 1].
            System.Windows.Point[] points = new System.Windows.Point[PointCount2];
            double step = 2.0 / (PointCount2 - 1);
            for (int i = 0; i < PointCount2; ++i)
            {
                double x = -1 + i * step;
                points[i] = new System.Windows.Point(ScaleX2 * (x + 1), ScaleY2 * (1 - 1 / (1 + 25 * x * x)));
            }
            return points;
        }

        /// <summary>
        /// Arc curve points in [0º,270º], radius=1.
        /// </summary>
        /// <returns></returns>
        System.Windows.Point[] Arc2()
        {
            // Fill point array with scaled in X,Y Arc values in [0º,270º].
            System.Windows.Point[] points = new System.Windows.Point[PointCount2];
            double step = 270.0 / (PointCount2 - 1);
            for (int i = 0; i < PointCount2; ++i)
            {
                double x = Math.PI * (0 + i * step) / 180;
                points[i] = new System.Windows.Point(ScaleX2 * (1 + Math.Cos(x)), ScaleY2 * (1 + Math.Sin(x)));
            }
            return points;
        }

        #endregion Curves

       

        #endregion

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void btnExportarPDF_Click(object sender, RoutedEventArgs e)
        {
            if (ResultadosPacientesReportesStatic.blnRepotePDF)
            {
                string strRutaAlmacenamientoPDF = ReportPDF.ToPdf(2);
                if (strRutaAlmacenamientoPDF.Substring(0, 5) == "ERROR" || strRutaAlmacenamientoPDF == "")
                {
                    ResultadosPacientesReportesStatic.strRutaPDF = "";

                    MessageBox.Show(strRutaAlmacenamientoPDF.Substring(8, strRutaAlmacenamientoPDF.Length-8));
                }
                else
                {
                    ResultadosPacientesReportesStatic.strRutaPDF = strRutaAlmacenamientoPDF;
                    if (ReporteDL.GuardarRutaPDF(ResultadosPacientesReportesStatic.iCodeReporte, strRutaAlmacenamientoPDF))
                    {
                        MessageBox.Show("Se generó con éxito el PDF");
                        ResultadosPacientesReportesStatic.blnRepotePDF = false;
                        ResultadosPacientesReportesStatic.ViewReport1 = null;
                    }
                }
               

            }

        }
    }
}
