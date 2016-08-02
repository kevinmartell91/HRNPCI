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

namespace UPC.HRNPCI.Model.ResultadosReportesPaciente
{
    public class ResultadoDL
    {
        //static DataModelTestDataContext dm = new DataModelTestDataContext();

        public static ObservableCollection<ResultadoBean> ListarResultadosPacientes()
        {
            #region linq to class
            //ObservableCollection<ResultadoBean> ocltnResultados = new ObservableCollection<ResultadoBean>();
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    var collection = dm.SP_ListarResultadosPacientes();
            //    foreach (var item in collection)
            //    {
            //        ResultadoBean resultado = new ResultadoBean();
            //        resultado.iCodigoResultado = (int)item.biCodigoResultado;
            //        resultado.iCodigoPaciente = item.iCodigoPaciente;
            //        resultado.iCodigoPlano = item.iCodigoPlano;
            //        resultado.iCodigoLateralidad = (int)item.iCodigoLateralidad;
            //        resultado.iCodigoUnidad = (int)item.iCodigoUnidad;
            //        resultado.strListaAngulos = item.vListaAngulos;
            //        resultado.strFecAnalisisPaciente = ((DateTime)item.dtFecAnalisisPaciente).Date.ToString();
            //        resultado.strNombresPaciente = item.vNombresPaciente;
            //        resultado.strApellidosPaciente = item.vApellidosPaciente;
            //        resultado.strNombrePlano = item.vNombrePlano;
            //        resultado.strNombreLateralidad = item.vNombreLateralidad;
            //        resultado.strNombreUnidad = item.vNombreUnidad;

            //        ocltnResultados.Add(resultado);
            //    }
            //}
            //catch (Exception)
            //{
            //    return null;
            //} 
            //return ocltnResultados;
            #endregion

            #region SQL compact connection
            ObservableCollection<ResultadoBean> result = new ObservableCollection<ResultadoBean>();

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT  r.biCodigoResultado,p.iCodigoPaciente,pl.iCodigoPlano,r.iCodigoLateralidad,r.iCodigoUnidad,r.vListaAngulos, r.dtFecAnalisisPaciente , p.vNombresPaciente,p.vApellidosPaciente,pl.vNombre as vNombrePlano, l.vNombre as vNombreLateralidad, u.vNombre as vNombreUnidad FROM Resultado r JOIN Paciente p ON r.iCodigoPaciente = p.iCodigoPaciente JOIN Unidad u ON r.iCodigoUnidad = u.iCodigoUnidad JOIN Lateralidad l ON r.iCodigoLateralidad = l.iCodigoLateralidad JOIN Plano pl ON r.iCodigoPlano = pl.iCodigoPlano ORDER by r.dtFecAnalisisPaciente DESC", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                        ResultadoBean f = new ResultadoBean();
                        if (!DBNull.Value.Equals(rdr[0])) f.iCodigoResultado = (int)(rdr.GetInt64(0));
                        if (!DBNull.Value.Equals(rdr[1])) f.iCodigoPaciente = rdr.GetInt32(1);
                        if (!DBNull.Value.Equals(rdr[2])) f.iCodigoPlano = rdr.GetInt32(2);
                        if (!DBNull.Value.Equals(rdr[3])) f.iCodigoLateralidad = rdr.GetInt32(3);
                        if (!DBNull.Value.Equals(rdr[4])) f.iCodigoUnidad = rdr.GetInt32(4);
                        if (!DBNull.Value.Equals(rdr[5])) f.strListaAngulos = rdr.GetString(5);
                        if (!DBNull.Value.Equals(rdr[6])) f.strFecAnalisisPaciente = rdr.GetDateTime(6).ToString();
                        if (!DBNull.Value.Equals(rdr[7])) f.strNombresPaciente = rdr.GetString(7);
                        if (!DBNull.Value.Equals(rdr[8])) f.strApellidosPaciente = rdr.GetString(8);
                        if (!DBNull.Value.Equals(rdr[9])) f.strNombrePlano = rdr.GetString(9);
                        if (!DBNull.Value.Equals(rdr[10])) f.strNombreLateralidad = rdr.GetString(10);
                        if (!DBNull.Value.Equals(rdr[11])) f.strNombreUnidad = rdr.GetString(11);
                        result.Add(f);
                }
                rdr.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }

            return result;


            #endregion
        }

        public static int GuardarResultado(int iCodigoPaciente,int iCodigoPlano,int iCodigoLateralidad,int iCodigoUnidad,string strlstAngulos,DateTime dtFecAnalisisPaciente )
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    dm.SP_GuardarResultado(iCodigoPaciente, iCodigoPlano, iCodigoLateralidad, iCodigoUnidad, strlstAngulos, dtFecAnalisisPaciente.Date);
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
                cmd = new SqlCeCommand("INSERT INTO Resultado ( iCodigoPaciente, iCodigoPlano, iCodigoLateralidad, iCodigoUnidad, vListaAngulos, dtFecAnalisisPaciente) " +
                                        "VALUES (@iCodigoPaciente, @iCodigoPlano, @iCodigoLateralidad, @iCodigoUnidad, @vListaAngulos, @dtFecAnalisisPaciente)", conn);

                cmd.Parameters.AddWithValue("@iCodigoPaciente", iCodigoPaciente);
                cmd.Parameters.AddWithValue("@iCodigoPlano", iCodigoPlano);
                cmd.Parameters.AddWithValue("@iCodigoLateralidad", iCodigoLateralidad);
                cmd.Parameters.AddWithValue("@iCodigoUnidad", iCodigoUnidad);
                cmd.Parameters.AddWithValue("@vListaAngulos", strlstAngulos);
                cmd.Parameters.AddWithValue("@dtFecAnalisisPaciente", dtFecAnalisisPaciente);

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

        public static ObservableCollection<ResultadoBean> BusquedaResultado(int iCodigoPaciente, int iCodigoUnidad, int iCodigoLateralidad, DateTime dtFecReportePacienteLimInf, DateTime dtFecReportePacienteLimSup)
        {
            #region linq to class
            //ObservableCollection<ResultadoBean> ocltnResultados = new ObservableCollection<ResultadoBean>();
            //try
            //{

            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    var collection = dm.SP_BuscarResultados((iCodigoPaciente == -1) ? (int?)null : iCodigoPaciente,
            //                                            (iCodigoUnidad == -1) ? (int?)null : iCodigoUnidad,
            //                                            (iCodigoLateralidad == -1) ? (int?)null : iCodigoLateralidad,
            //                                            (dtFecReportePacienteLimInf.ToString().Equals("01/01/0001 12:00:00 a.m.")) ? (DateTime?)null : dtFecReportePacienteLimInf.Date,
            //                                            (dtFecReportePacienteLimSup.ToString().Equals("01/01/0001 12:00:00 a.m.")) ? (DateTime?)null : dtFecReportePacienteLimSup.Date);

            //    foreach (var item in collection)
            //    {
            //        ResultadoBean resultado = new ResultadoBean();
            //        resultado.iCodigoResultado = (int)item.biCodigoResultado;
            //        resultado.iCodigoPaciente = item.iCodigoPaciente;
            //        resultado.iCodigoPlano = item.iCodigoPlano;
            //        resultado.iCodigoLateralidad = (int)item.iCodigoLateralidad;
            //        resultado.iCodigoUnidad = (int)item.iCodigoUnidad;
            //        resultado.strListaAngulos = item.vListaAngulos;
            //        resultado.strFecAnalisisPaciente = ((DateTime)item.dtFecAnalisisPaciente).Date.ToString();
            //        resultado.strNombresPaciente = item.vNombresPaciente;
            //        resultado.strApellidosPaciente = item.vApellidosPaciente;
            //        resultado.strNombrePlano = item.vNombrePlano;
            //        resultado.strNombreLateralidad = item.vNombreLateralidad;
            //        resultado.strNombreUnidad = item.vNombreUnidad;

            //        ocltnResultados.Add(resultado);
            //    }

            //}
            //catch (Exception)
            //{
            //    return null;

            //}

            //return ocltnResultados;
            #endregion

            //to Fix it
            #region SQL compact connection
            ObservableCollection<ResultadoBean> result = new ObservableCollection<ResultadoBean>();

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                //cmd = new SqlCeCommand("SELECT  r.biCodigoResultado,p.iCodigoPaciente,pl.iCodigoPlano,r.iCodigoLateralidad,r.iCodigoUnidad,r.vListaAngulos, r.dtFecAnalisisPaciente , p.vNombresPaciente,p.vApellidosPaciente,pl.vNombre as vNombrePlano, l.vNombre as vNombreLateralidad, u.vNombre as vNombreUnidad FROM Resultado r JOIN Paciente p ON r.iCodigoPaciente = p.iCodigoPaciente JOIN Unidad u ON r.iCodigoUnidad = u.iCodigoUnidad JOIN Lateralidad l ON r.iCodigoLateralidad = l.iCodigoLateralidad JOIN Plano pl ON r.iCodigoPlano = pl.iCodigoPlano WHERE   (r.iCodigoPaciente = @iCodigoPaciente or @iCodigoPaciente IS NULL) AND (r.iCodigoUnidad = @iCodigoUnidad or @iCodigoUnidad IS NULL) AND (r.iCodigoLateralidad = @iCodigoLateralidad or @iCodigoLateralidad IS NULL) AND (r.dtFecAnalisisPaciente >= @dtLimitInf or @dtLimitInf Is NULL) AND (r.dtFecAnalisisPaciente <= @dtLimitSup or @dtLimitSup IS NULL) ORDER by r.dtFecAnalisisPaciente DESC ", conn);
                cmd = new SqlCeCommand("SELECT  r.biCodigoResultado,p.iCodigoPaciente,pl.iCodigoPlano,r.iCodigoLateralidad,r.iCodigoUnidad,r.vListaAngulos, r.dtFecAnalisisPaciente , p.vNombresPaciente,p.vApellidosPaciente,pl.vNombre as vNombrePlano, l.vNombre as vNombreLateralidad, u.vNombre as vNombreUnidad FROM Resultado r JOIN Paciente p ON r.iCodigoPaciente = p.iCodigoPaciente JOIN Unidad u ON r.iCodigoUnidad = u.iCodigoUnidad JOIN Lateralidad l ON r.iCodigoLateralidad = l.iCodigoLateralidad JOIN Plano pl ON r.iCodigoPlano = pl.iCodigoPlano WHERE   (r.iCodigoPaciente = @iCodigoPaciente ) AND (r.iCodigoUnidad = @iCodigoUnidad ) AND (r.iCodigoLateralidad = @iCodigoLateralidad ) AND (r.dtFecAnalisisPaciente >= @dtLimitInf ) AND (r.dtFecAnalisisPaciente <= @dtLimitSup ) ORDER by r.dtFecAnalisisPaciente DESC ", conn);

                cmd.Parameters.AddWithValue("@iCodigoPaciente", (iCodigoPaciente == -1) ? (Int32?)null : iCodigoPaciente);
                cmd.Parameters.AddWithValue("@iCodigoUnidad", (iCodigoUnidad == -1) ? (Int32?)null : iCodigoUnidad);
                cmd.Parameters.AddWithValue("@iCodigoLateralidad", (iCodigoLateralidad == -1) ? (Int32?)null : iCodigoLateralidad);
                cmd.Parameters.AddWithValue("@dtLimitInf",(dtFecReportePacienteLimInf.ToString().Equals("01/01/0001 12:00:00 a.m.")) ? (DateTime?)null : dtFecReportePacienteLimInf.Date);
                cmd.Parameters.AddWithValue("@dtLimitSup", (dtFecReportePacienteLimSup.ToString().Equals("01/01/0001 12:00:00 a.m.")) ? (DateTime?)null : dtFecReportePacienteLimSup.Date);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ResultadoBean f = new ResultadoBean();
                    if (!DBNull.Value.Equals(rdr[0])) f.iCodigoResultado = (int)(rdr.GetInt64(0));
                    if (!DBNull.Value.Equals(rdr[1])) f.iCodigoPaciente = rdr.GetInt32(1);
                    if (!DBNull.Value.Equals(rdr[2])) f.iCodigoPlano = rdr.GetInt32(2);
                    if (!DBNull.Value.Equals(rdr[3])) f.iCodigoLateralidad = rdr.GetInt32(3);
                    if (!DBNull.Value.Equals(rdr[4])) f.iCodigoUnidad = rdr.GetInt32(4);
                    if (!DBNull.Value.Equals(rdr[5])) f.strListaAngulos = rdr.GetString(5);
                    if (!DBNull.Value.Equals(rdr[6])) f.strFecAnalisisPaciente = rdr.GetDateTime(6).ToString();
                    if (!DBNull.Value.Equals(rdr[7])) f.strNombresPaciente = rdr.GetString(7);
                    if (!DBNull.Value.Equals(rdr[8])) f.strApellidosPaciente = rdr.GetString(8);
                    if (!DBNull.Value.Equals(rdr[9])) f.strNombrePlano = rdr.GetString(9);
                    if (!DBNull.Value.Equals(rdr[10])) f.strNombreLateralidad = rdr.GetString(10);
                    if (!DBNull.Value.Equals(rdr[11])) f.strNombreUnidad = rdr.GetString(11);
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

        public static ResultadoB ObtenerResultadoCodigo(int iCod)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    return dm.Resultados.Where(i => i.biCodigoResultado == iCod).First();
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            #endregion

            #region SQL compact connection
            ResultadoB result = new ResultadoB();

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT  * FROM Resultado " +
                                        "WHERE biCodigoResultado = @biCodigoResultado", conn);
                cmd.Parameters.AddWithValue("@biCodigoResultado",iCod);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (!DBNull.Value.Equals(rdr[0])) result.biCodigoResultado = (rdr.GetInt64(0));
                    if (!DBNull.Value.Equals(rdr[1])) result.iCodigoPaciente = rdr.GetInt32(1);
                    if (!DBNull.Value.Equals(rdr[2])) result.iCodigoPlano = rdr.GetInt32(2);
                    if (!DBNull.Value.Equals(rdr[3])) result.iCodigoLateralidad = rdr.GetInt32(3);
                    if (!DBNull.Value.Equals(rdr[4])) result.iCodigoUnidad = rdr.GetInt32(4);
                    if (!DBNull.Value.Equals(rdr[5])) result.vListaAngulos = rdr.GetString(5);
                    if (!DBNull.Value.Equals(rdr[6])) result.dtFecAnalisisPaciente = rdr.GetDateTime(6);
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

    }
}
