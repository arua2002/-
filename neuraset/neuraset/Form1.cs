using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neuraset
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Button[,] but = new Button[5, 5];
        int [,] prim =new int[5, 25];
        Random rnd = new Random();
        double[] weight = new double[25];
        bool b = false;
        int pr = 0;
        int otvet = 0;
        double sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        double sum(int i)
        {
            double r1 = 0;
            for (int j = 0; j < weight.Length; j++) r1 += prim[i, j] * weight[j];//y=x*w
            return r1;
        }
        void learn()
        {
            int y = 0;
            for (int i = 0; i < weight.Length; i++) weight[i] = rnd.NextDouble();
            double err = 1;
            while (err>0.01 || err<-0.01)
            {
                for (int i = 0; i < 4; i++)
                {
                    err = otvet - sigmoid(sum(i));//err=output-sigmoid(y)
                    for (int j = 0; j < weight.Length; j++) weight[j] += 0.0001 * (prim[i, j]*err); //это первый вариант
                }
                y++;
            }
            label2.Text = y.ToString();
        }
        double test(int[] x)
        {
            double r1 = 0;
            for (int j = 0; j < weight.Length; j++) r1 += x[j] * weight[j];//y=x*w

            return sigmoid(r1);
        }
        void clk(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            b = !b;
            if (b) button.Text = "1";
            else button.Text = "0";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int x = 50, y = 50;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    but[i, j] = new Button();
                    but[i, j].Location = new Point(10+(i*x),30+(j*y));
                    but[i, j].Text = "0";
                    but[i, j].Size = new Size(50,50);
                    but[i, j].Click += new EventHandler(clk);
                    Controls.Add(but[i,j]);
                }
            }
        }
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    but[i, j].Text = "0";
                }
            }
        }
        private void добавитьПримерToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int g = 0;
            listBox1.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    prim[pr,g] = Convert.ToInt32(but[i,j].Text);
                    g++;
                }
            }
            for (int i = 0; i < pr+1; i++)
            {
                string s = "";
                for (int j = 0;j < 25; j++)
                {
                    s += prim[i, j].ToString();
                }
                listBox1.Items.Add(s);
            }         
            pr++;
            if (pr > 4) добавитьПримерToolStripMenuItem.Enabled = false;
        }
        private void начатьОбучениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pr >= 4)
            {
                groupBox1.Visible = true;
                label2.Text = "";
            }
            else label2.Text = "добавьте 5 примеров";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                otvet = Convert.ToInt32(textBox1.Text);
                label2.Text = "";
                learn();
                проверкаToolStripMenuItem.Visible = true;
            }
            catch (Exception)
            {
                label2.Text = "введено не корректное значение";
            }
        }

        private void проверкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] a = new int[25];
            int g = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    a[g] = Convert.ToInt32(but[i, j].Text);
                    g++;
                }
            }
            double sw =  test(a);
            if (sw > 0.9) label2.Text = "это с вероятностью" + (sw*100).ToString() + "%" + textBox1.Text;
            else label2.Text = "это не " + textBox1.Text + "так как вероятность " + (sw * 100).ToString();
            listBox2.Items.Add(sw);

        }
    }
}
