using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPC.HRNPCI.Model.PacienteModel
{
    public class PacienteDL
    {

     

        static DataModelDataContext dm = new DataModelDataContext();

        public static List<Paciente> ObtenerPacientes()
        {
            try
            {
                //TODO join para mostrar el mombre de los fisiterapeuta y esta enlazaso al BuscarCampo
                return dm.Pacientes.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static Paciente VerPaciente(int pIdPaciente)
        {
            try
            {
                Paciente paciente = dm.Pacientes.Where(i => i.iCodigoPaciente == pIdPaciente).First();

                return paciente;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool GuardarPaciente(Paciente pPaciente)
        {
            try
            {

                dm.Pacientes.InsertOnSubmit(pPaciente);
                dm.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ActualizarPaciente(Paciente pPaciente)
        {
            try
            {
                Paciente paciente = dm.Pacientes.Where(i => i.iCodigoPaciente == pPaciente.iCodigoPaciente).First();

                paciente.vNombresApoderado = pPaciente.vNombresApoderado;
                paciente.vApellidoApoderado = pPaciente.vApellidoApoderado;
                paciente.vCelularApoderado = pPaciente.vCelularApoderado;
                paciente.vTelefonoApoderado = pPaciente.vTelefonoApoderado;
                paciente.vParentescoApoderado = pPaciente.vParentescoApoderado;
                dm.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
                return false;
            }
        }

        public static bool BorrarPaciente(int pIdPaciente)
        {
            try
            {
                Paciente paciente = dm.Pacientes.Where(i => i.iCodigoPaciente == pIdPaciente).First();
                dm.Pacientes.DeleteOnSubmit(paciente);
                dm.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static PacienteColumnas ObtenerColumnasPaciente()
        {

            var collection = dm.SP_SeleccionarColumnasPaciente();
            PacienteColumnas columnas = new PacienteColumnas();
            foreach (var item in collection)
            {
                PacienteColumnas atributos = new PacienteColumnas();
                atributos.iIdColumna = ((int)item.column_id) - 1;
                atributos.strNombreColumnaDB = item.name;
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
            return columnas;
        }

        public static List<Paciente> BuscaPorCampoPaciente(int piColumna, string pstrBuscar)
        {
            List<Paciente> lstResultados = new List<Paciente>();

            if (piColumna != -1)
            {
                int codColumna = Convert.ToInt32(piColumna);
                try
                {
                    switch (codColumna)
                    {
                        case 0: lstResultados = (from f in dm.Pacientes where f.iCodigoPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 1: lstResultados = (from f in dm.Pacientes where f.vNombresPaciente.Contains(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 2: lstResultados = (from f in dm.Pacientes where f.vApellidosPaciente.Contains(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 3: lstResultados = (from f in dm.Pacientes where f.cGeneroPaciente == Convert.ToChar(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 4: lstResultados = (from f in dm.Pacientes where f.vDiagnosticoMedicoPaciente == (pstrBuscar) select f).ToList<Paciente>(); break;
                        case 5: lstResultados = (from f in dm.Pacientes where f.iNivelPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 6: lstResultados = (from f in dm.Pacientes where f.iPorcentajeNivelPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 7: lstResultados = (from f in dm.Pacientes where f.vEdadCronologicaPaciente.Contains(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 8: lstResultados = (from f in dm.Pacientes where f.daFecNacPaciente == Convert.ToDateTime(pstrBuscar).Date select f).ToList<Paciente>(); break;
                        
                        
                        //TODO hacer un join en linq
                        case 9: lstResultados = (from f in dm.Pacientes where f.iCodigoFisioterapeutaUno == Convert.ToInt32(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 10: lstResultados = (from f in dm.Pacientes where f.iCodigoFisioterapeutaDos == Convert.ToInt32(pstrBuscar) select f).ToList<Paciente>(); break;
                        //
                        case 11: lstResultados = (from f in dm.Pacientes where f.cDNIPaciente.Contains(pstrBuscar) select f).ToList<Paciente>(); break;

                        case 12: lstResultados = (from f in dm.Pacientes where f.vNomApeMedNeuroReferencia.Contains(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 13: lstResultados = (from f in dm.Pacientes where f.vCelMedNeuroReferencia.Contains(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 14: lstResultados = (from f in dm.Pacientes where f.vParentescoApoderado.Contains(pstrBuscar) select f).ToList<Paciente>(); break;

                        case 15: lstResultados = (from f in dm.Pacientes where f.vNombresApoderado.Contains(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 16: lstResultados = (from f in dm.Pacientes where f.vApellidoApoderado.Contains(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 17: lstResultados = (from f in dm.Pacientes where f.vCelularApoderado.Contains(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 18: lstResultados = (from f in dm.Pacientes where f.vTelefonoApoderado.Contains(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 19: lstResultados = (from f in dm.Pacientes where f.iFlagBorrradoPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 20: lstResultados = (from f in dm.Pacientes where f.iFlagFisioAsigPaciente == Convert.ToInt32(pstrBuscar) select f).ToList<Paciente>(); break;
                        case 21: lstResultados = (from f in dm.Pacientes where f.vUrlFotoPaciente.Contains(pstrBuscar) select f).ToList<Paciente>(); break;


                    }
                    

                }
                catch (Exception ex)
                {
                    //throw (ex);
                    return lstResultados;
                }

            }
            return lstResultados;


        }


        public static bool AsignarFisioterapuetasPaciente(int piCodigoPaciente, int piCodigoFisioterapeuta01, int piCodigoFisioterapeuta02)
        {
            try
            {
                Paciente pacienteAsignarFisioterapeutas = dm.Pacientes.Where(i => i.iCodigoPaciente == piCodigoPaciente).First();

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
                dm.SubmitChanges();
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }

        }



        public static string ObtenerNombresCompletosFisioterapeutasAsignado(int piCodigoFisioterapeuta)
        {
            try
            {
                Fisioterapeuta fisioterapeuta = dm.Fisioterapeutas.Where(i=> i.iCodigoFisioterapeuta == piCodigoFisioterapeuta).First();
                return fisioterapeuta.vNombresFisioterapeuta + " " + fisioterapeuta.vApellidosFisioterapeuta;
            }
            catch (Exception ex)
            {
                
                return "";
            }
        }

        public static FisioterapeutaNombresCompletos ObtenerFisioterapeutasNombresCoplemtos()
        {

            var collection = dm.SP_SeleccionarNombresFisioterapeutas();
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

    }
}
