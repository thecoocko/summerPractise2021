using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace t3_2
{
    public partial class Form1 : Form
    {

        

        static public double func1(double t1, double t2, double a, double t, double s0, double k)//формула для пошуку оптимального критерію
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
            
            List<double> iValues = new List<double> { };//список для зберігання значень функції I
            List<double> dValues = new List<double> { };//список для зберігання значень дисперсії шумів

            try//"вилов" помилок
            {
                double a = Convert.ToDouble(textBox1.Text);
                double t = Convert.ToDouble(textBox2.Text);//конвертація отриманих текстовиз значень в числа з плаваючою точкою
                double s0 = Convert.ToDouble(textBox3.Text);

                double t1 = 0.01, t2 = 0.01, k = 0.01;

                int n = Convert.ToInt32(textBox6.Text);//кількість ітерацій

                if ((checkBox1.Checked == true && checkBox2.Checked == true) || (checkBox1.Checked == true && checkBox3.Checked == true)|| (checkBox2.Checked == true && checkBox3.Checked == true))
                {
                    if (checkBox1.Checked == true && checkBox2.Checked == true)//якщо відомо т1 і к, то
                    {
                        t1 = Convert.ToDouble(textBox5.Text);
                        k = Convert.ToDouble(textBox4.Text);

                        label2.Text = Convert.ToString(Math.Round(func1(t1, t2 , a, t, s0, k),5));//обчилсення значення а(параметр)

                        while (j < n)
                        {
                            dValues.Add(Math.Round(Math.PI * s0 * Math.Pow(k, 2) / (t1 + t2),5));//додавання до списка зі значеннями дисперсії шумів результат обчислення
                            iValues.Add(Math.Round(Math.Pow(func1(t1, t2, a, t, s0, k), 2) / dValues[j],5));//додавання до списка зі значеннями функції І результат обчислення
                            t2 = t2 + 0.02;
                            j++;
                        }
                    }
                    else if (checkBox2.Checked == true && checkBox3.Checked == true)//якщо відомо т2 і к, то 
                    {
                        t2 = Convert.ToDouble(textBox7.Text);
                        k = Convert.ToDouble(textBox4.Text);

                        label2.Text = Convert.ToString(Math.Round(func1(t1, t2 , a, t, s0, k),5));

                        while (j < n)
                        {
                            dValues.Add(Math.Round(Math.PI * s0 * Math.Pow(k, 2) / (t1 + t2),5));
                            iValues.Add(Math.Round(Math.Pow(func1(t1, t2, a, t, s0, k), 2) / dValues[j],5));
                            t1 += 0.02;
                            j++;
                        }
                    }
                    else if (checkBox3.Checked == true && checkBox1.Checked == true)// якщо відомо т1 і т2, то
                    {
                        t1 = Convert.ToDouble(textBox5.Text);
                        t2 = Convert.ToDouble(textBox7.Text);
                        while (j < n)
                        {

                        dValues.Add(Math.Round(Math.PI * s0 * Math.Pow(k, 2) / (t1 + t2), 5));
                        iValues.Add(Math.Round(Math.Pow(func1(t1, t2, a, t, s0, k), 2) / dValues[j], 5));
                        k += 0.02;
                        j++;
                        }
                    }

                    double[] iValuesArr = iValues.ToArray<double>();//трансформування зі списка значень у масив значень
                    //ця метаморфоза необхідна для доступу до властивості .Length 

                    label6.Text = Convert.ToString(Math.Round(iValuesArr.Max(),5));//пошук максимального значення
                    label4.Text = Convert.ToString(dValues[dValues.ToArray<double>().Length - 1]);//вивід останнього елемента зі списку, що трансформовано у масив

                    dataGridView1.Rows.Clear();

                    dataGridView1.Columns.Add("i" + 0, "i");//додавння стовпчику з ітератором
                    dataGridView1.Columns.Add("I" + 0, "I");//додання стовпчику із значенням функції І
                    for (int i = 0; i < iValuesArr.Length; i++)
                    {
                        dataGridView1.Rows.Add();//додаємо комірку
                        dataGridView1.Rows[i].Cells[1].Value = iValuesArr[i].ToString();//заповнюємо комірку значенням и приводимо до текстового формату
                        dataGridView1[0, i].Value = i.ToString();
                    }

                    chart1.Series[0].Points.Clear();//очищаємо графік
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;//малюємо графік кривої
                    for (int i = 0; i < iValuesArr.Length; i++)
                    {
                        chart1.Series[0].Points.AddXY(i, iValuesArr[i]);//додаємо точки на графік для відмалювання його на chart1
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть параметр");//якщо ні один з параметрів не обрано
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

        private void button3_Click_1(object sender, EventArgs e)//кнопка для вводу значень за замовчуванням
        {
            textBox1.Text = "0,5";
            textBox2.Text = "10";
            textBox3.Text = "0,2";
            textBox4.Text = "2";
            textBox5.Text = "1,5";
            textBox6.Text = "201";
            textBox7.Text = "";
            checkBox1.Checked = true;

            checkBox2.Checked = true;
        }

        private void button4_Click(object sender, EventArgs e)//кнопка для очищення форми від даних
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            chart1.Series[0].Points.Clear();
        }

       
    }
}
