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

namespace Zadaca2_Pozoriste
{
    public partial class PredstaveTabela : Form
    {
        Pozoriste RPR = new Pozoriste();

        public PredstaveTabela(Pozoriste x)
        {
            InitializeComponent();
            RPR = x;

            //1.Kreiranje streama (citaca) tipa klase XmlTextReader i
            // povezivanje sa c:\TestDir\Predmet2.xml"
            XmlTextReader citac = new XmlTextReader(@"predstave.xml");
            
            // 2. Kreiranje seta podataka
            DataSet ds = new DataSet();
           
            //3. Čitanje sadržaja streama u set podataka ds
            ds.ReadXml(citac);
           
            // 4. Prikazivanje podataka u dataGridView kontroli
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            citac.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
