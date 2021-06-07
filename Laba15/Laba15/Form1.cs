using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba15
{
    public partial class Form1 : Form
    {

        double[,] Q;
        int n = 5;
        string[] states = { "ясно", "облачно", "пасмурно" }; string state0;
        double[] dur = { 0, 0, 0 };
        int i, inew, j, k; double t, tnew, tau;
        Random R = new Random();


        int I; int time, tt; double p, p1, r;

        public Form1()
        {
            InitializeComponent();
            Q = new double[3, 3];

            Q[0, 0] = -0.5; Q[0, 1] = 0.4; Q[0, 2] = 0.1;
            Q[1, 0] = 0.3; Q[1, 1] = -0.6; Q[1, 2] = 0.3;
            Q[2, 0] = 0.1; Q[2, 1] = 0.3; Q[2, 2] = -0.4;

            state0 = (string)comboBox1.SelectedItem;
            for (k = 0; k < 3; k++) if (states[k].Equals(state0)) I = k;
        }

        public void thing(int t1)
        {
            tt = 0;
            t = 0;

            while (tt <= time)
            {
                tau = Math.Log(R.NextDouble()) / Q[I, I];
                t += tau;

                p1 = 0;
                r = R.NextDouble();
                for (j = 0; j < 3; j++)
                {
                    if (j == I) { p = 0; }
                    else { p = -Q[I, j] / Q[I, I]; }
                    p1 += p;
                    if (r < p1) { I = j; break; }
                }

                if (t >= n)
                {
                    chart1.Series[0].Points.AddXY(tt, I);
                    t = 0;
                }
                tt++;
            }
        }

        public void but1()
        {
            time = (int)tbtime.Value;
            tt = 0;

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[0].Points.AddXY(tt, I);
            thing(time);
        }

        public void but2(double T)
        {
            t = 0;
            while (t <= T)
            {

                tau = Math.Log(R.NextDouble()) / Q[I, I];
                t += tau;

                p1 = 0;
                r = R.NextDouble();
                for (j = 0; j < 3; j++)
                {
                    if (j == I) { p = 0; }
                    else { p = -Q[I, j] / Q[I, I]; }
                    p1 += p;
                    if (r < p1) { I = j; break; }
                }
            }
            k = 0;
            tnew = t; inew = I;
            while (k < n)
            {
                tau = Math.Log(R.NextDouble()) / Q[I, I];
                tnew += tau;

                p1 = 0;
                r = R.NextDouble();
                for (j = 0; j < 3; j++)
                {
                    if (j == I) { p = 0; }
                    else { p = -Q[I, j] / Q[I, I]; }
                    p1 += p;
                    if (r < p1) { I = j; break; }
                }
                k++;

                dur[I] += (tnew - t);
                I = inew; t = tnew;
            }
            for (i = 0; i < 3; i++) dur[i] = dur[i] / dur.Sum();

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (i = 0; i < 3; i++) chart1.Series[1].Points.AddXY(i, dur[i]);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            but1();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            double T = 1000;
            but2(T);
        }
    }
      

       
    }
}
