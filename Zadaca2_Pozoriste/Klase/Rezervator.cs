using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca2_Pozoriste
{
    public class Rezervator
    {
        string ime, prezime, program, ime_predstave;
        double broj_telefona;
        decimal broj_karata;
        decimal cijena_karata;

        public string Program { get { return program; } set { program = value; } }
        public string ImePredstave { get { return ime_predstave; } set { ime_predstave = value; } }

        public decimal CijenaKarata { get { return cijena_karata; } set { cijena_karata = value; } }

        public string Ime { get { return ime; } set { ime = value; } }
        public string Prezime { get { return prezime; } set { prezime = value; } }

        public decimal BrojKarata { get { return broj_karata; } set { broj_karata = value; } }
        public double BrojTelefona { get { return broj_telefona; } set { broj_telefona = value; } }

        public Rezervator(string n_ime, string n_prezime, decimal n_broj_karata, double n_broj_telefona, decimal n_cijena_karte, string n_ime_programa, string n_predstava)
        {
            Ime = n_ime;
            Prezime = n_prezime;
            BrojKarata = n_broj_karata;
            BrojTelefona = n_broj_telefona;
            CijenaKarata = n_cijena_karte;
            ImePredstave = n_predstava;
            Program = n_ime_programa;
        }
    }
}
