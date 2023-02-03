using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace WindowsFormsApp1
{
    public partial class Reg : Form
    {
        public Label errorLabel = new Label();
        static string Encrypt(string plaintext, string key1, string key2)
        {
            // Permutation encryption
            char[] permutedText = new char[plaintext.Length];
            for (int i = 0; i < plaintext.Length; i++)
            {
                permutedText[i] = plaintext[(i + key1.Length) % plaintext.Length];
            }

            // Gamma encryption
            char[] gammaEncryptedText = new char[permutedText.Length];
            for (int i = 0; i < permutedText.Length; i++)
            {
                gammaEncryptedText[i] = (char)(permutedText[i] ^ key2[i % key2.Length]);
            }

            return new string(gammaEncryptedText);
        }

        static string Decrypt(string ciphertext, string key1, string key2)
        {
            // Reverse gamma encryption
            char[] gammaDecryptedText = new char[ciphertext.Length];
            for (int i = 0; i < ciphertext.Length; i++)
            {
                gammaDecryptedText[i] = (char)(ciphertext[i] ^ key2[i % key2.Length]);
            }

            // Reverse permutation encryption
            char[] permutedText = new char[gammaDecryptedText.Length];
            for (int i = 0; i < gammaDecryptedText.Length; i++)
            {
                permutedText[(i + key1.Length) % permutedText.Length] = gammaDecryptedText[i];
            }

            return new string(permutedText);
        }


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

                string key1 = "key1";
                string key2 = "key2";

                string ciphertext = Encrypt(confirmPassword, key1, key2);
                string decryptedText = Decrypt(ciphertext, key1, key2);

                using (StreamWriter writer = new StreamWriter("C:/Users/Doter/OneDrive/Desktop/dataBase.txt", true))
                {
                    writer.Write(username + " " + ciphertext);
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
            this.Hide();
            Aut aut = new Aut();
            aut.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
