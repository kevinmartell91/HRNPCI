using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ResultadosReportesStatic
    {

        public static ResultadosReportesStatic instance = null;

        public static ObservableCollection<ResultadosRViewModel> ocltnResultadosPacientes { get; set; }
        public static ObservableCollection<ReportesCRViewModel> ocltnReportesPacientes { get; set; }

        public static ResultadosReportesStatic Instance()
        {
            if (instance == null)
                instance = new ResultadosReportesStatic();
            return instance;
        }
    }
}
