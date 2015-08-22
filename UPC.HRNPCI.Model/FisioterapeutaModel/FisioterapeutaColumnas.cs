using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


namespace UPC.HRNPCI.Model.FisioterapeutaModel
{
    public class FisioterapeutaColumnas
    {
        public int idColumna { get; set; }
        public string NombreColumna { get; set; }
        public string NombreColumnaDB { get; set; }
        public bool visible {get;set;}
 
        public ObservableCollection<FisioterapeutaColumnas> ListaColumnasFisioterapeuta = null;

        public FisioterapeutaColumnas()
        {
            ListaColumnasFisioterapeuta = new ObservableCollection<FisioterapeutaColumnas>();
        }

        public int ObtenerIdColumna(string pNombreColumna)
        {
            for (int i = 0; i < ListaColumnasFisioterapeuta.Count; i++)
                if (ListaColumnasFisioterapeuta[i].NombreColumna == pNombreColumna)
                    return ListaColumnasFisioterapeuta[i].idColumna;
            return -1;
            
        }

    }
}
