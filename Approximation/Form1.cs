using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;

namespace Approximation
{
    public partial class Form1 : Form
    {
        PlotModel myModel = new PlotModel();
        public Form1()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> x = new List<int>();
            List<int> y = new List<int>();
            dataGridView1.AllowUserToAddRows = false; //removes last(Extra) row
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                x.Add(Convert.ToInt32(row.Cells[0].Value));
                y.Add(Convert.ToInt32(row.Cells[1].Value));
            }
            dataGridView1.AllowUserToAddRows = true;

            double a;
            double b;
            double yf;
            double part1 = sum(x) * sum(y) - x.Count * sum(x, y);
            double part2 = Math.Pow(sum(x), 2) - x.Count * sumPow(x);
            a = part1 / part2;



            part1 = sum(x) * sum(x, y) - sumPow(x) * sum(y);
            part2 = Math.Pow(sum(x), 2) - x.Count * sumPow(x);
            b = part1 / part2;
            
            label1.Text = a.ToString();
            label2.Text = b.ToString();
            //   label1.Text = y.ToString();


            myModel = Linear.setModel(x, y, a, b);
            this.plot1.Model = myModel;
        }
                       
        private double sum(List<int> arr)
        {
            return arr.Sum();
        }

        private double sum(List<int> arr1, List<int> arr2)
        {
            int sum = 0;
            for (int i = 0; i < arr1.Count; i++)
            {
                sum += arr1[i] * arr2[i];
            }
            return sum;
        }

        private double sumPow(List<int> arr)
        {
            double sum = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                sum += Math.Pow(arr[i], 2);
            };
            return sum;
        }
    }
}
