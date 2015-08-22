using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.HRNPCI.DesktopApplication._Interface;

namespace UPC.HRNPCI.DesktopApplication._Service
{
    class ServiceProvider
    {
        //ModuloAdmin Dialog
        public static IServiceLocator Instance { get; private set; }
        public static void RegisterServiceLocator(IServiceLocator s)
        {
            Instance = s;
        }

        // *****************************************************************************************
        //  Fisioteraputa
        // *****************************************************************************************
        // ver Dailalog Not ok
        public static IServiceLocator Instance1 { get; private set; }
        public static void RegisterServiceLocator1(IServiceLocator s)
        {
            Instance1 = s;
        }

        // agregar Dailalog ok
        public static IServiceLocator Instance2 { get; private set; }
        public static void RegisterServiceLocator2(IServiceLocator s)
        {
            Instance2 = s;
        }

        // actualizar Dailalog ok
        public static IServiceLocator Instance3 { get; private set; }
        public static void RegisterServiceLocator3(IServiceLocator s)
        {
            Instance3 = s;
        }

        // *****************************************************************************************
        //  paciente
        // *****************************************************************************************
        // ver Dailalog Not ok
        public static IServiceLocator Instance4 { get; private set; }
        public static void RegisterServiceLocator4(IServiceLocator s)
        {
            Instance4 = s;
        }

        // agregar Dailalog ok
        public static IServiceLocator Instance5 { get; private set; }
        public static void RegisterServiceLocator5(IServiceLocator s)
        {
            Instance5 = s;
        }

        // actualizar Dailalog ok
        public static IServiceLocator Instance6 { get; private set; }
        public static void RegisterServiceLocator6(IServiceLocator s)
        {
            Instance6 = s;
        }

        // *****************************************************************************************
        //  paciente aosciacion a fisioterapeuta
        // *****************************************************************************************
        // create / update  Dailalog Not ok
        public static IServiceLocator Instance7 { get; private set; }
        public static void RegisterServiceLocator7(IServiceLocator s)
        {
            Instance7 = s;
        }

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
