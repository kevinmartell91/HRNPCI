using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes;
using UPC.HRNPCI.Model;
using UPC.HRNPCI.Model.ResultadosReportesPaciente;


namespace UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes
{
    class ReportesBusinessObject
    {
        ObservableCollection<ReportesCRViewModel> ocltnReportesCR = null;

        public ReportesBusinessObject()
        {
            ocltnReportesCR = ListarReportesCRViewModel();
        }

        public ObservableCollection<ReportesCRViewModel> ListarReportesCRViewModel()
        {
            ObservableCollection<ReportesCRViewModel> ocltnResultadosR = new ObservableCollection<ReportesCRViewModel>();
            ObservableCollection<ReporteBean> ocltnReporteBeans = ReporteDL.ListarReportesPaciente();
            if(ocltnReporteBeans != null)
            {
                foreach (var resultadosBean in ocltnReporteBeans)
                {
                    ReportesCRViewModel rvm = new ReportesCRViewModel();

                    rvm.iCodigoReporte = resultadosBean.iCodigoReporte;
                    rvm.iCodigoPaciente = resultadosBean.iCodigoPaciente;
                    rvm.iCodigoTipoReporte = resultadosBean.iCodigoTipoReporte;
                    rvm.strNombreTipoReporte = resultadosBean.strNombreTipoReporte;
                    rvm.strFecReportePaciente = resultadosBean.strFecReportePaciente;
                    rvm.strNombresPaciente = resultadosBean.strNombresPaciente;
                    rvm.strApellidosPaciente = resultadosBean.strApellidosPaciente;
                    //sacarlos angulos pocodigo
                    rvm.iCodigoDetalleReporte = resultadosBean.iCodigoDetalleReporte;

                    rvm.strListaAngulosUno = resultadosBean.ObtenerListaAngulos(1);
                    rvm.strListaAngulosDos = resultadosBean.ObtenerListaAngulos(2);
                    rvm.strUnidadPaciente = resultadosBean.strUnidadPaciente;
                    rvm.strLateralidadPaciente = resultadosBean.strLateralidadPaciente;

                    ocltnResultadosR.Add(rvm);
                }

            }
            return ocltnResultadosR;
        }

        public ObservableCollection<ReportesCRViewModel> ListarBusquedaReporteCRViewModel(int iCodigoPaciente, int iCodigoReporte, DateTime dtFecReportePacienteLimInf, DateTime dtFecReportePacienteLimSup)
        {
            ObservableCollection<ReportesCRViewModel> ocltnResultadosR = new ObservableCollection<ReportesCRViewModel>();
            ObservableCollection<ReporteBean> ocltnReporteBeans = ReporteDL.BusquedaReportes(iCodigoPaciente, iCodigoReporte, dtFecReportePacienteLimInf, dtFecReportePacienteLimSup);
            if (ocltnReporteBeans != null)
            {
                foreach (var resultadosBean in ocltnReporteBeans)
                {
                    ReportesCRViewModel rvm = new ReportesCRViewModel();

                    rvm.iCodigoReporte = resultadosBean.iCodigoReporte;
                    rvm.iCodigoPaciente = resultadosBean.iCodigoPaciente;
                    rvm.iCodigoDetalleReporte = resultadosBean.iCodigoDetalleReporte;
                    rvm.iCodigoTipoReporte = resultadosBean.iCodigoTipoReporte;
                    rvm.strNombreTipoReporte = resultadosBean.strNombreTipoReporte;
                    rvm.strFecReportePaciente = resultadosBean.strFecReportePaciente;
                    rvm.strNombresPaciente = resultadosBean.strNombresPaciente;
                    rvm.strApellidosPaciente = resultadosBean.strApellidosPaciente;


                    rvm.strListaAngulosUno = resultadosBean.ObtenerListaAngulos(1);
                    rvm.strListaAngulosDos = resultadosBean.ObtenerListaAngulos(2);
                    rvm.strUnidadPaciente = resultadosBean.strUnidadPaciente;
                    rvm.strLateralidadPaciente = resultadosBean.strLateralidadPaciente;

                    ocltnResultadosR.Add(rvm);
                }
            }
            return ocltnResultadosR;

        }

        public bool GuardarReporte(int iCodigoTipoeReporte, int iCodigoPaciente, DateTime dtFecReportePaciente, int iCodigoResultadoUno, int iCodigoResultadoDos, int iCodigoResultadoTres, int iCodigoResultadoCuatro, int iCodigoResultadoCinco, int iCodigoResultadoSeis, int iCodigoResultadoSiete, int iCodigoResultadoOcho)
        {
            int iCodigoDetalleReporte = ReporteDL.GuardarReporteDetalle(iCodigoResultadoUno, iCodigoResultadoDos, iCodigoResultadoTres, iCodigoResultadoCuatro, iCodigoResultadoCinco, iCodigoResultadoSeis, iCodigoResultadoSiete, iCodigoResultadoOcho);
            if (iCodigoDetalleReporte != -1)
            {
                if (ReporteDL.GuardarReporte(iCodigoPaciente, iCodigoDetalleReporte, iCodigoTipoeReporte, dtFecReportePaciente) == 1)
                    return true;
                return false;
            }
            return false;
        }
    }
}
