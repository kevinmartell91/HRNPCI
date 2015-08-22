using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.HRNPCI.Model.PacienteModel;
using UPC.HRNPCI.Model;
using System.Collections.ObjectModel;
using UPC.HRNPCI.DesktopApplication.ViewModels.AsociarPacienteFisioterapeuta;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete
{
    class PacienteBusinessObject
    {
        public event EventHandler PacientesChanged;

        List<PacienteCRUDViewModel> lstPacientesCRUD { get; set; }

        public PacienteBusinessObject()
        {
            lstPacientesCRUD = ObtenerPacientesCRUD();
        }

        void OnPacientesChanged()
        {
            if (PacientesChanged != null)
                PacientesChanged(this, null);
        }

        public List<PacienteCRUDViewModel> ObtenerPacientesCRUD()
        {

            List<PacienteCRUDViewModel> lstPacientes = new List<PacienteCRUDViewModel>();
            List<Paciente> lstPacientesDL = PacienteDL.ObtenerPacientes();

            IEnumerable<Paciente> ordenadosPacietes = lstPacientesDL.OrderBy(p => p.vApellidosPaciente);

            if (ordenadosPacietes != null)
            {
                foreach (var pacienteDL in ordenadosPacietes)
                {
                    PacienteCRUDViewModel pacienteCRUD = new PacienteCRUDViewModel();


                    pacienteCRUD.iCodigo = pacienteDL.iCodigoPaciente;
                    pacienteCRUD.strNombres = pacienteDL.vNombresPaciente;
                    pacienteCRUD.strApellidos = pacienteDL.vApellidosPaciente;
                    pacienteCRUD.chrGenero = Convert.ToChar(pacienteDL.cGeneroPaciente);
                    pacienteCRUD.strDNI = pacienteDL.cDNIPaciente;
                    pacienteCRUD.strFecNacimiento =  Convert.ToDateTime(pacienteDL.daFecNacPaciente).ToString("yyyy/MM/dd");
                    pacienteCRUD.strFisioAsignadoUno = (pacienteDL.iCodigoFisioterapeutaUno).ToString();
                    pacienteCRUD.strFisioAsignadoDos = (pacienteDL.iCodigoFisioterapeutaDos).ToString();
                    pacienteCRUD.strDiagnosticoMedico = pacienteDL.vDiagnosticoMedicoPaciente;
                    pacienteCRUD.iNivel = Convert.ToInt32(pacienteDL.iNivelPaciente);
                    pacienteCRUD.iPorcentajeNivel = Convert.ToInt32(pacienteDL.iPorcentajeNivelPaciente);
                    pacienteCRUD.strNomApeMedNeuroReferencia = pacienteDL.vNomApeMedNeuroReferencia;
                    pacienteCRUD.strCelMedNeuroReferencia = pacienteDL.vCelMedNeuroReferencia;
                    pacienteCRUD.strNombresApod = pacienteDL.vNombresApoderado;
                    pacienteCRUD.strApellidosApod = pacienteDL.vApellidoApoderado;
                    pacienteCRUD.strCelularApod = pacienteDL.vCelularApoderado;
                    pacienteCRUD.strTeléfonoApod = pacienteDL.vTelefonoApoderado;
                    pacienteCRUD.strParentescoApod = pacienteDL.vParentescoApoderado;
                    pacienteCRUD.iBorrrado = Convert.ToInt32(pacienteDL.iFlagBorrradoPaciente);
                    pacienteCRUD.iFisioAsigPaciente = Convert.ToInt32(pacienteDL.iFlagFisioAsigPaciente);
                    pacienteCRUD.strEdadCronologica = pacienteDL.vEdadCronologicaPaciente;
                    pacienteCRUD.strUrlFotoPaciente = pacienteDL.vUrlFotoPaciente;

                    lstPacientes.Add(pacienteCRUD);
                }
            }
            return lstPacientes;

        }
       
        public List<PacienteCRUDViewModel> ObtenerPacientesBuscadosCRUD(int piColumna, string pstrBuscar)
        {

            List<PacienteCRUDViewModel> lstPacienteResutadoCRUD = new List<PacienteCRUDViewModel>();
            List<Paciente> lstPacienteResultadoDL = PacienteDL.BuscaPorCampoPaciente(piColumna, pstrBuscar);

            if (lstPacienteResultadoDL != null)
            {
                foreach (var pacienteDL in lstPacienteResultadoDL)
                {
                    PacienteCRUDViewModel pacienteCRUD = new PacienteCRUDViewModel();


                    pacienteCRUD.iCodigo = pacienteDL.iCodigoPaciente;
                    pacienteCRUD.strNombres = pacienteDL.vNombresPaciente;
                    pacienteCRUD.strApellidos = pacienteDL.vApellidosPaciente;
                    pacienteCRUD.chrGenero = Convert.ToChar(pacienteDL.cGeneroPaciente);
                    pacienteCRUD.strDNI = pacienteDL.cDNIPaciente;
                    pacienteCRUD.strFecNacimiento =  Convert.ToDateTime(pacienteDL.daFecNacPaciente).ToString("yyyy/MM/dd");
                    pacienteCRUD.strFisioAsignadoUno = (pacienteDL.iCodigoFisioterapeutaUno).ToString();
                    pacienteCRUD.strFisioAsignadoDos = (pacienteDL.iCodigoFisioterapeutaDos).ToString();
                    pacienteCRUD.strDiagnosticoMedico = pacienteDL.vDiagnosticoMedicoPaciente;
                    pacienteCRUD.iNivel = Convert.ToInt32(pacienteDL.iNivelPaciente);
                    pacienteCRUD.iPorcentajeNivel = Convert.ToInt32(pacienteDL.iPorcentajeNivelPaciente);
                    pacienteCRUD.strNomApeMedNeuroReferencia = pacienteDL.vNomApeMedNeuroReferencia;
                    pacienteCRUD.strCelMedNeuroReferencia = pacienteDL.vCelMedNeuroReferencia;
                    pacienteCRUD.strNombresApod = pacienteDL.vNombresApoderado;
                    pacienteCRUD.strApellidosApod = pacienteDL.vApellidoApoderado;
                    pacienteCRUD.strCelularApod = pacienteDL.vCelularApoderado;
                    pacienteCRUD.strTeléfonoApod = pacienteDL.vTelefonoApoderado;
                    pacienteCRUD.strParentescoApod = pacienteDL.vParentescoApoderado;
                    pacienteCRUD.iBorrrado = Convert.ToInt32(pacienteDL.iFlagBorrradoPaciente);
                    pacienteCRUD.iFisioAsigPaciente = Convert.ToInt32(pacienteDL.iFlagFisioAsigPaciente);
                    pacienteCRUD.strEdadCronologica = pacienteDL.vEdadCronologicaPaciente;
                    pacienteCRUD.strUrlFotoPaciente = pacienteDL.vUrlFotoPaciente;
                    lstPacienteResutadoCRUD.Add(pacienteCRUD);
                }
            }
            return lstPacienteResutadoCRUD;
        }

        public List<AsociacionCU> ObtenerPacientesAsociacionCU()
        {

            List<AsociacionCU> lstPacientes = new List<AsociacionCU>();
            List<Paciente> lstPacientesDL = PacienteDL.ObtenerPacientes();

            if (lstPacientesDL != null)
            {
                foreach (var pacienteDL in lstPacientesDL)
                {
                    AsociacionCU pacienteAsociacionCD = new AsociacionCU();


                    pacienteAsociacionCD.iCodigo = pacienteDL.iCodigoPaciente;
                    pacienteAsociacionCD.strNombres = pacienteDL.vNombresPaciente;
                    pacienteAsociacionCD.strApellidos = pacienteDL.vApellidosPaciente;
                    pacienteAsociacionCD.chrGenero = Convert.ToChar(pacienteDL.cGeneroPaciente);
                    pacienteAsociacionCD.strDNI = pacienteDL.cDNIPaciente;
                    pacienteAsociacionCD.strFecNacimiento = Convert.ToDateTime(pacienteDL.daFecNacPaciente).ToString("yyyy/MM/dd"); 
                    pacienteAsociacionCD.strFisioAsignadoUno = (pacienteDL.iCodigoFisioterapeutaUno).ToString();
                    pacienteAsociacionCD.strFisioAsignadoDos = (pacienteDL.iCodigoFisioterapeutaDos).ToString();
                    pacienteAsociacionCD.strDiagnosticoMedico = pacienteDL.vDiagnosticoMedicoPaciente;
                    pacienteAsociacionCD.iNivel = Convert.ToInt32(pacienteDL.iNivelPaciente);
                    pacienteAsociacionCD.iPorcentajeNivel = Convert.ToInt32(pacienteDL.iPorcentajeNivelPaciente);
                    pacienteAsociacionCD.strNomApeMedNeuroReferencia = pacienteDL.vNomApeMedNeuroReferencia;
                    pacienteAsociacionCD.strCelMedNeuroReferencia = pacienteDL.vCelMedNeuroReferencia;
                    pacienteAsociacionCD.strNombresApod = pacienteDL.vNombresApoderado;
                    pacienteAsociacionCD.strApellidosApod = pacienteDL.vApellidoApoderado;
                    pacienteAsociacionCD.strCelularApod = pacienteDL.vCelularApoderado;
                    pacienteAsociacionCD.strTeléfonoApod = pacienteDL.vTelefonoApoderado;
                    pacienteAsociacionCD.strParentescoApod = pacienteDL.vParentescoApoderado;
                    pacienteAsociacionCD.iBorrrado = Convert.ToInt32(pacienteDL.iFlagBorrradoPaciente);
                    pacienteAsociacionCD.iFisioAsigPaciente = Convert.ToInt32(pacienteDL.iFlagFisioAsigPaciente);
                    pacienteAsociacionCD.strEdadCronologica = pacienteDL.vEdadCronologicaPaciente;
                    pacienteAsociacionCD.strUrlFotoPaciente = pacienteDL.vUrlFotoPaciente;

                    lstPacientes.Add(pacienteAsociacionCD);
                }
            }
            return lstPacientes;

        }

        public List<AsociacionCU> ObtenerPacientesBuscadosAsociacionCU(int piColumna, string pstrBuscar)
        {

            List<AsociacionCU> lstPacienteResutadoCRUD = new List<AsociacionCU>();
            List<Paciente> lstPacienteResultadoDL = PacienteDL.BuscaPorCampoPaciente(piColumna, pstrBuscar);

            if (lstPacienteResultadoDL != null)
            {
                foreach (var pacienteDL in lstPacienteResultadoDL)
                {
                    AsociacionCU pacienteCRUD = new AsociacionCU();


                    pacienteCRUD.iCodigo = pacienteDL.iCodigoPaciente;
                    pacienteCRUD.strNombres = pacienteDL.vNombresPaciente;
                    pacienteCRUD.strApellidos = pacienteDL.vApellidosPaciente;
                    pacienteCRUD.chrGenero = Convert.ToChar(pacienteDL.cGeneroPaciente);
                    pacienteCRUD.strDNI = pacienteDL.cDNIPaciente;
                    pacienteCRUD.strFecNacimiento = Convert.ToDateTime(pacienteDL.daFecNacPaciente).ToString();
                    pacienteCRUD.strFisioAsignadoUno = (pacienteDL.iCodigoFisioterapeutaUno).ToString();
                    pacienteCRUD.strFisioAsignadoDos = (pacienteDL.iCodigoFisioterapeutaDos).ToString();
                    pacienteCRUD.strDiagnosticoMedico = pacienteDL.vDiagnosticoMedicoPaciente;
                    pacienteCRUD.iNivel = Convert.ToInt32(pacienteDL.iNivelPaciente);
                    pacienteCRUD.iPorcentajeNivel = Convert.ToInt32(pacienteDL.iPorcentajeNivelPaciente);
                    pacienteCRUD.strNomApeMedNeuroReferencia = pacienteDL.vNomApeMedNeuroReferencia;
                    pacienteCRUD.strCelMedNeuroReferencia = pacienteDL.vCelMedNeuroReferencia;
                    pacienteCRUD.strNombresApod = pacienteDL.vNombresApoderado;
                    pacienteCRUD.strApellidosApod = pacienteDL.vApellidoApoderado;
                    pacienteCRUD.strCelularApod = pacienteDL.vCelularApoderado;
                    pacienteCRUD.strTeléfonoApod = pacienteDL.vTelefonoApoderado;
                    pacienteCRUD.strParentescoApod = pacienteDL.vParentescoApoderado;
                    pacienteCRUD.iBorrrado = Convert.ToInt32(pacienteDL.iFlagBorrradoPaciente);
                    pacienteCRUD.iFisioAsigPaciente = Convert.ToInt32(pacienteDL.iFlagFisioAsigPaciente);
                    pacienteCRUD.strEdadCronologica = pacienteDL.vEdadCronologicaPaciente;
                    pacienteCRUD.strUrlFotoPaciente = pacienteDL.vUrlFotoPaciente;
                    lstPacienteResutadoCRUD.Add(pacienteCRUD);
                }
            }
            return lstPacienteResutadoCRUD;
        }

        //para actulizar  la lista de la aplicacion y para yo no estar actualizando directamente de la base de datos
        //agragar y actualizar
        public PacienteCRUDViewModel ObtenerPacienteCRUD(Paciente pPaciente)
        {
            if (pPaciente != null)
            {
                PacienteCRUDViewModel pacienteCRUD = new PacienteCRUDViewModel();


                pacienteCRUD.iCodigo = pPaciente.iCodigoPaciente;
                pacienteCRUD.strNombres = pPaciente.vNombresPaciente;
                pacienteCRUD.strApellidos = pPaciente.vApellidosPaciente;
                pacienteCRUD.chrGenero = Convert.ToChar(pPaciente.cGeneroPaciente);
                pacienteCRUD.strDNI = pPaciente.cDNIPaciente;
                pacienteCRUD.strFecNacimiento = Convert.ToDateTime(pPaciente.daFecNacPaciente).ToString("yyyy/MM/dd");
                pacienteCRUD.strFisioAsignadoUno = (pPaciente.iCodigoFisioterapeutaUno).ToString();
                pacienteCRUD.strFisioAsignadoDos = (pPaciente.iCodigoFisioterapeutaDos).ToString();
                pacienteCRUD.strDiagnosticoMedico = pPaciente.vDiagnosticoMedicoPaciente;
                pacienteCRUD.iNivel = Convert.ToInt32(pPaciente.iNivelPaciente);
                pacienteCRUD.iPorcentajeNivel = Convert.ToInt32(pPaciente.iPorcentajeNivelPaciente);
                pacienteCRUD.strNomApeMedNeuroReferencia = pPaciente.vNomApeMedNeuroReferencia;
                pacienteCRUD.strCelMedNeuroReferencia = pPaciente.vCelMedNeuroReferencia;
                pacienteCRUD.strNombresApod = pPaciente.vNombresApoderado;
                pacienteCRUD.strApellidosApod = pPaciente.vApellidoApoderado;
                pacienteCRUD.strCelularApod = pPaciente.vCelularApoderado;
                pacienteCRUD.strTeléfonoApod = pPaciente.vTelefonoApoderado;
                pacienteCRUD.strParentescoApod = pPaciente.vParentescoApoderado;
                pacienteCRUD.iBorrrado = Convert.ToInt32(pPaciente.iFlagBorrradoPaciente);
                pacienteCRUD.iFisioAsigPaciente = Convert.ToInt32(pPaciente.iFlagFisioAsigPaciente);
                pacienteCRUD.strEdadCronologica = pPaciente.vEdadCronologicaPaciente;
                pacienteCRUD.strUrlFotoPaciente = pPaciente.vUrlFotoPaciente;

                return pacienteCRUD;
            }
            return null;
        }
    }
}
