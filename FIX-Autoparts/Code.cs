using System;
using System.Drawing;
using System.Windows.Forms;

namespace FIX_Autoparts
{
    public partial class Code : Form
    {
        public static double price = 0;
        public static bool promo = false;
        public Code()
        {
            InitializeComponent();
            iniz();
        }

        // стоимость //
        private void iniz()
        {
            price = 0;
            foreach (SparePart S in Form1.db.SpareParts)
            {
                if (S.id == Form1.idProduct)
                {
                    if (S.stock != 0)
                    {
                        foreach (Stock S1 in Form1.db.Stocks)
                        {
                            if (S1.id == S.stock)
                            {
                                price = Math.Round((S.price - (S.price * (S1.discount / 100))) * Form1.numProduct,2);
                                break;
                            }
                        }
                    }
                    else
                        price = Math.Round(S.price * Form1.numProduct,2);
                    break;
                }
            }
            label1.Text = $"Цена: {price} руб.";
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
            button1.BackColor = Color.DarkRed;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.FromArgb(31, 40, 51);
            button1.BackColor = Color.FromArgb(69, 162, 158);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
            button2.BackColor = Color.DarkRed;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.FromArgb(31, 40, 51);
            button2.BackColor = Color.FromArgb(69, 162, 158);
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.White;
            button3.BackColor = Color.DarkRed;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.FromArgb(31, 40, 51);
            button3.BackColor = Color.FromArgb(69, 162, 158);
        }

        // отмена //
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        // применить скидку //
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
            {
                foreach (SparePart S in Form1.db.SpareParts)
                {
                    if (S.id == Form1.idProduct)
                    {
                        if (S.promoCode != 0)
                        {
                            foreach (PromoCode P in Form1.db.PromoCodes)
                            {
                                if (P.name == textBox1.Text && P.id == S.promoCode)
                                {
                                    price = Math.Round(price - (price * (P.discount / 100)), 2);
                                    label1.Text = $"Цена: {price} руб.   (-{P.discount}%)";
                                    textBox1.Enabled = false;
                                    button1.Enabled = false;
                                    promo = true;
                                    MessageBox.Show("Промокод успешно применен!", "Внимание!");
                                    break;
                                }
                            }
                            break;
                        }
                        break;
                    }
                }               
            }
            else
                MessageBox.Show("Промокод не найден!", "Внимание!");
        }

        // продолжить //
        private void button3_Click(object sender, EventArgs e)
        {
            createBuy();
        }

        // закидываем товар в корзину //
        private void createBuy()
        {
            for (int i = 0; i < Form1.numProduct; i++)
            {
                foreach (Client C in Form1.db.Clients)
                {
                    if (C.id == Form1.user)
                    {
                        foreach (Buyer B in Form1.db.Buyers)
                        {
                            if (B.id == C.buyerId)
                            {
                                foreach (SparePart S in Form1.db.SpareParts)
                                {
                                    if (S.id == Form1.idProduct)
                                    {
                                        SparePart spare = new SparePart()
                                        {
                                            name = S.name,
                                            type = S.type,
                                            brend = S.brend,
                                            price = S.price,
                                            like = S.like,
                                            number = S.number,
                                            promoCode = S.promoCode,
                                            stock = S.stock,
                                            copy = true,
                                            cod = promo,
                                            path = "",

                                        };
                                        if (promo == true)
                                            spare.cod = true;
                                        else
                                            spare.cod = false;
                                        Form1.db.SpareParts.Add(spare);
                                        B.SpareParts.Add(spare);
                                        break;
                                    }
                                }                              
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            Form1.db.SaveChanges();
            
            Profile profile = new Profile();
            profile.ShowDialog();
            Close();
        }
    }
}
