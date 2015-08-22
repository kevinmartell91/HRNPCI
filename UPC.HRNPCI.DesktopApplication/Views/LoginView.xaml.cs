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
using System.Windows.Threading;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;
using UPC.HRNPCI.Model.FisioterapeutaModel;


namespace UPC.HRNPCI.DesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        DispatcherTimer Timer;

        public LoginView()
        {
            InitializeComponent();

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(300);
            Timer.Tick += new EventHandler(RefreshUIListaFisioterapuetas);
            Timer.Start();
            txtUsuario.Focus();
        }
        public void RefreshUIListaFisioterapuetas(object sender, EventArgs e)
        {
            if (FisioterapeutaStatic.kblnLoginExitoso)
            {
                FisioterapeutaStatic.kblnLoginExitoso = false;
                
                pbContrasena.Password = "";
                txtUsuario.Text = "";
                this.Show();
            }
        }

        private void pbContrasena_TextInput_1(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //if (FisioterapeutaStatic.kblnLoginExitoso)
            //{
            //    this.Hide();
            //}
            ValidarUsuario();
        }

        private void ValidarUsuario()
        {
            var passwordBox = pbContrasena as PasswordBox;
            string Usuario = txtUsuario.Text;

            if (passwordBox.Password == "" && Usuario == "")
            {
                MessageBox.Show("Debe ingresar un usuario y contraseña");
                return;
            }
            if (Usuario == "")
            {
                MessageBox.Show("Debe ingresar un usuario.");
                return;
            }
            if (passwordBox.Password == "")
            {
                MessageBox.Show("Debe ingresar una contraseña");
                return;
            }

            int iStatusLogin = FisioterapeutaDL.ValidarUsuario(Usuario, passwordBox.Password);
            switch (iStatusLogin)
            {
                case -1:
                    MessageBox.Show("Usuario y/o contraseña incorrectas");
                    break;
                case 0:
                    //login fisoterapueta
                    this.Hide();

                    if (MessageBoxResult.OK == MessageBox.Show("Simulación de ingreso a módulofisioterapeuta", " Rol Fisioterapeuta", MessageBoxButton.OK)) ;
                    {
                        FisioterapeutaStatic.kblnLoginExitoso = true;
                        this.Show();
                    }

                    break;

                case 1:

                    this.Hide();

                    break;
            }


        }
    }
}
