using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Entity;

namespace FIX_Autoparts
{
    public partial class Profile : Form
    {
        public static double price = 0;
        public Profile()
        {
            InitializeComponent();
            saveData();
            maskedTextBox1.Enabled = false;
            checkBox1.Checked = true;
            BuyerProduct();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
        }

        // заполнение списка товаров корзины //
        private void BuyerProduct()
        {
            checkedListBox1.Items.Clear();
            foreach (Client C in Form1.db.Clients)
            {
                if (C.id == Form1.user)
                {
                    foreach (Buyer B in Form1.db.Buyers.Include(S=>S.SpareParts))
                    {
                        if (B.id == C.buyerId)
                        {
                            foreach (SparePart S in B.SpareParts)
                            {
                                double money = S.price;
                                if (S.stock != 0) // если есть акция
                                {
                                    foreach (Stock S1 in Form1.db.Stocks)
                                    {
                                        if (S1.id == S.stock)
                                        {
                                            money = money - (money * (S1.discount/100));
                                            break;
                                        }
                                    }
                                }
                                if (S.cod == true) // если был введен промокод и этот промокод действует
                                {
                                    foreach (PromoCode promo in Form1.db.PromoCodes)
                                    {
                                        if (S.promoCode == promo.id)
                                        {
                                            money = money - (money * (promo.discount / 100));
                                            break;
                                        }
                                    }
                                }
                                price += money;
                                checkedListBox1.Items.Add($"{S.type} {S.name} {money} руб.");
                            }
                            break;
                        }
                    }
                    break;
                }    
            }
            label4.Text = $"К оплате: {price} руб.";
        }

        // функция вывода данных //
        private void saveData()
        {
            foreach (Client C in Form1.db.Clients)
            {
                if (C.id == Form1.user)
                {
                    maskedTextBox1.Text = C.phone;
                    textBox1.Text = C.name;
                    textBox2.Text = C.address;
                    textBox3.Text = C.password;
                    break;
                }
            }
        }

        // выход //
        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        // сбросить //
        private void button1_Click(object sender, EventArgs e)
        {
            saveData();
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

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.White;
            button3.BackColor = Color.DarkRed;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.FromArgb(31, 40, 51);
            button3.BackColor = Color.FromArgb(102, 252, 241);
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.ForeColor = Color.White;
            button6.BackColor = Color.DarkRed;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.ForeColor = Color.FromArgb(31, 40, 51);
            button6.BackColor = Color.FromArgb(102, 252, 241);
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

        // изменить персональные данные //
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != ""
                && textBox3.Text.Trim() != "" && maskedTextBox1.MaskCompleted == true)
            {
                foreach (Client C in Form1.db.Clients)
                {
                    if (C.id == Form1.user)
                    {
                        C.name = textBox1.Text;
                        C.phone = maskedTextBox1.Text;
                        C.address = textBox2.Text;
                        C.password = textBox3.Text;
                        break;
                    }    
                }
                Form1.db.SaveChanges();
            }
            else
                saveData();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
            }
            moneyBuy();
        }

        // подсчет стоимости //
        private void moneyBuy()
        {
            price = 0;
            double help = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                string str = (string)checkedListBox1.Items[i];
                if (checkedListBox1.GetItemChecked(i))
                {
                    string[] words = str.Split(' ');
                    price += Convert.ToDouble(words[words.Length - 2]);
                }               
            }
            if (checkBox2.Checked == true)
                price = price * 0.95;                
            label4.Text = $"К оплате: {price} руб.";
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            moneyBuy();
        }

        // без доставки //
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            {
                price = price / 0.95;
                label4.Text = $"К оплате: {price} руб.";
            }
            else
                moneyBuy();
        }

        // удалить //
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = checkedListBox1.Items.Count - 1; i >= 0; i--)
            {
                string str = (string)checkedListBox1.Items[i];
                if (checkedListBox1.GetItemChecked(i))
                {
                    string[] words = str.Split(' ');
                    // удаляем из корзины в бд //
                    foreach (Client C in Form1.db.Clients)
                    {
                        if (C.id == Form1.user)
                        {
                            foreach (Buyer B in Form1.db.Buyers.Include(S=>S.SpareParts))
                            {
                                if (B.id == C.buyerId)
                                {
                                    foreach (SparePart S in B.SpareParts)
                                    {
                                        double money = S.price;
                                        if (S.stock != 0)
                                        {
                                            foreach (Stock stock in Form1.db.Stocks)
                                            {
                                                if (stock.id == S.stock)
                                                {
                                                    money = money - (money * (stock.discount / 100));
                                                    break;
                                                }
                                            }
                                        }
                                        if (S.cod == true)
                                        {
                                            foreach (PromoCode promo in Form1.db.PromoCodes)
                                            {
                                                if (promo.id == S.promoCode)
                                                {
                                                    money = money - (money * (promo.discount / 100));
                                                    break;
                                                }
                                            }
                                        }
                                        // нашли нужный из списка //
                                        string[] wor = S.type.Split(' ');
                                        if (words[0] == wor[0] && words[words.Length - 2] == money.ToString())
                                        {
                                            Form1.db.SpareParts.Remove(S);
                                            price -= money;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }    
                            break;
                        }    
                    }
                    Form1.db.SaveChanges();
                    checkedListBox1.Items.RemoveAt(i);
                }                   
            }
            saveData();
            moneyBuy();
        }

        // оплатить //
        private void button5_Click(object sender, EventArgs e)
        {
            double finish = 0;
            DialogResult dialogResult = MessageBox.Show($"Желаете продолжить?\nСумма к оплате: {price} руб.", "Оплата", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                finish = price;
                for (int i = checkedListBox1.Items.Count - 1; i >= 0; i--)
                {
                    string str = (string)checkedListBox1.Items[i];
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string[] words = str.Split(' ');
                        // удаляем из корзины в бд //
                        foreach (Client C in Form1.db.Clients)
                        {
                            if (C.id == Form1.user)
                            {
                                foreach (Buyer B in Form1.db.Buyers.Include(S => S.SpareParts))
                                {
                                    if (B.id == C.buyerId)
                                    {
                                        foreach (SparePart S in B.SpareParts)
                                        {
                                            double money = S.price;
                                            if (S.stock != 0)
                                            {
                                                foreach (Stock stock in Form1.db.Stocks)
                                                {
                                                    if (stock.id == S.stock)
                                                    {
                                                        money = money - (money * (stock.discount / 100));
                                                        break;
                                                    }
                                                }
                                            }
                                            if (S.cod == true)
                                            {
                                                foreach (PromoCode promo in Form1.db.PromoCodes)
                                                {
                                                    if (promo.id == S.promoCode)
                                                    {
                                                        money = money - (money * (promo.discount / 100));
                                                        break;
                                                    }
                                                }
                                            }
                                            // нашли нужный из списка //
                                            string[] wor = S.type.Split(' ');
                                            if (words[0] == wor[0] && words[words.Length - 2] == money.ToString())
                                            {
                                                // увеличиваем популярность и уменьшаем кол-во на складе продукт //
                                                foreach (SparePart part in Form1.db.SpareParts)
                                                {
                                                    if (part.name == S.name && part.type == S.type && part.brend == S.brend)
                                                    {
                                                        part.like++;
                                                        part.number--;
                                                        break;
                                                    }
                                                }
                                                Form1.db.SpareParts.Remove(S);
                                                price -= money;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        Form1.db.SaveChanges();
                        checkedListBox1.Items.RemoveAt(i);
                    }
                }
                saveData();
                moneyBuy();
            }
            Cheque cheque = new Cheque()
            {
                price = finish,
                date = DateTime.Today,
                delivery = checkBox2.Checked,
            };
            Form1.db.Cheques.Add(cheque);
            Form1.db.SaveChanges();
            foreach (Client C in Form1.db.Clients)
            {
                if (C.id == Form1.user)
                {
                    C.Cheques.Add(cheque);
                    break;
                }    
            }
            Form1.db.SaveChanges();
            string mess = $"Идентификатор чека: {cheque.id}\nДата оформления:{cheque.date.ToShortDateString()}";
            if (cheque.delivery == true)
                mess += $"\nДоставка ожидается втечение 48 часов, ожидайте звонка\n" +
                    $"Сумма к оплате: {finish} рублей\nне забудте показать чек доставщику\nЖелаем счастливого дня!";
            else
                mess += $"\nТовар зарезервирован до {DateTime.Today.AddDays(7)}\nЖдем вас с нетерпением:)\n" +
                    $"Сумма к оплате: {finish} рублей\nне забудте показать чек на кассе\nЖелаем счастливого дня!";
            MessageBox.Show($"{mess}", "Внимание!");
        }
    }
}
