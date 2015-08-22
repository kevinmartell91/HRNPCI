using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.HRNPCI.Model.FisioterapeutaModel;
using UPC.HRNPCI.Model;
using System.Collections.ObjectModel;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta
{
    class FisioterapeutaBusinessObject
    {

        public event EventHandler FisioterapeutasChanged;

        List<FisioterapeutaCRUDViewModel> listaFisioterapeutasCRUD { get; set; }

        public FisioterapeutaBusinessObject()
        {
            listaFisioterapeutasCRUD = ObtenerFisioterapeutasCRUD();
        }

        void OnFisioterapeutasChanged()
        {
            if (FisioterapeutasChanged != null)
                FisioterapeutasChanged(this, null);
        }

        public List<FisioterapeutaCRUDViewModel> ObtenerFisioterapeutasCRUD()
        {

            List<FisioterapeutaCRUDViewModel> listaFisioterapeutas = new List<FisioterapeutaCRUDViewModel>();
            List<Fisioterapeuta> aux = FisioterapeutaDL.ObtenerFisioterapeutas();

            IEnumerable<Fisioterapeuta> ordenadosFisioterapeutas = aux.OrderBy(f => f.vApellidosFisioterapeuta) ;
            foreach (var f in ordenadosFisioterapeutas)
            {
                FisioterapeutaCRUDViewModel fcrudvm = new FisioterapeutaCRUDViewModel();
                fcrudvm.Codigo = f.iCodigoFisioterapeuta;
                fcrudvm.Nombre = f.vNombresFisioterapeuta;
                fcrudvm.Apellidos = f.vApellidosFisioterapeuta;
                fcrudvm.Celular = f.vCelularFisioterapeuta;
                fcrudvm.Telefono = f.vTelefonoFisioterapeuta;
                fcrudvm.CentLaboral = f.vCentLabFisioterapeuta;
                fcrudvm.Email = f.vEmailFisioterapeuta;
                fcrudvm.Rol = f.vRolFisioterapeuta;
                fcrudvm.NCTMP = f.vNumCTMPFisioterapeuta;
                fcrudvm.NNDTA = f.vNumNDTAFisioterapeuta;
                fcrudvm.Sexo = (char)f.cGenero;
                fcrudvm.UrlFoto = f.vUrlFotoFosioterapeuta;
                fcrudvm.Usuario = f.vUsuarioFiosioterapeuta;
                fcrudvm.Contrasena = f.vContrasenaFisioterapeuta;
                fcrudvm.UrlFoto = f.vUrlFotoFosioterapeuta;
                listaFisioterapeutas.Add(fcrudvm);

            }
            return listaFisioterapeutas;
        }

        public List<FisioterapeutaCRUDViewModel> ObtenerFisioterapeutasBuscadosCRUD(int IdColumna, string Buscar)
        {

            List<FisioterapeutaCRUDViewModel> listaFisioterapeutaBuscar = new List<FisioterapeutaCRUDViewModel>();
            List<Fisioterapeuta> aux = FisioterapeutaDL.BuscaPorCampo(IdColumna, Buscar);

            if (aux != null)
            {
                foreach (var f in aux)
                {
                    FisioterapeutaCRUDViewModel fcrudvm = new FisioterapeutaCRUDViewModel();
                    fcrudvm.Codigo = f.iCodigoFisioterapeuta;
                    fcrudvm.Nombre = f.vNombresFisioterapeuta;
                    fcrudvm.Apellidos = f.vApellidosFisioterapeuta;
                    fcrudvm.Celular = f.vCelularFisioterapeuta;
                    fcrudvm.Telefono = f.vTelefonoFisioterapeuta;
                    fcrudvm.CentLaboral = f.vCentLabFisioterapeuta;
                    fcrudvm.Email = f.vEmailFisioterapeuta;
                    fcrudvm.Rol = f.vRolFisioterapeuta;
                    fcrudvm.NCTMP = f.vNumCTMPFisioterapeuta;
                    fcrudvm.NNDTA = f.vNumNDTAFisioterapeuta;
                    fcrudvm.Sexo = (char)f.cGenero;
                    fcrudvm.UrlFoto = f.vUrlFotoFosioterapeuta;

                    listaFisioterapeutaBuscar.Add(fcrudvm);

                }
                return listaFisioterapeutaBuscar;
            }
            return null;
        }
        
        //para actulizar  la lista de la aplicacion y para yo no estar actualizando directamente de la base de datos
        //agragar y actualizar
        public FisioterapeutaCRUDViewModel ObtenerFisioterapeutaCRUD(Fisioterapeuta f)
        {
            if (f != null)
            {
                FisioterapeutaCRUDViewModel fcrudvm = new FisioterapeutaCRUDViewModel();
                fcrudvm.Codigo = f.iCodigoFisioterapeuta;
                fcrudvm.Nombre = f.vNombresFisioterapeuta;
                fcrudvm.Apellidos = f.vApellidosFisioterapeuta;
                fcrudvm.Celular = f.vCelularFisioterapeuta;
                fcrudvm.Telefono = f.vTelefonoFisioterapeuta;
                fcrudvm.CentLaboral = f.vCentLabFisioterapeuta;
                fcrudvm.Email = f.vEmailFisioterapeuta;
                fcrudvm.Rol = f.vRolFisioterapeuta;
                fcrudvm.NCTMP = f.vNumCTMPFisioterapeuta;
                fcrudvm.NNDTA = f.vNumNDTAFisioterapeuta;
                fcrudvm.Sexo = (char)f.cGenero;
                fcrudvm.UrlFoto = f.vUrlFotoFosioterapeuta;
                fcrudvm.Usuario = f.vUsuarioFiosioterapeuta;
                fcrudvm.Contrasena = f.vContrasenaFisioterapeuta;
                fcrudvm.UrlFoto = f.vUrlFotoFosioterapeuta;

                return fcrudvm;
            }
            return null;

        }
    }
}
