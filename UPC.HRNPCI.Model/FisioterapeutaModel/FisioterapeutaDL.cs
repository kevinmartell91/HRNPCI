using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using System.Collections.ObjectModel;
using System.Windows.Forms;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using UPC.HRNPCI.Model.ConfiguracionModel;


namespace UPC.HRNPCI.Model.FisioterapeutaModel
{
    public class FisioterapeutaDL
    {
        //static DataModelTestDataContext dm = new DataModelTestDataContext();

        public static int ValidarUsuario(string usuario, string contrasena)
        {

            #region LINQ to class conection
            ////TODO Validar al fisioterapeuta tambien
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    if (dm.Administradors.Where(i => i.vUsuarioAdministrador.Equals(usuario) && i.vContrasena.Equals(contrasena)).Any())

            //        return 1;

            //    if (dm.Fisioterapeutas.Where(ii => ii.vUsuarioFiosioterapeuta.Equals(usuario) && ii.vContrasenaFisioterapeuta.Equals(contrasena)).Any())
            //        return 0;


            //    return -1;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Error de conxión a base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    return -2;
            //}
            #endregion

            //bool firstInserted;
            //if(!RutasConfiguracionDL.GetRowAdministrador())
            //    firstInserted = RutasConfiguracionDL.InsertRowAdministrador();

            #region SQL compact connection

            if (usuario == "" && contrasena == "")
                return -1;

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            string u = ""; 
            string p = "";
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();

                cmd = new SqlCeCommand("SELECT * From Administrador WHERE vUsuarioAdministrador = @vUsuarioAdministrador AND vContrasena = @vContrasena ", conn);
                cmd.Parameters.AddWithValue("@vContrasena", contrasena);
                cmd.Parameters.AddWithValue("@vUsuarioAdministrador", usuario);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    u = rdr.GetString(1);   // or: rdr["EmployeeKey"];
                    p = rdr.GetString(2); // or: rdr["FirstName"];
                }
                rdr.Close();
                cmd.Dispose();

                if (u == usuario && p == contrasena)
                    return 1;
               


                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();

                cmd = new SqlCeCommand("SELECT * From Fisioterapeuta WHERE vUsuarioFiosioterapeuta = @vUsuarioFiosioterapeuta AND vContrasenaFisioterapeuta = @vContrasenaFisioterapeuta ", conn);
                cmd.Parameters.AddWithValue("@vContrasenaFisioterapeuta", contrasena);
                cmd.Parameters.AddWithValue("@vUsuarioFiosioterapeuta", usuario);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    u = rdr.GetString(11);   // or: rdr["EmployeeKey"];
                    p = rdr.GetString(10); // or: rdr["FirstName"];
                }
                rdr.Close();
                cmd.Dispose();

                if (u == usuario && p == contrasena)
                    return 0;
              
                return -1;


            }
            catch (Exception ex)
            {
                return -2;
            }
            finally
            {
                conn.Close();
            }

            #endregion

        }

        public static List<FisioterapeutaB> ObtenerFisioterapeutas()
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    //return dm.Fisioterapeutas.OrderByDescending(i => i.vApellidosFisioterapeuta) as List<Fisioterapeuta>;
            //    return dm.Fisioterapeutas.ToList();

            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            List<FisioterapeutaB> result = new List<FisioterapeutaB>();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From Fisioterapeuta", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetInt32(12) == 0)
                    {
                        FisioterapeutaB f = new FisioterapeutaB();
                        if (!DBNull.Value.Equals(rdr[0])) f.iCodigoFisioterapeuta = rdr.GetInt32(0);
                        if (!DBNull.Value.Equals(rdr[1])) f.vNombresFisioterapeuta = rdr.GetString(1);
                        if (!DBNull.Value.Equals(rdr[2])) f.vApellidosFisioterapeuta = rdr.GetString(2);
                        if (!DBNull.Value.Equals(rdr[3])) f.vRolFisioterapeuta = rdr.GetString(3);
                        if (!DBNull.Value.Equals(rdr[4])) f.vCelularFisioterapeuta = rdr.GetString(4);
                        if (!DBNull.Value.Equals(rdr[5])) f.vTelefonoFisioterapeuta = rdr.GetString(5);
                        if (!DBNull.Value.Equals(rdr[6])) f.vCentLabFisioterapeuta = rdr.GetString(6);
                        if (!DBNull.Value.Equals(rdr[7])) f.vEmailFisioterapeuta = rdr.GetString(7);
                        if (!DBNull.Value.Equals(rdr[8])) f.vNumCTMPFisioterapeuta = rdr.GetString(8);
                        if (!DBNull.Value.Equals(rdr[9])) f.vNumNDTAFisioterapeuta = rdr.GetString(9);
                        if (!DBNull.Value.Equals(rdr[10])) f.vContrasenaFisioterapeuta = rdr.GetString(10);
                        if (!DBNull.Value.Equals(rdr[11])) f.vUsuarioFiosioterapeuta = rdr.GetString(11);
                        if (!DBNull.Value.Equals(rdr[12])) f.iFlagBorradoFisioterapeuta = rdr.GetInt32(12);
                        if (!DBNull.Value.Equals(rdr[13])) f.vUrlFotoFosioterapeuta = rdr.GetString(13);
                        if (!DBNull.Value.Equals(rdr[14])) f.cGenero = rdr.GetString(14);

                        result.Add(f);
                    }
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

        public static FisioterapeutaB VerFisioterapeuta(int id)
        {
            #region linq to class

            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    Fisioterapeuta aux = dm.Fisioterapeutas.Where(i => i.iCodigoFisioterapeuta == id).First();

            //    return aux;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            FisioterapeutaB result = new FisioterapeutaB();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From Fisioterapeuta where iCodigoFisioterapeuta = @iCodigoFisioterapeuta", conn);
                cmd.Parameters.AddWithValue("@iCodigoFisioterapeuta", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    FisioterapeutaB f = new FisioterapeutaB();
                    f.iCodigoFisioterapeuta = rdr.GetInt32(0);
                    f.vNombresFisioterapeuta = rdr.GetString(1);
                    f.vApellidosFisioterapeuta = rdr.GetString(2);
                    f.vRolFisioterapeuta = rdr.GetString(3);
                    f.vCelularFisioterapeuta = rdr.GetString(4);
                    f.vTelefonoFisioterapeuta = rdr.GetString(5);
                    f.vCentLabFisioterapeuta = rdr.GetString(6);
                    f.vEmailFisioterapeuta = rdr.GetString(7);
                    f.vNumCTMPFisioterapeuta = rdr.GetString(8);
                    f.vNumNDTAFisioterapeuta = rdr.GetString(9);
                    f.vContrasenaFisioterapeuta = rdr.GetString(10);
                    f.vUsuarioFiosioterapeuta = rdr.GetString(11);
                    f.iFlagBorradoFisioterapeuta = rdr.GetInt32(12);
                    f.vUrlFotoFosioterapeuta = rdr.GetString(13);
                    f.cGenero = rdr.GetString(14);
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

        public static bool GuardarFisiotaerapeuta(FisioterapeutaB f)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    dm.Fisioterapeutas.InsertOnSubmit(f);
            //    dm.SubmitChanges();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("INSERT INTO Fisioterapeuta( " +
                                        "vNombresFisioterapeuta," +
                                        "vApellidosFisioterapeuta," +
                                        "vRolFisioterapeuta," +
                                        "vCelularFisioterapeuta," +
                                        "vTelefonoFisioterapeuta," +
                                        "vCentLabFisioterapeuta," +
                                        "vEmailFisioterapeuta," +
                                        "vNumCTMPFisioterapeuta," +
                                        "vNumNDTAFisioterapeuta," +
                                        "vContrasenaFisioterapeuta," +
                                        "vUsuarioFiosioterapeuta," +
                                        "iFlagBorradoFisioterapeuta," +
                                        "vUrlFotoFosioterapeuta," +
                                        "cGenero)" +
                                        "VALUES (" +
                                        "@vNombresFisioterapeuta," +
                                        "@vApellidosFisioterapeuta," +
                                        "@vRolFisioterapeuta," +
                                        "@vCelularFisioterapeuta," +
                                        "@vTelefonoFisioterapeuta," +
                                        "@vCentLabFisioterapeuta," +
                                        "@vEmailFisioterapeuta," +
                                        "@vNumCTMPFisioterapeuta," +
                                        "@vNumNDTAFisioterapeuta," +
                                        "@vContrasenaFisioterapeuta," +
                                        "@vUsuarioFiosioterapeuta," +
                                        "@iFlagBorradoFisioterapeuta," +
                                        "@vUrlFotoFosioterapeuta," +
                                        "@cGenero)", conn);

                cmd.Parameters.AddWithValue("@iCodigoFisioterapeuta", (f.iCodigoFisioterapeuta as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vNombresFisioterapeuta", (f.vNombresFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vApellidosFisioterapeuta", (f.vApellidosFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vRolFisioterapeuta", (f.vRolFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vCelularFisioterapeuta", (f.vCelularFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vTelefonoFisioterapeuta", (f.vTelefonoFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vCentLabFisioterapeuta", (f.vCentLabFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vEmailFisioterapeuta", (f.vEmailFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vNumCTMPFisioterapeuta", (f.vNumCTMPFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vNumNDTAFisioterapeuta", (f.vNumNDTAFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vContrasenaFisioterapeuta", (f.vContrasenaFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vUsuarioFiosioterapeuta", (f.vUsuarioFiosioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iFlagBorradoFisioterapeuta", (f.iFlagBorradoFisioterapeuta as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vUrlFotoFosioterapeuta", (f.vUrlFotoFosioterapeuta as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@cGenero", (f.cGenero) as object ?? DBNull.Value);

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

        public static bool ActualizarFisioterapeuta(FisioterapeutaB f)
        {
            #region linq to class

            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    Fisioterapeuta aux = dm.Fisioterapeutas.Where(i => i.iCodigoFisioterapeuta == f.iCodigoFisioterapeuta).First();

            //    aux.vCelularFisioterapeuta = f.vCelularFisioterapeuta;
            //    aux.vTelefonoFisioterapeuta = f.vTelefonoFisioterapeuta;
            //    aux.vEmailFisioterapeuta = f.vEmailFisioterapeuta;
            //    dm.SubmitChanges();
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("UPDATE Fisioterapeuta SET " +
                                        "vCelularFisioterapeuta = @vCelularFisioterapeuta," +
                                        "vTelefonoFisioterapeuta = @vTelefonoFisioterapeuta," +
                                        "vEmailFisioterapeuta = @vEmailFisioterapeuta " +
                                        "WHERE iCodigoFisioterapeuta = @iCodigoFisioterapeuta", conn);

                cmd.Parameters.AddWithValue("@vCelularFisioterapeuta", (f.vCelularFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vTelefonoFisioterapeuta", (f.vTelefonoFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vEmailFisioterapeuta", (f.vEmailFisioterapeuta.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iCodigoFisioterapeuta", (f.iCodigoFisioterapeuta as object) ?? DBNull.Value);

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

        public static bool BorrarFisioterapeuta(int codigoFisioterapueta)
        {
            #region linq to class
            //try
            //{
            //    // DataModelTestDataContext dm1 = new DataModelTestDataContext();
            //    Fisioterapeuta aux = dm.Fisioterapeutas.First(i => i.iCodigoFisioterapeuta == codigoFisioterapueta);
            //    dm.Fisioterapeutas.DeleteOnSubmit(aux);
            //    dm.SubmitChanges();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return false;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("UPDATE Fisioterapeuta SET " +
                                        "iFlagBorradoFisioterapeuta = @iFlagBorradoFisioterapeuta ", conn);

                cmd.Parameters.AddWithValue("@iFlagBorradoFisioterapeuta", 1);
                cmd.Parameters.AddWithValue("@iCodigoFisioterapeuta", codigoFisioterapueta);

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

        public static FisioterapeutaColumnas ObtenerColumnasFisioterapeuta()
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    var collection = dm.SP_SeleccionarColumnasFisioterapueta();
            //    FisioterapeutaColumnas columnas = new FisioterapeutaColumnas();
            //    foreach (var item in collection)
            //    {
            //        FisioterapeutaColumnas atributos = new FisioterapeutaColumnas();
            //        atributos.idColumna = ((int)item.column_id) - 1;
            //        atributos.NombreColumnaDB = item.name;
            //        switch (atributos.idColumna)
            //        {
            //            case 0: atributos.NombreColumna = "Código"; break;
            //            case 1: atributos.NombreColumna = "Nombres"; break;
            //            case 2: atributos.NombreColumna = "Apellidos"; break;
            //            case 3: atributos.NombreColumna = "Rol"; break;
            //            case 4: atributos.NombreColumna = "Celular"; break;
            //            case 5: atributos.NombreColumna = "Teléfono"; break;
            //            case 6: atributos.NombreColumna = "Cent.Loboral"; break;
            //            case 7: atributos.NombreColumna = "Email"; break;
            //            case 8: atributos.NombreColumna = "N° CTMP"; break;
            //            case 9: atributos.NombreColumna = "N° NDTA"; break;
            //            case 10: atributos.NombreColumna = "Contraseña"; break;
            //            case 11: atributos.NombreColumna = "Usuario"; break;
            //            case 12: atributos.NombreColumna = "Borrado"; break;
            //            case 13: atributos.NombreColumna = "Url Foto"; break;
            //            case 14: atributos.NombreColumna = "Género"; break;
            //        }
            //        columnas.ListaColumnasFisioterapeuta.Add(atributos);
            //    }
            //    return columnas;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            #endregion

            #region SQL compact connection x  Hardcoded

            //SqlCeConnection conn = null;
            //SqlCeCommand cmd = null;
            //SqlCeDataReader rdr = null;
            FisioterapeutaColumnas columnas = new FisioterapeutaColumnas();

            try
            {
                //conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                //conn.Open();
                //cmd = new SqlCeCommand(@"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Fisioterapeuta'", conn);
                //rdr = cmd.ExecuteReader();

                //while (rdr.Read())
                //{
                for (int i = 0; i < 15; i++)
                {
                    
                    FisioterapeutaColumnas atributos = new FisioterapeutaColumnas();
                    atributos.idColumna = i ;
                    atributos.NombreColumnaDB = "";
                    switch (atributos.idColumna)
                    {
                        case 0: atributos.NombreColumna = "Código"; break;
                        case 1: atributos.NombreColumna = "Nombres"; break;
                        case 2: atributos.NombreColumna = "Apellidos"; break;
                        case 3: atributos.NombreColumna = "Rol"; break;
                        case 4: atributos.NombreColumna = "Celular"; break;
                        case 5: atributos.NombreColumna = "Teléfono"; break;
                        case 6: atributos.NombreColumna = "Cent.Loboral"; break;
                        case 7: atributos.NombreColumna = "Email"; break;
                        case 8: atributos.NombreColumna = "N° CTMP"; break;
                        case 9: atributos.NombreColumna = "N° NDTA"; break;
                        case 10: atributos.NombreColumna = "Contraseña"; break;
                        case 11: atributos.NombreColumna = "Usuario"; break;
                        case 12: atributos.NombreColumna = "Borrado"; break;
                        case 13: atributos.NombreColumna = "Url Foto"; break;
                        case 14: atributos.NombreColumna = "Género"; break;
                    }
                    columnas.ListaColumnasFisioterapeuta.Add(atributos);
                }

                //}


            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                //conn.Close();
            }

            return columnas;


            #endregion
        }

        public static List<FisioterapeutaB> BuscaPorCampo(int columna, string buscar)
        {
            #region Sql compact edited
            List<FisioterapeutaB> resultados = new List<FisioterapeutaB>();

            if (columna != -1)
            {

                int codColumna = Convert.ToInt32(columna);
                try
                {
                    //RemoteModelDataContext dm = new RemoteModelDataContext();
                    switch (codColumna)
                    {
                        case 0: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.iCodigoFisioterapeuta == Convert.ToInt32(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 1: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.vNombresFisioterapeuta.Contains(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 2: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.vApellidosFisioterapeuta.Contains(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 3: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.vRolFisioterapeuta.Contains(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 4: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.vCelularFisioterapeuta.Contains(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 5: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.vTelefonoFisioterapeuta.Contains(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 6: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.vCentLabFisioterapeuta.Contains(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 7: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.vEmailFisioterapeuta.Contains(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 8: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.vNumCTMPFisioterapeuta.Contains(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 9: resultados = (from f in ObtenerFisioterapeutas()
                                              where f.vNumNDTAFisioterapeuta.Contains(buscar)
                                              select f).ToList<FisioterapeutaB>(); break;
                        case 10: resultados = (from f in ObtenerFisioterapeutas()
                                               where f.vContrasenaFisioterapeuta.Contains(buscar)
                                               select f).ToList<FisioterapeutaB>(); break;
                        case 11: resultados = (from f in ObtenerFisioterapeutas()
                                               where f.vUsuarioFiosioterapeuta.Contains(buscar)
                                               select f).ToList<FisioterapeutaB>(); break;
                        case 12: resultados = (from f in ObtenerFisioterapeutas()
                                               where f.iFlagBorradoFisioterapeuta == Convert.ToInt32(buscar)
                                               select f).ToList<FisioterapeutaB>(); break;
                        case 13: resultados = (from f in ObtenerFisioterapeutas()
                                               where f.vUsuarioFiosioterapeuta.Contains(buscar)
                                               select f).ToList<FisioterapeutaB>(); break;
                        case 14: resultados = (from f in ObtenerFisioterapeutas()
                                               where f.cGenero.Equals(Convert.ToChar(buscar))
                                               select f).ToList<FisioterapeutaB>(); break;
                    }


                }
                catch (Exception ex)
                {
                    //throw (ex);
                    return resultados;
                }

            }
            return resultados;

            #endregion
        }

        public static FisioterapeutaComboBox ObtenerFisioterapuetasCombobox()
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    var collection = dm.SP_SeleccionarFisioterapuestasCombobox();
            //    FisioterapeutaComboBox fisioterapeutaComboBox = new FisioterapeutaComboBox();

            //    if (collection != null)
            //    {
            //        foreach (var fc in collection)
            //        {
            //            FisioterapeutaComboBox fisioterapeutaComboBoxBean = new FisioterapeutaComboBox();
            //            fisioterapeutaComboBoxBean.iCodigo = fc.iCodigoFisioterapeuta;
            //            fisioterapeutaComboBoxBean.strNombre = fc.vNombresFisioterapeuta;
            //            fisioterapeutaComboBoxBean.strApellidos = fc.VApellidosFisioterapeuta;
            //            fisioterapeutaComboBox.lstFisioterapeutasComboBox.Add(fisioterapeutaComboBoxBean);
            //        }
            //        return fisioterapeutaComboBox;
            //    }
            //    else
            //        return null;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            FisioterapeutaComboBox result = new FisioterapeutaComboBox();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From Fisioterapeuta", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetInt32(12) == 0)
                    {
                        FisioterapeutaComboBox f = new FisioterapeutaComboBox();
                        f.iCodigo = rdr.GetInt32(0);
                        f.strNombre = rdr.GetString(1);
                        f.strApellidos = rdr.GetString(2);
                        result.lstFisioterapeutasComboBox.Add(f);
                    }
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

        public static ObservableCollection<FisioterapeutaPacientesBean> ObtenerPacientesDeFisioterapeuta(int iCodigoFisioterapueta)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    var collection = dm.SP_ListaPacienteDeFisioterapeutas(iCodigoFisioterapueta);
            //    ObservableCollection<FisioterapeutaPacientesBean> lstFpb = new ObservableCollection<FisioterapeutaPacientesBean>();
            //    foreach (var item in collection)
            //    {
            //        FisioterapeutaPacientesBean fpb = new FisioterapeutaPacientesBean();
            //        fpb.iCodigoPaciente = item.iCodigoPaciente;
            //        fpb.strNombresApellidos = item.nombre;
            //        lstFpb.Add(fpb);
            //    }
            //    return lstFpb;
            //}
            //catch (Exception ex)
            //{

            //    return null;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            ObservableCollection<FisioterapeutaPacientesBean> result = new ObservableCollection<FisioterapeutaPacientesBean>();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand(@"SELECT p.iCodigoPaciente, (p.vNombresPaciente + ' ' + p.vApellidosPaciente) as  nombre FROM Paciente p WHERE p.iCodigoFisioterapeutaDos = @iCodigoFisioterapeutaUno OR p.iCodigoFisioterapeutaUno = @iCodigoFisioterapeutaDos", conn);
                cmd.Parameters.AddWithValue("@iCodigoFisioterapeutaUno", iCodigoFisioterapueta);
                cmd.Parameters.AddWithValue("@iCodigoFisioterapeutaDos", iCodigoFisioterapueta);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                        /* f.iCodigoFisioterapeuta = rdr.GetInt32(0);
                        f.vNombresFisioterapeuta = rdr.GetString(1);
                        f.vApellidosFisioterapeuta = rdr.GetString(2);
                         */
                        FisioterapeutaPacientesBean f = new FisioterapeutaPacientesBean();
                        f.iCodigoPaciente = rdr.GetInt32(0);
                        f.strNombresApellidos = rdr.GetString(1);
                       // f.strApellidos = rdr.GetString(2);
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

        public static FisioterapeutaB ObtenerFisioterapeutaLoguedo(string user, string pass)
        {
            #region sql compact edited
            try
            {
                //RemoteModelDataContext dm = new RemoteModelDataContext();

                FisioterapeutaB aux = ObtenerFisioterapeutas().First(i => i.vUsuarioFiosioterapeuta.Equals(user) &&
                                                                i.vContrasenaFisioterapeuta.Equals(pass));

                return aux;
            }
            catch (Exception)
            {
                return null;
            }
            #endregion
        }

        public static List<UnidadB> ObtenerListaUnidades()
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();

            //    return dm.Unidads.ToList();
            //}
            //catch (Exception)
            //{

            //    return null;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            List<UnidadB> result = new List<UnidadB>();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From Unidad", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    UnidadB f = new UnidadB();
                    f.iCodigoUnidad = rdr.GetInt32(0);
                    f.vNombre = rdr.GetString(1);
                    f.vDescripcion = rdr.GetString(2);

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
        
        public static List<LateralidadB> ObtenerListaLateralidades()
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();

            //    return dm.Lateralidads.ToList();
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            List<LateralidadB> result = new List<LateralidadB>();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From Lateralidad", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    LateralidadB f = new LateralidadB();
                    f.iCodigoLateralidad = rdr.GetInt32(0);
                    f.vNombre = rdr.GetString(1);
                    f.vDescripcion = rdr.GetString(2);

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

        public static List<PlanoB> ObtenerListaPlanos()
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();

            //    return dm.Lateralidads.ToList();
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            #endregion

            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;
            List<PlanoB> result = new List<PlanoB>();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From Plano", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    PlanoB f = new PlanoB();
                    f.iCodigoPlano = rdr.GetInt32(0);
                    f.vNombre = rdr.GetString(1);
                    f.vDescripcion = rdr.GetString(2);

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

        //public static ObservableCollection<FisioterapeutaPacientesBean> ObtenerListaPacientesDeFisioterapeuta(int iCodigoPaciente)
        //{
        //    try
        //    {
        //        return dm.SP_ListaPacienteDeFisioterapeutas(iCodigoPaciente) as ObservableCollection<FisioterapeutaPacientesBean>;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}



    }
}
