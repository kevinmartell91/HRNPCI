using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPC.HRNPCI.DesktopApplication.ViewModels.RutasAlmacenamiento
{
    class RutasAlmacenamientoViewModel
    {
        public static RutasAlmacenamientoViewModel instance;


        public static RutasAlmacenamientoViewModel Instance()
        {
            if (instance == null)
                instance = new RutasAlmacenamientoViewModel();
            return instance;
        }
    }
}
