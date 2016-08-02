using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ReportesVerViewModel
    {
        public List<double> strListaAngulosIzquierdo { get; set; }
        public List<double> strListaAngulosDerecho { get; set; }
        public string strFecAnalisisPaciente { get; set; }
        public string strNombresPaciente { get; set; }
        public string strApellidosPaciente { get; set; }
        public string strNombrePlano { get; set; }
        public string strNombreLateralidadIzquierda { get; set; }
        public string strNombreLateralidadDerecha { get; set; }
        public string strNombreUnidad { get; set; }
        public string strNivel { get; set; }
        public string strNivelPorcentaje { get; set; }

        public ReportesVerViewModel()
        { 
        }

        public ListarResultadosReportesViewModel Continer
        {
            get { return ListarResultadosReportesViewModel.Instance(); }
        }
    }
}
