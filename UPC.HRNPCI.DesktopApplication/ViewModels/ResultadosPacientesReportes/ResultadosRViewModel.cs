using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UPC.HRNPCI.DesktopApplication;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.FisioterapeutaModel;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using UPC.HRNPCI.Model.PacienteModel;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ResultadosRViewModel : BaseViewModel
    {
        #region Declaraciones

        //FisioterapeutaBusinessObject businesssObject = null;


        public int iCodigoResultado { get; set; }
        public int iCodigoPaciente { get; set; }
        public int iCodigoPlano { get; set; }
        public int iCodigoLateralidad { get; set; }
        public int iCodigoUnidad { get; set; }
        public List<double> strListaAngulos { get; set; }
        public string strFecAnalisisPaciente { get; set; }
        public string strNombresPaciente { get; set; }
        public string strApellidosPaciente { get; set; }
        public string strNombrePlano { get; set; }
        public string strNombreLateralidad { get; set; }
        public string strNombreUnidad { get; set; }

        public bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }

        //private bool blCheckBoxSelected { get; set; }
       
        public Button btnVer;
        public CheckBox cbxSeleccion;


        public RelayCommand VerResultadoCommand { get; set; }
        public RelayCommand PressedCheckBoxCommand { get; set; }


        #endregion

         #region Metodos

        public ResultadosRViewModel()
        {

            btnVer = new Button();
            btnVer.Command = VerResultadoCommand;
            

            cbxSeleccion = new CheckBox();
            isChecked = false;
            cbxSeleccion.IsChecked = isChecked;
            cbxSeleccion.Command = PressedCheckBoxCommand;


            VerResultadoCommand = new RelayCommand(VerResultadoDialog);
            PressedCheckBoxCommand = new RelayCommand(PressedCheckBoxChanged);

        }

        private void VerResultadoDialog(object parameter)
        {
            ResultadosVerViewModel fvm = new ResultadosVerViewModel();
            PacienteB paciente = PacienteDL.VerPaciente(iCodigoPaciente);

            fvm.strNombresPaciente = paciente.vNombresPaciente + " " + paciente.vApellidosPaciente ;
            fvm.strApellidosPaciente = paciente.vApellidosPaciente;
            string strNivel = "";
            if (paciente.iNivelPaciente == 1)
                strNivel = "I";
            else if (paciente.iNivelPaciente == 2)
                strNivel = "II";
            else if (paciente.iNivelPaciente == 3)
                strNivel = "III";
            else if (paciente.iNivelPaciente == 4)
                strNivel = "IV";
            else if (paciente.iNivelPaciente == 5)
                strNivel = "V";

            fvm.strNivel = strNivel;
            fvm.strNivelPorcentaje = paciente.iPorcentajeNivelPaciente.ToString();
            fvm.strNombreLateralidad = strNombreLateralidad;
            fvm.strNombreUnidad = strNombreUnidad;
            fvm.strFecAnalisisPaciente = strFecAnalisisPaciente;
            FisioterapeutaStatic.setAngles(strListaAngulos,1);
            FisioterapeutaStatic.strLateralidad = strNombreLateralidad;
            fvm.strListaAngulos = strListaAngulos;


            IModalDialog dialog = ServiceProvider.Instance9.Get<IModalDialog>();

            dialog.BindViewModel(fvm);
            dialog.ShowDialog();
        }
       
        private void PressedCheckBoxChanged(object parameter)
        {
            if (!isChecked)
            {
                cbxSeleccion.IsChecked = false;
                isChecked = false;
            }
            else
            {
                isChecked = true;
                cbxSeleccion.IsChecked = true;
            }

            //MessageBox.Show(isChecked.ToString());
        }


        public Fisioterapeuta fiosoterapeutaBean()
        {
            //Fisioterapeuta f = new Fisioterapeuta();
            //f.iCodigoFisioterapeuta = Codigo;
            //f.vNombresFisioterapeuta = Nombre;
            //f.vApellidosFisioterapeuta = Apellidos;
            //f.vCelularFisioterapeuta = Celular;
            //f.vTelefonoFisioterapeuta = Telefono;
            //f.vCentLabFisioterapeuta = CentLaboral;
            //f.vEmailFisioterapeuta = Email;
            //f.vNumCTMPFisioterapeuta = NCTMP;
            //f.vNumNDTAFisioterapeuta = NNDTA;
            //f.vRolFisioterapeuta = Rol;
            //f.cGenero = Sexo;
            //f.vUrlFotoFosioterapeuta = UrlFoto;
            //f.iFlagBorradoFisioterapeuta = Borrado;
            //f.vContrasenaFisioterapeuta = Contrasena;
            //f.vUsuarioFiosioterapeuta = Usuario;
            //return f;
            return null;

        }

        #endregion

    }
}
