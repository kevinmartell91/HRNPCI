using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.ResultadosReportesPaciente;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ResultadosBusinessObject
    {

        ObservableCollection<ResultadosRViewModel> ocltnResultadosR = null;

        public ResultadosBusinessObject()
        {
            ocltnResultadosR = ListarResultadosRViewModel();
        }

        public ObservableCollection<ResultadosRViewModel> ListarResultadosRViewModel()
        {
            ObservableCollection<ResultadosRViewModel> _ocltnResultadosR = new ObservableCollection<ResultadosRViewModel>();
            ObservableCollection<ResultadoBean> ocltnResultadosBean = ResultadoDL.ListarResultadosPacientes();

            if (ocltnResultadosBean != null)
            {
                foreach (var resultadosBean in ocltnResultadosBean)
                {
                    ResultadosRViewModel rvm = new ResultadosRViewModel();
                    rvm.iCodigoResultado = resultadosBean.iCodigoResultado;
                    rvm.iCodigoPaciente = resultadosBean.iCodigoPaciente;
                    rvm.iCodigoPlano = resultadosBean.iCodigoPlano;
                    rvm.iCodigoLateralidad = resultadosBean.iCodigoLateralidad;
                    rvm.iCodigoUnidad = resultadosBean.iCodigoUnidad;
                    rvm.strListaAngulos = resultadosBean.ObtenerListaAngulos();
                    rvm.strFecAnalisisPaciente = resultadosBean.strFecAnalisisPaciente;
                    rvm.strNombresPaciente = resultadosBean.strNombresPaciente;
                    rvm.strApellidosPaciente = resultadosBean.strApellidosPaciente;
                    rvm.strNombrePlano = resultadosBean.strNombrePlano;
                    rvm.strNombreLateralidad = resultadosBean.strNombreLateralidad;
                    rvm.strNombreUnidad = resultadosBean.strNombreUnidad;

                    _ocltnResultadosR.Add(rvm);
                }
            }
            return _ocltnResultadosR;
        }

        public ObservableCollection<ResultadosRViewModel> ListarBusquedaResultadosR(int iCodigoPaciente, int iCodigoUnidad, int iCodigoLateralidad, DateTime dtFecReportePacienteLimInf, DateTime dtFecReportePacienteLimSup)
        {
            ObservableCollection<ResultadosRViewModel> _ocltnResultadosR = new ObservableCollection<ResultadosRViewModel>();
            ObservableCollection<ResultadoBean> ocltnResultadosBean = ResultadoDL.BusquedaResultado(iCodigoPaciente, iCodigoUnidad, iCodigoLateralidad, dtFecReportePacienteLimInf, dtFecReportePacienteLimSup);

            foreach (var resultadosBean in ocltnResultadosBean)
            {
                ResultadosRViewModel rvm = new ResultadosRViewModel();

                rvm.iCodigoResultado = resultadosBean.iCodigoResultado;
                rvm.iCodigoPaciente = resultadosBean.iCodigoPaciente;
                rvm.iCodigoPlano = resultadosBean.iCodigoPlano;
                rvm.iCodigoLateralidad = resultadosBean.iCodigoLateralidad;
                rvm.iCodigoUnidad = resultadosBean.iCodigoUnidad;
                rvm.strListaAngulos = resultadosBean.ObtenerListaAngulos();
                rvm.strFecAnalisisPaciente = resultadosBean.strFecAnalisisPaciente;
                rvm.strNombresPaciente = resultadosBean.strNombresPaciente;
                rvm.strApellidosPaciente = resultadosBean.strApellidosPaciente;
                rvm.strNombrePlano = resultadosBean.strNombrePlano;
                rvm.strNombreLateralidad = resultadosBean.strNombreLateralidad;
                rvm.strNombreUnidad = resultadosBean.strNombreUnidad;

                _ocltnResultadosR.Add(rvm);
            }
            return _ocltnResultadosR;

        }


    }
}
