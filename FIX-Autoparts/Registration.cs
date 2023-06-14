using System;
using System.Drawing;
using System.Windows.Forms;

namespace FIX_Autoparts
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
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

        // выход //
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        // создать профиль //
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" 
                && textBox3.Text.Trim() != "" && maskedTextBox1.MaskCompleted == true)
            {
                // проверка, есть ли уже профиль на этот номер //
                int flag = 0;
                foreach (Client C in Form1.db.Clients)
                {
                    if (C.phone == maskedTextBox1.Text)
                    {
                        flag = 1;
                        break;
                    }         
                }
                if (flag == 0)
                {
                    // создаем профиль //
                    Client client = new Client()
                    {
                        name = textBox1.Text,
                        phone = maskedTextBox1.Text,
                        address = textBox2.Text,
                        password = textBox3.Text,
                        buyerId = 0
                    };
                    // создаем корзину покупателя //
                    Buyer buyer = new Buyer()
                    {
                        price = 0
                    };
                    Form1.db.Buyers.Add(buyer);
                    Form1.db.SaveChanges();
                    client.buyerId = buyer.id;
                    Form1.db.Clients.Add(client);
                    Form1.db.SaveChanges();
                    MessageBox.Show("Профиль успешно создан!", "Внимание!");
                    Close();
                }
                else
                    MessageBox.Show("Данный номер телефона уже занят!", "Внимание!");
            }
            else
                MessageBox.Show("Заполните все поля!", "Внимание!");
        }
    }
}
