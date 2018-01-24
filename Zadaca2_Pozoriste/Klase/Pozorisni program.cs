using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca2_Pozoriste
{
    public class Pozorisni_program
    {
        string naziv_programa;
        public string nazivPrograma { get { return naziv_programa; } set { naziv_programa = value; } }

        List<Predstava> predstave;
        public List<Predstava> Predstave { get { return predstave; } set { predstave = value; } }

        DateTime pocetak, kraj;
        public DateTime Pocetak { get { return pocetak; } set { pocetak = value; } }
        public DateTime Kraj { get { return kraj; } set { kraj = value; } }

        Dictionary<int, Rezervator> rezervacije;
        public Dictionary<int, Rezervator> Rezervacije { get { return rezervacije; } set { rezervacije = value; } }

        public Pozorisni_program(string n_naziv_programa, List<Predstava> n_predstave, DateTime n_pocetak, DateTime n_kraj)
        {
            nazivPrograma = n_naziv_programa;
            Predstave = n_predstave;
            Pocetak = n_pocetak;
            Kraj = n_kraj;
            rezervacije = new Dictionary<int, Rezervator>();
        }
    }
}
