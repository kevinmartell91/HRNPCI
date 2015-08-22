using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace UPC.HRNPCI.Model.PacienteModel
{
    public class FisioterapeutaNombresCompletos
    {
         public int iCodigo { get; set; }
        public string strNombres { get; set; }
        public string strApellidos { get; set; }

        public ObservableCollection<FisioterapeutaNombresCompletos> ocltnFisioterapeutasNombresCompletos = null;

        public FisioterapeutaNombresCompletos()
        {
            ocltnFisioterapeutasNombresCompletos = new ObservableCollection<FisioterapeutaNombresCompletos>();
        }

        public int ObtenerNombreFisioterapueta(string piCodigo)
        {
            for (int i = 0; i < ocltnFisioterapeutasNombresCompletos.Count; i++)
                if (ocltnFisioterapeutasNombresCompletos[i].strNombres == piCodigo)
                    return ocltnFisioterapeutasNombresCompletos[i].iCodigo;
            return -1;

        }
    }
}
