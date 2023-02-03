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
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class Aut : Form
    {
        public Label errorLabel = new Label();

        public Aut()
        {
            InitializeComponent();
            errorLabel.Text = "";
            errorLabel.AutoSize = false;
            errorLabel.Height = 30;
            errorLabel.Width = 130;
            errorLabel.Font = new Font("Arial", 7, FontStyle.Bold);
            errorLabel.Location = new Point(150, 25);
            Controls.Add(errorLabel);
        }
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

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            string key1 = "key1";
            string key2 = "key2";

            string encryptedPasswordFromFile;

            string path = "C:/Users/Doter/OneDrive/Desktop/dataBase.txt";

            if (!File.Exists(path))
            {
                errorLabel.Text = "Database not found";
                errorLabel.ForeColor = Color.Red;
                return;
            }

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
                            encryptedPasswordFromFile = line.Substring(index + 1);

                            string decryptedText = Decrypt(encryptedPasswordFromFile, key1, key2);

                            if (login == "admin" && password == "test")
                            {
                                errorLabel.Text = "Access granted";
                                errorLabel.ForeColor = Color.Green;
                                this.Close();
                                AdminPanel adminPanel = new AdminPanel();
                                adminPanel.Show();
                            }

                            if (decryptedText.Equals(password))
                            {
                                errorLabel.Text = "Access granted";
                                errorLabel.ForeColor = Color.Green;
                                return;
                            }
                        }
                    }
                }
                errorLabel.Text = "Access denied";
                errorLabel.ForeColor = Color.Red;
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