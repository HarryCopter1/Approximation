using Approximation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace Approximation
{
    public partial class Form3 : Form
    {
        public static ResourceManager rm = new ResourceManager("Approximation.Properties." + Settings.Default["Language"], Assembly.GetExecutingAssembly());

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = rm.GetString("DigitsText");
            label3.Text = rm.GetString("Language");

            label2.Text = Settings.Default["Digits"].ToString();
            trackBar1.Value = Convert.ToInt32(Settings.Default["Digits"]);
            this.TopMost = true;
            
            if (Settings.Default["Language"].ToString() == "ua")
                comboBox1.SelectedIndex = 0;
            if (Settings.Default["Language"].ToString() == "en")
                comboBox1.SelectedIndex = 1;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default["Digits"] = trackBar1.Value;
            if (comboBox1.SelectedIndex == 0)
                Settings.Default["Language"] = "ua";
            if (comboBox1.SelectedIndex == 1)
                Settings.Default["Language"] = "en";
            Settings.Default.Save();
            string message = "Restart program to accept changes";
            string title = "Saved";
            MessageBox.Show(message, title);
            this.Close();
        }
    }
}
