using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadaca2_Pozoriste
{
    public partial class RezervacijaUserControl : UserControl
    {
        UnosTelefonaUsControl k = null;

        public RezervacijaUserControl()
        {
            InitializeComponent();
            k = this.unosTelefonaUsControl1;
            Kod_textBox3.ReadOnly = true;
        }

        public double BrojTelefona { get { return k.BrojTelefona; } }

        public string Ime { 
            get { 
                if(string.IsNullOrEmpty(Ime_textBox1.Text))
                {
                    throw new System.Exception("Polje 'Ime' je prazno!");
                }
                else return Ime_textBox1.Text; 
            } 
        }
        public string Prezime { 
            get {
                if (string.IsNullOrEmpty(Prezime_textBox2.Text))
                {
                    throw new System.Exception("Polje 'Prezime' je prazno!");
                }
                else return Prezime_textBox2.Text; 
            } 
        }
        public decimal BrojKarata { 
            get {
                if (BrKarata_numericUpDown1.Value == 0)
                {
                    throw new System.Exception("Broj karata za rezervaciju ne može biti nula!");
                }
                else return BrKarata_numericUpDown1.Value; 
            } 
        }
        public int RezervacijskiKod { 
            get {
                if (string.IsNullOrEmpty(Kod_textBox3.Text))
                {
                    throw new System.Exception("Rezervacijski kod nije generisan!");
                }
                else return Convert.ToInt32(Kod_textBox3.Text); 
            } 
        }

        public void Generisi_button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int x = r.Next(0, 1000);
            Kod_textBox3.Text = Convert.ToString(x); 
        }

    }
}
