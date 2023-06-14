using System;
using System.Drawing;
using System.Windows.Forms;

namespace FIX_Autoparts
{
    public partial class SpareInfo : Form
    {
        string path = "";
        public SpareInfo()
        {
            InitializeComponent();
            iniz();
        }

        private void iniz()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Добавить");
            foreach (SparePart S in Form1.db.SpareParts)
            {
                if (S.copy == false)
                    comboBox1.Items.Add($"{S.id} -id) {S.name}");
            }
            comboBox1.SelectedIndex = 0;
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

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.ForeColor = Color.White;
            button4.BackColor = Color.DarkRed;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.ForeColor = Color.FromArgb(31, 40, 51);
            button4.BackColor = Color.FromArgb(102, 252, 241);
        }

        // удалить //
        private void button3_Click(object sender, EventArgs e)
        {
            string[] str = comboBox1.Text.Split(' ');
            foreach (SparePart S in Form1.db.SpareParts)
            {
                if (S.id == Convert.ToInt32(str[0]))
                {
                    Form1.db.SpareParts.Remove(S);
                    break;
                }
            }
            Form1.db.SaveChanges();
            iniz();
        }

        // сохранить (добавить/изменить) //
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" 
                && numericUpDown1.Value != 0 && numericUpDown2.Value != 0 && pictureBox1.BackgroundImage != null)
            {
                if (comboBox1.SelectedIndex == 0) // добавить
                {
                    SparePart spare = new SparePart()
                    {
                        name = textBox1.Text,
                        type = textBox2.Text,
                        brend = textBox3.Text,
                        price = (int)numericUpDown1.Value,
                        like = 0,
                        number = (int)numericUpDown2.Value,
                        promoCode = 0,
                        stock = 0,
                        copy = false,
                        cod = false,
                        path = path,
                    };
                    Form1.db.SpareParts.Add(spare);
                    Form1.db.SaveChanges();
                    iniz();
                }
                else // изменить
                {
                    string[] str = comboBox1.Text.Split(' ');
                    foreach (SparePart S in Form1.db.SpareParts)
                    {
                        if (S.id == Convert.ToInt32(str[0]))
                        {
                            S.name = textBox1.Text;
                            S.type = textBox2.Text;
                            S.brend = textBox3.Text;
                            S.price = (int)numericUpDown1.Value;
                            S.number = (int)numericUpDown2.Value;
                            S.path = path;
                            break;
                        }
                    }
                    Form1.db.SaveChanges();
                    iniz();
                }
            }
        }

        // добавить фото //
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog(); // создаем диалоговое окно
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; // формат загружаемых картинок

            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    path = open_dialog.FileName; // запоминаем путь к файлу
                    pictureBox1.BackgroundImage = Image.FromFile(path);
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // выход //
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        // список товаров //
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                button3.Enabled = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 1;
                pictureBox1.BackgroundImage = null;
            }   
            else
            {
                button3.Enabled = true;
                string[] str = comboBox1.Text.Split(' ');
                foreach (SparePart S in Form1.db.SpareParts)
                {
                    if (S.id == Convert.ToInt32(str[0]))
                    {             
                        try
                        {
                            textBox1.Text = S.name;
                            textBox2.Text = S.type;
                            textBox3.Text = S.brend;
                            numericUpDown1.Value = (decimal)S.price;
                            numericUpDown2.Value = (decimal)S.number;
                            pictureBox1.BackgroundImage = Image.FromFile(S.path);
                        }
                        catch { }
                        break;
                    }
                }
            }      
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

        // удалить фото //
        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = null;
        }
    }
}
