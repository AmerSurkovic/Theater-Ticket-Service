using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Zadaca2_Pozoriste
{
    public partial class BazaPodatakaAdmin : Form
    {
        Pozoriste RPR = new Pozoriste();
        BindingList<Predstava> predstave;
        BindingList<Izuzetak> izuzeci = new BindingList<Izuzetak>();
        PredstavaDB predstavaDB;
        IzuzetakDB izuzetakDB = new IzuzetakDB();

        public BazaPodatakaAdmin(Pozoriste x, BindingList<Predstava> predstaveX, PredstavaDB predstavaDBX, BindingList<Izuzetak> izuzecix, IzuzetakDB izuzetakDBX)
        {
            InitializeComponent();
            RPR = x;
            label6.Visible = false;
            predstavaDB = new PredstavaDB();
            predstave = new BindingList<Predstava>();
            predstave = predstaveX;
            predstavaDB = predstavaDBX;

            izuzeci = izuzecix;
            izuzetakDB = izuzetakDBX;
        }

        private void KreirajPredstave_button1_Click(object sender, EventArgs e)
        {
            bool indikator = predstavaDB.CreatePredstavaTable();
            if (indikator)
            {
                int newID = 1;
                if (predstave.Count > 0)
                    newID = predstave.Max(t => t.ID) + 1;

                Predstava x = new Predstava(newID, "Ljeto u zlatnoj dolini", "Drama", 12, DateTime.Today, "Odrasli");
                predstavaDB.InsertPredstava(x);
                predstave.Add(x);

                if (predstave.Count > 0)
                    newID = predstave.Max(t => t.ID) + 1;

                Predstava y = new Predstava("Hamlet u selu mrdusa donja", "Komedija", 12, DateTime.Today, "Odrasli");
                predstavaDB.InsertPredstava(y);
                predstave.Add(y);

                if (RPR.Predstave.Count() == 0)
                {
                    RPR.Predstave.Add(x);
                    RPR.Predstave.Add(y);
                }

                MessageBox.Show("Uspješno ste kreirali tabelu 'Predstave'.");
            }
            else
                MessageBox.Show("Greška pri kreiranju tabele 'Predstave'.");
        }

        private void IzbrisiPredstave_button1_Click(object sender, EventArgs e)
        {
            bool indikator = predstavaDB.DropPredstavaTable();
            if (indikator)
            {
                MessageBox.Show("Uspješno ste obrisali tabelu 'Predstave'.");
                RPR.Predstave.Clear();
            }
            else
                MessageBox.Show("Greška pri brisanju tabele 'Predstave'.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Load all from DB
            predstave = predstavaDB.ReadAllPredstave();
            listBox1.DataSource = predstave;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            Predstava izbaci = new Predstava();
            izbaci = RPR.Predstave[index];
            predstavaDB.DeletePredstava(izbaci);
            predstave.Remove(izbaci);

            RPR.Predstave.RemoveAt(index);
            predstave = predstavaDB.ReadAllPredstave();
            MessageBox.Show("Uspješno ste obrisali predstavu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PrikaziPredstave_Form2 frm = new PrikaziPredstave_Form2(RPR, predstave, predstavaDB);
            frm.Show();
        }

        private void KreirajTabeluIzuzeci_button5_Click(object sender, EventArgs e)
        {
            bool indikator = izuzetakDB.CreateIzuzetakTable();
            if (indikator)
                MessageBox.Show("Uspješno ste kreirali tabelu 'Izuzeci'.");
            else
                MessageBox.Show("Greška pri kreiranju tabele 'Izuzeci'.");
        }

        private void IzbrisiTabeluIzuzeci_button6_Click(object sender, EventArgs e)
        {
            bool indikator = izuzetakDB.DropIzuzetakTable();
            if (indikator)
            {
                MessageBox.Show("Uspješno ste obrisali tabelu 'Izuzeci'.");
            }
            else
                MessageBox.Show("Greška pri brisanju tabele 'Izuzeci'.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Load all from DB
            izuzeci = izuzetakDB.ReadAllIzuzeci();
            listBox2.DataSource = izuzeci;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(password_textBox1.Text))
            {
                label6.Visible = true;
            }
            else if (password_textBox1.Text == "1234")
                groupBox2.Visible = false;
            else
                label6.Visible = true;
        }

        private void statističkiPodaciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void barChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            izuzeci = izuzetakDB.ReadAllIzuzeci();
            BarChart frm = new BarChart(izuzeci);
            frm.Show();
        }

        private void pieChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            izuzeci = izuzetakDB.ReadAllIzuzeci();
            PieChart frm = new PieChart(izuzeci);
            frm.Show();
        }

    }
}
