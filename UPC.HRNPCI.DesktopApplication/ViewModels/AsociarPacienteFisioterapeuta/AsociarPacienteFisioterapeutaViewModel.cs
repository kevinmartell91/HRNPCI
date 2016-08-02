using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using UPC.HRNPCI.DesktopApplication;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.FisioterapeutaModel;
using UPC.HRNPCI.DesktopApplication.ViewModels.AsociarPacienteFisioterapeuta;
using UPC.HRNPCI.Model.PacienteModel;
using System.IO;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.AsociarPacienteFisioterapeuta
{
    class AsociarPacienteFisioterapeutaViewModel : BaseViewModel
    {
        public RelayCommand AsociarCommand {get;set;}

        public int iCodigoPaciente{ get; set; }
        public string strNombrePaciente { get; set; }
        public string strApellidoPaciente { get; set; }

        
        public string strTipoAction { get; set; }
        public string strNombreVentana { get; set; }
        public Mode Mode { get; set; }
        public string strNombreCompletoPaciente
        {
            get { return strNombrePaciente + " " + strApellidoPaciente; }
        }

        #region Declaraciones Fisioterapeutas Comboobx

        
        Dictionary<int, string> data = new Dictionary<int, string>();
        public IDictionary<int, string> Data
        {
            get { return this.data; }
        }

        private KeyValuePair<int, string>? selectedKeyUno = null;
        public KeyValuePair<int, string>? SelectedKeyUno
        {
            get { return this.selectedKeyUno; }
            set
            {
                this.selectedKeyUno = value;
                this.OnPropertyChanged("SelectedKeyUno");
                this.OnPropertyChanged("SelectedValueUno");
            }
        }
        public string SelectedValueUno
        {
            get
            {
                if (null == this.SelectedKeyUno)
                {
                    return string.Empty;
                }

                return this.data[this.SelectedKeyUno.Value.Key];
            }
            set
            {
                this.data[this.SelectedKeyUno.Value.Key] = value;
                this.OnPropertyChanged("SelectedValueUno");
            }
        }


        private KeyValuePair<int, string>? selectedKeyDos = null;
        public KeyValuePair<int, string>? SelectedKeyDos
        {
            get { return this.selectedKeyDos; }
            set
            {
                this.selectedKeyDos = value;
                this.OnPropertyChanged("SelectedKeyDos");
                this.OnPropertyChanged("SelectedValueDos");
            }
        }
        public string SelectedValueDos
        {
            get
            {
                if (null == this.SelectedKeyDos)
                {
                    return string.Empty;
                }

                return this.data[this.SelectedKeyDos.Value.Key];
            }
            set
            {
                this.data[this.SelectedKeyDos.Value.Key] = value;
                this.OnPropertyChanged("SelectedValueDos");
            }
        }

        public void InicializarFisioterapeutaComboBox()
        {
            data = new Dictionary<int, string>();

            FisioterapeutaComboBox collection = FisioterapeutaDL.ObtenerFisioterapuetasCombobox();
            

            data.Add(-1, "-");

            foreach (var item in collection.lstFisioterapeutasComboBox)
            {
                data.Add(item.iCodigo, item.strNombre + " " + item.strApellidos);
            }
            
        }


        #endregion


        public AsociarPacienteFisioterapeutaViewModel()
        {
            InicializarFisioterapeutaComboBox();
            AsociarCommand = new RelayCommand(AsociarPacienteFisioterapeuta);
        }

        private void AsociarPacienteFisioterapeuta(object parameter)
        {
            //golpeo l abase de datos
            int iCodigoFisioterapueta01 = SelectedKeyUno.Value.Key;
            int iCodigoFisioterapeuta02 = SelectedKeyDos.Value.Key;

            if (Mode == Mode.Add)
            {
                

                if (iCodigoFisioterapueta01 == -1 && -1 == iCodigoFisioterapeuta02)
                {
                    MessageBox.Show("Por favor, seleccione al menos un fisioterarpeuta para asignarlo a un paciente.", "Advertencia");
                }
                else if (iCodigoFisioterapueta01 == iCodigoFisioterapeuta02)
                {
                    MessageBox.Show("No se puede asociar dos mismos fisioterapeutas a un paciente.", "Advertencia");
                }
                else
                {
                    if (PacienteDL.AsignarFisioterapuetasPaciente(iCodigoPaciente, iCodigoFisioterapueta01, iCodigoFisioterapeuta02))
                    {
                        ListaPacientesAsociacionViewModel.Instance().ForzarListaRefresh();
                        MessageBox.Show("Paciente y fisioterapeuta(s) asociados.","Asignación realizada");
                    }
                }
            }
            if (Mode == Mode.Edit)
            {
                if (iCodigoFisioterapueta01 == -1 && -1 == iCodigoFisioterapeuta02)
                {
                    if (MessageBoxResult.Yes == MessageBox.Show("Está desasignando fisioterapeutas al paciente.", "Advertencia", MessageBoxButton.OKCancel))
                    {
                        MessageBox.Show("Paciente y fisioterapeuta(s) desasociados.", "Advertencia");

                        if (PacienteDL.AsignarFisioterapuetasPaciente(iCodigoPaciente, -1, -1))
                        {
                            ListaPacientesAsociacionViewModel.Instance().ForzarListaRefresh();
                            MessageBox.Show("Asociación actualizada.", "Asignación realizada");
                        }
                    }
                }
                else if (iCodigoFisioterapueta01 == iCodigoFisioterapeuta02)
                {
                    MessageBox.Show("No se puede asociar el mis fisioterapeutas a un paciente.", "Advertencia");
                }
                else
                {
                    if (PacienteDL.AsignarFisioterapuetasPaciente(iCodigoPaciente, iCodigoFisioterapueta01, iCodigoFisioterapeuta02))
                    {
                        ListaPacientesAsociacionViewModel.Instance().ForzarListaRefresh();
                        MessageBox.Show("Asociación actualizada.");
                    }
                }
                
            }


            // update modulo fisioterapeuta
            //FisioterapeutaTestAnalisisViewModel.Instance().ForzarUpdate();

        }
    }
}
