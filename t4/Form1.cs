using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace t4
{
    public partial class Form1 : Form
    {
        static public double[,] matrixA = new double[5, 5] { { 1, 1, 1, 1, 1 }, { 0.5, -2.6, 0.7, 0, 0 }, { 0, 1.2, -1.7, 0, 0.6 }, { 0, 0, 1, -0.7, 0 }, { 0, 0.6, 0, 0.4, -1.2 } };//матриця А
       
        

        static public double[] matrixB = new double[5] { 1, 0, 0, 0,0 };//матриця Б

        static public double[] solution = new double[5] { 0, 0, 0, 0, 0 };//масив для зберігання розв'язків
        
        static public double dt = 0.05;//крок квантування

        static public double [] t()//функція, що поверне масив, який буде зберігати значення параметру t
        {
            List<double> x = new List<double> { };
            double h = 0;

            for(int i = 0; i < 10 / 0.05; i++)
            {
                x.Add(h);
                h += dt;
            }

            return x.ToArray<double>();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] sX = textBox1.Text.Split(' ');

            if (sX.Length == 30)
            {
                for (int i = 0; i < sX.Length; i++)
                {
                    for (int j = 0; j < sX.Length; j++)
                    {
                        matrixA[i,j] = Convert.ToDouble(sX[i]);
                    }
                    
                }

            }
            else
            {
                MessageBox.Show("Перевищена або недостатня допустима кількість вимірів");
                textBox1.Text = "";
            }
            List<double> p0 = new List<double> { };
            List<double> p1 = new List<double> { };
            List<double> p2 = new List<double> { };
            List<double> p3 = new List<double> { };
            List<double> p4 = new List<double> { };


            p1.Add(0); p2.Add(0); p3.Add(0); p4.Add(0); p0.Add(p1[0] - p2[0] - p3[0] - p4[0]);

            for (int i = 1; i < 199; i++)//реалізація метода Ейлера
            {
                p1.Add(p1[i - 1] + dt * ((-2.6) * p1[i - 1] + 0.5 * (1 - p1[i - 1] - p2[i - 1] - p3[i - 1] - p4[i - 1]) + 0.7 * p2[i - 1]));
                p2.Add(p2[i - 1] + dt * ((-1.7) * p2[i - 1] + 1.2 * p1[i - 1] + 0.6 * p4[i - 1]));
                p3.Add(p3[i - 1] + dt * ((-0.7) * p3[i - 1] + p2[i - 1]));
                p4.Add(p4[i - 1] + dt * ((-1.2) * p4[i - 1] + 0.4 * p3[i - 1] + 0.6 * p1[i - 1]));
                p0.Add(1 - p1[i] - p2[i] - p3[i] - p4[i]);
            }

            label6.Text = Convert.ToString(Math.Round(p0.Last(), 5));//вивід на форму значення коефіцієнтів
            label7.Text = Convert.ToString(Math.Round(p1.Last(), 5));
            label8.Text = Convert.ToString(Math.Round(p2.Last(), 5));
            label9.Text = Convert.ToString(Math.Round(p3.Last(), 5));
            label10.Text = Convert.ToString(Math.Round(p4.Last(), 5));
            label12.Text = Convert.ToString(Math.Round(p4.Last() + p0.Last() + p1.Last() + p2.Last() + p3.Last(), 5));

            double [] systemSolver()//функція, що обчислює точне значення системи рівнянь
            {
                
                alglib.rmatrixsolve(matrixA, 5, matrixB, out int info, out alglib.densesolverreport rep, out solution);//обчислення системи рівнянь із залученням 
                return solution;//бібліотеки alglib
               
            }
            void systemSolverOut()//вивід отриманих значень
            {
                label13.Text = Convert.ToString("[" + Math.Round(systemSolver()[0], 5) + " " + Math.Round(systemSolver()[1], 5) + " " + Math.Round(systemSolver()[2], 5) + " " + Math.Round(systemSolver()[3], 5) + " " + Math.Round(systemSolver()[4], 5) + "]");
                label16.Text = Convert.ToString(systemSolver().Sum());
            }

            void tabulation()//табулювання функції у dataGridView
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Add("t" + 0, "t");
                dataGridView1.Columns.Add("p0" + 0, "p0");
                dataGridView1.Columns.Add("p1" + 1, "p1");
                dataGridView1.Columns.Add("p2" + 1, "p2");
                dataGridView1.Columns.Add("p2" + 1, "p3");
                dataGridView1.Columns.Add("p3" + 1, "p4");
                for (int i = 0; i < 10/0.05-1; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = t()[i].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = p0.ToArray<double>()[i].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = p1.ToArray<double>()[i].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = p2.ToArray<double>()[i].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = p3.ToArray<double>()[i].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = p4.ToArray<double>()[i].ToString();
                    
                }

            }

            void graph()//вивід графіків
            {
                chart1.Series[0].Points.Clear();//очищаємо графік
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;//малюємо графік кривої
                chart1.Series[0].Name = "p0";
                Series series1 = new Series("p1");//додаємо до колекції новий екземпляр об'єкту seriesN
                chart1.Series.Add(series1);
                chart1.Series[1].Points.Clear();
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                Series series2 = new Series("p2");
                chart1.Series.Add(series2);
                chart1.Series[2].Points.Clear();
                chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                Series series3 = new Series("p3");
                chart1.Series.Add(series3);
                chart1.Series[3].Points.Clear();
                chart1.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                Series series4 = new Series("p4");
                chart1.Series.Add(series4);
                chart1.Series[4].Points.Clear();
                chart1.Series[4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                for (int i = 0; i < 10/0.05-1; i++)
                {
                    chart1.Series[0].Points.AddXY(t()[i], p0.ToArray<double>()[i]);//додаємо точки на графік для відмалювання його на chart1
                    chart1.Series[1].Points.AddXY(t()[i], p1.ToArray<double>()[i]);
                    chart1.Series[2].Points.AddXY(t()[i], p2.ToArray<double>()[i]);
                    chart1.Series[3].Points.AddXY(t()[i], p3.ToArray<double>()[i]);
                    chart1.Series[4].Points.AddXY(t()[i], p4.ToArray<double>()[i]);
                }
            }

            systemSolverOut();
            tabulation();
            graph();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1 1 1 1 1\n0.5 -2.6 0.7 0 0\n0 1.2 -1.7 0 0.6\n0 0 1 -0.7 0\n0 0.6 0 0.4 -1.2";
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
