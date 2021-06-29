using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static public double[] x = new double[] { 8.96, 9.04, 8.23, 8.38, 7.96, 8.46, 6.90, 7.29, 6.84, 6.31, 7.47, 5.02, 4.43, 6.42, 3.95, 4.16, 3.72, 4.42, 3.97, 7.51, 4.92, 3.42, 3.73, 3.26, 4.94, 3.98, 5.70, 3.53, 4.30, 3.68};
        
        static public double expectedvalue()
        {
            double mx = 0;
            for (int i = 0; i < x.Length; i++)
            {
                mx += x[i];
            }
            return Math.Round(mx / 30, 5);
        }
        static public double exVal = expectedvalue();
        static public double variance()
        {
            double d = 0;

            for (int i = 0; i < x.Length; i++)
            {
                d += Math.Pow(x[i] - exVal, 2);

            }

            return Math.Round(d / (29), 5);
        }

        static public double[] correlV()
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
        private double getSumDesp(double[] array, int k)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i] * Math.Cos(i * (k * (-Math.PI / array.Length)) * 1);
            }
            return sum;
        }

        public Form1()
        {
            InitializeComponent();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {

            double[] val = correlV();

            void showGraphCorrelFunc()
            {
                dataGridView1.Rows.Clear();

                dataGridView1.Columns.Add("i" + 0, "i");
                dataGridView1.Columns[0].Width = 25;
                dataGridView1.Columns.Add("K" + 0, "K");
                dataGridView1.Columns[1].Width = 50;

                for (int i = 0; i < val.Length; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[1].Value = val[i].ToString();
                    dataGridView1[0, i].Value = i.ToString();
                }

                chart1.Series[0].Points.Clear();
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                for (int i = 0; i < val.Length; i++)
                {
                    chart1.Series[0].Points.AddXY(i, val[i]);
                }
            }


            void showGraphX()
            {
                chart2.Series[0].Points.Clear();
                chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                for (int i = 0; i < val.Length; i++)
                {
                    chart2.Series[0].Points.AddXY(i, x[i]);
                }
            }

            showGraphX();
            showGraphCorrelFunc();

            button2.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            label5.Text = Convert.ToString(expectedvalue());
            label6.Text = Convert.ToString(variance());
            button1.Enabled = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {

            double[] desp = new double[11];

            
            for (int i = 0; i <= 10; i++)
            {
                desp[i] = getSumDesp(correlV(), i) / 30;
                
            }



            void showGraphAndDataDisp()
            {
                dataGridView3.Rows.Clear();

                dataGridView3.Columns.Add("i" + 0, "i");
                dataGridView3.Columns[0].Width = 25;
                dataGridView3.Columns.Add("K" + 0, "K");
                dataGridView3.Columns[1].Width = 50;

                for (int i = 0; i < desp.Length; i++)
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[1].Value = desp[i].ToString();
                    dataGridView3[0, i].Value = i.ToString();
                }

                chart4.Series[0].Points.Clear();
                chart4.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                for (int i = 0; i < desp.Length; i++)
                {
                    chart4.Series[0].Points.AddXY(i, desp[i]);
                }
            }
            showGraphAndDataDisp();
            button6.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart4.Series[0].Points.Clear();

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();

            label5.Text = "0";
            label6.Text = "0";

            button1.Enabled = true;
            button2.Enabled = true;
            button6.Enabled = true;
        }
    }
}
