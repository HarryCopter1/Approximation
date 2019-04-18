using Approximation.Properties;
using Approximation.Regression;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Approximation
{
    public partial class Form2 : Form
    {
        private List<Graph> approx;
        public static ResourceManager rm = new ResourceManager("Approximation.Properties." + Settings.Default["Language"], Assembly.GetExecutingAssembly());

        public Form2(List<Graph> approx)
        {
            InitializeComponent();
            this.approx = approx;
            createDetails(approx);
            this.Size = new System.Drawing.Size(835, 60+80*this.approx.Count);
        }

        /*
         * Закінчити переклад
         * добавити переклад вікон
         * добавити переклад вікна збереження
         * відображення графіків якесь хєрове, не помішається на компонент
         *  
         */ 

        void createDetails(List<Graph> approx)
        {
            int digits = Convert.ToInt32(Settings.Default["Digits"]);
            for (int i = 0; i< approx.Count;i++)
            {
                Label label1 = new Label();
                Label label2 = new Label();
                Label label3 = new Label();
                Label label4 = new Label();
                Label labela = new Label();
                Label labelb = new Label();
                Label labelc = new Label();
                Label labeld = new Label();
                GroupBox groupBox1 = new GroupBox();
                groupBox1.Controls.Add(label1);
                groupBox1.Controls.Add(label2);
                groupBox1.Controls.Add(label3);
                groupBox1.Controls.Add(label4);
                groupBox1.Location = new System.Drawing.Point(12, 80*i+10);
                groupBox1.Size = new System.Drawing.Size(800, 80);
                groupBox1.Text = approx[i].getName();

                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                label1.Location = new System.Drawing.Point(17, 80 * i + 25);
                //label1.Size = new System.Drawing.Size(220, 55);
                label1.Size = new System.Drawing.Size(185, 55);
                label1.Text = approx[i].getFuncText().ToString();

                label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                label2.Location = new System.Drawing.Point(400, 80 * i + 25);
                label2.Size = new System.Drawing.Size(400, 18);
                label2.Text = rm.GetString("Determination") + " = " + Math.Round(approx[i].getDet(),digits).ToString();

                label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                label3.Location = new System.Drawing.Point(400, 80 * i + 42);
                label3.Size = new System.Drawing.Size(400, 18);
                label3.Text = rm.GetString("Error") + " = " + Math.Round(approx[i].getRelativeError(),digits).ToString() + " %";

                label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                label4.Location = new System.Drawing.Point(400, 80 * i + 59);
                label4.Size = new System.Drawing.Size(400, 18);
                label4.Text = rm.GetString("Correlation") + " = " + Math.Round(approx[i].getR(),digits).ToString();

                labela.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                labela.Location = new System.Drawing.Point(200, 80 * i + 20);
                labela.Size = new System.Drawing.Size(400, 18);
                labela.Text = "a = " + Math.Round(approx[i].getA(),digits).ToString();

                labelb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                labelb.Location = new System.Drawing.Point(200, 80 * i + 37);
                labelb.Size = new System.Drawing.Size(400, 18);
                labelb.Text = "b = " + Math.Round(approx[i].getB(),digits).ToString();

                Controls.Add(label1);
                Controls.Add(label2);
                Controls.Add(label3);
                Controls.Add(label4);
                Controls.Add(labela);
                Controls.Add(labelb);

                if (approx[i].getC() != 1/0F)
                {
                    labelc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    labelc.Location = new System.Drawing.Point(200, 80 * i + 54);
                    labelc.Size = new System.Drawing.Size(400, 16);
                    labelc.Text = "c = " + Math.Round(approx[i].getC(),digits).ToString();
                    Controls.Add(labelc);
                }

                if (approx[i].getD() != 1 / 0F)
                {
                    labeld.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    labeld.Location = new System.Drawing.Point(200, 80 * i + 71);
                    labeld.Size = new System.Drawing.Size(400, 16);
                    labeld.Text = "d = " + Math.Round(approx[i].getD(),digits).ToString();
                    Controls.Add(labeld);
                }

                Controls.Add(groupBox1);


            }
        }
    }
}
