using System;
using System.Windows.Forms;

namespace RunMe
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (GlobalR.Checked == true)
            {
                var seq2 = textBox1.Text;
                var seq1 = textBox2.Text;
                var matchWeight = Convert.ToInt32(textBox3.Text);
                var misMatchWeight = Convert.ToInt32(textBox4.Text);
                var indlWeight = Convert.ToInt32(textBox5.Text);
                Close();
                GlobalAlignment.Program.RunTheCode(seq1, seq2, matchWeight, misMatchWeight, indlWeight);
            }

            if (LocalR.Checked == true)
            {
                var seq2 = textBox1.Text;
                var seq1 = textBox2.Text;
                var matchWeight = Convert.ToInt32(textBox3.Text);
                var misMatchWeight = Convert.ToInt32(textBox4.Text);
                var indlWeight = Convert.ToInt32(textBox5.Text);
                Close();
                LocalAlignment.Program.RunTheCode(seq1, seq2, matchWeight, misMatchWeight, indlWeight);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var file = "";
            var fileDialog = new OpenFileDialog
            {
                Filter = "Database file (*.fasta) | *.fasta"
            };
            fileDialog.ShowDialog();
            if (fileDialog.CheckFileExists)
            {
                file = fileDialog.FileName;
            }
            textBox2.Text = file;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var file = "";
            var fileDialog = new OpenFileDialog
            {
                Filter = "Database file (*.fasta) | *.fasta"
            };
            fileDialog.ShowDialog();
            if (fileDialog.CheckFileExists)
            {
                file = fileDialog.FileName;
            }
            textBox1.Text = file;
        }

    }
}
