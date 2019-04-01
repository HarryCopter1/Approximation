using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Approximation.Regression;
using OxyPlot;
using OxyPlot.Series;

namespace Approximation
{
    public partial class Form1 : Form
    {
        PlotModel myModel;
        public Form1()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myModel = new PlotModel();
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            dataGridView1.AllowUserToAddRows = false; //removes last(Extra) row
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                x.Add(Convert.ToInt32(row.Cells[0].Value));
                y.Add(Convert.ToInt32(row.Cells[1].Value));
            }
            dataGridView1.AllowUserToAddRows = true;
            
            Graph graph = new Graph(x, y);

            /* label1.Text = linear.getRelativeError().ToString();
             label2.Text = linear.getR().ToString();
             label3.Text = linear.getDet().ToString();*/

            Quadratic quadratic = new Quadratic(x, y);

            label1.Text = quadratic.a.ToString();
            label2.Text = quadratic.b.ToString();
            label3.Text = quadratic.getR().ToString();
            label4.Text = quadratic.getDet().ToString();
            label5.Text = quadratic.getRelativeError().ToString();



            // myModel = linear.getModel();

            //   myModel = expo.getModel();
            // myModel = power.getModel();
            myModel = graph.getModel();

            this.plot1.Model = myModel;
        }

     /*   public static PlotModel operator + (Linear op1, Power op2)
        {
            PlotModel p;
            return p;
        }
        */
        private void button2_Click(object sender, EventArgs e)
        {
            //linear = null;
            //this.plot1.Model = null;
            myModel.Series.Clear();
            foreach (var axis in myModel.Axes)
                axis.Reset();
            myModel.ResetAllAxes();
            myModel.InvalidatePlot(true);
            
            //plot1.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
