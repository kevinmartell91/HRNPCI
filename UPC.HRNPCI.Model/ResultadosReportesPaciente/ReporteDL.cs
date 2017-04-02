using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using UPC.HRNPCI.Model.FisioterapeutaModel;

namespace UPC.HRNPCI.Model.ResultadosReportesPaciente
{
    public class ReporteDL
    {
        //static DataModelTestDataContext dm = new DataModelTestDataContext();

        public static ObservableCollection<ReporteBean> ListarReportesPaciente()
        {
            #region linq to class
            //ObservableCollection<ReporteBean> ocltnReporteBeans = new ObservableCollection<ReporteBean>();

            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    var collection = dm.SP_ListarReportesPacientes();

            //    foreach (var item in collection)
            //    {
            //        ReporteBean reporteBean = new ReporteBean();

            //        reporteBean.iCodigoDetalleReporte = (int)item.iCodigoDetalleReporte;
            //        reporteBean.iCodigoPaciente = (int)item.iCodigoPaciente;
            //        reporteBean.iCodigoReporte = item.iCodigoReporte;
            //        reporteBean.strApellidosPaciente = (string)item.vApellidosPaciente;
            //        reporteBean.strFecReportePaciente = (Convert.ToDateTime((DateTime)item.dtFecReportePaciente)).Date.ToString();
            //        reporteBean.iCodigoTipoReporte = (int)item.iCodigoTipoReporte;
            //        reporteBean.strNombreTipoReporte = (string)item.vNombreTipoReporte;
            //        reporteBean.strNombresPaciente = (string)item.vNombresPaciente;

            //        ReporteDetalle rd = ObtenerResultadosCodigos((int)item.iCodigoDetalleReporte);
            //        Resultado resultadoUno = ResultadoDL.ObtenerResultadoCodigo((int)rd.iCodigoResultadoUno);
            //        Resultado resultadoDos = ResultadoDL.ObtenerResultadoCodigo((int)rd.iCodigoResultadoDos);
            //        reporteBean.strListaAngulosUno = resultadoUno.vListaAngulos.ToString();
            //        reporteBean.strListaAngulosDos = resultadoDos.vListaAngulos.ToString();
            //        reporteBean.strUnidadPaciente = "Rodilla";

            //        int iCodigoResultadoUno = (int)ObtenerResultadosCodigos((int)item.iCodigoDetalleReporte).iCodigoResultadoUno;
            //        int iCodigoLateralidad = (int)(ResultadoDL.ObtenerResultadoCodigo(iCodigoResultadoUno).iCodigoLateralidad);
            //        Lateralidad lat = dm.Lateralidads.Where(l => l.iCodigoLateralidad == iCodigoLateralidad).First();
            //        reporteBean.strLateralidadPaciente = lat.vNombre;

            //        ocltnReporteBeans.Add(reporteBean);
            //    }
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            //return ocltnReporteBeans;
            #endregion

            #region sql compact edition
            ObservableCollection<ReporteBean> ocltnReporteBeans = new ObservableCollection<ReporteBean>();
            try
            {
                SqlCeConnection conn = null;
                SqlCeCommand cmd = null;
                SqlCeDataReader rdr = null;
                


                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT  r.iCodigoReporte, r.iCodigoPaciente, r.iCodigoDetalleReporte , tr.iCodigoTipoReporte , tr.vNombreTipoReporte, r.dtFecReportePaciente, p.vNombresPaciente,p.vApellidosPaciente " +
                                        "FROM Reporte r " +
                                        "JOIN Paciente p " +
                                        "ON r.iCodigoPaciente = p.iCodigoPaciente " +
                                        "JOIN TipoReporte tr " +
                                        "on r.iCodigoTipoReporte = tr.iCodigoTipoReporte " +
                                        "ORDER by r.dtFecReportePaciente DESC", conn);
                rdr = cmd.ExecuteReader();
                int i = 0 ;
                while (rdr.Read())
                {
                    ReporteBean reporteBean = new ReporteBean();
                    if (!DBNull.Value.Equals(rdr[0])) reporteBean.iCodigoReporte = (int)rdr.GetInt64(0);
                    if (!DBNull.Value.Equals(rdr[1])) reporteBean.iCodigoPaciente = rdr.GetInt32(1);
                    if (!DBNull.Value.Equals(rdr[2])) reporteBean.iCodigoDetalleReporte = (int)rdr.GetInt64(2);
                    if (!DBNull.Value.Equals(rdr[3])) reporteBean.iCodigoTipoReporte = rdr.GetInt32(3);
                    if (!DBNull.Value.Equals(rdr[4])) reporteBean.strNombreTipoReporte = rdr.GetString(4);
                    if (!DBNull.Value.Equals(rdr[5])) reporteBean.strFecReportePaciente = rdr.GetDateTime(5).ToString();
                    if (!DBNull.Value.Equals(rdr[6])) reporteBean.strNombresPaciente = rdr.GetString(6);
                    if (!DBNull.Value.Equals(rdr[7])) reporteBean.strApellidosPaciente = rdr.GetString(7);



                    ReporteDetalleB rd = ObtenerResultadosCodigosDetalle(reporteBean.iCodigoTipoReporte);

                    ResultadoB resultadoUno = ResultadoDL.ObtenerResultadoCodigo((int)rd.iCodigoResultadoUno);
                    ResultadoB resultadoDos = ResultadoDL.ObtenerResultadoCodigo((int)rd.iCodigoResultadoDos);
                    //add the two dates from each resultados 1 y 2
                    reporteBean.strFecResultadoUno = (resultadoUno.dtFecAnalisisPaciente).ToString();
                    reporteBean.strFecResultadoDos = (resultadoDos.dtFecAnalisisPaciente).ToString();
                    reporteBean.strListaAngulosUno = resultadoUno.vListaAngulos.ToString();
                    reporteBean.strListaAngulosDos = resultadoDos.vListaAngulos.ToString();
                    reporteBean.strUnidadPaciente = "Rodilla";

                    int iCodigoResultadoUno = (int)ObtenerResultadosCodigosDetalle(reporteBean.iCodigoDetalleReporte).iCodigoResultadoUno;
                    int iCodigoLateralidad = (int)(ResultadoDL.ObtenerResultadoCodigo(iCodigoResultadoUno).iCodigoLateralidad);
                    LateralidadB lat = FisioterapeutaDL.ObtenerListaLateralidades().Where(l => l.iCodigoLateralidad == iCodigoLateralidad).First();
                    reporteBean.strLateralidadPaciente = lat.vNombre;

                    ocltnReporteBeans.Add(reporteBean);
                }
                rdr.Close();
                cmd.Dispose();



            }
            catch (Exception)
            {
                return null;
            }
            return ocltnReporteBeans;
            #endregion

        }

        public static int GuardarReporte(int iCodigoPaciente, int iCodigoDetalleReporte, int iCodigoTipoReporte, DateTime dtFecReportePaciente)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    dm.SP_GenerarReporte(iCodigoPaciente, iCodigoDetalleReporte, iCodigoTipoeReporte, dtFecReportePaciente.Date);
            //    return 1;
            //}
            //catch (Exception)
            //{
            //    return 0;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("INSERT INTO Reporte (iCodigoPaciente, iCodigoDetalleReporte, iCodigoTipoReporte, dtFecReportePaciente) " +
                                        "VALUES (@iCodigoPaciente, @iCodigoDetalleReporte, @iCodigoTipoReporte, @dtFecReportePaciente)", conn);

                cmd.Parameters.AddWithValue("@iCodigoPaciente", iCodigoPaciente);
                cmd.Parameters.AddWithValue("@iCodigoDetalleReporte", iCodigoDetalleReporte);
                cmd.Parameters.AddWithValue("@iCodigoTipoReporte", iCodigoTipoReporte);
                cmd.Parameters.AddWithValue("@dtFecReportePaciente", dtFecReportePaciente);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                conn.Close();
            }

            return 1;


            #endregion
        }

        public static int GuardarReporteDetalle(int iCodigoResultadoUno,int iCodigoResultadoDos,int iCodigoResultadoTres,int iCodigoResultadoCuatro,int iCodigoResultadoCinco,int iCodigoResultadoSeis,int iCodigoResultadoSiete,int iCodigoResultadoOcho)
        {
            #region linq to class
            //int? iCodigoReporteDetalle = -1;
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    dm.SP_GuardarReporteDetalle((iCodigoResultadoUno == -1) ? (int?)null : iCodigoResultadoUno,
            //                                               (iCodigoResultadoDos == -1) ? (int?)null : iCodigoResultadoDos,
            //                                               (iCodigoResultadoTres == -1) ? (int?)null : iCodigoResultadoTres,
            //                                               (iCodigoResultadoCuatro == -1) ? (int?)null : iCodigoResultadoCuatro,
            //                                               (iCodigoResultadoCinco == -1) ? (int?)null : iCodigoResultadoCinco,
            //                                               (iCodigoResultadoSeis == -1) ? (int?)null : iCodigoResultadoSeis,
            //                                               (iCodigoResultadoSiete == -1) ? (int?)null : iCodigoResultadoSiete,
            //                                               (iCodigoResultadoOcho == -1) ? (int?)null : iCodigoResultadoOcho,
            //                                               ref iCodigoReporteDetalle);
            //    return (int)iCodigoReporteDetalle;
            //}
            //catch (Exception)
            //{
            //    return (int)iCodigoReporteDetalle;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;

            int iCodigoReporteDetalle = -1;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("INSERT INTO ReporteDetalle(iCodigoResultadoUno,iCodigoResultadoDos,iCodigoResultadoTres,iCodigoResultadoCuatro,iCodigoResultadoCinco,iCodigoResultadoSeis,iCodigoResultadoSiete,iCodigoResultadoOcho) VALUES (@iCodigoResultadoUno,@iCodigoResultadoDos,@iCodigoResultadoTres,@iCodigoResultadoCuatro,@iCodigoResultadoCinco,@iCodigoResultadoSeis,@iCodigoResultadoSiete,@iCodigoResultadoOcho)", conn);
                cmd.Parameters.AddWithValue("@iCodigoResultadoUno", iCodigoResultadoUno);
                cmd.Parameters.AddWithValue("@iCodigoResultadoDos", iCodigoResultadoDos);
                cmd.Parameters.AddWithValue("@iCodigoResultadoTres", iCodigoResultadoDos);
                cmd.Parameters.AddWithValue("@iCodigoResultadoCuatro", iCodigoResultadoDos);
                cmd.Parameters.AddWithValue("@iCodigoResultadoCinco", iCodigoResultadoDos);
                cmd.Parameters.AddWithValue("@iCodigoResultadoSeis", iCodigoResultadoDos);
                cmd.Parameters.AddWithValue("@iCodigoResultadoSiete", iCodigoResultadoDos);
                cmd.Parameters.AddWithValue("@iCodigoResultadoOcho", iCodigoResultadoDos);
                cmd.ExecuteNonQuery();

                cmd = new SqlCeCommand("SELECT iCodigioReporteDetalle FROM ReporteDetalle WHERE iCodigioReporteDetalle = @@IDENTITY;", conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    iCodigoReporteDetalle = (int)rdr.GetInt64(0);
                }

            }
            catch (Exception)
            {
                return iCodigoReporteDetalle;
            }
            finally
            {
                conn.Close();
            }

            return iCodigoReporteDetalle;


            #endregion
        }

        //Corregir
        public static ObservableCollection<ReporteBean> BusquedaReportes(int iCodigoPaciente, int iCodigoReporte, DateTime dtFecReportePacienteLimInf, DateTime dtFecReportePacienteLimSup)
        {
            #region linq to class

            //ObservableCollection<ReporteBean> ocltnReporteBeans = new ObservableCollection<ReporteBean>();
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    var collection = dm.SP_BuscarReportes((iCodigoPaciente == -1) ? (int?)null : iCodigoPaciente,
            //                                (iCodigoReporte == -1) ?  (int?)null :iCodigoReporte ,
            //                                (dtFecReportePacienteLimInf.ToString().Equals("01/01/0001 12:00:00 a.m.")) ? (DateTime?)null : dtFecReportePacienteLimInf.Date,
            //                                (dtFecReportePacienteLimSup.ToString().Equals("01/01/0001 12:00:00 a.m.")) ? (DateTime?)null : dtFecReportePacienteLimSup.Date);

            //    foreach (var item in collection)
            //    {
            //        ReporteBean reporteBean = new ReporteBean();

            //        reporteBean.iCodigoDetalleReporte = (int)item.iCodigoDetalleReporte;
            //        reporteBean.iCodigoPaciente = (int)item.iCodigoPaciente;
            //        reporteBean.iCodigoReporte = item.iCodigoReporte;
            //        reporteBean.strApellidosPaciente = (string)item.vApellidosPaciente;
            //        reporteBean.strFecReportePaciente = (Convert.ToDateTime((DateTime)item.dtFecReportePaciente)).Date.ToString();
            //        reporteBean.iCodigoTipoReporte = (int)item.iCodigoTipoReporte;
            //        reporteBean.strNombreTipoReporte = (string)item.vNombreTipoReporte;
            //        reporteBean.strNombresPaciente = (string)item.vNombresPaciente;


            //        ReporteDetalleB rd = ObtenerResultadosCodigosDetalle((int)item.iCodigoDetalleReporte);
            //        ResultadoB resultadoUno = ResultadoDL.ObtenerResultadoCodigo((int)rd.iCodigoResultadoUno);
            //        ResultadoB resultadoDos = ResultadoDL.ObtenerResultadoCodigo((int)rd.iCodigoResultadoDos);
            //        reporteBean.strListaAngulosUno = resultadoUno.vListaAngulos.ToString();
            //        reporteBean.strListaAngulosDos = resultadoDos.vListaAngulos.ToString();
            //        reporteBean.strUnidadPaciente = "Rodilla";

            //        int iCodigoResultadoUno = (int)ObtenerResultadosCodigosDetalle((int)item.iCodigoDetalleReporte).iCodigoResultadoUno;
            //        int iCodigoLateralidad = (int)(ResultadoDL.ObtenerResultadoCodigo(iCodigoResultadoUno).iCodigoLateralidad);
            //        Lateralidad lat = dm.Lateralidads.Where(l => l.iCodigoLateralidad == iCodigoLateralidad).First();
            //        reporteBean.strLateralidadPaciente = lat.vNombre;

            //        ocltnReporteBeans.Add(reporteBean);
            //    }
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            //return ocltnReporteBeans;
            #endregion

            //to Fix it
            #region SQL compact connection
            ObservableCollection<ReporteBean> result = new ObservableCollection<ReporteBean>();

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT r.iCodigoReporte, r.iCodigoPaciente, r.iCodigoDetalleReporte, tr.iCodigoTipoReporte, tr.vNombreTipoReporte, r.dtFecReportePaciente, p.vNombresPaciente, p.vApellidosPaciente FROM Reporte r JOIN Paciente p ON r.iCodigoPaciente = p.iCodigoPaciente JOIN TipoReporte tr ON r.iCodigoTipoReporte = tr.iCodigoTipoReporte WHERE (r.iCodigoPaciente = @iCodigoPaciente ) AND (r.iCodigoTipoReporte = @iCodigoTipoReporte ) AND (r.dtFecReportePaciente >= @dtLimitInf ) AND (r.dtFecReportePaciente <= @dtLimitSup ) ORDER by r.dtFecReportePaciente DESC", conn);
                cmd.Parameters.AddWithValue("@iCodigoPaciente", (iCodigoPaciente == -1) ? (Int32?)null : iCodigoPaciente);
                cmd.Parameters.AddWithValue("@iCodigoTipoReporte", (iCodigoReporte == -1) ? (Int32?)null : iCodigoReporte);
                cmd.Parameters.AddWithValue("@dtLimitInf", (dtFecReportePacienteLimInf.ToString().Equals("01/01/0001 12:00:00 a.m.")) ? (DateTime?)null : dtFecReportePacienteLimInf.Date);
                cmd.Parameters.AddWithValue("@dtLimitSup", (dtFecReportePacienteLimSup.ToString().Equals("01/01/0001 12:00:00 a.m.")) ? (DateTime?)null : dtFecReportePacienteLimSup.Date);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ReporteBean f = new ReporteBean();
                    if (!DBNull.Value.Equals(rdr[0])) f.iCodigoReporte = (int)(rdr.GetInt64(0));
                    if (!DBNull.Value.Equals(rdr[1])) f.iCodigoPaciente = rdr.GetInt32(1);
                    if (!DBNull.Value.Equals(rdr[2])) f.iCodigoDetalleReporte = (int)rdr.GetInt64(2);
                    if (!DBNull.Value.Equals(rdr[3])) f.iCodigoTipoReporte = rdr.GetInt32(3);
                    if (!DBNull.Value.Equals(rdr[4])) f.strNombreTipoReporte = rdr.GetString(4);
                    if (!DBNull.Value.Equals(rdr[5])) f.strFecReportePaciente = rdr.GetDateTime(5).ToString();
                    if (!DBNull.Value.Equals(rdr[6])) f.strNombresPaciente = rdr.GetString(6);
                    if (!DBNull.Value.Equals(rdr[7])) f.strApellidosPaciente = rdr.GetString(7);
                    result.Add(f);
                }
                rdr.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
                return result;
            }
            finally
            {
                conn.Close();
            }

            return result;


            #endregion
        
        }

        public static ReporteDetalleB ObtenerResultadosCodigosDetalle(int iCodigoReporteDetalle)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    return dm.ReporteDetalles.Where(i => i.iCodigioReporteDetalle == iCodigoReporteDetalle).First();
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            #endregion

            //testear
            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            ReporteDetalleB f = new ReporteDetalleB();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From ReporteDetalle WHERE iCodigioReporteDetalle = @iCodigioReporteDetalle", conn);
                cmd.Parameters.AddWithValue("@iCodigioReporteDetalle", iCodigoReporteDetalle);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    f.iCodigioReporteDetalle = rdr.GetInt64(0);
                    f.iCodigoResultadoUno = rdr.GetInt64(1);
                    f.iCodigoResultadoDos = rdr.GetInt64(2);
                    f.iCodigoResultadoTres = rdr.GetInt64(3);
                    f.iCodigoResultadoCuatro = rdr.GetInt64(4);
                    f.iCodigoResultadoCinco = rdr.GetInt64(5);
                    f.iCodigoResultadoSeis = rdr.GetInt64(6);
                    f.iCodigoResultadoSiete = rdr.GetInt64(7);
                    f.iCodigoResultadoOcho = rdr.GetInt64(8);
                }
                rdr.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
                return f;
            }
            finally
            {
                conn.Close();
            }

            return f;


            #endregion
        }

        public static bool GuardarRutaPDF(int iCodigoReporte, string strRutaPDF)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    Reporte actualiarReporte = dm.Reportes.Where(r => r.iCodigoReporte == iCodigoReporte).First();
            //    actualiarReporte.vRutaReportePDF = strRutaPDF;
            //    dm.SubmitChanges();

            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            //return true;
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("UPDATE Reporte SET vRutaReportePDF = @vRutaReportePDF WHERE iCodigoReporte = @iCodigoReporte", conn);
                cmd.Parameters.AddWithValue("@iCodigoReporte", iCodigoReporte);
                cmd.Parameters.AddWithValue("@vRutaReportePDF", strRutaPDF);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            return true;


            #endregion
        }

        public static string ObtenerRutaPDF(int iCodigoReporte)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    Reporte reportePDF = dm.Reportes.Where(r => r.iCodigoReporte == iCodigoReporte).First();
            //    return (reportePDF.vRutaReportePDF == null) ? "" : reportePDF.vRutaReportePDF;

            //}
            //catch (Exception ex)
            //{
            //    return "";
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            ReporteB f = new ReporteB();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From Reporte WHERE iCodigoReporte = @iCodigoReporte", conn);
                cmd.Parameters.AddWithValue("@iCodigoReporte", iCodigoReporte);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    f.iCodigoReporte = rdr.GetInt64(0);
                    f.iCodigoPaciente = rdr.GetInt32(1);
                    f.iCodigoDetalleReporte = (int)rdr.GetInt64(2);
                    f.iCodigoTipoReporte = rdr.GetInt32(3);
                    f.dtFecReportePaciente = rdr.GetDateTime(4);
                    f.vRutaReportePDF = rdr.GetString(5);
                }
                rdr.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                conn.Close();
            }

            return (f.vRutaReportePDF == null) ? "" : f.vRutaReportePDF;;


            #endregion
        }
    }
}
