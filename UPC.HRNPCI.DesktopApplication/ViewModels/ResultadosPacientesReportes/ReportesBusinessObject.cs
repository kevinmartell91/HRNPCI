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
                foreach (var reportesBean in ocltnReporteBeans)
                {
                    ReportesCRViewModel rvm = new ReportesCRViewModel();

                    rvm.iCodigoReporte = reportesBean.iCodigoReporte;
                    rvm.iCodigoPaciente = reportesBean.iCodigoPaciente;
                    rvm.iCodigoTipoReporte = reportesBean.iCodigoTipoReporte;
                    rvm.strNombreTipoReporte = reportesBean.strNombreTipoReporte;
                    rvm.strFecReportePaciente = reportesBean.strFecReportePaciente;
                    rvm.strNombresPaciente = reportesBean.strNombresPaciente;
                    rvm.strApellidosPaciente = reportesBean.strApellidosPaciente;
                    //sacarlos angulos pocodigo
                    rvm.iCodigoDetalleReporte = reportesBean.iCodigoDetalleReporte;

                    rvm.strListaAngulosUno = reportesBean.ObtenerListaAngulos(1);
                    rvm.strListaAngulosDos = reportesBean.ObtenerListaAngulos(2);

                    rvm.strFecResultadoUno = reportesBean.strFecResultadoUno;
                    rvm.strFecResultadoDos = reportesBean.strFecResultadoDos;

                    rvm.strFecReportePaciente = reportesBean.strFecReportePaciente;
                    rvm.strUnidadPaciente = reportesBean.strUnidadPaciente;
                    rvm.strLateralidadPaciente = reportesBean.strLateralidadPaciente;

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
                foreach (var reportesBean in ocltnReporteBeans)
                {
                    ReportesCRViewModel rvm = new ReportesCRViewModel();

                    rvm.iCodigoReporte = reportesBean.iCodigoReporte;
                    rvm.iCodigoPaciente = reportesBean.iCodigoPaciente;
                    rvm.iCodigoDetalleReporte = reportesBean.iCodigoDetalleReporte;
                    rvm.iCodigoTipoReporte = reportesBean.iCodigoTipoReporte;
                    rvm.strNombreTipoReporte = reportesBean.strNombreTipoReporte;
                    rvm.strFecReportePaciente = reportesBean.strFecReportePaciente;
                    rvm.strNombresPaciente = reportesBean.strNombresPaciente;
                    rvm.strApellidosPaciente = reportesBean.strApellidosPaciente;


                    rvm.strListaAngulosUno = reportesBean.ObtenerListaAngulos(1);
                    rvm.strListaAngulosDos = reportesBean.ObtenerListaAngulos(2);
                    rvm.strFecReportePaciente = reportesBean.strFecReportePaciente;

                    rvm.strUnidadPaciente = reportesBean.strUnidadPaciente;
                    rvm.strLateralidadPaciente = reportesBean.strLateralidadPaciente;

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
