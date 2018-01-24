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
    public partial class BarChart : Form
    {
        BindingList<Izuzetak> izuzeci = new BindingList<Izuzetak>();

        Chart barChart = new Chart();
        public BarChart(BindingList<Izuzetak> x)
        {
            InitializeComponent();
            izuzeci = x;

            barChart.Size = new Size(398, 321);

            ChartArea area = new ChartArea();
            barChart.ChartAreas.Add(area);

            barChart.Titles.Add("Tip i broj pojavljivanja grešaka tog tipa pri radu aplikacije");
            barChart.BackColor = Color.Transparent;

            barChart.ChartAreas[0].BackColor = Color.Transparent;
            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            Series series = new Series()
            {
                Name = "series2",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };

            barChart.Series.Add(series);

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
            p1.LegendText =  "There is an error in XML document (0,0)";
            p1.Label = Convert.ToString(brojPojava31);

            series.Points.Add(p1);

            DataPoint p2 = new DataPoint(0, brojPojava55);
            p2.Color = Color.Turquoise;
            p2.AxisLabel = "System.IO.FileNotFoundException";
            p2.LegendText = "System.IO.FileNotFoundException";
            p2.Label = Convert.ToString(brojPojava55);

            series.Points.Add(p2);

            this.Controls.Add(barChart);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(barChart);
            barChart.Series.Clear();
            barChart.ChartAreas.Clear();
            barChart.Titles.Clear();

            barChart.Size = new Size(398, 321);

            ChartArea area = new ChartArea();
            barChart.ChartAreas.Add(area);

            barChart.Titles.Add("Tip i broj pojavljivanja grešaka tog tipa pri radu aplikacije");
            barChart.BackColor = Color.Transparent;

            barChart.ChartAreas[0].BackColor = Color.Transparent;
            barChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            barChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            Series series = new Series()
            {
                Name = "series2",
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column
            };

            barChart.Series.Add(series);

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

            series.Points.Add(p1);

            DataPoint p2 = new DataPoint(0, brojPojava55);
            p2.Color = Color.Turquoise;
            p2.AxisLabel = "System.IO.FileNotFoundException";
            p2.LegendText = "System.IO.FileNotFoundException";
            p2.Label = Convert.ToString(brojPojava55);

            series.Points.Add(p2);

            this.Controls.Add(barChart);
        }
    }
}
