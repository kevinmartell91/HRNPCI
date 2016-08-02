using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UPC.HRNPCI.Model.ResultadosReportesPaciente
{
    public class ReporteBean
    {
        public int iCodigoReporte { get; set; }
        public int iCodigoPaciente { get; set; }
        public int iCodigoDetalleReporte { get; set; }
        public int iCodigoTipoReporte { get; set; }
        public string strNombreTipoReporte { get; set; }
        public string strFecReportePaciente { get; set; }
        public string strNombresPaciente { get; set; }
        public string strApellidosPaciente { get; set; }
        public string strUnidadPaciente { get; set; }
        public string strLateralidadPaciente { get; set; }

        public string strListaAngulosUno { set; get; }
        public string strListaAngulosDos { set; get; }

        public List<double> ObtenerListaAngulos(int p)
        {
            //split comas, recorrer uno por uno
            List<double> angles = new List<double>();
            string[] strAngles = null;
            switch (p)
            {
                case 1 :
                    strAngles = Regex.Split(strListaAngulosUno, ",");
                    break;

                case 2 :
                    strAngles = Regex.Split(strListaAngulosDos, ",");
                    break;
            }

           
            foreach (string strAngle in strAngles)
            {
                if (strAngle != "")
                    angles.Add(Convert.ToDouble(strAngle));
            }
            return angles;
        }
    }

    public class ReporteDetalleBean
    {
        public int iCodigoResultadoUno { get; set; }
        public int iCodigoResultadoDos { get; set; }
        public int iCodigoResultadoTres { get; set; }
        public int iCodigoResultadoCuatro { get; set; }
        public int iCodigoResultadoCinco { get; set; }
        public int iCodigoResultadoSeis { get; set; }
        public int iCodigoResultadoSiete { get; set; }
        public int iCodigoResultadoOcho { get; set; }
    }

    public class ReporteDetalleB
    {
        public System.Nullable<long> iCodigioReporteDetalle { get; set; }
        public System.Nullable<long> iCodigoResultadoUno { get; set; }
        public System.Nullable<long> iCodigoResultadoDos { get; set; }
        public System.Nullable<long> iCodigoResultadoTres { get; set; }
        public System.Nullable<long> iCodigoResultadoCuatro { get; set; }
        public System.Nullable<long> iCodigoResultadoCinco { get; set; }
        public System.Nullable<long> iCodigoResultadoSeis { get; set; }
        public System.Nullable<long> iCodigoResultadoSiete { get; set; }
        public System.Nullable<long> iCodigoResultadoOcho { get; set; }
    }

    public class ReporteB
    {
        public System.Nullable<long> iCodigoReporte { get; set; }
        public System.Nullable<int> iCodigoPaciente { get; set; }
        public System.Nullable<int> iCodigoDetalleReporte { get; set; }
        public System.Nullable<int> iCodigoTipoReporte { get; set; }
        public System.Nullable<System.DateTime> dtFecReportePaciente { get; set; }
        public string vRutaReportePDF { get; set; }
    }
}
