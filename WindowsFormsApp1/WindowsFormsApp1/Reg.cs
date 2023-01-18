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

namespace WindowsFormsApp1
{
    public partial class Reg : Form
    {

        public Label errorLabel = new Label();

        public Reg()
        {
            InitializeComponent();
            
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ControlBox = false;
            errorLabel.Text = "Enter a username";
            errorLabel.AutoSize = false;
            errorLabel.Height = 30;
            errorLabel.Width = 130;
            errorLabel.Font = new Font("Arial", 7, FontStyle.Bold);
            errorLabel.Location = new Point(150, 30);
            errorLabel.ForeColor = Color.Green;
            Controls.Add(errorLabel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;

            if (string.IsNullOrEmpty(username))
            {
                errorLabel.Text = "Username is empty";
                errorLabel.ForeColor = Color.Red;
                return;
            }

            string password = textBox2.Text;

            Thread myThread = new Thread(MyMethod);

            void MyMethod()
            {
                while (string.IsNullOrEmpty(password))
                {
                    errorLabel.Text = "Enter a password";
                    errorLabel.ForeColor = Color.Green;
                    break;
                }
            }

            myThread.Start();

            if (!password.Any(char.IsDigit) || !password.Any(char.IsPunctuation) || !password.Any(char.IsSymbol))
            {
                errorLabel.Text = "Password must contain at least one digit, one punctuation mark, and one mathematical symbol.";
                errorLabel.ForeColor = Color.Red;
                return;
            }
            errorLabel.Text = "Password is good";
            errorLabel.ForeColor = Color.Green;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}