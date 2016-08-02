using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;

namespace UPC.HRNPCI.Model.PacienteModel
{
    public class PacienteDL
    {

        //static DataModelTestDataContext dm = new DataModelTestDataContext();

        public static List<PacienteB> ObtenerPacientes()
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    //TODO join para mostrar el mombre de los fisiterapeuta y esta enlazaso al BuscarCampo
            //    return dm.Pacientes.ToList();
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
            List<PacienteB> result = new List<PacienteB>();
            try
            {
                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("SELECT * From Paciente", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.GetInt32(19) == 0)
                    {
                        PacienteB f = new PacienteB();
                        if (!DBNull.Value.Equals(rdr[0])) f.iCodigoPaciente = rdr.GetInt32(0);
                        if (!DBNull.Value.Equals(rdr[1])) f.vNombresPaciente = rdr.GetString(1);
                        if (!DBNull.Value.Equals(rdr[2])) f.vApellidosPaciente = rdr.GetString(2);
                        if (!DBNull.Value.Equals(rdr[3])) f.cGeneroPaciente = rdr.GetString(3);
                        if (!DBNull.Value.Equals(rdr[4])) f.vDiagnosticoMedicoPaciente = rdr.GetString(4);
                        if (!DBNull.Value.Equals(rdr[5])) f.iNivelPaciente = rdr.GetInt32(5);
                        if (!DBNull.Value.Equals(rdr[6])) f.iPorcentajeNivelPaciente = rdr.GetInt32(6);
                        if (!DBNull.Value.Equals(rdr[7])) f.vEdadCronologicaPaciente = rdr.GetString(7);
                        if (!DBNull.Value.Equals(rdr[8])) f.daFecNacPaciente = rdr.GetDateTime(8);
                        if (!DBNull.Value.Equals(rdr[9])) f.iCodigoFisioterapeutaUno = rdr.GetInt32(9);
                        if (!DBNull.Value.Equals(rdr[10])) f.iCodigoFisioterapeutaDos = rdr.GetInt32(10);
                        if (!DBNull.Value.Equals(rdr[11])) f.cDNIPaciente = rdr.GetString(11);
                        if (!DBNull.Value.Equals(rdr[12])) f.vNomApeMedNeuroReferencia = rdr.GetString(12);
                        if (!DBNull.Value.Equals(rdr[13])) f.vCelMedNeuroReferencia = rdr.GetString(13);
                        if (!DBNull.Value.Equals(rdr[14])) f.vParentescoApoderado = rdr.GetString(14);
                        if (!DBNull.Value.Equals(rdr[15])) f.vNombresApoderado = rdr.GetString(15);
                        if (!DBNull.Value.Equals(rdr[16])) f.vApellidoApoderado = rdr.GetString(16);
                        if (!DBNull.Value.Equals(rdr[17])) f.vCelularApoderado = rdr.GetString(17);
                        if (!DBNull.Value.Equals(rdr[18])) f.vTelefonoApoderado = rdr.GetString(18);
                        if (!DBNull.Value.Equals(rdr[19])) f.iFlagBorrradoPaciente = rdr.GetInt32(19);
                        if (!DBNull.Value.Equals(rdr[20])) f.iFlagFisioAsigPaciente = rdr.GetInt32(20);
                        if (!DBNull.Value.Equals(rdr[21])) f.vUrlFotoPaciente = rdr.GetString(21);
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

        public static PacienteB VerPaciente(int pIdPaciente)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    PacienteB paciente = ObtenerPacientes().Where(i => i.iCodigoPaciente == pIdPaciente).First();

            //    return paciente;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            #endregion

            #region sql compact connection
            try
            {
                //RemoteModelDataContext dm = new RemoteModelDataContext();
                PacienteB paciente = ObtenerPacientes().Where(i => i.iCodigoPaciente == pIdPaciente).First();

                return paciente;
            }
            catch (Exception ex)
            {
                return null;
            }
            #endregion
        }

        public static bool GuardarPaciente(PacienteB pPaciente)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    dm.Pacientes.InsertOnSubmit(pPaciente);
            //    dm.SubmitChanges();
            //    return true;
            //}
            //catch
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
                cmd = new SqlCeCommand("INSERT INTO Paciente (vNombresPaciente, vApellidosPaciente, cGeneroPaciente, vDiagnosticoMedicoPaciente, iNivelPaciente, iPorcentajeNivelPaciente, vEdadCronologicaPaciente, daFecNacPaciente, vUrlFotoPaciente,iFlagFisioAsigPaciente, iFlagBorrradoPaciente, vTelefonoApoderado, vCelularApoderado, vApellidoApoderado, vNombresApoderado, vParentescoApoderado, vCelMedNeuroReferencia, vNomApeMedNeuroReferencia, cDNIPaciente, iCodigoFisioterapeutaDos, iCodigoFisioterapeutaUno) " +
                                        "VALUES (@vNombresPaciente, @vApellidosPaciente, @cGeneroPaciente, @vDiagnosticoMedicoPaciente, @iNivelPaciente, @iPorcentajeNivelPaciente, @vEdadCronologicaPaciente, @daFecNacPaciente, @vUrlFotoPaciente, @iFlagFisioAsigPaciente, @iFlagBorrradoPaciente, @vTelefonoApoderado, @vCelularApoderado, @vApellidoApoderado, @vNombresApoderado, @vParentescoApoderado, @vCelMedNeuroReferencia, @vNomApeMedNeuroReferencia, @cDNIPaciente, @iCodigoFisioterapeutaDos, @iCodigoFisioterapeutaUno)", conn);

                cmd.Parameters.AddWithValue("@vNombresPaciente", (pPaciente.vNombresPaciente.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vApellidosPaciente", (pPaciente.vApellidosPaciente.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@cGeneroPaciente", (pPaciente.cGeneroPaciente as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vDiagnosticoMedicoPaciente", (pPaciente.vDiagnosticoMedicoPaciente.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iNivelPaciente", (pPaciente.iNivelPaciente as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iPorcentajeNivelPaciente", (pPaciente.iPorcentajeNivelPaciente as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vEdadCronologicaPaciente", (pPaciente.vEdadCronologicaPaciente.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@daFecNacPaciente", (pPaciente.daFecNacPaciente as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vUrlFotoPaciente", (pPaciente.vUrlFotoPaciente.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iFlagFisioAsigPaciente", (pPaciente.iFlagFisioAsigPaciente as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iFlagBorrradoPaciente", (pPaciente.iFlagBorrradoPaciente as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vTelefonoApoderado", (pPaciente.vTelefonoApoderado.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vCelularApoderado", (pPaciente.vCelularApoderado.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vApellidoApoderado", (pPaciente.vApellidoApoderado.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vNombresApoderado", (pPaciente.vNombresApoderado.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vParentescoApoderado", (pPaciente.vParentescoApoderado.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vCelMedNeuroReferencia", (pPaciente.vCelMedNeuroReferencia.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vNomApeMedNeuroReferencia", (pPaciente.vNomApeMedNeuroReferencia.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@cDNIPaciente", (pPaciente.cDNIPaciente.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iCodigoFisioterapeutaUno", (pPaciente.iCodigoFisioterapeutaUno as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iCodigoFisioterapeutaDos", (pPaciente.iCodigoFisioterapeutaDos as object) ?? DBNull.Value);

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

        public static bool ActualizarPaciente(PacienteB pPaciente)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    Paciente paciente = dm.Pacientes.Where(i => i.iCodigoPaciente == pPaciente.iCodigoPaciente).First();

            //    paciente.vNombresApoderado = pPaciente.vNombresApoderado;
            //    paciente.vApellidoApoderado = pPaciente.vApellidoApoderado;
            //    paciente.vCelularApoderado = pPaciente.vCelularApoderado;
            //    paciente.vTelefonoApoderado = pPaciente.vTelefonoApoderado;
            //    paciente.vParentescoApoderado = pPaciente.vParentescoApoderado;
            //    dm.SubmitChanges();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    //throw (ex);
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
                cmd = new SqlCeCommand("UPDATE Paciente SET " +
                                        "vParentescoApoderado = @vParentescoApoderado," +
                                        "vNombresApoderado = @vNombresApoderado," +
                                        "vApellidoApoderado = @vApellidoApoderado, " +
                                        "vCelularApoderado = @vCelularApoderado, " +
                                        "vTelefonoApoderado = @vTelefonoApoderado " +
                                        "WHERE iCodigoPaciente = @iCodigoPaciente", conn);

                cmd.Parameters.AddWithValue("@iCodigoPaciente", (pPaciente.iCodigoPaciente as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vNombresApoderado", (pPaciente.vNombresApoderado.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vParentescoApoderado", (pPaciente.vParentescoApoderado.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vApellidoApoderado", (pPaciente.vApellidoApoderado.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vCelularApoderado", (pPaciente.vCelularApoderado.Trim() as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@vTelefonoApoderado", (pPaciente.vTelefonoApoderado.Trim() as object) ?? DBNull.Value);

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

        public static bool BorrarPaciente(int pIdPaciente)
        {
            #region linq to class
            //try
            //{
            //    //DataModelTestDataContext dm1 = new DataModelTestDataContext();
            //    Paciente paciente = dm.Pacientes.Where(i => i.iCodigoPaciente == pIdPaciente).First();
            //    dm.Pacientes.DeleteOnSubmit(paciente);
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
                cmd = new SqlCeCommand("UPDATE Paciente SET " +
                                        "iFlagBorrradoPaciente = @iFlagBorrradoPaciente " +
                                        "WHERE iCodigoPaciente = @iCodigoPaciente", conn);

                cmd.Parameters.AddWithValue("@iCodigoPaciente", pIdPaciente);
                cmd.Parameters.AddWithValue("@iFlagBorrradoPaciente", 1);

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

        public static PacienteColumnas ObtenerColumnasPaciente()
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    var collection = dm.SP_SeleccionarColumnasPaciente();
            //    PacienteColumnas columnas = new PacienteColumnas();
            //    foreach (var item in collection)
            //    {
            //        PacienteColumnas atributos = new PacienteColumnas();
            //        atributos.iIdColumna = ((int)item.column_id) - 1;
            //        atributos.strNombreColumnaDB = item.name;
            //        switch (atributos.iIdColumna)
            //        {
            //            case 0: atributos.strNombreColumna = "Código"; atributos.blnVisible = false; break;
            //            case 1: atributos.strNombreColumna = "Nombres"; atributos.blnVisible = false; break;
            //            case 2: atributos.strNombreColumna = "Apellidos"; atributos.blnVisible = false; break;
            //            case 3: atributos.strNombreColumna = "Género"; atributos.blnVisible = false; break;
            //            case 4: atributos.strNombreColumna = "Diagnóstico Médico"; atributos.blnVisible = false; break;
            //            case 5: atributos.strNombreColumna = "Nivel"; atributos.blnVisible = false; break;
            //            case 6: atributos.strNombreColumna = "% Nivel"; atributos.blnVisible = false; break;
            //            case 7: atributos.strNombreColumna = "Edad Cronológica"; atributos.blnVisible = false; break;
            //            case 8: atributos.strNombreColumna = "Fecha Nacimiento"; atributos.blnVisible = false; break;
            //            case 9: atributos.strNombreColumna = "Fisioterapeuta Asig. Uno"; atributos.blnVisible = false; break;
            //            case 10: atributos.strNombreColumna = "Fisioterapeuta Asig. Dos"; atributos.blnVisible = false; break;
            //            case 11: atributos.strNombreColumna = "DNI"; atributos.blnVisible = true; break;
            //            case 12: atributos.strNombreColumna = "Nom.Ape.Med.Neuro.Referencia"; atributos.blnVisible = true; break;
            //            case 13: atributos.strNombreColumna = "Cel.Med.Neuro.Referencia"; atributos.blnVisible = true; break;
            //            case 14: atributos.strNombreColumna = "Parentesco apoderado"; atributos.blnVisible = true; break;
            //            case 15: atributos.strNombreColumna = "Nombres apoderado"; atributos.blnVisible = true; break;
            //            case 16: atributos.strNombreColumna = "Apellido apoderado"; atributos.blnVisible = true; break;
            //            case 17: atributos.strNombreColumna = "Celular apoderado"; atributos.blnVisible = true; break;
            //            case 18: atributos.strNombreColumna = "Telefono apoderado"; atributos.blnVisible = true; break;
            //            case 19: atributos.strNombreColumna = "Borrrado de lista"; atributos.blnVisible = true; break;
            //            case 20: atributos.strNombreColumna = "Fisioteraputa Asignado"; atributos.blnVisible = true; break;
            //            case 21: atributos.strNombreColumna = "Url Foto"; atributos.blnVisible = true; break;
            //        }



            //        columnas.ocltnColumnasPaciente.Add(atributos);

            //    }
            //    return columnas;
            //}
            //catch (Exception ex)
            //{

            //    return null;
            //}
            #endregion

            #region sql compact edited

            PacienteColumnas columnas = new PacienteColumnas();

            try
            {
                for (int i = 0; i < 22; i++)
                {
                    PacienteColumnas atributos = new PacienteColumnas();
                    atributos.iIdColumna = i;
                    atributos.strNombreColumnaDB = "";
                    switch (atributos.iIdColumna)
                    {
                        case 0: atributos.strNombreColumna = "Código"; atributos.blnVisible = false; break;
                        case 1: atributos.strNombreColumna = "Nombres"; atributos.blnVisible = false; break;
                        case 2: atributos.strNombreColumna = "Apellidos"; atributos.blnVisible = false; break;
                        case 3: atributos.strNombreColumna = "Género"; atributos.blnVisible = false; break;
                        case 4: atributos.strNombreColumna = "Diagnóstico Médico"; atributos.blnVisible = false; break;
                        case 5: atributos.strNombreColumna = "Nivel"; atributos.blnVisible = false; break;
                        case 6: atributos.strNombreColumna = "% Nivel"; atributos.blnVisible = false; break;
                        case 7: atributos.strNombreColumna = "Edad Cronológica"; atributos.blnVisible = false; break;
                        case 8: atributos.strNombreColumna = "Fecha Nacimiento"; atributos.blnVisible = false; break;
                        case 9: atributos.strNombreColumna = "Fisioterapeuta Asig. Uno"; atributos.blnVisible = false; break;
                        case 10: atributos.strNombreColumna = "Fisioterapeuta Asig. Dos"; atributos.blnVisible = false; break;
                        case 11: atributos.strNombreColumna = "DNI"; atributos.blnVisible = true; break;
                        case 12: atributos.strNombreColumna = "Nom.Ape.Med.Neuro.Referencia"; atributos.blnVisible = true; break;
                        case 13: atributos.strNombreColumna = "Cel.Med.Neuro.Referencia"; atributos.blnVisible = true; break;
                        case 14: atributos.strNombreColumna = "Parentesco apoderado"; atributos.blnVisible = true; break;
                        case 15: atributos.strNombreColumna = "Nombres apoderado"; atributos.blnVisible = true; break;
                        case 16: atributos.strNombreColumna = "Apellido apoderado"; atributos.blnVisible = true; break;
                        case 17: atributos.strNombreColumna = "Celular apoderado"; atributos.blnVisible = true; break;
                        case 18: atributos.strNombreColumna = "Telefono apoderado"; atributos.blnVisible = true; break;
                        case 19: atributos.strNombreColumna = "Borrrado de lista"; atributos.blnVisible = true; break;
                        case 20: atributos.strNombreColumna = "Fisioteraputa Asignado"; atributos.blnVisible = true; break;
                        case 21: atributos.strNombreColumna = "Url Foto"; atributos.blnVisible = true; break;
                    }



                    columnas.ocltnColumnasPaciente.Add(atributos);

                }
               
            }
            catch (Exception ex)
            {

                return null;
            }

            return columnas;

            #endregion
        }

        public static List<PacienteB> BuscaPorCampoPaciente(int piColumna, string pstrBuscar)
        {
            #region sql compact edited
            List<PacienteB> lstResultados = new List<PacienteB>();

            if (piColumna != -1)
            {
                int codColumna = Convert.ToInt32(piColumna);
                try
                {
                    //RemoteModelDataContext dm = new RemoteModelDataContext();
                    switch (codColumna)
                    {
                        case 0: lstResultados = (from f in ObtenerPacientes() where f.iCodigoPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 1: lstResultados = (from f in ObtenerPacientes() where f.vNombresPaciente.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 2: lstResultados = (from f in ObtenerPacientes() where f.vApellidosPaciente.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 3: lstResultados = (from f in ObtenerPacientes() where f.cGeneroPaciente == pstrBuscar select f).ToList<PacienteB>(); break;
                        case 4: lstResultados = (from f in ObtenerPacientes() where f.vDiagnosticoMedicoPaciente == (pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 5: lstResultados = (from f in ObtenerPacientes() where f.iNivelPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 6: lstResultados = (from f in ObtenerPacientes() where f.iPorcentajeNivelPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 7: lstResultados = (from f in ObtenerPacientes() where f.vEdadCronologicaPaciente.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 8: lstResultados = (from f in ObtenerPacientes() where f.daFecNacPaciente == Convert.ToDateTime(pstrBuscar).Date select f).ToList<PacienteB>(); break;


                        //TODO hacer un join en linq
                        case 9: lstResultados = (from f in ObtenerPacientes() where f.iCodigoFisioterapeutaUno == Convert.ToInt32(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 10: lstResultados = (from f in ObtenerPacientes() where f.iCodigoFisioterapeutaDos == Convert.ToInt32(pstrBuscar) select f).ToList<PacienteB>(); break;
                        //
                        case 11: lstResultados = (from f in ObtenerPacientes() where f.cDNIPaciente.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;

                        case 12: lstResultados = (from f in ObtenerPacientes() where f.vNomApeMedNeuroReferencia.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 13: lstResultados = (from f in ObtenerPacientes() where f.vCelMedNeuroReferencia.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 14: lstResultados = (from f in ObtenerPacientes() where f.vParentescoApoderado.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;

                        case 15: lstResultados = (from f in ObtenerPacientes() where f.vNombresApoderado.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 16: lstResultados = (from f in ObtenerPacientes() where f.vApellidoApoderado.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 17: lstResultados = (from f in ObtenerPacientes() where f.vCelularApoderado.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 18: lstResultados = (from f in ObtenerPacientes() where f.vTelefonoApoderado.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 19: lstResultados = (from f in ObtenerPacientes() where f.iFlagBorrradoPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 20: lstResultados = (from f in ObtenerPacientes() where f.iFlagFisioAsigPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<PacienteB>(); break;
                        case 21: lstResultados = (from f in ObtenerPacientes() where f.vUrlFotoPaciente.Contains(pstrBuscar) select f).ToList<PacienteB>(); break;


                    }


                }
                catch (Exception ex)
                {
                    //throw (ex);
                    return lstResultados;
                }

            }
            return lstResultados;

            #endregion
        }

        public static bool AsignarFisioterapuetasPaciente(int piCodigoPaciente, int piCodigoFisioterapeuta01, int piCodigoFisioterapeuta02)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    Paciente pacienteAsignarFisioterapeutas = dm.Pacientes.Where(i => i.iCodigoPaciente == piCodigoPaciente).First();

            //    if (piCodigoFisioterapeuta01 > 0)
            //        pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaUno = piCodigoFisioterapeuta01;
            //    else
            //        pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaUno = null;
            //    if (piCodigoFisioterapeuta02 > 0)
            //        pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaDos = piCodigoFisioterapeuta02;
            //    else
            //        pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaDos = null;



            //    if (piCodigoFisioterapeuta01 == -1 && piCodigoFisioterapeuta02 == -1)
            //        pacienteAsignarFisioterapeutas.iFlagFisioAsigPaciente = 0;
            //    else
            //        pacienteAsignarFisioterapeutas.iFlagFisioAsigPaciente = 1;// 1 => SI tiene almento un fisioterapeuta asignado
            //    // 0 => NO tiene almento un fisioterapeuta asignado
            //    dm.SubmitChanges();
            //    return true;
            //}
            //catch (Exception Ex)
            //{
            //    return false;
            //}
            #endregion

            #region sql compact connection

            SqlCeConnection conn = null;
            SqlCeCommand cmd = null;
            try
            {
                PacienteB pacienteAsignarFisioterapeutas = ObtenerPacientes().Where(i => i.iCodigoPaciente == piCodigoPaciente).First();

                if (piCodigoFisioterapeuta01 > 0)
                    pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaUno = piCodigoFisioterapeuta01;
                else
                    pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaUno = null;
                if (piCodigoFisioterapeuta02 > 0)
                    pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaDos = piCodigoFisioterapeuta02;
                else
                    pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaDos = null;



                if (piCodigoFisioterapeuta01 == -1 && piCodigoFisioterapeuta02 == -1)
                    pacienteAsignarFisioterapeutas.iFlagFisioAsigPaciente = 0;
                else
                    pacienteAsignarFisioterapeutas.iFlagFisioAsigPaciente = 1;// 1 => SI tiene almento un fisioterapeuta asignado
                // 0 => NO tiene almento un fisioterapeuta asignado



                conn = new SqlCeConnection("Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));
                conn.Open();
                cmd = new SqlCeCommand("UPDATE Paciente SET " +
                                        "iCodigoFisioterapeutaUno = @iCodigoFisioterapeutaUno, " +
                                        "iCodigoFisioterapeutaDos = @iCodigoFisioterapeutaDos, " +
                                        "iFlagFisioAsigPaciente = @iFlagFisioAsigPaciente " +
                                        "WHERE iCodigoPaciente = @iCodigoPaciente", conn);
                // (pPaciente.iCodigoFisioterapeutaUno as object) ?? DBNull.Value)
                cmd.Parameters.AddWithValue("@iCodigoFisioterapeutaUno", (pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaUno as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iCodigoFisioterapeutaDos", (pacienteAsignarFisioterapeutas.iCodigoFisioterapeutaDos as object) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@iCodigoPaciente", piCodigoPaciente);
                cmd.Parameters.AddWithValue("@iFlagFisioAsigPaciente", (pacienteAsignarFisioterapeutas.iFlagFisioAsigPaciente as object) ?? DBNull.Value);

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

        public static string ObtenerNombresCompletosFisioterapeutasAsignado(int piCodigoFisioterapeuta)
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    Fisioterapeuta fisioterapeuta = dm.Fisioterapeutas.Where(i => i.iCodigoFisioterapeuta == piCodigoFisioterapeuta).First();
            //    return fisioterapeuta.vNombresFisioterapeuta + " " + fisioterapeuta.vApellidosFisioterapeuta;
            //}
            //catch (Exception ex)
            //{

            //    return "";
            //}
            #endregion
           
            #region sql compact connection
            try
            {
                //RemoteModelDataContext dm = new RemoteModelDataContext();
                FisioterapeutaModel.FisioterapeutaB fisioterapeuta = FisioterapeutaModel.FisioterapeutaDL.ObtenerFisioterapeutas().Where(i => i.iCodigoFisioterapeuta == piCodigoFisioterapeuta).First();
                return fisioterapeuta.vNombresFisioterapeuta + " " + fisioterapeuta.vApellidosFisioterapeuta;
            }
            catch (Exception ex)
            {

                return "";
            }
            #endregion
        }

        public static FisioterapeutaNombresCompletos ObtenerFisioterapeutasNombresCompletos()
        {
            #region linq to class
            //try
            //{
            //    //RemoteModelDataContext dm = new RemoteModelDataContext();
            //    var collection = dm.SP_SeleccionarNombresFisioterapeutas();
            //    FisioterapeutaNombresCompletos nombresFisioterapeutas = new FisioterapeutaNombresCompletos();
            //    foreach (var item in collection)
            //    {
            //        FisioterapeutaNombresCompletos datosFisioterapeuta = new FisioterapeutaNombresCompletos();
            //        datosFisioterapeuta.iCodigo = item.iCodigoFisioterapeuta;
            //        datosFisioterapeuta.strNombres = item.vNombresFisioterapeuta;
            //        datosFisioterapeuta.strApellidos = item.vApellidosFisioterapeuta;
            //        nombresFisioterapeutas.ocltnFisioterapeutasNombresCompletos.Add(datosFisioterapeuta);

            //    }
            //    return nombresFisioterapeutas;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            #endregion

            #region sql compact connection

            try
            {
                var collection = FisioterapeutaModel.FisioterapeutaDL.ObtenerFisioterapeutas();
                FisioterapeutaNombresCompletos nombresFisioterapeutas = new FisioterapeutaNombresCompletos();
                foreach (var item in collection)
                {
                    FisioterapeutaNombresCompletos datosFisioterapeuta = new FisioterapeutaNombresCompletos();
                    datosFisioterapeuta.iCodigo = item.iCodigoFisioterapeuta;
                    datosFisioterapeuta.strNombres = item.vNombresFisioterapeuta;
                    datosFisioterapeuta.strApellidos = item.vApellidosFisioterapeuta;
                    nombresFisioterapeutas.ocltnFisioterapeutasNombresCompletos.Add(datosFisioterapeuta);

                }
                return nombresFisioterapeutas;
            }
            catch (Exception ex)
            {
                return null;
            }

            #endregion


        }

    }
}
