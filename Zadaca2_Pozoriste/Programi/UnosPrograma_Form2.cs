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
    public partial class UnosPrograma_Form2 : Form
    {
        Pozoriste RPR = new Pozoriste();

        public UnosPrograma_Form2(Pozoriste x)
        {
            InitializeComponent();
            RPR = x;

            NazivProgama_comboBox1.Items.Add("Ljetni");
            NazivProgama_comboBox1.Items.Add("Zimski");
            NazivProgama_comboBox1.Items.Add("Praznički");
            NazivProgama_comboBox1.Items.Add("[Drugo]");

            statusStrip1.Visible = false;
            toolStripStatusLabel1.Text = " ";
            toolStripStatusLabel2.Text = " ";
            toolStripStatusLabel3.Text = " ";
            toolStripStatusLabel4.Text = " ";
            toolStripStatusLabel5.Text = " ";

            foreach(Predstava y in RPR.Predstave)
                Predstave_checkedListBox1.Items.Add(y.nazivPredstave);

            for (int i = 1; i <= RPR.Sale.Count(); i++)
                Sale_checkedListBox1.Items.Add(i);


        }

        private void Prekid_button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UnosPrograma_button1_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabel1.Text == "" && toolStripStatusLabel2.Text == "" && toolStripStatusLabel3.Text == "" && toolStripStatusLabel4.Text == "" && toolStripStatusLabel5.Text == "")
            {
                string n_nazivPrograma = Convert.ToString(NazivProgama_comboBox1.SelectedItem);

                List<Predstava> n_predstave = new List<Predstava>();
                foreach (int indexChecked in Predstave_checkedListBox1.CheckedIndices)
                    n_predstave.Add(RPR.Predstave[indexChecked]);

                DateTime n_pocetak = pocetak_dateTimePicker1.Value;
                DateTime n_kraj = kraj_dateTimePicker2.Value;

                RPR.Programi.Add(new Pozorisni_program(n_nazivPrograma, n_predstave, n_pocetak, n_kraj));
                MessageBox.Show("Uspješno ste unijeli program", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                statusStrip1.Visible = true;
                if (toolStripStatusLabel1.Text == " ")
                {
                    toolStripStatusLabel1.Text = "Odaberite naziv programa.";
                }
                if (toolStripStatusLabel2.Text == " ")
                {
                    toolStripStatusLabel2.Text = "Odaberite minimalno jednu predstavu.";
                }
                if (toolStripStatusLabel3.Text == " ")
                {
                    toolStripStatusLabel3.Text = "Odaberite minimalno jednu salu za održavanje programa.";
                }
                if (toolStripStatusLabel4.Text == " ")
                {
                    toolStripStatusLabel4.Text = "Odaberite ispravan datum početka programa ili ponovno odaberite današnji.";
                }
                if (toolStripStatusLabel5.Text == " ")
                {
                    toolStripStatusLabel5.Text = "Odaberite datum završetka programa poslije postavljenog početka programa.";
                }
            }
        }

        #region Validacija unosa naziva programa
        private void NazivProgama_comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (NazivProgama_comboBox1.SelectedIndex < 0)
            {
                NazivPrograma_errorProvider1.SetError(NazivProgama_comboBox1, "Odaberite naziv programa");
                NazivPrograma_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel1.Text = "Odaberite naziv programa.";
            }
            else
            {
                NazivPrograma_errorProvider1.SetError(NazivProgama_comboBox1, "Ispravan unos");
                NazivPrograma_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel1.Text = "";
            }
        }
        #endregion

        #region Validacija odabira predstave
        private void Predstave_checkedListBox1_Validating(object sender, CancelEventArgs e)
        {
            if (Predstave_checkedListBox1.CheckedItems.Count <= 0)
            {
                OdabirPredstava_errorProvider1.SetError(Predstave_checkedListBox1, "Odaberite minimalno jednu predstavu");
                OdabirPredstava_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel2.Text = "Odaberite minimalno jednu predstavu.";
            }
            else
            {
                OdabirPredstava_errorProvider1.SetError(Predstave_checkedListBox1, "Ispravan unos");
                OdabirPredstava_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel2.Text = "";
            }
        }
        #endregion

        #region Validacija odabira sala
        private void Sale_checkedListBox1_Validating(object sender, CancelEventArgs e)
        {
            if (Sale_checkedListBox1.CheckedItems.Count <= 0)
            {
                OdabirSala_errorProvider1.SetError(Sale_checkedListBox1, "Odaberite minimalno jednu predstavu");
                OdabirSala_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel3.Text = "Odaberite minimalno jednu salu za održavanje programa.";
            }
            else
            {
                OdabirSala_errorProvider1.SetError(Sale_checkedListBox1, "Ispravan unos");
                OdabirSala_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel3.Text = "";
            }
        }
        #endregion

        #region Validacija pocetka i kraja programa
        private void pocetak_dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (pocetak_dateTimePicker1.Value < DateTime.Today)
            {
                PocetakDatum_errorProvider1.SetError(pocetak_dateTimePicker1, "Neispravan datum");
                PocetakDatum_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel4.Text = "Odaberite ispravan datum.";
            }
            else
            {
                PocetakDatum_errorProvider1.SetError(pocetak_dateTimePicker1, "Ispravan datum");
                PocetakDatum_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel4.Text = "";
            }
        }

        private void kraj_dateTimePicker2_Validating(object sender, CancelEventArgs e)
        {
            DateTime pocetak = pocetak_dateTimePicker1.Value;
            if (kraj_dateTimePicker2.Value < pocetak)
            {
                KrajDatum_errorProvider1.SetError(kraj_dateTimePicker2, "Neispravan datum");
                KrajDatum_errorProvider1.Icon = Properties.Resources.er;
                toolStripStatusLabel5.Text = "Odaberite datum poslije postavljenog početka programa.";
            }
            else
            {
                KrajDatum_errorProvider1.SetError(kraj_dateTimePicker2, "Ispravan datum");
                KrajDatum_errorProvider1.Icon = Properties.Resources.Check;
                toolStripStatusLabel5.Text = "";
            }
        }
        #endregion
    }
}
