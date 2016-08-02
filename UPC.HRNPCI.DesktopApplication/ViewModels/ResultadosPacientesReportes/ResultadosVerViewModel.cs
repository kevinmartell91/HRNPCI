using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.HRNPCI.DesktopApplication.Helpers;
using UPC.HRNPCI.DesktopApplication._Common;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication._Service;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ResultadosVerViewModel : BaseViewModel
    {
        public List<double> strListaAngulos { get; set; }
        public string strFecAnalisisPaciente { get; set; }
        public string strNombresPaciente { get; set; }
        public string strApellidosPaciente { get; set; }
        public string strNombrePlano { get; set; }
        public string strNombreLateralidad { get; set; }
        public string strNombreUnidad { get; set; }
        public string strNivel { get; set; }
        public string strNivelPorcentaje { get; set; }

        public ResultadosVerViewModel()
        { 
        }

        public ListarResultadosReportesViewModel Continer
        {
            get { return ListarResultadosReportesViewModel.Instance(); }
        }
    }
}
