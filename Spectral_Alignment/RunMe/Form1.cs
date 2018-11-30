using System;
using System.IO;
using System.Windows.Forms;


namespace RunMe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var datafile = textBox1.Text;
            var database = textBox2.Text;
            var fragmentTolerance = Convert.ToDouble(textBox3.Text);
            var mwTolerance = Convert.ToDouble(textBox4.Text);
            var maximumNoOfModifications = Convert.ToInt32(textBox5.Text);
            Close();
            Spectral_Alignment.Program.RunTheCode(datafile, database, fragmentTolerance, mwTolerance, maximumNoOfModifications);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var file = "";
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "MS Data file (*.txt) | *.txt";
            fileDialog.ShowDialog();
            if (fileDialog.CheckFileExists)
            {
                file = fileDialog.FileName;
            }
            textBox1.Text = file;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var file = "";
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Database file (*.fasta) | *.fasta";
            fileDialog.ShowDialog();
            if (fileDialog.CheckFileExists)
            {
                file = fileDialog.FileName;
            }
            textBox2.Text = file;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            var t = new ToolTip();
            var o = (Control)sender;
            t.InitialDelay = 0;
            t.Show("Load MS Database File in Text Format", o, 1000);
            
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            var t = new ToolTip();
            var o = (Control)sender;
            t.InitialDelay = 0;
            t.Show("Load Database File in Fasta Format", o, 1000);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            var t = new ToolTip();
            var o = (Control)sender;
            t.InitialDelay = 0;
            t.Show("Perform Spectral Alignment", o, 1000);
        }

        private void textBox4_MouseEnter(object sender, EventArgs e)
        {
            var t = new ToolTip();
            var o = (Control)sender;
            t.InitialDelay = 0;
            t.Show("Please Enter Fragment Tolerance for Spectral Alignment", o, 1000);
        }

        private void textBox3_MouseEnter(object sender, EventArgs e)
        {
            var t = new ToolTip();
            var o = (Control)sender;
            t.InitialDelay = 0;
            t.Show("Please Select MW Tolerance for Filtering Protein DB", o, 1000);
        }

        private void textBox5_MouseEnter(object sender, EventArgs e)
        {
            var t = new ToolTip();
            var o = (Control)sender;
            t.InitialDelay = 0;
            t.Show("Please Enter Maximum No of Allowed Mass Shifts in Spectral Alignment", o, 1000);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
