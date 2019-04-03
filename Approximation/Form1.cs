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

            checkBoxes();

            myModel = graph.getModel();

            this.plot1.Model = myModel;
        }

        void checkBoxes()
        {
            if (checkBox1.Checked)
            {
                Graph.functionList.RemoveAll(o => o.Title == "abexpo");
                abExponential abexpo = new abExponential(x, y);
                Graph.functionList.Add(new FunctionSeries(abexpo.function, x.Min(), x.Max(), 0.0001, "abexpo"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "abexpo");
            }

            if (checkBox2.Checked)
            {
                Graph.functionList.RemoveAll(o => o.Title == "cubic");
                Cubic cubic = new Cubic(x, y);
                Graph.functionList.Add(new FunctionSeries(cubic.function, x.Min(), x.Max(), 0.0001, "cubic"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "cubic");
            }

            if (checkBox3.Checked)
            {
                Graph.functionList.RemoveAll(o => o.Title == "exponential");
                Exponential exponential = new Exponential(x, y);
                Graph.functionList.Add(new FunctionSeries(exponential.function, x.Min(), x.Max(), 0.0001, "exponential"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "exponential");
            }

            if (checkBox4.Checked)
            {
                Graph.functionList.RemoveAll(o => o.Title == "hyperbolic");
                Hyperbolic hyperbolic = new Hyperbolic(x, y);
                Graph.functionList.Add(new FunctionSeries(hyperbolic.function, x.Min(), x.Max(), 0.0001, "hyperbolic"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "hyperbolic");
            }

            if (checkBox5.Checked)
            {
                Graph.functionList.RemoveAll(o => o.Title == "linear");
                Linear linear = new Linear(x, y);
                Graph.functionList.Add(new FunctionSeries(linear.function, x.Min(), x.Max(), 0.0001, "linear"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "linear");
            }

            if (checkBox6.Checked)
            {
                Graph.functionList.RemoveAll(o => o.Title == "logarithmic");
                Logarithmic logarithmic = new Logarithmic(x, y);
                Graph.functionList.Add(new FunctionSeries(logarithmic.function, x.Min(), x.Max(), 0.0001, "logarithmic"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "logarithmic");
            }

            if (checkBox7.Checked)
            {
                Graph.functionList.RemoveAll(o => o.Title == "power");
                Power power = new Power(x, y);
                Graph.functionList.Add(new FunctionSeries(power.function, x.Min(), x.Max(), 0.0001, "power"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "power");
            }

            if (checkBox8.Checked)
            {
                Graph.functionList.RemoveAll(o => o.Title == "quadratic");
                Quadratic quadratic = new Quadratic(x, y);
                Graph.functionList.Add(new FunctionSeries(quadratic.function, x.Min(), x.Max(), 0.0001, "quadratic"));
            }
            else
            {
                Graph.functionList.RemoveAll(o => o.Title == "quadratic");
            }

        }

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
    }
}
