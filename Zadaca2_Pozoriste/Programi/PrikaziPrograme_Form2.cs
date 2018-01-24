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
    public partial class PrikaziPrograme_Form2 : Form
    {
        Pozoriste RPR = new Pozoriste();
        bool validacija = false;

        public PrikaziPrograme_Form2(Pozoriste x)
        {
            InitializeComponent();
            InfoPrograma_groupBox2.Visible = false;
            RPR = x;

            foreach (Pozorisni_program y in RPR.Programi)
                Programi_comboBox1.Items.Add(y.nazivPrograma);

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

                Predstave_comboBox1.Items.Clear();
                NazivPrograma_textBox1.Text = RPR.Programi[index].nazivPrograma;
                for (int i = 0; i < RPR.Programi[index].Predstave.Count(); i++)
                { 
                    Predstave_comboBox1.Items.Add(RPR.Programi[index].Predstave[i].nazivPredstave);
                }

                PocetakPrograma_textBox2.Text = Convert.ToString(RPR.Programi[index].Pocetak);
                KrajPrograma_textBox3.Text = Convert.ToString(RPR.Programi[index].Kraj);
            }
        }

        private void IzbrisiProgram_button1_Click(object sender, EventArgs e)
        {
            int index = Programi_comboBox1.SelectedIndex;
            RPR.Programi.RemoveAt(index);
            Programi_comboBox1.Items.RemoveAt(index);
            MessageBox.Show("Uspješno ste obrisali program", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.Close();
        }

        private void Predstave_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InfoPredstave_button1_Click(object sender, EventArgs e)
        {
            if (validacija == true)
            {
                OdabirPredstave_errorProvider1.Clear();
                int indexpro = Programi_comboBox1.SelectedIndex;
                int index = Predstave_comboBox1.SelectedIndex;
                Predstava info = RPR.Programi[indexpro].Predstave[index];
                InfoPredstave frm = new InfoPredstave(info);
                frm.Show();
            }
            else
            {
                OdabirPredstave_errorProvider1.SetError(Predstave_comboBox1, "Odaberite predstavu");
                OdabirPredstave_errorProvider1.Icon = Properties.Resources.er;
                validacija = false;
            }
        }

        private void Predstave_comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (Predstave_comboBox1.SelectedIndex < 0)
            {
                OdabirPredstave_errorProvider1.SetError(Predstave_comboBox1, "Odaberite predstavu");
                OdabirPredstave_errorProvider1.Icon = Properties.Resources.er;
                validacija = false;
            }
            else
            {
                OdabirPredstave_errorProvider1.SetError(Predstave_comboBox1, "Odabrana predstava");
                OdabirPredstave_errorProvider1.Icon = Properties.Resources.Check;
                validacija = true;
            }
        }

    }
}
