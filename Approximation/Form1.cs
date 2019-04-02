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

        List<double> x = new List<double>();
        List<double> y = new List<double>();

        public Form1()
        {
            this.InitializeComponent();
            myModel = new PlotModel();
        }


        //TODO FIX UPDATE CHECBOX

        private void button1_Click(object sender, EventArgs e)
        {
            clearPlot();
            myModel = new PlotModel();
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
            
            label1.Text = Math.Round(quadratic.a, 4).ToString();
            label2.Text = Math.Round(quadratic.b, 4).ToString();
            label3.Text = Math.Round(quadratic.c, 4).ToString();
            label4.Text = Math.Round(quadratic.d, 4).ToString();
            label5.Text = Math.Round(quadratic.r, 4).ToString();
            label6.Text = Math.Round(quadratic.det, 4).ToString();
            label7.Text = Math.Round(quadratic.err, 4).ToString();



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
            clearPlot();
        }

        private void clearPlot()
        {
            //linear = null;
            //this.plot1.Model = null;
            myModel.Title = "";
            myModel.Series.Clear();
            foreach (var axis in myModel.Axes)
                axis.Reset();
            myModel.ResetAllAxes();
            myModel.InvalidatePlot(true);

            //plot1.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                abExponential abexpo = new abExponential(x, y);
                Graph.functionList.Add(new FunctionSeries(abexpo.function, x.Min(), x.Max(), 0.0001, "abexpo"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "abexpo");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Cubic cubic = new Cubic(x, y);
                Graph.functionList.Add(new FunctionSeries(cubic.function, x.Min(), x.Max(), 0.0001, "cubic"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "cubic");
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                Exponential exponential = new Exponential(x, y);
                Graph.functionList.Add(new FunctionSeries(exponential.function, x.Min(), x.Max(), 0.0001, "exponential"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "exponential");
            }         
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                Hyperbolic hyperbolic = new Hyperbolic(x, y);
                Graph.functionList.Add(new FunctionSeries(hyperbolic.function, x.Min(), x.Max(), 0.0001, "hyperbolic"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "hyperbolic");
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                Linear linear = new Linear(x, y);
                Graph.functionList.Add(new FunctionSeries(linear.function, x.Min(), x.Max(), 0.0001, "linear"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "linear");
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                Logarithmic logarithmic = new Logarithmic(x, y);
                Graph.functionList.Add(new FunctionSeries(logarithmic.function, x.Min(), x.Max(), 0.0001, "logarithmic"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "logarithmic");
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                Power power = new Power(x, y);
                Graph.functionList.Add(new FunctionSeries(power.function, x.Min(), x.Max(), 0.0001, "power"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "power");
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                Quadratic quadratic = new Quadratic(x, y);
                Graph.functionList.Add(new FunctionSeries(quadratic.function, x.Min(), x.Max(), 0.0001, "quadratic"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "quadratic");
            }
        }
    }
}
