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


namespace UPC.HRNPCI.Model.AdministradorModel
{
    public class AdministradorDL
    {
        public static bool UserAdminExist()
        {
            #region SQL compact connection

            string u = "";
            string p = "";

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            SqlCeDataReader rdr = null;

            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();

                cmd = new SqlCeCommand("SELECT * From Administrador WHERE vUsuarioAdministrador = @vUsuarioAdministrador AND vContrasena = @vContrasena ", conn);
                cmd.Parameters.AddWithValue("@vContrasena", "admin");
                cmd.Parameters.AddWithValue("@vUsuarioAdministrador", "admin");
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    u = rdr.GetString(1);   // or: rdr["EmployeeKey"];
                    p = rdr.GetString(2); // or: rdr["FirstName"];
                }
                rdr.Close();
                cmd.Dispose();

            }
            finally
            {
                conn.Close();
            }

            if (u == p)
                return true;
            else
                return false;

            #endregion
        }

        public static bool ActualizarAdministradorPassword(string user,string pPass)
        {
            #region SQL compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            
            try
            {
                int cod =1 ;
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("UPDATE Administrador SET " +
                                        "vContrasena = @vContrasena," +
                                        "vUsuarioAdministrador = @vUsuarioAdministrador " +
                                        "WHERE iCodigoAdministrador = @iCodigoAdministrador", conn);

                cmd.Parameters.AddWithValue("@vUsuarioAdministrador", (user.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vContrasena", (pPass.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iCodigoAdministrador", (cod as object) ?? DBNull.Value);


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

    }
}
