using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;


namespace UPC.HRNPCI.Model.FisioterapeutaModel
{
    public class FisioterapeutaDL
    {
        static DataModelDataContext dm = new DataModelDataContext();


        public static int ValidarUsuario(string usuario, string contrasena)
        {
            //TODO Validar al fisioterapeuta tambien
            try
            {

                if (dm.Administradors.Where(i => i.vUsuarioAdministrador.Equals(usuario) && i.vContrasena.Equals(contrasena)).Any())
                    return 1;

                if (dm.Fisioterapeutas.Where(ii => ii.vUsuarioFiosioterapeuta.Equals(usuario) && ii.vContrasenaFisioterapeuta.Equals(contrasena)).Any())
                    return 0;


                return -1;
            }
            catch (Exception e)
            {
                throw e;
               
            }

        }

        public static List<Fisioterapeuta> ObtenerFisioterapeutas()
        {
            try
            {
                //return dm.Fisioterapeutas.OrderByDescending(i => i.vApellidosFisioterapeuta) as List<Fisioterapeuta>;
                return dm.Fisioterapeutas.ToList();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Fisioterapeuta VerFisioterapeuta(int id)
        {
            try
            {
                Fisioterapeuta aux = dm.Fisioterapeutas.Where(i => i.iCodigoFisioterapeuta == id).First();

                return aux;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool GuardarFisiotaerapeuta(Fisioterapeuta f)
        {
            try
            {

                dm.Fisioterapeutas.InsertOnSubmit(f);
                dm.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public static bool ActualizarFisioterapeuta(Fisioterapeuta f)
        {
            try
            {
                Fisioterapeuta aux = dm.Fisioterapeutas.Where(i => i.iCodigoFisioterapeuta == f.iCodigoFisioterapeuta).First();

                aux.vCelularFisioterapeuta = f.vCelularFisioterapeuta;
                aux.vTelefonoFisioterapeuta = f.vTelefonoFisioterapeuta;
                aux.vEmailFisioterapeuta = f.vEmailFisioterapeuta;
                dm.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool BorrarFisioterapeuta(int codigoFisioterapueta)
        {
            try
            {
                Fisioterapeuta aux = dm.Fisioterapeutas.Where(i => i.iCodigoFisioterapeuta == codigoFisioterapueta).First();
                dm.Fisioterapeutas.DeleteOnSubmit(aux);
                dm.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public static FisioterapeutaColumnas ObtenerColumnasFisioterapeuta()
        {

            var collection = dm.SP_SeleccionarColumnasFisioterapueta();
            FisioterapeutaColumnas columnas = new FisioterapeutaColumnas();
            foreach (var item in collection)
            {
                FisioterapeutaColumnas atributos = new FisioterapeutaColumnas();
                atributos.idColumna = ((int)item.column_id) -1 ;
                atributos.NombreColumnaDB = item.name;
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
            return columnas;
        }

        public static List<Fisioterapeuta> BuscaPorCampo(int columna, string buscar)
        {
            List<Fisioterapeuta> ListaFisioterapeutas = ObtenerFisioterapeutas();
            List<Fisioterapeuta> resultados = new List<Fisioterapeuta>();
           
            if (columna != -1)
            {

                int codColumna = Convert.ToInt32(columna);
                try
                {
                    switch (codColumna)
                    {
                        case 0: resultados = (from f in dm.Fisioterapeutas
                                              where f.iCodigoFisioterapeuta == Convert.ToInt32(buscar)
                                              select f).ToList<Fisioterapeuta>(); break;
                        case 1: resultados = (from f in dm.Fisioterapeutas
                                              where f.vNombresFisioterapeuta.Contains(buscar)
                                              select f).ToList<Fisioterapeuta>();break;
                        case 2: resultados = (from f in dm.Fisioterapeutas
                                              where f.vApellidosFisioterapeuta.Contains(buscar)
                                              select f).ToList<Fisioterapeuta>(); break;
                        case 3: resultados = (from f in dm.Fisioterapeutas
                                              where f.vRolFisioterapeuta.Contains(buscar)
                                              select f).ToList<Fisioterapeuta>(); break;
                        case 4: resultados = (from f in dm.Fisioterapeutas
                                              where f.vCelularFisioterapeuta.Contains(buscar)
                                              select f).ToList<Fisioterapeuta>(); break;
                        case 5: resultados = (from f in dm.Fisioterapeutas
                                              where f.vTelefonoFisioterapeuta.Contains(buscar)
                                              select f).ToList<Fisioterapeuta>(); break;
                        case 6: resultados = (from f in dm.Fisioterapeutas
                                              where f.vCentLabFisioterapeuta.Contains(buscar)
                                              select f).ToList<Fisioterapeuta>(); break;
                        case 7: resultados = (from f in dm.Fisioterapeutas
                                              where f.vEmailFisioterapeuta.Contains(buscar)
                                              select f).ToList<Fisioterapeuta>(); break;
                        case 8: resultados = (from f in dm.Fisioterapeutas
                                              where f.vNumCTMPFisioterapeuta.Contains(buscar)
                                              select f).ToList<Fisioterapeuta>(); break;
                        case 9: resultados = (from f in dm.Fisioterapeutas
                                              where f.vNumNDTAFisioterapeuta.Contains(buscar)
                                              select f).ToList<Fisioterapeuta>(); break;
                        case 10: resultados = (from f in dm.Fisioterapeutas
                                               where f.vContrasenaFisioterapeuta.Contains(buscar)
                                               select f).ToList<Fisioterapeuta>(); break;
                        case 11: resultados = (from f in dm.Fisioterapeutas
                                               where f.vUsuarioFiosioterapeuta.Contains(buscar)
                                               select f).ToList<Fisioterapeuta>(); break;
                        case 12: resultados = (from f in dm.Fisioterapeutas
                                               where f.iFlagBorradoFisioterapeuta == Convert.ToInt32(buscar)
                                               select f).ToList<Fisioterapeuta>(); break;
                        case 13: resultados = (from f in dm.Fisioterapeutas
                                               where f.vUsuarioFiosioterapeuta.Contains(buscar)
                                               select f).ToList<Fisioterapeuta>(); break;
                        case 14: resultados = (from f in dm.Fisioterapeutas
                                               where f.cGenero.Equals(Convert.ToChar(buscar))
                                               select f).ToList<Fisioterapeuta>(); break;
                    }
                    

                }
                catch (Exception ex)
                {
                    //throw (ex);
                    return resultados;
                }

            }
            return resultados;

            
        }

        public static FisioterapeutaComboBox ObtenerFisioterapuetasCombobox()
        {
            var collection = dm.SP_SeleccionarFisioterapuestasCombobox();
            FisioterapeutaComboBox fisioterapeutaComboBox = new FisioterapeutaComboBox();

            if (collection != null)
            {
                foreach (var fc in collection)
                {
                    FisioterapeutaComboBox fisioterapeutaComboBoxBean = new FisioterapeutaComboBox();
                    fisioterapeutaComboBoxBean.iCodigo = fc.iCodigoFisioterapeuta;
                    fisioterapeutaComboBoxBean.strNombre = fc.vNombresFisioterapeuta;
                    fisioterapeutaComboBoxBean.strApellidos = fc.VApellidosFisioterapeuta;
                    fisioterapeutaComboBox.lstFisioterapeutasComboBox.Add(fisioterapeutaComboBoxBean);
                }
                return fisioterapeutaComboBox;
            }
            return null;
        }

    }
}
