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

namespace GUI_Studentai
{
    public partial class Form1 : Form
    {
        const string cfd = "Studentai.txt";
        const string cfr = "Rezultatai.txt";
        Studentai masyvas;
        Pazymys[] pazymiai = new Pazymys[10]
        {
            new Pazymys(10, "Puikiai"),
            new Pazymys(9,  "Labai gerai"),
            new Pazymys(8,  "Gerai"),
            new Pazymys(7,  "Vidutiniškai"),
            new Pazymys(6,  "Patenkinamai"),
            new Pazymys(5,  "Silpnai"),
            new Pazymys(4,  "Nepatenkinamai"),
            new Pazymys(3,  "Nepatenkinamai"),
            new Pazymys(2,  "Nepatenkinamai"),
            new Pazymys(1,  "Nepatenkinamai")};
    public Form1()
        {
            InitializeComponent();
            if (File.Exists(cfr)) File.Delete(cfr);
            spausdinti.Enabled = false;
            skaiciuoti.Enabled = false;
            rasti.Enabled = false;
            foreach (Pazymys paz in pazymiai)
                vertinimai.Items.Add(paz.Pazym + " " + paz.PazZodR);
        }
        static Studentai skaitymas (string cfd)
        {
            Studentai s = new Studentai();
            using (StreamReader reader=new StreamReader(cfd))
            {
                string line;
                while((line=reader.ReadLine())!=null)
                {
                    string[] parts = line.Split(';');
                    string pav = parts[0];
                    int paz = int.Parse(parts[1]);
                    Studentas ss = new Studentas(pav, paz);
                    s.deti(ss);
                }
            }
            return s;

        }
        static void spausdinimas(string cfr, Studentai s, string antraste)
        {
            const string virsus= "-----------------------------------\r\n"
                + " Nr.  Pavardė ir vardas     Pažymys \r\n"
                + "-----------------------------------";
            using (var fr=File.AppendText(cfr))
            {
                fr.WriteLine("\n " + antraste); 
                fr.WriteLine(virsus);
                for(int i=0; i<s.kiek; i++)
                {
                    Studentas a = s.imti(i);
                    fr.WriteLine("{0, 3}   {1}", i + 1, a);

                }
                fr.WriteLine("-----------------------------------\n");
            }
        }
        static int kiekis (Studentai m, int p)
        {
            int ats = 0;
            for(int i=0; i<m.kiek; i++)
            {
                if (m.imti(i).Pazym == p) ats++;
            }
            return ats;
        }
        static int rastiindeksa(Studentai m, string vardas)
        {
            for (int i = 0; i < m.kiek; i++)
            {
                if (m.imti(i).PavVrd == vardas) return i;
            }
            return -1;
        }
        private void ivesti_Click(object sender, EventArgs e)
        {
            //rezultatai.LoadFile(cfd, RichTextBoxStreamType.PlainText);
            string a = File.ReadAllText(cfd);
            rezultatai.Text = a;
            masyvas = skaitymas(cfd);
            ivesti.Enabled = false;
            spausdinti.Enabled = true;
            skaiciuoti.Enabled = true;
            rasti.Enabled = true;
        }

        private void spausdinti_Click(object sender, EventArgs e)
        {
            spausdinimas(cfr, masyvas, "Studentų sąrašas");
            string a = File.ReadAllText(cfr);
            rezultatai.Text = a;
            //rezultatai.LoadFile(cfr, RichTextBoxStreamType.PlainText);
            vertinimai.SelectedIndex = 0;
        }

        private void skaiciuoti_Click(object sender, EventArgs e)
        {
            string verte = vertinimai.SelectedItem.ToString();
            string[] parts = verte.Split(' ');
            int ieskomas = int.Parse(parts[0]);
            int ats = 0;
            for(int i=0; i<masyvas.kiek; i++)
            {
                if (masyvas.imti(i).Pazym == ieskomas) ats++;
            }
            if (ats > 0) rezultatas.Text = "Tokių studentų yra tiek: " + ats.ToString();
            else rezultatas.Text = "Tokių studentų nėra";
        }

        private void rasti_Click(object sender, EventArgs e)
        {
            pavardeVardas.Text = "Pavardė ir vardas";
            string a = pavardeVrd.Text;
            int indeksas = rastiindeksa(masyvas, a);
            if (indeksas > -1)
            {
                Studentas ss = masyvas.imti(indeksas);
                int paz = ss.Pazym;
                pavardeVardas.Text = pavardeVardas.Text + " (pažymys: " + paz.ToString() + ")";
            }
            else pavardeVardas.Text = pavardeVardas.Text+ " (Tokio studento nėra)";

        }

        private void baigti_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
