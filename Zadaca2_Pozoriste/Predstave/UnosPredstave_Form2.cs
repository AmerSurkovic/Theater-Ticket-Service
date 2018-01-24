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
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Oracle.ManagedDataAccess.Client;

namespace Zadaca2_Pozoriste
{
    
    public partial class UnosPredstave_Form2 : Form
    {
        static public void XmlSerijalizacija(List<Predstava> listaPredstava)
        {
            XmlSerializer x = new XmlSerializer(typeof(List<Predstava>));
            StreamWriter writer = new StreamWriter(@"predstave.xml");
            x.Serialize(writer, listaPredstava);
            writer.Close();
        }

        Pozoriste RPR = new Pozoriste();

        BindingList<Predstava> predstave;
        PredstavaDB predstavaDB;

        BindingList<Izuzetak> izuzeci = new BindingList<Izuzetak>();
        IzuzetakDB izuzetakDB = new IzuzetakDB();

        public UnosPredstave_Form2(Pozoriste x, BindingList<Predstava> predstaveX, PredstavaDB predstavaDBX, BindingList<Izuzetak> izuzeciX, IzuzetakDB izuzetakDBX)
        {
            InitializeComponent();

            RPR = x;
            predstavaDB = new PredstavaDB();
            predstave = new BindingList<Predstava>();
            predstave = predstaveX;
            predstavaDB = predstavaDBX;
            izuzeci = izuzeciX;
            izuzetakDB = izuzetakDBX;

            toolStripStatusLabel5.Visible = false;
            #region Pingovanje Oracle baze da provjerimo konekciju
            OracleConnection ping = predstavaDB.GetConnection();
            try
            {
                ping.Open();
                toolStripStatusLabel5.Visible = true;
                toolStripStatusLabel5.Text = "Konektovani na bazu podataka.";
                toolStripStatusLabel5.ForeColor = Color.ForestGreen;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel5.Visible = true;
                toolStripStatusLabel5.Text = "Niste konektovani na bazu podataka.";
                toolStripStatusLabel5.ForeColor = Color.Red;
            }
            #endregion


            KategorijaPredstave_listBox1.Items.Add("Djete");
            KategorijaPredstave_listBox1.Items.Add("Odrasli");
            KategorijaPredstave_listBox1.Items.Add("Penzioner");
            KategorijaPredstave_listBox1.Items.Add("Školarac");
            toolStripStatusLabel1.Text = (" ");
            toolStripStatusLabel1.ForeColor = Color.Red;
            toolStripStatusLabel2.Text = (" ");
            toolStripStatusLabel2.ForeColor = Color.Red;
            toolStripStatusLabel3.Text = (" ");
            toolStripStatusLabel3.ForeColor = Color.Red;
            toolStripStatusLabel4.Text = (" ");
            toolStripStatusLabel4.ForeColor = Color.Red;
            statusStrip1.Visible = false;

            #region Forsiranje izuzetka
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<Predstava>));
                List<Predstava> test = new List<Predstava>();

                StreamReader citac = new StreamReader(@"exception.xml");

                test = (List<Predstava>)deserializer.Deserialize(citac);

                citac.Close();
            }
            catch (Exception ex)
            {
                int newID = 1;
                if (izuzeci.Count > 0)
                    newID = izuzeci.Max(t => t.ID) + 1;

                Izuzetak z = new Izuzetak(newID, Convert.ToString(ex.GetType()), DateTime.Today);
                izuzetakDB.InsertIzuzetak(z);
                izuzeci.Add(z);
            }
            #endregion

        }

        private void Cancel_button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Unesi_button1_Click(object sender, EventArgs e)
        {
            string n_naziv_predstave, n_tip_predstave;
            decimal n_cijena_karte;
            DateTime n_datum_predstave;
            string n_kategorija_predstave;

            if (toolStripStatusLabel1.Text == ("") && toolStripStatusLabel2.Text == ("") && toolStripStatusLabel3.Text == ("") && toolStripStatusLabel4.Text == (""))
            {
                n_naziv_predstave = NazivPredstave_textBox1.Text;
                n_tip_predstave = TipPredstave_textBox2.Text;
                n_cijena_karte = CijenaKarte_numericUpDown1.Value;
                n_datum_predstave = DatumPredstave_dateTimePicker1.Value;
                n_kategorija_predstave = Convert.ToString(KategorijaPredstave_listBox1.SelectedItem);

                int newID = 1;
                if (predstave.Count > 0)
                    newID = predstave.Max(t => t.ID) + 1;

                Predstava unos = new Predstava(newID, n_naziv_predstave, n_tip_predstave, n_cijena_karte, n_datum_predstave, n_kategorija_predstave);
                
                bool indikator = predstavaDB.InsertPredstava(unos);
                if (indikator)
                {
                    RPR.DodajPredstavu(unos);
                    predstave.Add(unos);

                    // BIN Datoteka Serijalizacija
                    IFormatter serializer = new BinaryFormatter();
                    FileStream dat = new FileStream(@"predstave.bin", FileMode.Create, FileAccess.Write);
                    serializer.Serialize(dat, RPR.Predstave);
                    dat.Close();

                    /// XML Serijalizacija
                    XmlSerijalizacija(RPR.Predstave);

                    MessageBox.Show("Uspješno ste unijeli predstavu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Problem pristupa bazi podataka! Probajte osvješiti konekciju ili kontaktirajte administratora. Moguće je da tabela 'Predstave' u bazi podataka nije kreirana.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                if (toolStripStatusLabel1.Text == (" "))
                {
                    toolStripStatusLabel1.Text = ("Ime predstave nije uneseno.");
                }
                if (toolStripStatusLabel2.Text == (" "))
                {
                    toolStripStatusLabel2.Text = ("Tip predstave nije unesen.");
                }
                if (toolStripStatusLabel3.Text == (" "))
                {
                    toolStripStatusLabel3.Text = ("Odaberite kategoriju predstave.");
                }
                if (toolStripStatusLabel4.Text == (" "))
                {
                    toolStripStatusLabel4.Text = ("Odaberite ispravan datum.");
                }
                statusStrip1.Visible = true;
            }
        }

        #region Validacija unosa naziva predstave
        private void NazivPredstave_textBox1_Validating(object sender, CancelEventArgs e)
        {
            string porukagreske;
            if (!ValidNazivPredstave(NazivPredstave_textBox1.Text, out porukagreske)) { }
            this.NazivPredstave_errorProvider1.SetError(NazivPredstave_textBox1, porukagreske);
        }

        public bool ValidNazivPredstave(string nazivPredstave, out string pgreske)
        {
            if (string.IsNullOrEmpty(nazivPredstave))
            {
                pgreske = "Unesite ime predstave";
                NazivPredstave_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel1.Text = ("Ime predstave nije uneseno.");
                return false;
            }
            else
            {
                pgreske = "Ispravan unos";
                NazivPredstave_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel1.Text = ("");
                return true;
            }
        }
        #endregion

        #region Validacija unosa tipa predstave
        private void TipPredstave_textBox2_Validating(object sender, CancelEventArgs e)
        {
            string porukagreske;
            if(!ValidTipPredstave(TipPredstave_textBox2.Text, out porukagreske)) { }
            this.TipPredstave_errorProvider1.SetError(TipPredstave_textBox2, porukagreske);
        }

        public bool ValidTipPredstave(string tipPredstave, out string pgreske)
        {
            if (string.IsNullOrEmpty(tipPredstave))
            {
                pgreske = "Unesite tip predstave";
                TipPredstave_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel2.Text  = ("Tip predstave nije unesen.");
                return false;
            }
            else
            {
                pgreske = "Ispravan unos";
                TipPredstave_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel2.Text = ("");
                return true;
            }
        }
        #endregion

        #region Validacija selekcije kategorije predstave
        private void KategorijaPredstave_listBox1_Validating(object sender, CancelEventArgs e)
        {
            if (KategorijaPredstave_listBox1.SelectedIndex < 0)
            {
                KategorijaPredstave_errorProvider1.SetError(KategorijaPredstave_listBox1, "Odaberite kategoriju");
                KategorijaPredstave_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel3.Text = ("Odaberite kategoriju predstave.");
            }
            else
            {
                KategorijaPredstave_errorProvider1.SetError(KategorijaPredstave_listBox1, "Odabrana je kategorija");
                KategorijaPredstave_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel3.Text = ("");
            }
        }
        #endregion

        #region Validacija selekcije datuma igranja predstave
        private void DatumPredstave_dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (DatumPredstave_dateTimePicker1.Value < DateTime.Today)
            {
                Datum_errorProvider1.SetError(DatumPredstave_dateTimePicker1, "Neispravan datum");
                Datum_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel4.Text = ("Odaberite ispravan datum.");
            }
            else
            {
                Datum_errorProvider1.SetError(DatumPredstave_dateTimePicker1, "Ispravan datum");
                Datum_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel4.Text = ("");
            }
        }
        #endregion

        private void konektujSeNaBazuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region Pingovanje Oracle baze da provjerimo konekciju
            OracleConnection ping = predstavaDB.GetConnection();
            try
            {
                ping.Open();
                toolStripStatusLabel5.Visible = true;
                toolStripStatusLabel5.Text = "Konektovani na bazu podataka.";
                toolStripStatusLabel5.ForeColor = Color.ForestGreen;
                MessageBox.Show("Konekcija na bazu je uspjela.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);         
            }
            catch (Exception ex)
            {
                toolStripStatusLabel5.Visible = true;
                toolStripStatusLabel5.Text = "Niste konektovani na bazu podataka.";
                toolStripStatusLabel5.ForeColor = Color.Red;
                MessageBox.Show("Konekcija na bazu nije uspjela.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
        }

    }
}
