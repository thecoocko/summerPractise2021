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

namespace t2
{
    public partial class Form1 : Form
    {
        static public double[] x = new double[31] {8.95812222196633, 9.04213974804818 ,   8.22837739803321  ,  8.38233516040625   , 7.96026116395293 ,   8.45832085857074 ,   6.90430609612860 ,   7.29108611040741 ,   6.83773404573824 ,  6.31468644204417   , 7.47190299624606  ,  5.02015153957762   , 4.42716369959236 ,   6.42074111604984  ,  3.94574341908293 ,   4.16000000000000,    3.71896662905046  ,  4.41901672002027 ,   3.97260254254854  ,  7.51459547223694 ,   4.92231581492826 ,   3.41637686833560 ,   3.73187800833698  ,  3.25507818797356 ,   4.94317265839110  ,  3.98174270514267  ,  5.70002130305613 ,   3.53160049284908   , 4.29846733147555   , 3.68223325119182,    4.62472075723728
 };
        static public int m = 30;
        static public double omega = Math.PI / x.Length;
        static public double expectedvalue()
        {
            double mx = 0;
            foreach (int i in x)
            {
                mx += i;
            }
            return Math.Round(mx *(1.0/m),5);
        }
        static public double exVal = expectedvalue();
        static public double variance()
        {
            double d = 0;
            double koef = 1 / m;
            for (int i = 0; i < x.Length; i++)
            {
                d += Math.Pow(x[i] - exVal, 2);

            }
            return Math.Round(koef * d, 5);
        }
        
        static private double[] correlFunc()
        {
            List<double> correlValues = new List<double> { };
            double v = 0, koef = 0; int j = 0;
            for (int i = 1; i < m - j; i++)
            {
                koef = 1.0 / (x[i] - exVal);
                while (j < 15)
                {
                    v = (x[i] - exVal) * (x[i + j] - exVal);
                    j++;
                }

                correlValues.Add(Math.Round(v * koef, 5));
            }
            double[] val = correlValues.ToArray<double>();
            return val;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double phiSq = 4.0 / (Math.Pow(omega, 2) + 1);
            double[] correlValues = correlFunc();
            double[] spectralDensityValuesY = new double[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] correlValuesY = new double[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            double [] spectralDensity()
            {
                double[] spectralDensityValues = new double[15] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                
                for(int i = 0; i < correlValues.Length; i++)
                {
                    spectralDensityValues[i] = correlValues[i] * Math.Cos(i * omega);
                }
                return spectralDensityValues;
            }

            for(int i = 0; i < spectralDensity().Length; i++)
            {
                spectralDensityValuesY[i] = phiSq * spectralDensity()[i];
            }
            for(int i = 0 ;i < spectralDensityValuesY.Length; i++)
            {
                correlValuesY[i] = spectralDensityValuesY[i] * Math.Cos(i * omega);
            }

            void graphSpectralFunc()
            {
                dataGridView1.Rows.Clear();

                dataGridView1.Columns.Add("i" + 0, "i");
                dataGridView1.Columns.Add("Sy" + 0, "Sy");
                for (int i = 0; i < spectralDensityValuesY.Length; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[1].Value = spectralDensityValuesY[i].ToString();
                    dataGridView1[0, i].Value = i.ToString();
                }

                chart1.Series[0].Points.Clear();
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chart1.Series[0].Name = "Spectral Density X(t)";
                Series series1 = new Series("Spectral Density Y(t)");
                chart1.Series.Add(series1);
                chart1.Series[1].Points.Clear();
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

                for (int i = 0; i < spectralDensityValuesY.Length; i++)
                {
                    chart1.Series[1].Points.AddXY(i, spectralDensityValuesY[i]);
                    chart1.Series[0].Points.AddXY(i, spectralDensity()[i]);
                }
            }

            void graphCorrelFuncY(double[] val)
            {
                dataGridView2.Rows.Clear();

                dataGridView2.Columns.Add("i" + 0, "i");
                dataGridView2.Columns.Add("Ky" + 0, "Ky");
                for (int i = 0; i < correlValuesY.Length; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[1].Value = correlValuesY[i].ToString();
                    dataGridView2[0, i].Value = i.ToString();
                }

                chart2.Series[0].Points.Clear();
                chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chart2.Series[0].Name = "Сorrelation Fuction X(t)";
                Series series1 = new Series("Сorrelation Fuction Y(t)");
                chart2.Series.Add(series1);
                chart2.Series[1].Points.Clear();
                chart2.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                for (int i = 0; i < correlValues.Length; i++)
                {

                    chart2.Series[1].Points.AddXY(i, correlValuesY[i]);
                    chart2.Series[0].Points.AddXY(i, correlValues[i]);
                }
            }

            graphCorrelFuncY(correlValuesY);
            graphSpectralFunc();
   




        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(expectedvalue());
        }

        private void button3_Click(object sender, EventArgs e)
        {

            label3.Text = Convert.ToString(Math.Round(4.0 / (16 * Math.Pow(omega,2) + 1),3)); 
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
