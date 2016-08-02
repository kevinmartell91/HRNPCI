using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.HRNPCI.DesktopApplication._Interface;

namespace UPC.HRNPCI.DesktopApplication._Service
{
    class ServiceProvider
    {
        //-------------------------------- Modulo ADMIN  -----------------------------------//
        //----------------------------------------------------------------------------------//
        //ModuloAdmin Dialog
        public static IServiceLocator Instance { get; private set; }
        public static void RegisterServiceLocator(IServiceLocator s){Instance = s;}

        // -------------------------  Fisioteraputa
        // ver Dailalog Not ok
        public static IServiceLocator Instance1 { get; private set; }
        public static void RegisterServiceLocator1(IServiceLocator s){Instance1 = s;}

        // agregar Dailalog ok
        public static IServiceLocator Instance2 { get; private set; }
        public static void RegisterServiceLocator2(IServiceLocator s){Instance2 = s;}

        // actualizar Dailalog ok
        public static IServiceLocator Instance3 { get; private set; }
        public static void RegisterServiceLocator3(IServiceLocator s){Instance3 = s;}

        // ------------------------- Paciente 
        // ver Dailalog Not ok
        public static IServiceLocator Instance4 { get; private set; }
        public static void RegisterServiceLocator4(IServiceLocator s){Instance4 = s;}

        // agregar Dailalog ok
        public static IServiceLocator Instance5 { get; private set; }
        public static void RegisterServiceLocator5(IServiceLocator s){Instance5 = s;}

        // actualizar Dailalog ok
        public static IServiceLocator Instance6 { get; private set; }
        public static void RegisterServiceLocator6(IServiceLocator s){Instance6 = s;}

        // ------------------------- Paciente aosciacion a fisioterapeuta
        // create / update  Dailalog Not ok
        public static IServiceLocator Instance7 { get; private set; }
        public static void RegisterServiceLocator7(IServiceLocator s){Instance7 = s;}



        //----------------------------- Modulo FISIOTERAPEUTA  -----------------------------//
        //----------------------------------------------------------------------------------//
        // ModuloFisioterapeuta Dialog
        public static IServiceLocator Instance8 { get; private set; }
        public static void RegisterServiceLocator8(IServiceLocator s){Instance8 = s;}

        // Ver Resultados
        public static IServiceLocator Instance9 { get; private set; }
        public static void RegisterServiceLocator9(IServiceLocator s) { Instance9 = s; }

        // Ver Reportes
        public static IServiceLocator Instance10 { get; private set; }
        public static void RegisterServiceLocator10(IServiceLocator s) { Instance10 = s; }

        // Ver Reportes 2
        public static IServiceLocator Instance13 { get; private set; }
        public static void RegisterServiceLocator13(IServiceLocator s) { Instance13 = s; }

        // Busqueda Resultados
        public static IServiceLocator Instance11 { get; private set; }
        public static void RegisterServiceLocator11(IServiceLocator s) { Instance11 = s; }

        // Busqueda Reportes
        public static IServiceLocator Instance12 { get; private set; }
        public static void RegisterServiceLocator12(IServiceLocator s) { Instance12 = s; }


    }

    //class ServiceProvider
    //{
    //    public  IServiceLocator Instance { get; private set; }

    //    public  void RegisterServiceLocator(IServiceLocator s)
    //    {
    //        Instance = s;
    //    }
    //}
}
