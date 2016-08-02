using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ResultadosPacientesReportesStatic
    {

        public static bool blnRepotePDF { get; set; }
        public static string strNombrePaciente { get; set; }
        public static string strNivel { get; set; }
        public static string strPorcentajeNivel { get; set; }

        public static ReportesVerViewModel ViewReport1 = null;
        public static ReportesVer2ViewModel ViewReport2 { set; get; }


        public static int iCodeReporte { get; set; }
        public static string strRutaPDF { get; set; }
    }
}
