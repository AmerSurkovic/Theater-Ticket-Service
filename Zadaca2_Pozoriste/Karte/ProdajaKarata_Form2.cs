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
    public partial class ProdajaKarata_Form2 : Form
    {
        Pozoriste RPR = new Pozoriste();
        bool validacija;

        public ProdajaKarata_Form2(Pozoriste x)
        {
            InitializeComponent();
            RPR = x;
            validacija = false;
            InfoPrograma_groupBox2.Visible = false;
            PredRacun_groupBox1.Visible = false;

            for (int i = 0; i < RPR.Programi.Count(); i++)
            {
                Programi_comboBox1.Items.Add(RPR.Programi[i].nazivPrograma);
            }
            NazivPrograma_textBox1.ReadOnly = true;
            PocetakPrograma_textBox2.ReadOnly = true;
            KrajPrograma_textBox3.ReadOnly = true;
            Cijena_textBox1.ReadOnly = true;
            PotrebnoPlatiti_textBox1.ReadOnly = true;

            statusStrip1.Visible = false;
            toolStripStatusLabel1.Text = ("Unesite količinu karata za proračun ili prodaju!");
            toolStripStatusLabel1.ForeColor = Color.Red;
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
            PredRacun_groupBox1.Visible = true;
            int index = Programi_comboBox1.SelectedIndex;
            int indexPRED = Predstave_comboBox1.SelectedIndex;
            Cijena_textBox1.Text = Convert.ToString(RPR.Programi[index].Predstave[indexPRED].cijenaKarte);
        }

        private void Proracun_button1_Click(object sender, EventArgs e)
        {
            if (validacija == true)
            {
                int index = Programi_comboBox1.SelectedIndex;
                int indexPRED = Predstave_comboBox1.SelectedIndex;

                decimal racun = RPR.Programi[index].Predstave[indexPRED].cijenaKarte;
                int kolicina = Convert.ToInt32(Kolicina_textBox2.Text);

                if (SaPopustom_checkBox1.Checked || kolicina > 5)
                {
                    racun = racun - racun % 10;
                }
                PotrebnoPlatiti_textBox1.Text = Convert.ToString(racun * kolicina);
            }
            else statusStrip1.Visible = true;
        }

        private void Racun_button1_Click(object sender, EventArgs e)
        {
            #region
            if (validacija == true)
            {
                int index = Programi_comboBox1.SelectedIndex;
                int indexPRED = Predstave_comboBox1.SelectedIndex;

                decimal racun = RPR.Programi[index].Predstave[indexPRED].cijenaKarte;
                int kolicina = Convert.ToInt32(Kolicina_textBox2.Text);

                if (SaPopustom_checkBox1.Checked || kolicina > 5)
                {
                    racun = racun - racun % 10;
                }
                PotrebnoPlatiti_textBox1.Text = Convert.ToString(racun * kolicina);

                MessageBox.Show("Da li ste sigurni da želite isprintati račun?", "Upit", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            #endregion
            else statusStrip1.Visible = true;
        }

        private void Kolicina_textBox2_Validating(object sender, CancelEventArgs e)
        {
            int kolicina;
            if (string.IsNullOrEmpty(Kolicina_textBox2.Text))
            {
                Kolicina_errorProvider1.SetError(Kolicina_textBox2, "Unesite količinu");
                Kolicina_errorProvider1.Icon = Properties.Resources.er;
            }
            else if (!int.TryParse(Kolicina_textBox2.Text, out kolicina))
            {
                Kolicina_errorProvider1.SetError(Kolicina_textBox2, "Unesite cjelobrojnu vrijednost");
                Kolicina_errorProvider1.Icon = Properties.Resources.er;
            }
            else
            {
                Kolicina_errorProvider1.SetError(Kolicina_textBox2, "Ispravan unos");
                Kolicina_errorProvider1.Icon = Properties.Resources.Check;
                validacija = true;
                statusStrip1.Visible = false;
            }
        }
    }
}
