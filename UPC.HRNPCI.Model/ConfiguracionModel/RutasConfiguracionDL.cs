using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;


namespace UPC.HRNPCI.Model.ConfiguracionModel
{
    public class RutasConfiguracionDL
    {
        //static DataModelTestDataContext dm = new DataModelTestDataContext();

        public static List<ConfiguracionB> ObtenerConfiguraciones()
        {
            // metodo agregado
            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            List<ConfiguracionB> result = new List<ConfiguracionB>();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From Configuracion", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                        ConfiguracionB f = new ConfiguracionB();
                        if (!DBNull.Value.Equals(rdr[0])) f.iCodigoConfiguracion = rdr.GetInt32(0);
                        if (!DBNull.Value.Equals(rdr[1])) f.vParametro = rdr.GetString(1);
                        if (!DBNull.Value.Equals(rdr[2])) f.vRutaEstatica = rdr.GetString(2);
                        if (!DBNull.Value.Equals(rdr[3])) f.dtFechaCreacion = rdr.GetDateTime(3);
                        if (!DBNull.Value.Equals(rdr[4])) f.dtFechaActualizacion = rdr.GetDateTime(4);
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

        public static ConfiguracionB GetRutaFotos()
        {

            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    List<Configuracion> configuraciones = dm.Configuracions.ToList();
            //    if (configuraciones == null || configuraciones.Count == 0)
            //        return null;

            //    return dm.Configuracions.Where(i => i.vParametro == "RutaFotos").First();
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
            #endregion

            #region sql compact edited
            try
            {
                //RemoteModelDataContext dm = new RemoteModelDataContext();
                List<ConfiguracionB> configuraciones = ObtenerConfiguraciones();
                if (configuraciones == null || configuraciones.Count == 0)
                    return null;

                return configuraciones.Where(i => i.vParametro == "RutaFotos").First();
            }
            catch (Exception e)
            {
                return null;
            }
            #endregion

        }

        public static ConfiguracionB GetRutaReportes()
        {

            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    List<ConfiguracionB> configuraciones = dm.Configuracions.ToList();
            //    if (configuraciones == null || configuraciones.Count == 0)
            //        return null;

            //    return dm.Configuracions.Where(i => i.vParametro == "RutaReportes").First();
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}

            #endregion
            
            #region sql compact connection
            try
            {
                //RemoteModelDataContext dm = new RemoteModelDataContext();
                List<ConfiguracionB> configuraciones = ObtenerConfiguraciones();
                if (configuraciones == null || configuraciones.Count == 0)
                    return null;

                return configuraciones.Where(i => i.vParametro == "RutaReportes").First();
            }
            catch (Exception e)
            {
                return null;
            }
            
            #endregion

        }

        public static bool GuardarConfiguracion(ConfiguracionB c, int modo)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    switch (modo)
            //    {
            //        case 0:  //guarda por primera vez
            //            dm.Configuracions.InsertOnSubmit(c);
            //            dm.SubmitChanges();
            //            break;

            //        case 1: //Actualiza ruta
            //            Configuracion aux = dm.Configuracions.Where(i => i.iCodigoConfiguracion == c.iCodigoConfiguracion).First();
            //            aux.vRutaEstatica = c.vRutaEstatica;
            //            aux.dtFechaActualizacion = c.dtFechaActualizacion;
            //            dm.SubmitChanges();
            //            break;
            //    }
            //}
            //catch (Exception)
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
                switch (modo)
                {
                    case 0:
                        conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                        conn.Open();
                        cmd = new SqlCeCommand("INSERT INTO Configuracion( " +
                                                "vParametro, " +
                                                "vRutaEstatica, " +
                                                "dtFechaCreacion, " +
                                                "dtFechaActualizacion ) " +
                                                "VALUES (" +
                                                "@vParametro, " +
                                                "@vRutaEstatica, " +
                                                "@dtFechaCreacion, " +
                                                "@dtFechaActualizacion ) ", conn);

                        cmd.Parameters.AddWithValue("@vParametro", (c.vParametro.Trim() as object) ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@vRutaEstatica", (c.vRutaEstatica.Trim() as object) ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@dtFechaCreacion", (c.dtFechaCreacion as object) ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@dtFechaActualizacion", (c.dtFechaActualizacion as object) ?? DBNull.Value);
                        
                        break;

                    case 1:
                         conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                        conn.Open();
                        cmd = new SqlCeCommand("UPDATE SET Configuracion " +
                                                "vParametro = @vParametro," +
                                                "dtFechaActualizacion = @dtFechaActualizacion " +
                                                "WHERE iCodigoConfiguracion = @iCodigoConfiguracion", conn);

                        cmd.Parameters.AddWithValue("@iCodigoConfiguracion", (c.iCodigoConfiguracion as object) ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@vParametro", (c.vRutaEstatica.Trim() as object) ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@dtFechaActualizacion", (c.dtFechaActualizacion as object) ?? DBNull.Value);

                        break;

                }
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

        public static string GenerarObtenerPathBuckup(string rutaLocalGuardarBuckup)
        {
            string path = "";
            try
            {
                //dm.SP_BackupDB(rutaLocalGuardarBuckup, ref path);
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return path;
        }

        //public static bool InsertRowAdministrador()
        //{
            
        //    #region SQL compact connection

        //    SqlCeConnection conn = null;
        //    SqlCeCommand cmd = null;
        //    try
        //    {

        //        conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
        //        conn.Open();
        //        cmd = new SqlCeCommand("INSERT INTO Administrador( vUsuarioAdministrador, vContrasena) VALUES (@vUsuarioAdministrador,@vContrasena)", conn);

        //        //cmd.Parameters.AddWithValue("@iCodigoAdministrador", 1);
        //        cmd.Parameters.AddWithValue("@vUsuarioAdministrador", "admin");
        //        cmd.Parameters.AddWithValue("@vContrasena", "admin");


        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        //return false;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return true;


        //    #endregion

        //}

        //public static bool GetRowAdministrador()
        //{

        //    #region SQL compact connection

        //    SqlCeConnection conn = null;
        //    SqlCeCommand cmd = null;
        //    SqlCeDataReader rdr = null;
        //    try
        //    {

        //        conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
        //        conn.Open();
        //        cmd = new SqlCeCommand("SELECT vUsuarioAdministrador FROM Administrador WHERE vContrasena = @vContrasena AND  vUsuarioAdministrador = @vUsuarioAdministrador", conn);

        //        //cmd.Parameters.AddWithValue("@iCodigoAdministrador", 1);
        //        cmd.Parameters.AddWithValue("@vUsuarioAdministrador", "admin");
        //        cmd.Parameters.AddWithValue("@vContrasena", "admin");


        //       rdr  = cmd.ExecuteReader();
                
        //       while (rdr.Read())
        //       { 
        //                if (rdr.GetString(0) == "admin")
        //                    return true;
                
        //       }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        //return false;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return false;


        //    #endregion

        //}
    }
}
