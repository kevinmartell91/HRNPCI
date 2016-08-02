using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using UPC.HRNPCI.DesktopApplication.ViewModels.ResultadosPacientesReportes;
using UPC.HRNPCI.DesktopApplication.ViewModels.Fisioterapueta;
using System.Windows;
using System.Globalization;
using UPC.HRNPCI.DesktopApplication.ViewModels.RutasAlmacenamiento;

namespace UPC.HRNPCI.DesktopApplication.Helpers
{
    class ReportPDF
    {
        public static string ToPdf(int iNumeroGraficasReporte)
        {
            string strNombreReporte = "";
            switch (iNumeroGraficasReporte)
            {
                case 1: strNombreReporte = "InformeCompleto"; break;
                case 2: strNombreReporte = "InformeCruzado"; break;
            }

            string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

            string local = RutasAlmacenamientoStatic.strRutaReportes;

            string path = local + "\\HRNPCI_" + time + "_" + strNombreReporte + "_" + ResultadosPacientesReportesStatic.strNombrePaciente + ".pdf";
           
            try
            {


                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                doc.Open();

                iTextSharp.text.Paragraph paragraph = null;

                switch (iNumeroGraficasReporte)
                {
                    case 1:  // reporte una grafica  (cruce de graficas de rodillas)
                        //ReportesVerViewModel resultadosPaciente1 = ResultadosPacientesReportesStatic.ViewReport1;
                        //string resultadosPaciente1 = ResultadosPacientesReportesStatic.strNombrePaciente;
                        //ResultadosPacientesReportesStatic.strNombrePaciente  
                        //ResultadosPacientesReportesStatic.iCodeReporte       
                        //ResultadosPacientesReportesStatic.strRutaPDF         
                        //ResultadosPacientesReportesStatic.strNivel 
                        //ResultadosPacientesReportesStatic.strPorcentajeNivel 


                        //paragraph = new iTextSharp.text.Paragraph(" Nombre " + resultadosPaciente1.strNombresPaciente + "\n" +
                        //                                                                    " Nivel " + resultadosPaciente1.strNivel);

                       // Font f =  new Font(
                        paragraph = new iTextSharp.text.Paragraph(" Nombre :  " + ResultadosPacientesReportesStatic.strNombrePaciente + "  " +
                                                                  "Nivel :" + ResultadosPacientesReportesStatic.strNivel + "    " +
                                                                    "Nivel % :" + ResultadosPacientesReportesStatic.strPorcentajeNivel);
                        break; 

                    case 2:   // reporte 2 graficas   (graficas de distintas rodillas por separado)
                        //ReportesVer2ViewModel resultadosPaciente2 = ResultadosPacientesReportesStatic.ViewReport2;
                        string resultadosPaciente2 = ResultadosPacientesReportesStatic.strNombrePaciente;

                        paragraph = new iTextSharp.text.Paragraph(" Nombre :  " + ResultadosPacientesReportesStatic.strNombrePaciente + "  " +
                                                                  "Nivel :" + ResultadosPacientesReportesStatic.strNivel + "    " +
                                                                    "Nivel % :" + ResultadosPacientesReportesStatic.strPorcentajeNivel);
                        break;
                }
                
                // Graficas de pacietnes resultados
                string rutafoto = ScreenCapture.TomarFotoObtenerRuta(iNumeroGraficasReporte);
                if (rutafoto == "ERROR")
                {
                    return "ERROR : La ruta de almacenamiento de fotos ha sidio eliminada, definir una nueva por favor.";
                }
                iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(rutafoto);
                    //PNG.ScaleAbsolute(new Rectangle(200, 200, 200, 200));
                    doc.Add(PNG);
                

                //doc.Add(paragraph);

                doc.Close();




            }
            catch (Exception ex)
            {
                return "ERROR :" + ex.ToString();
            }

            return path;
        }
    }
}
