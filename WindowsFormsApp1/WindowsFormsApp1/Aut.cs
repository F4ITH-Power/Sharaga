using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Aut : Form
    {

        public Label errorLabel = new Label();

        public Aut()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string path = "C:/Users/Doter/OneDrive/Desktop/dataBase.txt";

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int index = line.IndexOf(' ');
                    if (index != -1)
                    {
                        string loginInFile = line.Substring(0, index);
                        if (loginInFile.Equals(login))
                        {
                            string passwordInFile = line.Substring(index + 1);
                            if (passwordInFile.Equals(password))
                            {
                                errorLabel.Text = "YES";
                                errorLabel.AutoSize = false;
                                errorLabel.Height = 30;
                                errorLabel.Width = 130;
                                errorLabel.Font = new Font("Arial", 7, FontStyle.Bold);
                                errorLabel.Location = new Point(150, 25);
                                errorLabel.ForeColor = Color.Green;
                                Controls.Add(errorLabel);
                                return;
                            }
                        }
                    }
                }
                errorLabel.Text = "NO";
                errorLabel.AutoSize = false;
                errorLabel.Height = 30;
                errorLabel.Width = 130;
                errorLabel.Font = new Font("Arial", 7, FontStyle.Bold);
                errorLabel.Location = new Point(150, 25);
                errorLabel.ForeColor = Color.Green;
                Controls.Add(errorLabel);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}