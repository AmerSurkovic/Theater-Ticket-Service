using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca2_Pozoriste
{
    [Serializable]
    public class Predstava
    {
        int id;
        public int ID { get { return id; } set { id = value; } }

        string naziv_predstave;
        public string nazivPredstave { get { return naziv_predstave; } set { naziv_predstave = value; } }

        string tip_predstave;
        public string tipPredstave { get { return tip_predstave; } set { tip_predstave = value; } }

        decimal cijena_karte;
        public decimal cijenaKarte { get { return cijena_karte; } set { cijena_karte = value; } }

        DateTime datum_predstave;
        public DateTime datumPredstave { get { return datum_predstave; } set { datum_predstave = value; } }

        string kategorija_predstave;
        public string kategorijaPredstave { get { return kategorija_predstave; } set { kategorija_predstave = value; } }

        // Parametar sala se definise pri dodjeljivanju predstave programu
        int sala;
        public int Sala { get { return sala; } set { sala = value; } }

        // Broj mjesta ce biti definisan pocetno na kapacitet sale te biti smanjivan tokom prodaje karata
        int broj_mjesta;
        public int brojMjesta { get { return broj_mjesta; } set { broj_mjesta = value; } }

        public Predstava(){ }
        public Predstava(string n_naziv_predstave, string n_tip_predstave, decimal n_cijena_karte, DateTime n_datum_predstave, string n_kategorija_predstave)
        {
            nazivPredstave = n_naziv_predstave;
            tipPredstave = n_tip_predstave;
            cijenaKarte = n_cijena_karte;
            datumPredstave = n_datum_predstave;
            kategorijaPredstave = n_kategorija_predstave;
        }
        public Predstava(int n_id, string n_naziv_predstave, string n_tip_predstave, decimal n_cijena_karte, DateTime n_datum_predstave, string n_kategorija_predstave)
        {
            ID = n_id;
            nazivPredstave = n_naziv_predstave;
            tipPredstave = n_tip_predstave;
            cijenaKarte = n_cijena_karte;
            datumPredstave = n_datum_predstave;
            kategorijaPredstave = n_kategorija_predstave;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}", naziv_predstave, tipPredstave, cijenaKarte, datumPredstave.ToShortDateString(), kategorijaPredstave);
        }
    
    }   
}
