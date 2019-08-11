using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Approximation.Properties;
using Approximation.Regression;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace Approximation
{
    public partial class MainForm : Form
    {
        PlotModel myModel;
        public static ResourceManager rm;
        public static int rmDigits;

        List<double> x = new List<double>();
        List<double> y = new List<double>();


        public MainForm()
        {
            this.InitializeComponent();
            myModel = new PlotModel();
          
            rm = new ResourceManager("Approximation.Properties." + Settings.Default["Language"], Assembly.GetExecutingAssembly());
            rmDigits = Convert.ToInt32(Settings.Default["Digits"]);

            checkBox1.Text = rm.GetString("Linear");
            checkBox2.Text = rm.GetString("Quadratic");
            checkBox3.Text = rm.GetString("Cubic");
            checkBox4.Text = rm.GetString("Power");
            checkBox5.Text = rm.GetString("abExponential");
            checkBox6.Text = rm.GetString("Logarithmic");
            checkBox7.Text = rm.GetString("Hyperbolic");
            checkBox8.Text = rm.GetString("Exponential");

            button1.Text = rm.GetString("Calculate");
            button2.Text = rm.GetString("ClearGraph");
            button3.Text = rm.GetString("ClearInput");
            button4.Text = rm.GetString("ShowInf");

            groupBox1.Text = rm.GetString("InputData");
            groupBox2.Text = rm.GetString("SelectFunction");

            fileToolStripMenuItem.Text = rm.GetString("File");
            settingsToolStripMenuItem.Text = rm.GetString("Settings");
            helpToolStripMenuItem.Text = rm.GetString("Help");
            exportToolStripMenuItem.Text = rm.GetString("Export");
            aboutToolStripMenuItem1.Text = rm.GetString("About");
            exitToolStripMenuItem.Text = rm.GetString("Exit");

        }

        //Коли клітинка пуста, вона заповнюється нулем
        private void WhenCellIsEmptySetToZero()
        {
            dataGridView1.AllowUserToAddRows = false; //видаляє останній рядок
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
       
        //кнопка для малювання графіку
        private void button1_Click(object sender, EventArgs e)
        {
            Graph.graphList.Clear();
            if ((dataGridView1.Rows.Count != 1))
            {
                WhenCellIsEmptySetToZero();

                clearPlot();
                myModel = new PlotModel();


                //Заповнює дані з таблиці
                x.Clear();
                y.Clear();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    x.Add(Convert.ToDouble(row.Cells[0].Value));
                    y.Add(Convert.ToDouble(row.Cells[1].Value));
                }
                dataGridView1.AllowUserToAddRows = true;

                
                    Graph graph = new Graph(x, y);
                    
                    myModel = graph.getModel(checkBoxes());

                    this.plot1.Model = myModel;
            }
        }

        //Отримає вибрані типи апроксимації
        List<FunctionSeries> checkBoxes()
        {
            List<FunctionSeries> functionList = new List<FunctionSeries>();


            if (checkBox1.Checked)
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Linear"));
                Linear linear = new Linear(x, y);
                Graph.graphList.Add(linear);
                functionList.Add(new FunctionSeries(linear.function, x.Min(), x.Max(), 0.0001, rm.GetString("Linear")));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Linear"));
            }

            if (checkBox2.Checked)
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Quadratic"));
                Quadratic quadratic = new Quadratic(x, y);
                Graph.graphList.Add(quadratic);
                functionList.Add(new FunctionSeries(quadratic.function, x.Min(), x.Max(), 0.0001, rm.GetString("Quadratic")));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Quadratic"));
            }

            if (checkBox3.Checked)
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Cubic"));
                Cubic cubic = new Cubic(x, y);
                Graph.graphList.Add(cubic);
                functionList.Add(new FunctionSeries(cubic.function, x.Min(), x.Max(), 0.0001, rm.GetString("Cubic")));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Cubic"));
            }


            if (checkBox4.Checked)
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Power"));
                Power power = new Power(x, y);
                Graph.graphList.Add(power);
                functionList.Add(new FunctionSeries(power.function, x.Min(), x.Max(), 0.0001, rm.GetString("Power")));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Power"));
            }

            if (checkBox5.Checked)
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("abExponential"));
                abExponential abexpo = new abExponential(x, y);
                Graph.graphList.Add(abexpo);
                functionList.Add(new FunctionSeries(abexpo.function, x.Min(), x.Max(), 0.0001, rm.GetString("abExponential")));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("abExponential"));
            }

            if (checkBox6.Checked)
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Logarithmic"));
                Logarithmic logarithmic = new Logarithmic(x, y);
                Graph.graphList.Add(logarithmic);
                functionList.Add(new FunctionSeries(logarithmic.function, x.Min(), x.Max(), 0.0001, rm.GetString("Logarithmic")));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Logarithmic"));
            }

            if (checkBox7.Checked)
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Hyperbolic"));
                Hyperbolic hyperbolic = new Hyperbolic(x, y);
                Graph.graphList.Add(hyperbolic);
                functionList.Add(new FunctionSeries(hyperbolic.function, x.Min(), x.Max(), 0.0001, rm.GetString("Hyperbolic")));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Hyperbolic"));
            }

            if (checkBox8.Checked)
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Exponential"));
                Exponential exponential = new Exponential(x, y);
                Graph.graphList.Add(exponential);
                functionList.Add(new FunctionSeries(exponential.function, x.Min(), x.Max(), 0.0001, rm.GetString("Exponential")));
            }
            else
            {
                functionList.RemoveAll(o => o.Title == rm.GetString("Exponential"));
            }


            return functionList;
        }

        //Кнопка для очищення графіка
        private void button2_Click(object sender, EventArgs e)
        {
            clearPlot();
        }

        //Очищення графіка
        private void clearPlot()
        {
            myModel.Title = "";
            myModel.Series.Clear();
            foreach (var axis in myModel.Axes)
                axis.Reset();
            myModel.ResetAllAxes();
            myModel.InvalidatePlot(true);
            
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        
        //Зберігає графік в форматі png        
        private void pngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if there is no graph drawn show error message
            if ((Graph.graphList != null) && (!Graph.graphList.Any()))
            {
                string message = rm.GetString("NoGraph");
                string title = rm.GetString("Error");
                MessageBox.Show(message, title);
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //desktop path
                saveFile.InitialDirectory = desktop;
                saveFile.Filter = "Image Files(*.png) |*.png;";
                saveFile.Title = rm.GetString("SaveImg");
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
        }


        //Зберігає графік в форматі pdf
        private void pdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if there is no graph drawn show error message
            if ((Graph.graphList != null) && (!Graph.graphList.Any()))
            {
                string message = rm.GetString("NoGraph");
                string title = rm.GetString("Error");
                MessageBox.Show(message, title);
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //desktop path
                saveFile.InitialDirectory = desktop;
                saveFile.Filter = "Pdf Files(*.pdf) |*.pdf;";
                saveFile.Title = rm.GetString("SaveImg");
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
        }


        //Зберігає графік в форматі svg
        private void svgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if there is no graph drawn show error message
            if ((Graph.graphList != null) && (!Graph.graphList.Any()))
            {
                string message = rm.GetString("NoGraph");
                string title = rm.GetString("Error");
                MessageBox.Show(message, title);
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //desktop path
                saveFile.InitialDirectory = desktop;
                saveFile.Filter = "Svg Files(*.svg) |*.svg;";
                saveFile.Title = rm.GetString("SaveImg");
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
        }
                
        //Забороняє вводити в таблицю щось крім цифр
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = e.Control as TextBox;
            tb.ShortcutsEnabled = false;
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
     && e.KeyChar != ',')
            {
                e.Handled = true;
            }
            
            if (e.KeyChar == ','
                && (sender as TextBox).Text.IndexOf(',') > -1)
            {
                e.Handled = true;
            }
        }

        //Кнопка виходу
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        //Кнопка додаткової інформації
        private void button4_Click(object sender, EventArgs e)
        {
            //if there is no graph drawn show error message
            if ((Graph.graphList != null) && (!Graph.graphList.Any()))
            {
                string message = rm.GetString("NoGraph");
                string title = rm.GetString("Error");
                MessageBox.Show(message, title);
            }
            else //create form
            {
                FormCollection fc = Application.OpenForms;
                bool isShown = false;

                //search for form named Form2
                foreach (Form frm in fc)
                {
                    if (frm.Name == "Form2")
                        isShown = true;
                }

                fc = null;

                //if form is not already opened
                if (isShown == false)
                {
                    var myForm = new InfForm(Graph.graphList); //create form
                    myForm.Show();
                }
            }
        }

        //Settings button
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            FormCollection fc = Application.OpenForms;
            bool isShown = false;

            //search for form named Form3
            foreach (Form frm in fc)
            {
                if (frm.Name == "Form3")
                    isShown = true;
            }

            fc = null;

            //if form is not already opened
            if (isShown == false)
            {
                var myForm = new SettingsForm();
                myForm.Show(); //create form
            }
        }

        
        //Кнопка про програму
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string message = rm.GetString("AboutText");
            string title = rm.GetString("About");
            MessageBox.Show(message, title);
        }
    }    

}
