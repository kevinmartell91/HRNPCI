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
}
