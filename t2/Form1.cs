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
        static public double[] x = new double[] { 8.96, 9.04, 8.23, 8.38, 7.96, 8.46, 6.90, 7.29, 6.84, 6.31, 7.47, 5.02, 4.43, 6.42, 3.95, 4.16, 3.72, 4.42, 3.97, 7.51, 4.92, 3.42, 3.73, 3.26, 4.94, 3.98, 5.70, 3.53, 4.30, 3.68, 4.62 };
        static public int m = 30;
        static public double omega = Math.PI / x.Length;
        static public double expectedvalue()
        {
            double mx = 0;
            for (int i = 1; i < x.Length; i++)
            {
                mx += x[i];
            }
            return Math.Round(mx/30,5);
        }
        static public double exVal = expectedvalue();
        static public double variance()
        {
            double d = 0;

            for (int i = 1; i < x.Length; i++)
            {
                d += Math.Pow(x[i] - exVal, 2);

            }

            return Math.Round(d / 29, 5);
        }
        
        static private double[] correlFunc()
        {
            List<double> correlValues = new List<double> { };
            double[] v = new double[30]; int i = 0;

            do
            {
                for (int j = 0; j < x.Length - i; j++)
                {
                    v[i] += ((x[j] - exVal) * (x[i + j] - exVal));
                }
                v[i] /= (x.Length - i);

                correlValues.Add(Math.Round(v[i], 5));
                i++;
            } while (i <= (x.Length / 2));
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
            double[] spectralDensityValuesY = new double[15];
            double[] correlValuesY = new double[15];

            double [] spectralDensity()
            {
                double[] spectralDensityValues = new double[15];
                
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
