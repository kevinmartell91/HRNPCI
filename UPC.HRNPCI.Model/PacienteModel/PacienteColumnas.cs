using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


namespace UPC.HRNPCI.Model.PacienteModel
{
    public class PacienteColumnas
    {
        public int iIdColumna { get; set; }
        public string strNombreColumna { get; set; }
        public string strNombreColumnaDB { get; set; }
        public bool blnVisible { get; set; }

        public ObservableCollection<PacienteColumnas> ocltnColumnasPaciente = null;

        public PacienteColumnas()
        {
            ocltnColumnasPaciente = new ObservableCollection<PacienteColumnas>();
        }

        public int ObtenerIdColumna(string pNombreColumna)
        {
            for (int i = 0; i < ocltnColumnasPaciente.Count; i++)
                if (ocltnColumnasPaciente[i].strNombreColumna == pNombreColumna)
                    return ocltnColumnasPaciente[i].iIdColumna;
            return -1;

        }

    }

    public class PacienteB
    {
        public int iCodigoPaciente { get; set; }
        public string vNombresPaciente { get; set; }
        public string vApellidosPaciente { get; set; }
        public string cGeneroPaciente { get; set; }
        public string vDiagnosticoMedicoPaciente { get; set; }
        public System.Nullable<int> iNivelPaciente { get; set; }
        public System.Nullable<int> iPorcentajeNivelPaciente { get; set; }
        public string vEdadCronologicaPaciente { get; set; }
        public System.Nullable<System.DateTime> daFecNacPaciente { get; set; }
        public System.Nullable<int> iCodigoFisioterapeutaUno { get; set; }
        public System.Nullable<int> iCodigoFisioterapeutaDos { get; set; }
        public string cDNIPaciente { get; set; }
        public string vNomApeMedNeuroReferencia { get; set; }
        public string vCelMedNeuroReferencia { get; set; }
        public string vParentescoApoderado { get; set; }
        public string vNombresApoderado { get; set; }
        public string vApellidoApoderado { get; set; }
        public string vCelularApoderado { get; set; }
        public string vTelefonoApoderado { get; set; }
        public int iFlagBorrradoPaciente { get; set; }
        public int iFlagFisioAsigPaciente { get; set; }
        public string vUrlFotoPaciente { get; set; }
    }
}
