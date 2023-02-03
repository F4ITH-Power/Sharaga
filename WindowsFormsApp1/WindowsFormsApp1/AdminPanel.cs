using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class AdminPanel : Form
    {
        public Label errorLabel = new Label();
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {

            string username = textBox1.Text;

            //string decryptedText = Decrypt(encryptedPasswordFromFile, key1, key2);

            string filePath = @"C:/Users/Doter/OneDrive/Desktop/dataBase.txt";

            if (checkBox1.Checked)
            {
                string[] lines = File.ReadAllLines(filePath);
                List<string> updatedLines = new List<string>();
                foreach (string line in lines)
                {
                    if (!line.StartsWith(username + " "))
                    {
                        updatedLines.Add(line);
                    }
                }
                File.WriteAllLines(filePath, updatedLines);
                MessageBox.Show("User has been deleted successfully!");
            }
            else
            {
                MessageBox.Show("Please check the checkbox before deleting the user.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}