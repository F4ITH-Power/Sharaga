using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Reg : Form
    {
        public Label errorLabel = new Label();
        public Reg()
        {
            InitializeComponent();

            textBox3.Focus();
            textBox3.SelectionStart = 0;
            textBox3.SelectionLength = 0;
            textBox3.Select();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ControlBox = false;
            errorLabel.Text = "Enter a username";
            errorLabel.AutoSize = false;
            errorLabel.Height = 30;
            errorLabel.Width = 130;
            errorLabel.Font = new Font("Arial", 7, FontStyle.Bold);
            errorLabel.Location = new Point(150, 25);
            errorLabel.ForeColor = Color.Green;
            Controls.Add(errorLabel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox3.Text;

            if (string.IsNullOrEmpty(username))
            {
                errorLabel.Text = "Username is empty";
                errorLabel.ForeColor = Color.Red;
                return;
            }

            string password = textBox1.Text;

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

            string confirmPassword = textBox2.Text;

            if (confirmPassword != password)
            {
                errorLabel.Text = "Password not confirmed";
            }
            else
            {
                errorLabel.Text = "Password is confirmed";
                using (StreamWriter writer = new StreamWriter("C:/Users/Doter/OneDrive/Desktop/dataBase.txt", true))
                {
                    writer.Write(username + " " + confirmPassword);
                    writer.WriteLine();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Aut aut = new Aut();
            aut.Show();
        }
    }
}
