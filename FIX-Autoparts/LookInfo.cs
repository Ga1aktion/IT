using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Entity;

namespace FIX_Autoparts
{
    public partial class LookInfo : Form
    {
        public LookInfo()
        {
            InitializeComponent();
            comboBox1.Items.Add("Клиенты");
            comboBox1.Items.Add("Покупки");
            comboBox1.Items.Add("Обращения");
            comboBox1.SelectedIndex = 0;
            dataGridView1.ReadOnly = true;
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

        // выход //
        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        // выбор информации //
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            info();
        }

        // функция вывода информации в таблицу //
        private void info()
        {
            // чистим таблицу от предыдущего содержимого //
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // выравниваем все ячейки в заголовке по центру //
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (comboBox1.Text == "Клиенты")
            {
                // добавляем столбцы //
                var column1 = new DataGridViewTextBoxColumn();
                var column2 = new DataGridViewTextBoxColumn();
                var column3 = new DataGridViewTextBoxColumn();
                var column4 = new DataGridViewTextBoxColumn();
                var column5 = new DataGridViewTextBoxColumn();
                var column6 = new DataGridViewTextBoxColumn();

                // параметры столбцов //
                column1.HeaderText = "Id клиента";
                column1.Name = "Column1";
                column2.HeaderText = "Имя клиента";
                column2.Name = "Column2";
                column3.HeaderText = "Телефон";
                column3.Name = "Column3";
                column4.HeaderText = "Адрес для доставки";
                column4.Name = "Column4";
                column5.HeaderText = "Пароль от аккаунта";
                column5.Name = "Column5";
                column6.HeaderText = "Количество покупок";
                column6.Name = "Column6";

                // Добавляем созданные столбцы в таблицу //
                this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { column1, column2, column3, column4, column5,
                column6 });

                // указываем ширину стобцов //
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 170;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 150;
                dataGridView1.Columns[5].Width = 170;

                // для того, чтобы был виден весь текст
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Заполняем столбцы данными //
                int i = 0; // счетчик строк
                foreach (Client C in Form1.db.Clients.Include(c=>c.Cheques))
                {
                    dataGridView1.Rows.Add(); // добавляем новую строку

                    // вставляем данные в ячейки строки //
                    dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(C.id);
                    dataGridView1.Rows[i].Cells[1].Value = C.name.ToString();
                    dataGridView1.Rows[i].Cells[2].Value = C.phone;
                    dataGridView1.Rows[i].Cells[3].Value = C.address.ToString();
                    dataGridView1.Rows[i].Cells[4].Value = C.password.ToString();
                    int num = 0;
                    foreach (Cheque cheque in C.Cheques)
                        num++;
                    dataGridView1.Rows[i].Cells[5].Value = num.ToString();
                    i++;
                }
            }
            if (comboBox1.Text == "Покупки")
            {
                // добавляем столбцы //
                var column1 = new DataGridViewTextBoxColumn();
                var column2 = new DataGridViewTextBoxColumn();
                var column3 = new DataGridViewTextBoxColumn();
                var column4 = new DataGridViewTextBoxColumn();

                // параметры столбцов //
                column1.HeaderText = "Id чека";
                column1.Name = "Column1";
                column2.HeaderText = "Стоимость покупки";
                column2.Name = "Column2";
                column3.HeaderText = "Дата покупки";
                column3.Name = "Column3";
                column4.HeaderText = "Доставка";
                column4.Name = "Column4";

                // Добавляем созданные столбцы в таблицу //
                this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { column1, column2, column3, column4 });

                // указываем ширину стобцов //
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 170;
                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[3].Width = 200;

                // для того, чтобы был виден весь текст
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Заполняем столбцы данными //
                int i = 0; // счетчик строк
                foreach (Cheque C in Form1.db.Cheques)
                {
                    dataGridView1.Rows.Add(); // добавляем новую строку

                    // вставляем данные в ячейки строки //
                    dataGridView1.Rows[i].Cells[0].Value = C.id.ToString();
                    dataGridView1.Rows[i].Cells[1].Value = C.price.ToString();
                    dataGridView1.Rows[i].Cells[2].Value = C.date.ToShortDateString();
                    if (C.delivery == true)
                        dataGridView1.Rows[i].Cells[3].Value = "С доставкой";
                    else
                        dataGridView1.Rows[i].Cells[3].Value = "Без доставки";
                    i++;
                }
            }
            if (comboBox1.Text == "Обращения")
            {
                // добавляем столбцы //
                var column1 = new DataGridViewTextBoxColumn();
                var column2 = new DataGridViewTextBoxColumn();
                var column3 = new DataGridViewTextBoxColumn();

                // параметры столбцов //
                column1.HeaderText = "Id обращения";
                column1.Name = "Column1";
                column2.HeaderText = "Почта для связи";
                column2.Name = "Column2";
                column3.HeaderText = "Текст";
                column3.Name = "Column3";

                // Добавляем созданные столбцы в таблицу //
                this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { column1, column2, column3 });

                // указываем ширину стобцов //
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 170;
                dataGridView1.Columns[2].Width = 200;

                // для того, чтобы был виден весь текст
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // Заполняем столбцы данными //
                int i = 0; // счетчик строк
                foreach (Message M in Form1.db.Messages)
                {
                    dataGridView1.Rows.Add(); // добавляем новую строку

                    // вставляем данные в ячейки строки //
                    dataGridView1.Rows[i].Cells[0].Value = M.id.ToString();
                    dataGridView1.Rows[i].Cells[1].Value = M.email.ToString();
                    dataGridView1.Rows[i].Cells[2].Value = M.text;

                    i++;
                }
            }    
        }
    }
}
