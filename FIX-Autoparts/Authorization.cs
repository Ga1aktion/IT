using System;
using System.Drawing;
using System.Windows.Forms;

namespace FIX_Autoparts
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
            textBox1.UseSystemPasswordChar = true;
        }

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

        // регистрация //
        private void button2_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.ShowDialog();
            Close();
        }

        // вход //
        private void button1_Click(object sender, EventArgs e)
        {
            int flag = 0;
            int id = 0;
            foreach (Client C in Form1.db.Clients)
            {               
                if (C.phone == maskedTextBox1.Text && C.password == textBox1.Text)
                {
                    id = C.id;
                    flag = 1;
                    break;
                }    
            }
            if (flag == 1)
            {
                Form1.user = id;
                Profile profile = new Profile();
                profile.ShowDialog();
                Close();
            }
            else
                MessageBox.Show("Неправильный логин или пароль!", "Внимание!");
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

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
            button2.BackColor = Color.DarkRed;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.FromArgb(31, 40, 51);
            button2.BackColor = Color.FromArgb(102, 252, 241);
        }
    }
}
