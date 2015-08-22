using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UPC.HRNPCI.DesktopApplication.Helpers
{
    interface IClosableViewModel
    {
        event EventHandler CloseWindowEvent;
    }
}
