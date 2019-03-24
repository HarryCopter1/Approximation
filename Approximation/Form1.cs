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
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            dataGridView1.AllowUserToAddRows = false; //removes last(Extra) row
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                x.Add(Convert.ToInt32(row.Cells[0].Value));
                y.Add(Convert.ToInt32(row.Cells[1].Value));
            }
            dataGridView1.AllowUserToAddRows = true;

            Linear linear = new Linear(x, y);

            label1.Text = linear.getRelativeError().ToString();
            label2.Text = linear.getR().ToString();
            label3.Text = linear.getDet().ToString();

            /*  label1.Text = linear.a.ToString();
              label2.Text = linear.b.ToString();*/

            myModel = linear.setModel();

            this.plot1.Model = myModel;
        }


    }
}
