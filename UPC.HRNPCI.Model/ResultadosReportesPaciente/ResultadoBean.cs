using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UPC.HRNPCI.Model.ResultadosReportesPaciente
{
    public class ResultadoBean
    {
        public int iCodigoResultado { get; set; }
        public int iCodigoPaciente { get; set; }
        public int iCodigoPlano { get; set; }
        public int iCodigoLateralidad { get; set; }
        public int iCodigoUnidad { get; set; }
        public string strListaAngulos { set; get; }
        public string strFecAnalisisPaciente { get; set; }
        public string strNombresPaciente { get; set; }
        public string strApellidosPaciente { get; set; }
        public string strNombrePlano { get; set; }
        public string strNombreLateralidad { get; set; }
        public string strNombreUnidad { get; set; }

        public List<double> ObtenerListaAngulos()
        {
            //split comas, recorrer uno por uno
            List<double> angles = new List<double>();

            string[] strAngles = Regex.Split(strListaAngulos, ",");
            foreach (string strAngle in strAngles)
            {
                if (strAngle != "")
                    angles.Add(Convert.ToDouble(strAngle));
            }
            return angles;
        }

       
    }

     public class ResultadoB
     {
         public long biCodigoResultado { get; set; }
         public System.Nullable<int> iCodigoPaciente { get; set; }
         public System.Nullable<int> iCodigoPlano { get; set; }
         public System.Nullable<int> iCodigoLateralidad { get; set; }
         public System.Nullable<int> iCodigoUnidad { get; set; }
         public string vListaAngulos { get; set; }
         public System.Nullable<System.DateTime> dtFecAnalisisPaciente { get; set; }
     }
}
