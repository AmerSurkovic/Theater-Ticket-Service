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
    public partial class UnosTelefonaUsControl : UserControl
    {
        public UnosTelefonaUsControl()
        {
            InitializeComponent();
        }

        public double BrojTelefona { 
            get {
                double unos;
                if (string.IsNullOrEmpty(BrTelefona_textBox1.Text))
                {
                    throw new System.Exception("Unesite broj telefona!");
                }
                else if (!double.TryParse(BrTelefona_textBox1.Text, out unos))
                {
                    throw new System.FormatException("Uneseni telefonski broj nije validan. Jedino je unos brojeva dozvoljen u pomenuto polje!");
                }
                else if (unos > 99999999999 || unos < 11111111)
                {
                    throw new FormatUnosaException("Prekoračenje - unesen je prevelik ili premali broj telefona.\n" + "Minimum cifara: 8 <6_xxxyyy>\n" + "Maksimum cifara: 11 <38733xxxyyy>");
                }
                else return Convert.ToDouble(BrTelefona_textBox1.Text); 
            } 
        }
    }
}
