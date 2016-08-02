using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.HRNPCI.Model.PacienteModel;


namespace UPC.HRNPCI.DesktopApplication.ViewModels.Pacinete
{
    class PacienteStatic
    {
        public static List<bool> lstblnColumnasSeleccionadas { get; set; }
        public static PacienteColumnas PacienteColumnas { get; set; }
        public static bool blnNombre { get; set; }
        public static bool blnTextBusquedaVacio { get; set; }



        public static string kstrRutaFoto = "";

    }
}
