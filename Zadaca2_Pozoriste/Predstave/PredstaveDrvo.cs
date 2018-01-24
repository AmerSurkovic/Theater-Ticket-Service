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
using System.IO;

namespace Zadaca2_Pozoriste
{
    public partial class PredstaveDrvo : Form
    {
        private void dodajCvor(XmlNode XmlCvor, TreeNode drvoCvor)
        {
            XmlNode XmlTekuciCvor; // trenutni/tekući čvor u XML dokumentu
            TreeNode drvoTekuciCvor; // trenutni/tekući čvor u treeView kontroli
            XmlNodeList XmlListaCvorova; //koristi se za formiranje liste čvorova djece za tekući cvor
            if (XmlCvor.HasChildNodes) // provjerava da li XML čvor ima djece
            {
                XmlListaCvorova = XmlCvor.ChildNodes; // formira se lista XML djece za trenutni čvor
                // dok god čvor ima djece
                for (int i = 0; i <= XmlListaCvorova.Count - 1; i++)
                {
                    // tekući čvor je i-djete
                    XmlTekuciCvor = XmlCvor.ChildNodes[i];
                    drvoCvor.Nodes.Add(new TreeNode(XmlTekuciCvor.Name)); // dodaj XML čvor u drvo 
                    drvoTekuciCvor = drvoCvor.Nodes[i];
                    dodajCvor(XmlTekuciCvor, drvoTekuciCvor); // rekurzivni poziv metode dodajCvor
                }
            }
            else
            {
                drvoCvor.Text = XmlCvor.InnerText.ToString(); // tekst (sadržaj) elementa
            }
        }

        Pozoriste RPR = new Pozoriste();

        public PredstaveDrvo(Pozoriste x)
        {
            InitializeComponent();
            RPR = x;

            //1.Kreiranje dokument objekta tipa klase XmlDocument i punjenje XML dokumenta u memoriju
            XmlDocument dokument = new XmlDocument();
            dokument.Load(@"predstave.xml");

            //2. Čitanje korijena dokumenta, u ovom slučaju to je tag - predmeti
            XmlElement korijen = dokument.DocumentElement;
            treeView1.Nodes.Clear(); // brisanje čvorova, ako postoje, u treeView kontroli

            //3.Dodavanje korijena Xml dokumenta u treeView kontrolu
            treeView1.Nodes.Add(new TreeNode(dokument.DocumentElement.Name));

            //4. Kreiranja čvora u treeView kontroli
            TreeNode drvoCvor = treeView1.Nodes[0];
            
            //5. Dodavanje XML elemenata u hijerarhijsku strukturu treeView kontrole
            // Parametri metode dodajCvor - cvor XMLdokumenta i cvor treeView kontrole
            dodajCvor(korijen, drvoCvor);
        }
    }
}
