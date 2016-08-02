using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPC.HRNPCI.Model.ConfiguracionModel
{
    public class ConfiguracionB
    {
        public int iCodigoConfiguracion { get; set; }
        public string vParametro { get; set; }
        public string vRutaEstatica { get; set; }
        public System.Nullable<System.DateTime> dtFechaCreacion { get; set; }
        public System.Nullable<System.DateTime> dtFechaActualizacion { get; set; }
    }
}
