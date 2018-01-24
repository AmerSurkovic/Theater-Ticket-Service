using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Zadaca2_Pozoriste
{
    public partial class PieChart : Form
    {
        BindingList<Izuzetak> izuzeci = new BindingList<Izuzetak>();

        Chart pieChart = new Chart();
        public PieChart(BindingList<Izuzetak> x)
        {
            InitializeComponent();
            izuzeci = x;

            pieChart.Size = new Size(422, 294);

            ChartArea area = new ChartArea("PieChartArea");
            area.BorderWidth = this.Width;
            pieChart.ChartAreas.Add(area);

            pieChart.Series.Clear();
            pieChart.Palette = ChartColorPalette.EarthTones;
            pieChart.BackColor = Color.LightBlue;
            pieChart.Titles.Add("Tip i broj pojavljivanja grešaka tog tipa pri radu aplikacije");
            pieChart.ChartAreas[0].BackColor = Color.Transparent;

            Legend l = new Legend()
            {
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                Title = "Legend"
            };

            pieChart.Legends.Add(l);

            Series s1 = new Series()
            {
                Name = "s1",
                IsVisibleInLegend = true,
                Color = Color.Transparent,
                ChartType = SeriesChartType.Pie
            };

            pieChart.Series.Add(s1);

            int brojPojava31 = 0;
            int brojPojava55 = 0;
            foreach (Izuzetak t in izuzeci)
            {
                if (t.tipIzuzetka == "There is an error in XML document (0, 0).") brojPojava31++;
                if (t.tipIzuzetka == "System.IO.FileNotFoundException") brojPojava55++;
            }


            DataPoint p1 = new DataPoint(0, brojPojava31);
            p1.Color = Color.LightGreen;
            p1.AxisLabel = "There is an error in XML document (0,0)";
            p1.LegendText = "There is an error in XML document (0,0)";
            p1.Label = Convert.ToString(brojPojava31);

            s1.Points.Add(p1);

            DataPoint p2 = new DataPoint(0, brojPojava55);
            p2.Color = Color.Turquoise;
            p2.AxisLabel = "System.IO.FileNotFoundException";
            p2.LegendText = "System.IO.FileNotFoundException";
            p2.Label = Convert.ToString(brojPojava55);

            s1.Points.Add(p2);

            this.Controls.Add(pieChart);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(pieChart);
            pieChart.Series.Clear();
            pieChart.Titles.Clear();
            pieChart.ChartAreas.Clear();
            pieChart.Legends.Clear();

            ChartArea area = new ChartArea("PieChartArea");
            area.BorderWidth = this.Width;
            pieChart.ChartAreas.Add(area);

            pieChart.Series.Clear();
            pieChart.Palette = ChartColorPalette.EarthTones;
            pieChart.BackColor = Color.LightBlue;
            pieChart.Titles.Add("Tip i broj pojavljivanja grešaka tog tipa pri radu aplikacije");
            pieChart.ChartAreas[0].BackColor = Color.Transparent;

            Legend l = new Legend()
            {
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                Title = "Legend"
            };

            pieChart.Legends.Add(l);

            Series s1 = new Series()
            {
                Name = "s1",
                IsVisibleInLegend = true,
                Color = Color.Transparent,
                ChartType = SeriesChartType.Pie
            };

            pieChart.Series.Add(s1);

            int brojPojava31 = 0;
            int brojPojava55 = 0;
            foreach (Izuzetak t in izuzeci)
            {
                if (t.tipIzuzetka == "There is an error in XML document (0, 0)."
                    && t.datumIzuzetka > dateTimePicker1.Value
                    && t.datumIzuzetka < dateTimePicker2.Value)
                {
                    brojPojava31++;
                }
                if (t.tipIzuzetka == "System.IO.FileNotFoundException"
                    && t.datumIzuzetka > dateTimePicker1.Value
                    && t.datumIzuzetka < dateTimePicker2.Value)
                {
                    brojPojava55++;
                }
            }


            DataPoint p1 = new DataPoint(0, brojPojava31);
            p1.Color = Color.LightGreen;
            p1.AxisLabel = "There is an error in XML document (0,0)";
            p1.LegendText = "There is an error in XML document (0,0)";
            p1.Label = Convert.ToString(brojPojava31);

            s1.Points.Add(p1);

            DataPoint p2 = new DataPoint(0, brojPojava55);
            p2.Color = Color.Turquoise;
            p2.AxisLabel = "System.IO.FileNotFoundException";
            p2.LegendText = "System.IO.FileNotFoundException";
            p2.Label = Convert.ToString(brojPojava55);

            s1.Points.Add(p2);

            this.Controls.Add(pieChart);
        }
    }
}
