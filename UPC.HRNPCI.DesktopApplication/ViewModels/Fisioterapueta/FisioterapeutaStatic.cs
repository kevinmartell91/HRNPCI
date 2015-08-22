using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.HRNPCI.Model .FisioterapeutaModel;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta
{
    public static class FisioterapeutaStatic
    {
        public static List<bool> ListaColumnasSeleccionadas { get; set; }
        public static FisioterapeutaColumnas FisioterapuestasColumnas {get;set;}
        public static bool Nombre { get; set; }
        public static bool TextBusquedaVacio {get;set;}

        public static string Rol = "Fisioterapueta Asignado";

        public static string kstrRutaFoto = "E:\\UPC\\Tesis\\Software demos\\Primary demos\\HRNPCI_v1.3\\UPC.HRNPCI.DesktopApplication\\Imagenes\\Fisioterapeutas\\";

        public static bool kblnLoginExitoso = false;

    }
}
