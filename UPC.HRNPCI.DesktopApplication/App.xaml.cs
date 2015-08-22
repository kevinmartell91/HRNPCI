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
            window.Show();

            BootStrapper.Initialize();
        }
    }
}
