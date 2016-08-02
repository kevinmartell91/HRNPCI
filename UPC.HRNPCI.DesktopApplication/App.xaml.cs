using System.Windows;
using UPC.HRNPCI.DesktopApplication.Views;

namespace UPC.HRNPCI.DesktopApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {


            base.OnStartup(e);

            LoginView window = new LoginView();
            //ModuloFisioterapeutaView window = new ModuloFisioterapeutaView();
            //ModuloAdminView window = new ModuloAdminView();

            window.Show();

            BootStrapper.Initialize();
        }
    }
}
