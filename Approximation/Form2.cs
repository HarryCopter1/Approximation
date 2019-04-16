using Approximation.Regression;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Approximation
{
    public partial class Form2 : Form
    {
        private List<Graph> approx;

        public Form2(List<Graph> approx)
        {
            InitializeComponent();
            this.approx = approx;
            createDetails(approx);
        }

        void createDetails(List<Graph> approx)
        {
            for (int i = 0; i< approx.Count;i++)
            {
                Label label1 = new Label();
                GroupBox groupBox1 = new GroupBox();
                groupBox1.Controls.Add(label1);
                groupBox1.Location = new System.Drawing.Point(12, 100*i+28);
                groupBox1.Size = new System.Drawing.Size(800, 100);
                groupBox1.Text = approx[i].getName();


                label1.AutoSize = true;
                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                label1.Location = new System.Drawing.Point(12, 100 * i + 50);
                label1.Size = new System.Drawing.Size(86, 31);
                label1.Text = "Error = " + approx[i].getRelativeError().ToString();

                Controls.Add(label1);
                Controls.Add(groupBox1);
            }
        }
    }
}
