using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace UPC.HRNPCI.Model.FisioterapeutaModel
{
    public class FisioterapeutaComboBox
    {
        public int iCodigo { get; set; }
        public string strNombre { get; set; }
        public string strApellidos { get; set; }

        public ObservableCollection<FisioterapeutaComboBox> lstFisioterapeutasComboBox = null;

        public FisioterapeutaComboBox()
        {
            lstFisioterapeutasComboBox = new ObservableCollection<FisioterapeutaComboBox>();
        }

        //public int ObtenerIdColumna(string pNombreColumna)
        //{
        //    for (int i = 0; i < lstFisioterapeutasComboBox.Count; i++)
        //        if (lstFisioterapeutasComboBox[i].NombreColumna == pNombreColumna)
        //            return lstFisioterapeutasComboBox[i].idColumna;
        //    return -1;
            
        //}
    }
}
