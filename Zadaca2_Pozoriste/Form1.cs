using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using System.Threading;

namespace Zadaca2_Pozoriste
{
    public partial class RPRPozoriste_MainForm : Form
    {
        Thread nit1;
        Thread nit2;
        List<string> filesTXT;
        List<string> filesXML;
        Stopwatch sw = new Stopwatch();
        double proteklo;

        private void AddFiles()
        {
            listView1.Clear();
            foreach (string s in filesXML)
            {
                listView1.Items.Add(s);
                Application.DoEvents();
            }

            foreach (string s in filesTXT)
            {
                listView1.Items.Add(s);
                Application.DoEvents();
            }
        }

        public void NitPretrage()
        {
            filesTXT = Directory.EnumerateFiles(folderBrowserDialog1.SelectedPath, "*.txt", SearchOption.AllDirectories).ToList();
            filesXML = Directory.EnumerateFiles(folderBrowserDialog1.SelectedPath, "*.xml", SearchOption.AllDirectories).ToList();
            sw.Stop();

            proteklo = sw.ElapsedMilliseconds;
            sw.Reset();

            label1.Text = filesTXT.Count.ToString();
            label2.Text = filesXML.Count.ToString();
            label6.Text = Convert.ToString(proteklo);

            Thread t = new Thread(() => this.Invoke(new Action(() => AddFiles())));
            t.Start();
        }

        public void NitBrojac()
        {
            sw.Start();
        }

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

        BindingList<Izuzetak> izuzeci = new BindingList<Izuzetak>();
        IzuzetakDB izuzetakDB = new IzuzetakDB();

        System.Windows.Forms.Timer logoTimer = new System.Windows.Forms.Timer();
        int slika = 0;

        private void logoEvent(Object myObject, EventArgs myEventArgs)
        {
            logoTimer.Stop();

            if (slika == 0)
            {
                pictureBox2.ImageLocation = (@"C:\Users\Amer\Pictures\teatarBASIC2.jpg");
                slika += 1;
            }
            else if (slika == 1)
            {
                pictureBox2.ImageLocation = (@"C:\Users\Amer\Pictures\teatarBASIC3.jpg");
                slika += 1;
            }
            else if (slika == 2)
            {
                pictureBox2.ImageLocation = (@"C:\Users\Amer\Pictures\teatarBASIC4.jpg");
                slika += 1;
            }
            else if (slika == 3)
            {
                pictureBox2.ImageLocation = (@"C:\Users\Amer\Pictures\teatarBASIC5.jpg");
                slika += 1;
            }
            else if (slika == 4)
            {
                pictureBox2.ImageLocation = (@"C:\Users\Amer\Pictures\teatarBASIC2.jpg");
                slika += 1;
            }
            else if (slika == 5)
            {
                pictureBox2.ImageLocation = (@"C:\Users\Amer\Pictures\teatarBASIC5.jpg");
                slika += 1;
            }
            else if (slika == 6)
            {
                pictureBox2.ImageLocation = (@"C:\Users\Amer\Pictures\teatarBASIC2.jpg");
                slika += 1;
            }
            else if (slika == 7)
            {
                pictureBox2.ImageLocation = (@"C:\Users\Amer\Pictures\teatarBASIC.jpg");
                slika = 0;
            }

            logoTimer.Enabled = true;
        } 

        public RPRPozoriste_MainForm()
        {
            InitializeComponent();

            MessageBox.Show("Poštovani, \n\nU zadaći broj 4 zadatak 2 sam dodao animirani logo koji je vidljiv na početnoj formi dok pristup grafovima imate u formi 'Administrator/Baze podataka/Statistika'.\n\nAmer Šurković\n\n\nPassword za pristup formi 'Administrator' je 1234.\n", "Informacije o zadaći 4 zadatku 2 [Testna aplikacija]", MessageBoxButtons.OK, MessageBoxIcon.Information);

            logoTimer.Tick += new EventHandler(logoEvent);

            logoTimer.Interval = 1500;
            logoTimer.Start();

            predstave = new BindingList<Predstava>();
            predstavaDB = new PredstavaDB();
            izuzeci = izuzetakDB.ReadAllIzuzeci();

            toolStripStatusLabel1.Visible = false;

            #region Pingovanje Oracle baze da provjerimo konekciju
            OracleConnection ping = predstavaDB.GetConnection();
            try
            {
                ping.Open();
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Konektovani na bazu podataka.";
                toolStripStatusLabel1.ForeColor = Color.ForestGreen;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Niste konektovani na bazu podataka.";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            #endregion

            // Load predstave
            predstave = predstavaDB.ReadAllPredstave();     

            #region Ručno dodani picture box
            PictureBox PCB = new PictureBox();
            this.Controls.Add(PCB);
            PCB.Location = new Point(49, 40);
            PCB.Size = new Size(119, 87);
            PCB.ImageLocation = (@"C:\Users\Amer\Pictures\teatarBASIC.jpg");
            PCB.SizeMode = PictureBoxSizeMode.StretchImage;
            #endregion

            #region Ručno dodano dugme
            Button BTN = new Button();
            BTN.Text = "Copyright";
            this.Controls.Add(BTN);
            BTN.Click += new System.EventHandler(this.BTNMetoda);
            BTN.Location = new Point(65, 412);
            #endregion

            for (int i = 0; i < 4; i++) // Dodajemo 4 sale
                RPR.Sale.Add(150);

            Predstava x = new Predstava(1, "Ljeto u zlatnoj dolini", "Drama", 12, DateTime.Today, "Odrasli");
            Predstava y = new Predstava(0, "Hamlet u selu mrdusa donja", "Komedija", 12, DateTime.Today, "Odrasli");
            RPR.Predstave.Add(x);
            RPR.Predstave.Add(y);

            List<Predstava> L1 = new List<Predstava>();
            L1.Add(x); L1.Add(y);
            List<Predstava> L2 = new List<Predstava>();
            L2.Add(x);
            RPR.Programi.Add(new Pozorisni_program("Zimski", L1, DateTime.Today, DateTime.Today));
            RPR.Programi.Add(new Pozorisni_program("Ljetni", L2, DateTime.Today, DateTime.Today));

            // BIN Datoteka Serijalizacija
            IFormatter serializer = new BinaryFormatter();
            FileStream dat = new FileStream(@"predstave.dat", FileMode.Create, FileAccess.Write);
            serializer.Serialize(dat, RPR.Predstave);
            dat.Close();

            #region XML Serijalizacija
            try
            {
                XmlSerijalizacija(RPR.Predstave);
            }
            catch (Exception ex)
            {
                Izuzetak z = new Izuzetak();
                z.datumIzuzetka = DateTime.Today;
                z.tipIzuzetka = ex.Message;
                izuzetakDB.InsertIzuzetak(z);
                izuzeci.Add(z);
            }
            #endregion

            #region Isforsirana greška u svrhu upisivanja te greške u bazu podataka
            try
            {
                var ms = new System.IO.MemoryStream();
                var deser = new System.Xml.Serialization.XmlSerializer(typeof(string));
                deser.Deserialize(ms);
            }
            catch (Exception ex)
            {
                int newID = 1;
                if (izuzeci.Count > 0)
                    newID = izuzeci.Max(t => t.ID) + 1;

                Izuzetak z = new Izuzetak(newID, ex.Message, DateTime.Today);
                izuzetakDB.InsertIzuzetak(z);
                izuzeci.Add(z);
            }
            #endregion

        }

        private void BTNMetoda(object sender, EventArgs e)
        {
            MessageBox.Show("Napravio Amer Šurković. © 2015", "Copyright", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UnesiPredstavu_button1_Click(object sender, EventArgs e)
        {
            UnosPredstave_Form2 frm = new UnosPredstave_Form2(RPR, predstave, predstavaDB, izuzeci, izuzetakDB);
            frm.Show();
        }

        private void PrikazPredstava_button2_Click(object sender, EventArgs e)
        {
            PrikaziPredstave_Form2 frm = new PrikaziPredstave_Form2(RPR, predstave, predstavaDB);
            frm.Show();
        }

        private void UnesiProgram_button3_Click(object sender, EventArgs e)
        {
            UnosPrograma_Form2 frm = new UnosPrograma_Form2(RPR);
            frm.Show();
        }

        private void PrikazPrograma_button2_Click(object sender, EventArgs e)
        {
            PrikaziPrograme_Form2 frm = new PrikaziPrograme_Form2(RPR);
            frm.Show();
        }

        private void ProdajaKarata_button1_Click(object sender, EventArgs e)
        {
            ProdajaKarata_Form2 frm = new ProdajaKarata_Form2(RPR);
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RezervacijaKarata_Form2 frm = new RezervacijaKarata_Form2(RPR);
            frm.Show();
        }

        private void konektujSeNaBazuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PredstavaDB test = new PredstavaDB();
            OracleConnection ping = test.GetConnection();
            try
            {
                ping.Open();
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Konektovani na bazu podataka.";
                toolStripStatusLabel1.ForeColor = Color.ForestGreen;
                MessageBox.Show("Konekcija na bazu je uspjela.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Niste konektovani na bazu podataka.";
                toolStripStatusLabel1.ForeColor = Color.Red;
                MessageBox.Show("Konekcija na bazu nije uspjela.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bazaPodatakaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BazaPodatakaAdmin frm = new BazaPodatakaAdmin(RPR, predstave, predstavaDB, izuzeci, izuzetakDB);
            frm.Show();
        }

        private void pomoćToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Poštovani, \n\nU prilogu zadaće koju sam poslao nalazi se folder pod nazivom 'Test'.\nKreirao sam ga u vidu testiranja višenitnosti moje aplikacije.\nPomenuti folder sadrži ukupno 6985 foldera i 27895 fajlova.\nOd toga:\n.txt fajlova: 20808\n.xml fajlova: 153\nPretraga traje od 2 do 5 sekundi te se u datom periodu može testirati dostupnost drugih formi aplikacije.\nJedina mana pomenutog testa je što upis u listbox oduzima poprilično mnogo vremena.\n\nAmer Šurković\n\n\nPassword za pristup formi 'Administrator' je 1234.\n", "Informacije o zadaći 5 [Testna aplikacija]", MessageBoxButtons.OK, MessageBoxIcon.Information);    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            nit1 = new Thread(new ThreadStart(NitPretrage));
            nit2 = new Thread(new ThreadStart(NitBrojac));

            nit2.Start();
            nit1.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(".txt fajlova pronađeno: " + filesTXT.Count.ToString() + "\n" +
                            ".xml fajlova pronađeno: " + filesXML.Count.ToString() +
                            "\nVrijeme izvršenja: " + proteklo + "ms", "Poruka");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            nit1.Abort();
            nit2.Abort();
            sw.Stop();

            proteklo = sw.ElapsedMilliseconds;
            sw.Reset();

            MessageBox.Show("Vrijeme izvršenja: " + proteklo + "ms", "Poruka");
        }

        private void RPRPozoriste_MainForm_Load(object sender, EventArgs e)
        {

        }

    }
}
