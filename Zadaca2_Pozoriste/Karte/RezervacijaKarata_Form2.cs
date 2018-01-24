using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadaca2_Pozoriste
{
    public partial class RezervacijaKarata_Form2 : Form
    {
        Pozoriste RPR = new Pozoriste();
        RezervacijaUserControl r = null;

        public RezervacijaKarata_Form2(Pozoriste x)
        {
            InitializeComponent();
            RPR = x;
            r = this.rezervacijaUserControl1;
            r.Visible = false;
            Rezervacija_button1.Visible = false;

            InfoPrograma_groupBox2.Visible = false;

            for (int i = 0; i < RPR.Programi.Count(); i++)
            {
                Programi_comboBox1.Items.Add(RPR.Programi[i].nazivPrograma);
            }
            NazivPrograma_textBox1.ReadOnly = true;
            PocetakPrograma_textBox2.ReadOnly = true;
            KrajPrograma_textBox3.ReadOnly = true;

        }

        private void Programi_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Programi_comboBox1.SelectedIndex > -1)
            {
                InfoPrograma_groupBox2.Visible = true;
                int index = Programi_comboBox1.SelectedIndex;

                NazivPrograma_textBox1.Text = RPR.Programi[index].nazivPrograma;
                Predstave_comboBox1.Items.Clear();
                for (int i = 0; i < RPR.Programi[index].Predstave.Count(); i++)
                {
                    Predstave_comboBox1.Items.Add(RPR.Programi[index].Predstave[i].nazivPredstave);
                }

                PocetakPrograma_textBox2.Text = Convert.ToString(RPR.Programi[index].Pocetak);
                KrajPrograma_textBox3.Text = Convert.ToString(RPR.Programi[index].Kraj);
            }
        }

        private void Predstave_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            r.Visible = true;
            Rezervacija_button1.Visible = true;
        }

        private void Rezervacija_button1_Click(object sender, EventArgs e)
        {
            try
            {
                string n_ime = r.Ime;
                string n_prezime = r.Prezime;
                decimal broj_karata = r.BrojKarata;
                double broj_telefon = r.BrojTelefona;

                int index = Programi_comboBox1.SelectedIndex;
                int index_predstave = Predstave_comboBox1.SelectedIndex;
                decimal cijena_karata = RPR.Programi[index].Predstave[index_predstave].cijenaKarte * broj_karata;

                string ime_programa = RPR.Programi[index].nazivPrograma;
                string predstava = RPR.Programi[index].Predstave[index_predstave].nazivPredstave;

                int rezervacijski_kod = r.RezervacijskiKod;
                if (RPR.Programi[index].Rezervacije.ContainsKey(rezervacijski_kod)) 
                {
                    throw new Exception("Rezervacijski kod postoji. Generišite novi!");
                }

                RPR.Programi[index].Rezervacije[rezervacijski_kod] = new Rezervator(n_ime, n_prezime, broj_karata, broj_telefon, cijena_karata, ime_programa, predstava);

                MessageBox.Show("Unos validan. Cijena karte iznosi " + cijena_karata + "KM.\n" + "Rezervacijski kod: " + rezervacijski_kod, "Rezervacija");
            }
            catch(System.Exception poruka)
            {
                MessageBox.Show("Greška: " + poruka.Message, "Izuzetak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Naplati_button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(RezervacijskiKod_textBox1.Text)) throw new System.Exception("Unesite rezervacijski kod.");

                int program = -1;
                for (int i = 0; i < RPR.Programi.Count(); i++)
                {
                    if (RPR.Programi[i].Rezervacije.ContainsKey(Convert.ToInt32(RezervacijskiKod_textBox1.Text)))
                    {
                        program = i;
                    }
                }
                if (program == -1) throw new System.Exception("Rezervacijski kod ne postoji.");

                int kod = Convert.ToInt32(RezervacijskiKod_textBox1.Text);
                MessageBox.Show("Ime: " + RPR.Programi[program].Rezervacije[kod].Ime + "\n" +
                                "Prezime: " + RPR.Programi[program].Rezervacije[kod].Prezime + "\n" +
                                "Broj telefona: " + RPR.Programi[program].Rezervacije[kod].BrojTelefona + "\n" +
                                "Program: " + RPR.Programi[program].Rezervacije[kod].Program + "\n" +
                                "Predstava: " + RPR.Programi[program].Rezervacije[kod].ImePredstave + "\n" +
                                "Broj karata: " + RPR.Programi[program].Rezervacije[kod].BrojKarata + "\n" +
                                "Cijena: " + RPR.Programi[program].Rezervacije[kod].CijenaKarata + "\n" + "\n" +
                                "YES - Prodaj" + "\n" +
                                "NO - Nazad",
                                "Informacije o rezervaciji", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                                );
            }
            catch (Exception poruka)
            {
                MessageBox.Show("Greška: " + poruka.Message, "Izuzetak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
