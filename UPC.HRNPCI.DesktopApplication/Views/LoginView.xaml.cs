using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
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
using UPC.HRNPCI.Model.AdministradorModel;

using UPC.HRNPCI.Model;
using System.ComponentModel;


namespace UPC.HRNPCI.DesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        DispatcherTimer Timer;

        private readonly BackgroundWorker worker = new BackgroundWorker();
        string mensajeBuckup;
        bool blnUserAdminExist;

        public LoginView()
        {
            InitializeComponent();

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(300);
            Timer.Tick += new EventHandler(RefreshUIListaFisioterapuetas);
            Timer.Start();
            txtUsuario.Focus();
            Loading.Visibility = System.Windows.Visibility.Hidden;


            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            lblChangePassword.Visibility = System.Windows.Visibility.Hidden;
            btnCrearCuentaAdmin.Visibility = System.Windows.Visibility.Hidden;


            //Validate if admin user exist
            blnUserAdminExist = AdministradorDL.UserAdminExist();
            if (!blnUserAdminExist)
            {
                btnLogin.Visibility = System.Windows.Visibility.Hidden;
                btnCancel.Visibility = System.Windows.Visibility.Hidden;
                lblChangePassword.Visibility = System.Windows.Visibility.Visible;
                btnCrearCuentaAdmin.Visibility = System.Windows.Visibility.Visible;
            }



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

        private void pbContrasena_TextInput_1(object sender, TextCompositionEventArgs e) { }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Loading.Visibility = System.Windows.Visibility.Visible;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            mensajeBuckup = Class_MainConStr.UDF_MainCnStr();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(mensajeBuckup, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ValidarUsuario();
        }

        public bool ValidateFields(string Password, string Usuario)
        {
            if (Password == "" && Usuario == "")
            {
                System.Windows.Forms.MessageBox.Show("Debe ingresar un usuario y contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Usuario == "")
            {
                System.Windows.Forms.MessageBox.Show("Debe ingresar un usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Password == "")
            {
                System.Windows.Forms.MessageBox.Show("Debe ingresar una contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        private void ValidarUsuario()
        {
            try
            {
                var passwordBox = pbContrasena as PasswordBox;
                string Usuario = txtUsuario.Text;

                if (ValidateFields(passwordBox.Password, Usuario))
                {

                    int iStatusLogin = FisioterapeutaDL.ValidarUsuario(Usuario, passwordBox.Password);
                    switch (iStatusLogin)
                    {
                        case -2:
                            System.Windows.Forms.MessageBox.Show("Error de conxión a base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            break;
                        case -1:
                            System.Windows.Forms.MessageBox.Show("Usuario y/o contraseña incorrectas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 0:
                            //login fisoterapueta
                            this.Hide();

                            //if (MessageBoxResult.OK == MessageBox.Show("Simulación de ingreso a módulofisioterapeuta", " Rol Fisioterapeuta", MessageBoxButton.OK)) 
                            //{
                            //    FisioterapeutaStatic.kblnLoginExitoso = true;
                            //    this.Show();
                            //}

                            break;

                        case 1:

                            this.Hide();

                            break;
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void lblChangePassword_MouseDown(object sender, MouseButtonEventArgs e) { }

        private void btnCrearCuentaAdmin_Click(object sender, RoutedEventArgs e)
        {
            var passwordBox = pbContrasena as PasswordBox;

            if (ValidateFields(passwordBox.Password, txtUsuario.Text))
            {
                if (AdministradorDL.ActualizarAdministradorPassword(txtUsuario.Text, passwordBox.Password.ToString()))
                {
                    btnLogin.Visibility = System.Windows.Visibility.Visible;
                    btnCancel.Visibility = System.Windows.Visibility.Visible;
                    lblChangePassword.Visibility = System.Windows.Visibility.Hidden;
                    btnCrearCuentaAdmin.Visibility = System.Windows.Visibility.Hidden;

                }
            }
        }
    }
}
