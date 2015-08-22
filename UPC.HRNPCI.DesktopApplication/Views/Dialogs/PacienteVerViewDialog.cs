using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.HRNPCI.DesktopApplication._Interface;
using UPC.HRNPCI.DesktopApplication.Views.Pacinete;

namespace UPC.HRNPCI.DesktopApplication.Views.Dialogs
{
    class PacienteVerViewDialog : IModalDialog
    {
        private PacienteVerView view;

        void IModalDialog.BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetDialog().DataContext = viewModel;
        }

        void IModalDialog.ShowDialog()
        {
            GetDialog().ShowDialog();
        }

        void IModalDialog.Close()
        {
            GetDialog().Close();
        }

        private PacienteVerView GetDialog()
        {
            if (view == null)
            {
                //create the view if the view does not exist
                view = new PacienteVerView();
                view.Closed += new EventHandler(view_Closed);
            }
            return view;
        }

        void view_Closed(object sender, EventArgs e)
        {
            view = null;
        }


    }
}
