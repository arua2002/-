using System;
using System.Windows.Forms;

namespace нейросеть
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[,] a = { { 0, 0, 1 }, { 1, 1, 1 }, { 1, 0, 1 }, { 0, 1, 1 } };//input
        int[] b = { 0, 1, 1, 0 };//output
        double[] w = new double[3];
        Random rnd = new Random();
        double s(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        double  ob(int i)
        {
            double r1 = 0;
            for (int j = 0; j < 3; j++) r1 += a[i, j] * w[j];//y=x*w
            return r1;
        }
        double test(int[] a1)
        {
            double r1 = 0;
            for (int j = 0; j < 3; j++) r1 += a1[j] * w[j];//y=x*w
            return r1;
        }
        void learn()
        {  
            int y = 0;
            int re = 200000;
            progressBar1.Maximum = re;
            while (y<=re)
            {
                for (int i = 0; i < 4; i++)
                {
                 double err = b[i] - s(ob(i));//err=output-sigmoid(y)
                 for (int j = 0; j < 3; j++) w[j] += 0.001* err * a[i,j];               
                }
                progressBar1.Value = y;
                y++;
            }           
        } 
        private void button1_Click(object sender, EventArgs e)//training
        {
            listBox1.Items.Clear();
            for (int i = 0; i < 3; i++)  w[i] = rnd.NextDouble();
            learn();
            foreach (var i in w) listBox1.Items.Add(i);
            button2.Visible = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int []ninp = { Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text) };
           label1.Text = s(test(ninp)).ToString();
        }
    }
}
