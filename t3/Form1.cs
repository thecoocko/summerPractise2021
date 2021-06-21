using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace t3_2
{
    public partial class Form1 : Form
    {

        

        static public double func1(double t1, double t2, double a, double t, double s0, double k)
        {

            return k * a * (1.0 - (t1 / (t1 - t2)) * Math.Exp(-t / t1) - (t2 / (t2 - t1)) * Math.Exp(-t / t2));
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int j = 0;
            
            List<double> iValues = new List<double> { };
            List<double> dValues = new List<double> { };

            try
            {
                double a = Convert.ToDouble(textBox1.Text);
                double t = Convert.ToDouble(textBox2.Text);
                double s0 = Convert.ToDouble(textBox3.Text);
                double k = Convert.ToDouble(textBox4.Text);
                double t1 = 0.01, t2 = 0.01;

                int n = Convert.ToInt32(textBox6.Text);

                if (radioButton1.Checked == true || radioButton2.Checked == true)
                {
                    if (radioButton1.Checked)
                    {
                        t1 = Convert.ToDouble(textBox5.Text);

                        label2.Text = Convert.ToString(Math.Round(func1(t1, t2 , a, t, s0, k),5));

                        while (j < n)
                        {
                            dValues.Add(Math.Round(Math.PI * s0 * Math.Pow(k, 2) / (t1 + t2),5));
                            iValues.Add(Math.Round(Math.Pow(func1(t1, t2, a, t, s0, k), 2) / dValues[j],5));
                            t2 = t2 + 0.02;
                            j++;
                        }
                    }
                    else if (radioButton2.Checked)
                    {
                        t2 = Convert.ToDouble(textBox5.Text);

                        label2.Text = Convert.ToString(Math.Round(func1(t1, t2 , a, t, s0, k),5));

                        while (j < n)
                        {
                            dValues.Add(Math.Round(Math.PI * s0 * Math.Pow(k, 2) / (t1 + t2),5));
                            iValues.Add(Math.Round(Math.Pow(func1(t1, t2, a, t, s0, k), 2) / dValues[j],5));
                            t1 = t1 + 0.02;
                            j++;
                        }
                    }

                    double[] iValuesArr = iValues.ToArray<double>();

                    label6.Text = Convert.ToString(Math.Round(iValuesArr.Max(),5));
                    label4.Text = Convert.ToString(dValues[dValues.ToArray<double>().Length - 1]);
                    dataGridView1.Rows.Clear();

                    dataGridView1.Columns.Add("i" + 0, "i");
                    dataGridView1.Columns.Add("I" + 0, "I");
                    for (int i = 0; i < iValuesArr.Length; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[1].Value = iValuesArr[i].ToString();
                        dataGridView1[0, i].Value = i.ToString();
                    }

                    chart1.Series[0].Points.Clear();
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                    for (int i = 0; i < iValuesArr.Length; i++)
                    {
                        chart1.Series[0].Points.AddXY(i, iValuesArr[i]);
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть параметр");
                }
            }
            catch
            {
                MessageBox.Show("Дані введено некоректно. Перевірте комірки");
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "0,5";
            textBox2.Text = "10";
            textBox3.Text = "0,2";
            textBox4.Text = "2";
            textBox5.Text = "1,5";
            textBox6.Text = "201";
            radioButton1.Checked = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            chart1.Series[0].Points.Clear();
        }
    }
}
