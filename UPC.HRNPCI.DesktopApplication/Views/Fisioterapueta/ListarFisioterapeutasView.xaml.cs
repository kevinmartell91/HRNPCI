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
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;


namespace UPC.HRNPCI.DesktopApplication.Views.Fisioterapueta
{
    /// <summary>
    /// Interaction logic for ListarFisioterapeutasView.xaml
    /// </summary>
    public partial class ListarFisioterapeutasView : UserControl
    {
        DispatcherTimer Timer;
        bool ShowStatusColumn;
        Visibility show;
        List<bool>ListaColumnasSeleccionadas{get;set;}

        public ListarFisioterapeutasView()
        {
            InitializeComponent();
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(500);
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Start();

            this.ListaColumnasSeleccionadas = new List<bool>();

        }

        void Timer_Tick(object sender, EventArgs e)
        {
            this.ListaColumnasSeleccionadas = FisioterapeutaStatic.ListaColumnasSeleccionadas;
            if (this.ListaColumnasSeleccionadas != null)
            {
                for (int i = 0; i < ListaColumnasSeleccionadas.Count; i++)
                {
                    if (!ListaColumnasSeleccionadas[i])
                        show = Visibility.Visible;
                    else
                        show = Visibility.Collapsed;

                    ((GridViewColumnExt)GridViewColumns.Columns[i]).Visibility = show;
                }
            }
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }


    }
    //class GridViewColumnVisibilityManager
    //{
    //    static void UpdateListView(ListView lv)
    //    {
    //        GridView gridview = lv.View as GridView;
    //        if (gridview == null || gridview.Columns == null) return;
    //        List<GridViewColumn> toRemove = new List<GridViewColumn>();
    //        foreach (GridViewColumn gc in gridview.Columns)
    //        {
    //            if (GetIsVisible(gc) == false)
    //            {
    //                toRemove.Add(gc);
    //            }
    //        }
    //        foreach (GridViewColumn gc in toRemove)
    //        {
    //            gridview.Columns.Remove(gc);
    //        }
    //    }

    //    public static bool GetIsVisible(DependencyObject obj)
    //    {
    //        return (bool)obj.GetValue(IsVisibleProperty);
    //    }

    //    public static void SetIsVisible(DependencyObject obj, bool value)
    //    {
    //        obj.SetValue(IsVisibleProperty, value);
    //    }

    //    public static readonly DependencyProperty IsVisibleProperty =
    //        DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(GridViewColumnVisibilityManager), new UIPropertyMetadata(true));


    //    public static bool GetEnabled(DependencyObject obj)
    //    {
    //        return (bool)obj.GetValue(EnabledProperty);
    //    }

    //    public static void SetEnabled(DependencyObject obj, bool value)
    //    {
    //        obj.SetValue(EnabledProperty, value);
    //    }

    //    public static readonly DependencyProperty EnabledProperty =
    //        DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(GridViewColumnVisibilityManager), new UIPropertyMetadata(false,
    //            new PropertyChangedCallback(OnEnabledChanged)));

    //    private static void OnEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    //    {
    //        ListView view = obj as ListView;
    //        if (view != null)
    //        {
    //            bool enabled = (bool)e.NewValue;
    //            if (enabled)
    //            {
    //                view.Loaded += (sender, e2) =>
    //                {
    //                    UpdateListView((ListView)sender);
    //                };
    //                view.TargetUpdated += (sender, e2) =>
    //                {
    //                    UpdateListView((ListView)sender);
    //                };
    //                view.DataContextChanged += (sender, e2) =>
    //                {
    //                    UpdateListView((ListView)sender);
    //                };
    //            }
    //        }
    //    }
    //}

    public class GridViewColumnExt : GridViewColumn
    {
        public Visibility Visibility
        {
            get
            {
                return (Visibility)GetValue(VisibilityProperty);
            }
            set
            {
                SetValue(VisibilityProperty, value);
            }
        }

        public static readonly DependencyProperty VisibilityProperty =
            DependencyProperty.Register("Visibility", typeof(Visibility),
            typeof(GridViewColumnExt),
            new FrameworkPropertyMetadata(Visibility.Visible,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            new PropertyChangedCallback(OnVisibilityPropertyChanged)));

        private static void OnVisibilityPropertyChanged(DependencyObject d,
                                      DependencyPropertyChangedEventArgs e)
        {
            var column = d as GridViewColumnExt;
            if (column != null)
            {
                column.OnVisibilityChanged((Visibility)e.NewValue);
            }
        }

        private void OnVisibilityChanged(Visibility visibility)
        {
            if (visibility == Visibility.Visible)
            {
                Width = _visibleWidth;
            }
            else
            {
                _visibleWidth = Width;
                Width = 0.0;
            }
        }

        double _visibleWidth;

    }

    //public class ColumnDefinitionExtended : ColumnDefinition
    //{
    //    // Variables
    //    public static DependencyProperty VisibleProperty;

    //    // Properties
    //    public Boolean Visible
    //    {
    //        get { return (Boolean)GetValue(VisibleProperty); }
    //        set { SetValue(VisibleProperty, value); }
    //    }

    //    // Constructors
    //    static ColumnDefinitionExtended()
    //    {
    //        VisibleProperty = DependencyProperty.Register("Visible",
    //            typeof(Boolean),
    //            typeof(ColumnDefinitionExtended),
    //            new PropertyMetadata(true, new PropertyChangedCallback(OnVisibleChanged)));

    //        ColumnDefinition.WidthProperty.OverrideMetadata(typeof(ColumnDefinitionExtended),
    //            new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null,
    //                new CoerceValueCallback(CoerceWidth)));

    //        ColumnDefinition.MinWidthProperty.OverrideMetadata(typeof(ColumnDefinitionExtended),
    //            new FrameworkPropertyMetadata((Double)0, null,
    //                new CoerceValueCallback(CoerceMinWidth)));
    //    }

    //    // Get/Set
    //    public static void SetVisible(DependencyObject obj, Boolean nVisible)
    //    {
    //        obj.SetValue(VisibleProperty, nVisible);
    //    }
    //    public static Boolean GetVisible(DependencyObject obj)
    //    {
    //        return (Boolean)obj.GetValue(VisibleProperty);
    //    }

    //    static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    //    {
    //        obj.CoerceValue(ColumnDefinition.WidthProperty);
    //        obj.CoerceValue(ColumnDefinition.MinWidthProperty);
    //    }
    //    static Object CoerceWidth(DependencyObject obj, Object nValue)
    //    {
    //        return (((ColumnDefinitionExtended)obj).Visible) ? nValue : new GridLength(0);
    //    }
    //    static Object CoerceMinWidth(DependencyObject obj, Object nValue)
    //    {
    //        return (((ColumnDefinitionExtended)obj).Visible) ? nValue : (Double)0;
    //    }
    //}
}
