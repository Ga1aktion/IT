using System;
using System.Drawing;
using System.Windows.Forms;

namespace FIX_Autoparts
{
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(102, 252, 241);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(102, 252, 241);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.FromArgb(102, 252, 241);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.FromArgb(102, 252, 241);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.FromArgb(102, 252, 241);
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.White;
        }

        // выход //
        private void label5_Click(object sender, EventArgs e)
        {
            Close();
        }

        // просмотр клиентов и покупки //
        private void label4_Click(object sender, EventArgs e)
        {
            LookInfo lookInfo = new LookInfo();
            lookInfo.ShowDialog();
        }

        // промокоды //
        private void label1_Click(object sender, EventArgs e)
        {
            PromoInfo promoInfo = new PromoInfo();
            promoInfo.ShowDialog();
        }

        // акции //
        private void label3_Click(object sender, EventArgs e)
        {
            StockInfo stockInfo = new StockInfo();
            stockInfo.ShowDialog();
        }

        // запчасти //
        private void label2_Click(object sender, EventArgs e)
        {
            SpareInfo spareInfo = new SpareInfo();
            spareInfo.ShowDialog();
        }
    }
}
