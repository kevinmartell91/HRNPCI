using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.HRNPCI.Model.FisioterapeutaModel;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta
{
    class FisioterapeutaStatic
    {
        //public static FisioterapeutaStatic instance = null;

        public static List<bool> ListaColumnasSeleccionadas { get; set; }
        public static FisioterapeutaColumnas FisioterapuestasColumnas {get;set;}
        public static bool Nombre { get; set; }
        public static bool TextBusquedaVacio {get;set;}
        public static List<double> anglesKneeAnalysisOne =null;
        public static List<double> anglesKneeAnalysisDos = null;

        public static string Rol = "Fisioterapueta Asignado";

        public static string kstrRutaFoto = "";

        public static bool kblnLoginExitoso = false;
       
        public static Model.FisioterapeutaModel.FisioterapeutaB FisioterapeutaLogueado { get; set; }

        public static bool blnPaciente = false;
        public static bool blnUnidad= false;
        public static bool blnLateralidad = false;
        public static bool blnGuardar = false;

        //public static FisioterapeutaStatic Instance()
        //{
        //    if (instance == null)
        //        instance = new FisioterapeutaStatic();
        //    return instance;
        //}

        //public FisioterapeutaStatic()
        //{

        //}

        public static void setAngles(List<double> angles, int i)
        {
            switch (i)
            {
                case 1:
                    if (anglesKneeAnalysisOne == null)
                        anglesKneeAnalysisOne = new List<double>();

                    anglesKneeAnalysisOne.Clear();

                    foreach (var item in angles)
                    {
                        anglesKneeAnalysisOne.Add(item);
                    }

                    break;

                case 2:
                    if (anglesKneeAnalysisDos == null)
                        anglesKneeAnalysisDos = new List<double>();

                    anglesKneeAnalysisDos.Clear();

                    foreach (var item in angles)
                    {
                        anglesKneeAnalysisDos.Add(item);
                    }

                    break;
            }





            //if (i == 1)
            //{
            //    if (anglesKneeAnalysisOne == null)
            //    {
            //        anglesKneeAnalysisOne = new List<double>();
            //    }

            //    anglesKneeAnalysisOne.Clear();

            //    foreach (var item in angles)
            //    {
            //        anglesKneeAnalysisOne.Add(item);
            //    }
            //}
            //else if (i == 2)
            //{
            //    if (anglesKneeAnalysisDos == null)
            //    {
            //        anglesKneeAnalysisDos = new List<double>();
            //    }

            //    anglesKneeAnalysisDos.Clear();

            //    foreach (var item in angles)
            //    {
            //        anglesKneeAnalysisDos.Add(item);
            //    }
            //}
        }
        public static List<double> getAngles(int i)
        {
            List<double> res = null;
            switch (i)
            {
                case 1: res = anglesKneeAnalysisOne; break;
                case 2: res = anglesKneeAnalysisDos; break;
            }
            return res;
        }

    }
}
