using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPC.HRNPCI.Model.FisioterapeutaModel
{
    public class FisioterapeutaPacientesBean
    {
        public int iCodigoPaciente { get; set; }
        public string strNombresApellidos { get; set; }
    }

    public class ComboPersolizable
    {
        public int iCodigo { get; set; }
        public string strValor { get; set; }
    }

    public class FisioterapeutaB
    {
        public int iCodigoFisioterapeuta { get; set; }
        public string vNombresFisioterapeuta { get; set; }
        public string vApellidosFisioterapeuta { get; set; }
        public string vRolFisioterapeuta { get; set; }
        public string vCelularFisioterapeuta { get; set; }
        public string vTelefonoFisioterapeuta { get; set; }
        public string vCentLabFisioterapeuta { get; set; }
        public string vEmailFisioterapeuta { get; set; }
        public string vNumCTMPFisioterapeuta { get; set; }
        public string vNumNDTAFisioterapeuta { get; set; }
        public string vContrasenaFisioterapeuta { get; set; }
        public string vUsuarioFiosioterapeuta { get; set; }
        public int iFlagBorradoFisioterapeuta { get; set; }
        public string vUrlFotoFosioterapeuta { get; set; }
        public string cGenero { get; set; }
    }

    public class UnidadB
    {
        public int iCodigoUnidad { get; set; }
        public string vNombre { get; set; }
        public string vDescripcion { get; set; }
    }

    public class LateralidadB
    {
        public int iCodigoLateralidad { get; set; }
        public string vNombre { get; set; }
        public string vDescripcion { get; set; }
    }

    public class PlanoB
    {
        public int iCodigoPlano { get; set; }
        public string vNombre { get; set; }
        public string vDescripcion { get; set; }
    }
}
