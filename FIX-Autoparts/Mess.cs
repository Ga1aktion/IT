using System;
using System.Drawing;
using System.Windows.Forms;

namespace FIX_Autoparts
{
    public partial class Mess : Form
    {
        public Mess()
        {
            InitializeComponent();
        }

        // выход //
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
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

        // отправить //
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && richTextBox1.Text != "")
            {
                Message message = new Message()
                {
                    email = textBox1.Text,
                    text = richTextBox1.Text
                };
                Form1.db.Messages.Add(message);
                Form1.db.SaveChanges();
                MessageBox.Show("Письмо успешно отправлено!\nОжидайте ответа", "Внимание!");
                Close();
            }
            else
                MessageBox.Show("Заполните все поля!", "Ошибка");
        }
    }
}
