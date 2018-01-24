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
    public partial class InfoPredstave : Form
    {
        public InfoPredstave(Predstava x)
        {
            InitializeComponent();

            NazivPredstave_textBox1.ReadOnly = true;
            TipPredstave_textBox2.ReadOnly = true;
            CijenaKarte_textBox1.ReadOnly = true;
            DatumPredstave_textBox2.ReadOnly = true;
            KategorijaPredstave_textBox3.ReadOnly = true;

            NazivPredstave_textBox1.Text = x.nazivPredstave;
            TipPredstave_textBox2.Text = x.tipPredstave;
            CijenaKarte_textBox1.Text = Convert.ToString(x.cijenaKarte);
            DatumPredstave_textBox2.Text = Convert.ToString(x.datumPredstave);
            KategorijaPredstave_textBox3.Text = x.kategorijaPredstave;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
