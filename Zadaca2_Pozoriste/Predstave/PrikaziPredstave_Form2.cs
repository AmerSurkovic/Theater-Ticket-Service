using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Oracle.ManagedDataAccess.Client;

namespace Zadaca2_Pozoriste
{
    public partial class PrikaziPredstave_Form2 : Form
    {
        static public void XmlSerijalizacija(List<Predstava> listaPredstava)
        {
            XmlSerializer x = new XmlSerializer(typeof(List<Predstava>));
            StreamWriter writer = new StreamWriter(@"predstave.xml");
            x.Serialize(writer, listaPredstava);
            writer.Close();
        }

        static public void XmlDeserijalizacija(List<Predstava> listaPredstava)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Predstava>));
            
            //2. Kreiranje streama za pisanja
            StreamReader citac = new StreamReader(@"predstave.xml");
                
            //3.Poziva se Deserialize metoda da vrati stanje objekta
            listaPredstava = (List<Predstava>)deserializer.Deserialize(citac);

            citac.Close();//4. Zatvaranje streama/datoteke
        }

        Pozoriste RPR = new Pozoriste();
        BindingList<Predstava> predstave;
        PredstavaDB predstavaDB;

        public PrikaziPredstave_Form2(Pozoriste x, BindingList<Predstava> predstaveX, PredstavaDB predstavaDBX)
        {
            InitializeComponent();

            RPR = x;
            predstavaDB = new PredstavaDB();
            predstave = new BindingList<Predstava>();
            predstave = predstaveX;
            predstavaDB = predstavaDBX;

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

            InfoPredstave_groupBox2.Visible = false;
            NazivPredstave_textBox1.ReadOnly = true;
            TipPredstave_textBox2.ReadOnly = true;
            CijenaKarte_textBox1.ReadOnly = true;
            DatumPredstave_textBox2.ReadOnly = true;
            KategorijaPredstave_textBox3.ReadOnly = true;
            Izmjene_button1.Visible = false;
            CijenaKarte_numericUpDown1.Visible = false;
            DatumPredstave_dateTimePicker1.Visible = false;
            KategorijaPredstave_comboBox1.Visible = false;

            statusStrip1.Visible = false;
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";
            toolStripStatusLabel3.Text = "";
            toolStripStatusLabel4.Text = " ";

            KategorijaPredstave_comboBox1.Items.Add("Djete");
            KategorijaPredstave_comboBox1.Items.Add("Odrasli");
            KategorijaPredstave_comboBox1.Items.Add("Penzioner");
            KategorijaPredstave_comboBox1.Items.Add("Školarac");

            // Deserijalizacija
            IFormatter serializer = new BinaryFormatter();
            FileStream cdat =new FileStream(@"predstave.dat", FileMode.Open, FileAccess.Read);
            List<Predstava> spasenePredstave = serializer.Deserialize(cdat) as List<Predstava>;
            cdat.Close();

            foreach (Predstava y in spasenePredstave)
            {
                Predstave_comboBox1.Items.Add(y.nazivPredstave);
            }

        }

        private void PrikaziPredstave_Form2_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Predstave_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Predstave_comboBox1.SelectedIndex > -1)
            {
                InfoPredstave_groupBox2.Visible = true;
                int index = Predstave_comboBox1.SelectedIndex;
                Predstava info = RPR.Predstave[index];

                NazivPredstave_textBox1.Text = info.nazivPredstave;
                TipPredstave_textBox2.Text = info.tipPredstave;
                CijenaKarte_textBox1.Text = Convert.ToString(info.cijenaKarte);
                DatumPredstave_textBox2.Text = Convert.ToString(info.datumPredstave);
                KategorijaPredstave_textBox3.Text = info.kategorijaPredstave;
            }
        }

        private void Izbrisi_button1_Click(object sender, EventArgs e)
        {
            Predstava izbaci = new Predstava();

            int index = Predstave_comboBox1.SelectedIndex;

            izbaci = RPR.Predstave[index];
            predstavaDB.DeletePredstava(izbaci);
            predstave.Remove(izbaci);

            RPR.Predstave.RemoveAt(index);
            Predstave_comboBox1.Items.RemoveAt(index);
            MessageBox.Show("Uspješno ste obrisali predstavu", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            InfoPredstave_groupBox2.Visible = false;
        }

        private void IzmjenaInfo_checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (IzmjenaInfo_checkBox1.Checked)
            {
                Izmjene_button1.Visible = true;
                NazivPredstave_textBox1.ReadOnly = false;
                TipPredstave_textBox2.ReadOnly = false;
                CijenaKarte_textBox1.ReadOnly = false;
                DatumPredstave_textBox2.ReadOnly = false;
                KategorijaPredstave_textBox3.ReadOnly = false;

                CijenaKarte_textBox1.Visible = false;
                DatumPredstave_textBox2.Visible = false;
                KategorijaPredstave_textBox3.Visible = false;

                CijenaKarte_numericUpDown1.Visible = true;
                DatumPredstave_dateTimePicker1.Visible = true;
                KategorijaPredstave_comboBox1.Visible = true;

            }
            #region
            else 
            {
                NazivPredstave_errorProvider1.Clear();
                TipPredstave_errorProvider2.Clear();
                DatumPredstave_errorProvider2.Clear();
                KategorijaPredstave_errorProvider3.Clear();

                Izmjene_button1.Visible = false;
                NazivPredstave_textBox1.ReadOnly = true;
                TipPredstave_textBox2.ReadOnly = true;
                CijenaKarte_textBox1.ReadOnly = true;
                DatumPredstave_textBox2.ReadOnly = true;
                KategorijaPredstave_textBox3.ReadOnly = true;

                CijenaKarte_textBox1.Visible = true;
                DatumPredstave_textBox2.Visible = true;
                KategorijaPredstave_textBox3.Visible = true;

                CijenaKarte_numericUpDown1.Visible = false;
                DatumPredstave_dateTimePicker1.Visible = false;
                KategorijaPredstave_comboBox1.Visible = false;

                statusStrip1.Visible = false;
            }
            #endregion
        }

        private void Izmjene_button1_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabel1.Text == ("") && toolStripStatusLabel2.Text == ("") && toolStripStatusLabel3.Text == ("") && toolStripStatusLabel4.Text == (""))
            {
                // Editovanje predstave
                int index = Predstave_comboBox1.SelectedIndex;

                string x1 = RPR.Predstave[index].nazivPredstave;
                RPR.Predstave[index].nazivPredstave = NazivPredstave_textBox1.Text;
                RPR.Predstave[index].tipPredstave = TipPredstave_textBox2.Text;
                RPR.Predstave[index].datumPredstave = DatumPredstave_dateTimePicker1.Value;
                RPR.Predstave[index].cijenaKarte = CijenaKarte_numericUpDown1.Value;
                RPR.Predstave[index].kategorijaPredstave = Convert.ToString(KategorijaPredstave_comboBox1.SelectedItem);

                Predstava promjena = RPR.Predstave[index];
                
                string x2 = NazivPredstave_textBox1.Text;
                if (x1==x2)
                {               
                    predstavaDB.UpdatePredstava(promjena);
                }
                else
                {
                    Predstava ubaci = RPR.Predstave[index];
                    predstavaDB.DeletePredstava(promjena);
                    predstavaDB.InsertPredstava(new Predstava(ubaci.ID, NazivPredstave_textBox1.Text, ubaci.tipPredstave, ubaci.cijenaKarte, ubaci.datumPredstave, ubaci.kategorijaPredstave));
                }

                MessageBox.Show("Uspješno ste izmjenili informacije o predstavi", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InfoPredstave_groupBox2.Visible = false;

                // BIN Datoteka Serijalizacija
                IFormatter serializer = new BinaryFormatter();
                FileStream dat = new FileStream(@"predstave.bin", FileMode.Create, FileAccess.Write);
                serializer.Serialize(dat, RPR.Predstave);
                dat.Close();

                /// Xml Serijalizacija
                XmlSerijalizacija(RPR.Predstave);

                this.Close();
            }
            else
            {
                if (toolStripStatusLabel4.Text == " ")
                {
                    toolStripStatusLabel4.Text = "Odaberite kategoriju predstave.";
                }
                statusStrip1.Visible = true;
            }
        }

        #region Validacija promjene naziva predstave
        private void NazivPredstave_textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(NazivPredstave_textBox1.Text) && IzmjenaInfo_checkBox1.Checked)
            {
                NazivPredstave_errorProvider1.SetError(NazivPredstave_textBox1, "Unesite ime predstave");
                NazivPredstave_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel1.Text = ("Ime predstave nije uneseno.");
            }
            else if (IzmjenaInfo_checkBox1.Checked)
            {
                NazivPredstave_errorProvider1.SetError(NazivPredstave_textBox1, "Ispravan unos");
                NazivPredstave_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel1.Text = ("");
            }
        }
        #endregion

        #region Validacija promjene tipa predstave
        private void TipPredstave_textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(TipPredstave_textBox2.Text) && IzmjenaInfo_checkBox1.Checked)
            {
                TipPredstave_errorProvider2.SetError(TipPredstave_textBox2, "Unesite tip predstave");
                TipPredstave_errorProvider2.Icon = Properties.Resources.er;
                toolStripStatusLabel2.Text = ("Tip predstave nije unesen.");
            }
            else if (IzmjenaInfo_checkBox1.Checked)
            {
                TipPredstave_errorProvider2.SetError(TipPredstave_textBox2, "Ispravan unos");
                TipPredstave_errorProvider2.Icon = Properties.Resources.Check;
                toolStripStatusLabel2.Text = ("");
            }
        }
        #endregion

        #region Validacija promjene datuma predstave
        private void DatumPredstave_dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (DatumPredstave_dateTimePicker1.Value < DateTime.Today && IzmjenaInfo_checkBox1.Checked)
            {
                DatumPredstave_errorProvider2.SetError(DatumPredstave_dateTimePicker1, "Neispravan datum");
                DatumPredstave_errorProvider2.Icon = Properties.Resources.er;
                toolStripStatusLabel3.Text = ("Unesite ispravan datum.");
            }
            else if (IzmjenaInfo_checkBox1.Checked)
            {
                DatumPredstave_errorProvider2.SetError(DatumPredstave_dateTimePicker1, "Ispravan datum");
                DatumPredstave_errorProvider2.Icon = Properties.Resources.Check;
                toolStripStatusLabel3.Text = ("");
            }
        }
        #endregion

        #region Validacija promjene kategorije predstave
        private void KategorijaPredstave_comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (KategorijaPredstave_comboBox1.SelectedIndex < 0 && IzmjenaInfo_checkBox1.Checked)
            {
                KategorijaPredstave_errorProvider3.SetError(KategorijaPredstave_comboBox1, "Odaberite kategoriju");
                KategorijaPredstave_errorProvider3.Icon = Properties.Resources.er;
                toolStripStatusLabel4.Text = ("Odaberite kategoriju predstave.");
            }
            else if (IzmjenaInfo_checkBox1.Checked)
            {
                KategorijaPredstave_errorProvider3.SetError(KategorijaPredstave_comboBox1, "Odabrana je kategorija");
                KategorijaPredstave_errorProvider3.Icon = Properties.Resources.Check;
                toolStripStatusLabel4.Text = ("");
            }
        }
        #endregion

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            PredstaveTabela frm = new PredstaveTabela(RPR);
            frm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PredstaveDrvo frm = new PredstaveDrvo(RPR);
            frm.Show();
        }

        private void konektujSeNaBazuPodatakaToolStripMenuItem_Click(object sender, EventArgs e)
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
