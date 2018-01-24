using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca2_Pozoriste
{
    public class Pozoriste
    {
        List<Pozorisni_program> programi;
        public List<Pozorisni_program> Programi { get { return programi; } set { programi = value; } }

        List<Predstava> predstave;
        public List<Predstava> Predstave { get { return predstave; } set { predstave = value; } }

        List<int> sale;
        public List<int> Sale { get { return sale; } set { sale = value; } }

        public Pozoriste()
        {
            programi = new List<Pozorisni_program>();
            predstave = new List<Predstava>();
            sale = new List<int>(); // Index sale je ujedno i broj sale
        }

        public void DodajPredstavu(Predstava x) { predstave.Add(x); }

    }
}
