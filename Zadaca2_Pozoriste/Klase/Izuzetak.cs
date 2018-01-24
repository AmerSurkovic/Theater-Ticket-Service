using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca2_Pozoriste
{
    public class Izuzetak
    {
        int id;
        public int ID { get { return id; } set { id = value; } }
        string tip_izuzetka;
        public string tipIzuzetka { get { return tip_izuzetka; } set { tip_izuzetka = value; } }
        DateTime datum_izuzetka;
        public DateTime datumIzuzetka { get { return datum_izuzetka; } set { datum_izuzetka = value; } }

        public Izuzetak()
        {

        }
        public Izuzetak(int n_id, string n_tip_izuzetka, DateTime n_datum_izuzetka)
        {
            ID = n_id;
            tipIzuzetka = n_tip_izuzetka;
            datumIzuzetka = n_datum_izuzetka;
        }
        
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", tipIzuzetka, datumIzuzetka.ToLongDateString(), datumIzuzetka.ToString("h:mm:ss tt"));
        }
    }
}
