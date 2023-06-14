using System;
using System.Drawing;
using System.Windows.Forms;

namespace FIX_Autoparts
{
    public partial class StockInfo : Form
    {
        public StockInfo()
        {
            InitializeComponent();
            iniz();
        }

        private void iniz()
        {
            int flag = 0;
            foreach (SparePart S in Form1.db.SpareParts)
            {
                if (S.copy == false)
                {
                    comboBox1.Items.Add($"{S.id} -id) {S.name}");
                    flag = 1;
                }
            }
            if (flag == 1)
                comboBox1.SelectedIndex = 0;
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

        // поиск акций //
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] str = comboBox1.Text.Split(' ');
            int flag = 0;
            foreach (SparePart S in Form1.db.SpareParts)
            {
                if (S.id == Convert.ToInt32(str[0]))
                {
                    if (S.stock != 0)
                    {
                        foreach (Stock P in Form1.db.Stocks)
                        {
                            if (P.id == S.stock)
                            {
                                flag = 1;
                                textBox1.Text = P.name;
                                numericUpDown1.Value = (decimal)P.discount;
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            if (flag == 0)
            {
                textBox1.Text = "";
                numericUpDown1.Value = 0;
            }
        }

        // сохранить акцию //
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && numericUpDown1.Value != 0)
            {
                string[] str = comboBox1.Text.Split(' ');
                Stock stock = new Stock();
                SparePart S1 = new SparePart();
                foreach (SparePart S in Form1.db.SpareParts)
                {
                    if (S.id == Convert.ToInt32(str[0]))
                    {
                        stock.name = textBox1.Text;
                        stock.discount = (int)numericUpDown1.Value;
                        S1 = S;
                        break;
                    }
                }
                Form1.db.Stocks.Add(stock);
                Form1.db.SaveChanges();
                S1.stock = stock.id;
                Form1.db.SaveChanges();
                MessageBox.Show("Акция успешно сохранен!", "Внимание!");
            }
            else
                MessageBox.Show("Ошбика заполнения данных!", "Внимание!");
        }
    }
}
