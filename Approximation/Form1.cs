using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Approximation.Regression;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

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

        //When cell is empty set it value to 0
        private void WhenCellIsEmptySetToZero()
        {
            dataGridView1.AllowUserToAddRows = false; //removes last(Extra) row
            foreach (DataGridViewRow row in dataGridView1.Rows) 
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (string.IsNullOrEmpty(row.Cells[i].Value as string))
                    {
                        row.Cells[i].Value = 0;
                    }
                }
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            Graph.graphList.Clear();
            if ((dataGridView1.Rows.Count != 1))
            {
                WhenCellIsEmptySetToZero();

                clearPlot();
                myModel = new PlotModel();


                //Reads data from gridview
                x.Clear();
                y.Clear();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    x.Add(Convert.ToInt32(row.Cells[0].Value));
                    y.Add(Convert.ToInt32(row.Cells[1].Value));
                }
                dataGridView1.AllowUserToAddRows = true;


                // Prevent memory loss
                if (x.Max() - x.Min() < 1000 &&
                        y.Max() - y.Min() < 1000)
                {
                    Graph graph = new Graph(x, y);
                    
                    myModel = graph.getModel(checkBoxes());

                    this.plot1.Model = myModel;
                }
            }
        }

        //Gets selected methods of approximation
        List<FunctionSeries> checkBoxes()
        {
            List<FunctionSeries> functionList = new List<FunctionSeries>();

            if (checkBox1.Checked)
            {
                functionList.RemoveAll(o => o.Title == "abexpo");
                abExponential abexpo = new abExponential(x, y);
                Graph.graphList.Add(abexpo);
                functionList.Add(new FunctionSeries(abexpo.function, x.Min(), x.Max(), 0.0001, "abexpo"));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == "abexpo");
            }

            if (checkBox2.Checked)
            {
                functionList.RemoveAll(o => o.Title == "cubic");
                Cubic cubic = new Cubic(x, y);
                Graph.graphList.Add(cubic);
                functionList.Add(new FunctionSeries(cubic.function, x.Min(), x.Max(), 0.0001, "cubic"));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == "cubic");
            }

            if (checkBox3.Checked)
            {
                functionList.RemoveAll(o => o.Title == "exponential");
                Exponential exponential = new Exponential(x, y);
                Graph.graphList.Add(exponential);
                functionList.Add(new FunctionSeries(exponential.function, x.Min(), x.Max(), 0.0001, "exponential"));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == "exponential");
            }

            if (checkBox4.Checked)
            {
                functionList.RemoveAll(o => o.Title == "hyperbolic");
                Hyperbolic hyperbolic = new Hyperbolic(x, y);
                Graph.graphList.Add(hyperbolic);
                functionList.Add(new FunctionSeries(hyperbolic.function, x.Min(), x.Max(), 0.0001, "hyperbolic"));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == "hyperbolic");
            }

            if (checkBox5.Checked)
            {
                functionList.RemoveAll(o => o.Title == "linear");
                Linear linear = new Linear(x, y);
                Graph.graphList.Add(linear);
                functionList.Add(new FunctionSeries(linear.function, x.Min(), x.Max(), 0.0001, "linear"));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == "linear");
            }

            if (checkBox6.Checked)
            {
                functionList.RemoveAll(o => o.Title == "logarithmic");
                Logarithmic logarithmic = new Logarithmic(x, y);
                Graph.graphList.Add(logarithmic);
                functionList.Add(new FunctionSeries(logarithmic.function, x.Min(), x.Max(), 0.0001, "logarithmic"));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == "logarithmic");
            }

            if (checkBox7.Checked)
            {
                functionList.RemoveAll(o => o.Title == "power");
                Power power = new Power(x, y);
                Graph.graphList.Add(power);
                functionList.Add(new FunctionSeries(power.function, x.Min(), x.Max(), 0.0001, "power"));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == "power");
            }

            if (checkBox8.Checked)
            {
                functionList.RemoveAll(o => o.Title == "quadratic");
                Quadratic quadratic = new Quadratic(x, y);
                Graph.graphList.Add(quadratic);
                functionList.Add(new FunctionSeries(quadratic.function, x.Min(), x.Max(), 0.0001, "quadratic"));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == "quadratic");
            }
            return functionList;
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


        /*
         * Save graph as png file
         */
        private void pngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //desktop path
            saveFile.InitialDirectory = desktop;
            saveFile.Filter = "Image Files(*.png) |*.png;";
            saveFile.Title = "Save an image";
            saveFile.FileName = "graph";
            saveFile.AddExtension = true;
            saveFile.DefaultExt = "png";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                var pngExporter = new PngExporter { Width = 600, Height = 400, Background = OxyColors.White };
                pngExporter.ExportToFile(myModel, saveFile.FileName);
            }
        }

        /*
         * Save graph as pdf file
         */
        private void pdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //desktop path
            saveFile.InitialDirectory = desktop;
            saveFile.Filter = "Pdf Files(*.pdf) |*.pdf;";
            saveFile.Title = "Save an image";
            saveFile.FileName = "graph";
            saveFile.AddExtension = true;
            saveFile.DefaultExt = "pdf";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400, Background = OxyColors.White };
                pdfExporter.ExportToFile(myModel, saveFile.FileName);
            }
        }

        /*
         * Save graph as svg file
         */
        private void svgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //desktop path
            saveFile.InitialDirectory = desktop;
            saveFile.Filter = "Svg Files(*.svg) |*.svg;";
            saveFile.Title = "Save an image";
            saveFile.FileName = "graph";
            saveFile.AddExtension = true;
            saveFile.DefaultExt = "svg";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                var svgExporter = new OxyPlot.WindowsForms.SvgExporter { Width = 600, Height = 400 };
                svgExporter.ExportToFile(myModel, saveFile.FileName);
            }
        }
                
        //Prevent entering non-numeric values to gridview
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress +=
      new KeyPressEventHandler(Control_KeyPress);
            }
            catch (Exception)
            {
            }
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
     && !char.IsDigit(e.KeyChar)
     && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool isShown = false;

            foreach (Form frm in fc)
            {
                if (frm.Name == "Form2")
                    isShown = true;
            }

            fc = null;

            if (isShown == false)
            {
                var myForm = new Form2(Graph.graphList);
                myForm.Show();
            }

            label1.Text = "a" + (char)0X02E3 + " + " + "a" + (char)0x1D47;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }    

}
