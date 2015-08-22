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

            //   Fisioterapeuta
            ServiceProvider.RegisterServiceLocator(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator1(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator2(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator3(new UnityServiceLocator());
            //   Paciente
            ServiceProvider.RegisterServiceLocator4(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator5(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator6(new UnityServiceLocator());
            ServiceProvider.RegisterServiceLocator7(new UnityServiceLocator());

            //register the IModalDialog using an instance of the CustomerViewDialog
            //this sets up the view

            //   Fisioterapeuta
            ServiceProvider.Instance.Register<IModalDialog, ModuloAdminViewDialog>();
            ServiceProvider.Instance1.Register<IModalDialog, FisioterapeutaVerViewDialog>();
            ServiceProvider.Instance2.Register<IModalDialog, FisioterapeutaAgregarViewDialog>();
            ServiceProvider.Instance3.Register<IModalDialog, FisioterapeutaActualizarViewDialog>();
            //   Paciente
            ServiceProvider.Instance4.Register<IModalDialog, PacienteVerViewDialog>();
            ServiceProvider.Instance5.Register<IModalDialog, PacienteAgregarViewDialog>();
            ServiceProvider.Instance6.Register<IModalDialog, PacienteActualizarViewDialog>();
            ServiceProvider.Instance7.Register<IModalDialog, AsociarPacienteFisioterapeutaViewDialog>();



            
        }
    }
}
