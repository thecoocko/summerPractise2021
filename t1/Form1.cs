using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
    //    static public double[] x = new double[31] { 8.95812222196633 ,9.04213974804818    ,8.22837739803321   , 8.38233516040625  ,  7.96026116395293 ,   8.45832085857074 ,   6.9043060961286 ,7.29108611040741  ,  6.83773404573824 ,   6.31468644204417 ,   7.47190299624606  ,  5.02015153957762 ,   4.42716369959236 ,   6.42074111604984 ,   3.94574341908293,    4.16  ,  3.71896662905046  ,  4.41901672002027    ,3.97260254254854 ,7.51459547223694
    //, 4.92231581492826 ,  3.4163768683356, 3.73187800833698  ,  3.25507818797356   , 4.9431726583911, 3.98174270514267  ,  5.70002130305613   , 3.53160049284908  ,  4.29846733147555   , 3.68223325119182 , 4.123993};

        static public int m = 30;
        static public double[] x = new double[31] { 2.17, 1.955, 1.394, 0.206, -1.135, -1.208, -1.585, -1.099, 0.165, 0.31, 0.913, 0.981, 0.633, -0.222, -1.814, -2.07, -2.685, -1.809, -1.397, -0.547, 0.415, 1.159, 1.754, 1.083, 0.561, 0.353, -0.882, -0.568, 0.387, 1.123, 0.436 };

        static public double expectedvalue()
        {
            double mx = 0;
            for (int i = 1; i < m; i++)
            {
                mx += x[i];
            }
            return Math.Round(mx/m,5);
        }
        static public double exVal = expectedvalue();
        static public double variance()
        {
            double d = 0;
            
            for (int i = 1; i < m; i++)
            {
                d += Math.Pow(x[i] - exVal, 2);
                
            }
            return Math.Round(d/(m-1),5);
        }
        
        static  public double [] correlV()
        {
            List<double> correlValues = new List<double> { };
            double v = 0, koef = 0; int k = 0;

            while (k < 15)
            {
                koef = 1.0 / (m - k);

                for (int i = 1; i < m - k; i++)
                {
                    v += (x[i] - exVal) * (x[i + k] - exVal);
                }
                correlValues.Add(Math.Round(v * koef, 5));
                k++;
            }
            double[] val = correlValues.ToArray<double>();
            return val;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            double[] val = correlV();
            
            void graphCorrelFunc()
            {
                dataGridView1.Rows.Clear();

                dataGridView1.Columns.Add("i" + 0, "i");
                dataGridView1.Columns.Add("K" + 0, "K");
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


            void graphx()
            {
                chart2.Series[0].Points.Clear();
                chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                for (int i = 0; i < val.Length; i++)
                {
                    chart2.Series[0].Points.AddXY(i, x[i]);
                }
            }

            graphx();
            graphCorrelFunc();

            this.button2.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            label5.Text = Convert.ToString(expectedvalue());
            label6.Text = Convert.ToString(variance());
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Бутенко Надія Віталіївна\n124-19-2");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<double> d = new List<double> { };
            double[] k = correlV();
            double d0 = 0;
            double omega1 = Math.PI / 30;
            double []
            
            for(int i =0; i < m - 1; i++)
            {
                d0 += k[i]; 
            }
            d0 = d0 / m;

            for( int i = 0; i < m - 1; i++)
            {
                d.Add(k[i]*Math.Cos(i*omega);
            }
        }
    }
}
