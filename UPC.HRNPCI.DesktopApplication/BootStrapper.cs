using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.HRNPCI.DesktopApplication._Service;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication.Views.Dialogs;

namespace UPC.HRNPCI.DesktopApplication
{
    class BootStrapper
    {
        public static void Initialize()
        {
            //initialize all the services needed for dependency injection
            //we use the unity framework for dependency injection, but you can choose others

            //-------------------------------- Modulo ADMIN  -----------------------------------//
            //Modulo
            ServiceProvider.RegisterServiceLocator(new UnityServiceLocator());
            //Fisioterapeuta
            ServiceProvider.RegisterServiceLocator1(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator2(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator3(new UnityServiceLocator());
            //Paciente
            ServiceProvider.RegisterServiceLocator4(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator5(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator6(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator7(new UnityServiceLocator());
            //Ver Resultados y reportes   // ver 2
            ServiceProvider.RegisterServiceLocator9(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator10(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator13(new UnityServiceLocator());
            //Buscar Resultados y reportes   
            ServiceProvider.RegisterServiceLocator11(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator12(new UnityServiceLocator());
                        
            //-----------------------------Modulo FISIOTERAPUETA -------------------------------//
            //Modulo
            ServiceProvider.RegisterServiceLocator8(new UnityServiceLocator());
            








            //Instances Registrations

            //-------------------------------- Modulo ADMIN  -----------------------------------//
            //Modulo
            ServiceProvider.Instance.Register<IModalDialog, ModuloAdminViewDialog>();
            //Fisioterapeuta
            ServiceProvider.Instance1.Register<IModalDialog, FisioterapeutaVerViewDialog>();
            ServiceProvider.Instance2.Register<IModalDialog, FisioterapeutaAgregarViewDialog>();
            ServiceProvider.Instance3.Register<IModalDialog, FisioterapeutaActualizarViewDialog>();
            //Paciente
            ServiceProvider.Instance4.Register<IModalDialog, PacienteVerViewDialog>();
            ServiceProvider.Instance5.Register<IModalDialog, PacienteAgregarViewDialog>();
            ServiceProvider.Instance6.Register<IModalDialog, PacienteActualizarViewDialog>();
            ServiceProvider.Instance7.Register<IModalDialog, AsociarPacienteFisioterapeutaViewDialog>();
            //Ver Resultados y reportes   //  ver 2
            ServiceProvider.Instance9.Register<IModalDialog, ResultadosVerViewDialog>();
            ServiceProvider.Instance10.Register<IModalDialog, ReportesVerViewDialog>();
            ServiceProvider.Instance13.Register<IModalDialog, ReportesVer2ViewDialog>();
            //Buscar Resultados y reportes 
            ServiceProvider.Instance11.Register<IModalDialog, ResultadosBuscarViewDialog>();
            ServiceProvider.Instance12.Register<IModalDialog, ReportesBuscarViewDialog>();

            //-----------------------------Modulo FISIOTERAPUETA -------------------------------//
            //Modulo
            ServiceProvider.Instance8.Register<IModalDialog, ModuloFisioterapeutaViewDialog>();
           


            
        }
    }
}
