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
using UPC.HRNPCI.DesktopApplication.ViewModels.RutasAlmacenamiento;
//using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes.Helpers;
using UPC.HRNPCI.Model.ResultadosReportesPaciente;


namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ReportesCRViewModel : BaseViewModel
    {
        #region Declaraciones

        //FisioterapeutaBusinessObject businesssObject = null;

        public int iCodigoReporte { get; set; }
        public int iCodigoPaciente { get; set; }
        public int iCodigoDetalleReporte { get; set; }
        public int iCodigoTipoReporte { get; set; }
        public string strNombreTipoReporte { get; set; }
        public string strFecReportePaciente { get; set; }
        public string strNombresPaciente { get; set; }
        public string strApellidosPaciente { get; set; }
        public List<double> strListaAngulosUno { get; set; } //Derecho
        public List<double> strListaAngulosDos { get; set; } //Izquierdo
        public string strFecResultadoUno { get; set; }
        public string strFecResultadoDos { get; set; }
        public string strUnidadPaciente { get; set; }
        public string strLateralidadPaciente { get; set; }

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

        public Button btnVer;
        public CheckBox cbxSeleccion;
        public Button btnImportPDF;


        public RelayCommand VerReporteCommand { get; set; }
        public RelayCommand PressedCheckBoxReporteCommand { get; set; }
        public RelayCommand ImportPDFCommand { get; set; }


        #endregion


        #region Metodos

        public ReportesCRViewModel()
        {

            btnVer = new Button();
            btnVer.Command = VerReporteCommand;

            btnImportPDF = new Button();
            btnImportPDF.Command = ImportPDFCommand;

            cbxSeleccion = new CheckBox();
            isChecked = false;
            cbxSeleccion.IsChecked = isChecked;
            cbxSeleccion.Command = PressedCheckBoxReporteCommand;


            VerReporteCommand = new RelayCommand(VerReporteDialog);
            PressedCheckBoxReporteCommand = new RelayCommand(PressedCheckBoxReporteChanged);
            ImportPDFCommand = new RelayCommand(ImportPDFAction);



        }

        private void PressedCheckBoxReporteChanged(object parameter)
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

        private void VerReporteDialog(object parameter)
        {

            switch (iCodigoTipoReporte)
            {
                case 1:// Reporte rodiallas separadas
                    ReportesVerViewModel rvvm = new ReportesVerViewModel();

                    PacienteB paciente = PacienteDL.VerPaciente(iCodigoPaciente);

                    rvvm.strNombresPaciente = paciente.vNombresPaciente + " " + paciente.vApellidosPaciente;
                    rvvm.strApellidosPaciente = paciente.vApellidosPaciente;
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

                    rvvm.strNivel = strNivel;
                    rvvm.strNivelPorcentaje = paciente.iPorcentajeNivelPaciente.ToString();
                    //rvvm.strNombreLateralidad = strNombreLateralidad;
                    //rvvm.strNombreUnidad = strNombreUnidad;
                    //rvvm.strFecAnalisisPaciente = ((DateTime)paciente.daFecNacPaciente).Date.ToString();
                    FisioterapeutaStatic.setAngles(strListaAngulosUno, 1);
                    FisioterapeutaStatic.setAngles(strListaAngulosDos, 2);
                    //rvvm.strListaAngulos = strListaAngulos;


                    ResultadosPacientesReportesStatic.blnRepotePDF = true;
                    ResultadosPacientesReportesStatic.ViewReport1 = rvvm;

                    ResultadosPacientesReportesStatic.strNombrePaciente = rvvm.strApellidosPaciente;
                    ResultadosPacientesReportesStatic.iCodeReporte = iCodigoReporte;
                    ResultadosPacientesReportesStatic.strRutaPDF = "";
                    ResultadosPacientesReportesStatic.strNivel = strNivel;
                    ResultadosPacientesReportesStatic.strPorcentajeNivel = paciente.iPorcentajeNivelPaciente.ToString();


                    //fvm.SetFisioterapeutaSelecionado(fiosoterapeutaBean());
                    IModalDialog dialog1 = ServiceProvider.Instance10.Get<IModalDialog>();

                    dialog1.BindViewModel(rvvm);
                    dialog1.ShowDialog();
                    break;

                case 2:// Reporte rodiallas junstas en una grafica
                    ReportesVer2ViewModel rv2vm = new ReportesVer2ViewModel();

                    PacienteB paciente2 = PacienteDL.VerPaciente(iCodigoPaciente);

                    rv2vm.strNombresPaciente = paciente2.vNombresPaciente + " " + paciente2.vApellidosPaciente;
                    rv2vm.strApellidosPaciente = paciente2.vApellidosPaciente;
                    string strNivel2 = "";
                    if (paciente2.iNivelPaciente == 1)
                        strNivel2 = "I";
                    else if (paciente2.iNivelPaciente == 2)
                        strNivel2 = "II";
                    else if (paciente2.iNivelPaciente == 3)
                        strNivel2 = "III";
                    else if (paciente2.iNivelPaciente == 4)
                        strNivel2 = "IV";
                    else if (paciente2.iNivelPaciente == 5)
                        strNivel2 = "V";

                    rv2vm.strNivel = strNivel2;
                    rv2vm.strNivelPorcentaje = paciente2.iPorcentajeNivelPaciente.ToString();
                    rv2vm.strNombreLateralidadDerecha = strLateralidadPaciente;
                    rv2vm.strNombreUnidad = strUnidadPaciente;
                    rv2vm.strFecAnalisisPaciente = strFecReportePaciente;
                    rv2vm.strFecResultadoUno = strFecResultadoUno;
                    rv2vm.strFecResultadoDos = strFecResultadoDos;
                    FisioterapeutaStatic.setAngles(strListaAngulosUno, 1);
                    FisioterapeutaStatic.setAngles(strListaAngulosDos, 2);
                    //rv2vm.strListaAngulos = strListaAngulos;


                    ResultadosPacientesReportesStatic.blnRepotePDF = true;
                    ResultadosPacientesReportesStatic.ViewReport2 = rv2vm;


                    ResultadosPacientesReportesStatic.strNombrePaciente = rv2vm.strApellidosPaciente;
                    ResultadosPacientesReportesStatic.iCodeReporte = iCodigoReporte;
                    ResultadosPacientesReportesStatic.strRutaPDF = "";
                    ResultadosPacientesReportesStatic.strNivel = strNivel2;
                    ResultadosPacientesReportesStatic.strPorcentajeNivel = paciente2.iPorcentajeNivelPaciente.ToString();


                    //fvm.SetFisioterapeutaSelecionado(fiosoterapeutaBean());
                    IModalDialog dialog2 = ServiceProvider.Instance13.Get<IModalDialog>();
                    dialog2.BindViewModel(rv2vm);
                    dialog2.ShowDialog();
                    break;

            }
        }

        private void ImportPDFAction(object obj)
        {
            try
            {
                string strRutaPDF = ReporteDL.ObtenerRutaPDF(iCodigoReporte);
                if (strRutaPDF != "")
                {
                    System.Diagnostics.Process.Start(strRutaPDF);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Aún no ha generado el archivo PDF del reporte seleccionado. Para visualizarlo genérelo y vuelva a intentarlo.", "Advertencia", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show("El archivo ha sido eliminado de donde ha sido almacenamiento, genere nuevamente el reporte PDF para visualizarlo.", "Advertencia", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);

            }


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
