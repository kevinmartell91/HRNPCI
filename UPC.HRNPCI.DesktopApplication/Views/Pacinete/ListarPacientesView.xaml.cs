using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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
using UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete;

namespace UPC.HRNPCI.DesktopApplication.Views.Pacinete
{
    /// <summary>
    /// Interaction logic for ListarPacientesView.xaml
    /// </summary>
    public partial class ListarPacientesView : UserControl
    {
        DispatcherTimer Timer;
        Visibility vbtShow;
        List<bool> lstBlnColumnasSeleccionadas { get; set; }

        public ListarPacientesView()
        {
            InitializeComponent();
      
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(500);
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Start();

            this.lstBlnColumnasSeleccionadas = new List<bool>();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            this.lstBlnColumnasSeleccionadas = PacienteStatic.lstblnColumnasSeleccionadas;
            
            if (this.lstBlnColumnasSeleccionadas != null)
            {
                for (int i = 0; i < lstBlnColumnasSeleccionadas.Count; i++)
                {
                    if (!lstBlnColumnasSeleccionadas[i])
                        vbtShow = Visibility.Visible;
                    else
                        vbtShow = Visibility.Collapsed;


                    ((GridViewColumnExt)GridViewColumnsPaciente.Columns[i]).Visibility = vbtShow;
                    //((GridViewColumnExt)GridViewColumnsPaciente.Columns[i]).Header = "dsadsadsa";
                    //((GridViewColumnExt)GridViewColumnsPaciente.Columns[i]).Width = 150;
                    //((GridViewColumnExt)GridViewColumnsPaciente.Columns[i]).DisplayMemberBinding = new Binding("")
                }
            }
        }
       
    }
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
}
