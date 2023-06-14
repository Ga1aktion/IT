using System;
using System.Drawing;
using System.Windows.Forms;

namespace FIX_Autoparts
{
    public partial class AdminOpen : Form
    {
        public AdminOpen()
        {
            InitializeComponent();
            textBox1.UseSystemPasswordChar = true;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.ForeColor = Color.White;
            button5.BackColor = Color.DarkRed;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.ForeColor = Color.FromArgb(31, 40, 51);
            button5.BackColor = Color.FromArgb(102, 252, 241);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
            button1.BackColor = Color.DarkRed;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.FromArgb(31, 40, 51);
            button1.BackColor = Color.FromArgb(102, 252, 241);
        }

        // выход //
        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                textBox1.UseSystemPasswordChar = false;
            else
                textBox1.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == Form1.login && textBox1.Text == Form1.password)
            {
                AdminMenu adminMenu = new AdminMenu();
                adminMenu.ShowDialog();
                Close();
            }
            else
                MessageBox.Show("Неправильный логин или пароль!","Внимание!");
        }
    }
}
