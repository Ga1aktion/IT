using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.Entity;

namespace FIX_Autoparts
{
    public partial class Form1 : Form
    {
        public static ContextBD db = new ContextBD(); // контекст данных
        public static int user = 0; // пременная аккаунта пользователя

        // покупка товара //
        public static int idProduct = 0; // продукт
        public static int numProduct = 1; // количество продукта

        // меню админа //
        public static string login = "admin";
        public static string password = "12345";

        public Form1()
        {
            InitializeComponent();
            helpTextProductSearch();
            filtrData();
            printData();
        }
        
        // заполнение фильтра //
        private void filtrData()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 9999999;

            comboBox1.Items.Add("По популярности");
            comboBox1.Items.Add("Сначала дешевые");
            comboBox1.Items.Add("Сначала дорогие");
            comboBox1.Items.Add("По размеру скидки");
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Add("Все типы");
            foreach (SparePart S in db.SpareParts)
            {
                int flag = 0;
                for (int i = 0; i < comboBox2.Items.Count; i++)
                {
                    if (comboBox2.Items[i].ToString() == S.type)
                        flag = 1;
                }
                if (flag == 0)
                    comboBox2.Items.Add(S.type);
            }
            comboBox2.SelectedIndex = 0;

            comboBox3.Items.Add("Все бренды");
            foreach (SparePart S in db.SpareParts)
            {
                int flag = 0;
                for (int i = 0; i < comboBox3.Items.Count; i++)
                {
                    if (comboBox3.Items[i].ToString() == S.brend)
                        flag = 1;
                }
                if (flag == 0)
                    comboBox3.Items.Add(S.brend);
            }
            comboBox3.SelectedIndex = 0;
        }

        // функция вывода запчастей //
        private void printData()
        {
            panel1.Controls.Clear();
            int numProduct = 0;
            foreach (SparePart S in db.SpareParts)
                numProduct++;
            int[] listProductSort = new int[numProduct]; // массив id подходящих под сортировку товаров
            sortedListProduct(listProductSort, numProduct); // сортировка по умолчанию
            sortedTypeListProduct(listProductSort, numProduct); // сортировка по типу
            sortedBrendListProduct(listProductSort, numProduct); // сортировка по бренду
            sortedPriceListProduct(listProductSort, numProduct); // сортировка по стоимости
            sortedDiscontListProduct(listProductSort, numProduct); // сортировка по наличию скидки
            sortedNumListProduct(listProductSort, numProduct); // сортировка по наличии на складе

            if (textBox1.Text != "Введите запрос")
                filterTextProduct(listProductSort, numProduct); // фильтр по поиску

            int check = 0; // проверка, нашлись ли элементы согласно фильтрам
            for (int i = 0; i < numProduct; i++)
            {
                if (listProductSort[i] != 0) // если есть хоть одно совпадение
                {
                    check = 1;
                    break;
                }
            }

            if (check == 0) // нет совпадений - выводим надпись об этом
            {
                Label error = new Label();

                error.ForeColor = Color.Black;
                error.BackColor = this.BackColor;
                error.Size = new Size(800, 90);
                error.Font = new Font("Microsoft Sans Serif", 36, FontStyle.Bold);
                error.TextAlign = ContentAlignment.TopCenter;
                error.Text = "Запчасти не найдены";

                error.Location = new Point(30, 150);
                panel1.Controls.Add(error);
            }
            else
            {
                // массивы оъектов элементов управления //
                PictureBox[] PictureBackground = new PictureBox[numProduct]; // фон основной инфы
                PictureBox[] PictureBackgroundPath = new PictureBox[numProduct]; // картинка
                Label[] infoNum = new Label[numProduct]; // кол-во раз куплено
                Label[] infoName = new Label[numProduct]; // имя запчасти и бренд
                Label[] infoType = new Label[numProduct]; // тип запачасти и кол-во на складе
                Label[] buyNum = new Label[numProduct]; // кол-во для покупки
                Button[] btn = new Button[numProduct]; // кнопка в корзину
                NumericUpDown[] numBuy = new NumericUpDown[numProduct]; // выбор кол-во для покупки
                Label[] price = new Label[numProduct]; // стоимость
                Label[] priceWow = new Label[numProduct]; // стоимость

                // размеры //
                int priceWidth = 305;
                int priceHeight = 40;

                int numBuyWidth = 80;
                int numBuyHeight = 80;

                int btnWidth = 130;
                int btnHeight = 50;

                int buyNumWidth = 100;
                int buyNumHeight = 30;

                int infoTypeWidth = 200;
                int infoTypeHeight = 100;

                int infoNameWidth = 200;
                int infoNameHeight = 80;

                int infoNumWidth = 250;
                int infoNumHeight = 40;
                
                int PictureBackgroundWidth = 770;
                int PictureBackgroundHeight = 130;

                int PictureBackgroundPathWidth = 120;
                int PictureBackgroundPathHeight = 120;

                // координаты на форме //
                int priceX = 500;
                int priceY = 162;

                int numBuyX = 700;
                int numBuyY = 45;

                int btnX = 650;
                int btnY = 95;

                int buyNumX = 600;
                int buyNumY = 45;

                int infoTypeX = 400;
                int infoTypeY = 45;

                int infoNameX = 240;
                int infoNameY = 45;

                int infoNumX = 35;
                int infoNumY = 162;

                int PictureBackgroundX = 35;
                int PictureBackgroundY = 30;

                int PictureBackgroundPathX = 90;
                int PictureBackgroundPathY = 35;

                int k = 0; // счетчик
                for (int i = 0; i < numProduct; i++)
                {
                    foreach (SparePart S in db.SpareParts)
                    {
                        if (listProductSort[i] == S.id && S.copy == false)
                        {
                            // стоимость //
                            price[k] = new Label();
                            price[k].ForeColor = Color.FromArgb(31, 40, 51);
                            price[k].BackColor = Color.FromArgb(102, 252, 241);
                            price[k].Size = new Size(priceWidth, priceHeight);
                            price[k].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
                            price[k].TextAlign = ContentAlignment.TopLeft;
                            
                            // поиск акций //
                            if (S.stock != 0)
                            {
                                priceWow[k] = new Label();
                                priceWow[k].ForeColor = Color.DarkRed;
                                priceWow[k].BackColor = Color.FromArgb(102, 252, 241);
                                priceWow[k].Size = new Size(priceWidth-170, priceHeight);
                                priceWow[k].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Strikeout);
                                priceWow[k].TextAlign = ContentAlignment.TopLeft;
                                priceWow[k].Text = "";
                                foreach (Stock stock in db.Stocks)
                                {
                                    if (stock.id == S.stock)
                                    {
                                        double ops = Math.Round(S.price - (S.price * (stock.discount / 100)), 2);
                                        price[k].Text = $"Цена: {ops} руб.";
                                        priceWow[k].Text = $"{S.price} руб.";
                                        // вывод объекта по следующим координатам //
                                        priceWow[k].Location = new Point(priceX+170, priceY);
                                        panel1.Controls.Add(priceWow[k]);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                price[k].Text = $"Цена: {S.price} руб.";
                            }                          
                            // вывод объекта по следующим координатам //
                            price[k].Location = new Point(priceX, priceY);
                            panel1.Controls.Add(price[k]);
                            // переход к след координатам //
                            priceY += PictureBackgroundHeight + 70;

                            // счетчик корзины //
                            numBuy[k] = new NumericUpDown();
                            numBuy[k].Size = new Size(numBuyWidth, numBuyHeight);
                            numBuy[k].Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                            numBuy[k].Maximum = S.number;
                            numBuy[k].Minimum = 1;
                            numBuy[k].Value = 1;
                            // вывод объекта по следующим координатам //
                            numBuy[k].Location = new Point(numBuyX, numBuyY);
                            panel1.Controls.Add(numBuy[k]);
                            numBuy[k].Name = S.id.ToString();
                            numBuyY += PictureBackgroundHeight + 70;

                            // кнопка //
                            btn[k] = new Button();
                            btn[k].ForeColor = Color.FromArgb(31, 40, 51);
                            btn[k].BackColor = Color.FromArgb(69, 162, 158);
                            btn[k].Size = new Size(btnWidth, btnHeight);
                            btn[k].Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                            btn[k].Text = "В корзину";
                            // вывод объекта по следующим координатам //
                            btn[k].Location = new Point(btnX, btnY);
                            panel1.Controls.Add(btn[k]);
                            // переход к след координатам //
                            btnY += PictureBackgroundHeight + 70;
                            // событие нажатия на кнопку //
                            btn[k].Click += new EventHandler(button_Clicked);
                            // в качестве имени кнопки будем использовать айди товара //
                            btn[k].Name = S.id.ToString();
                            // событие наведение на кнопку курсора //
                            btn[k].MouseEnter += new EventHandler(button_MouseEnter);
                            // событие отведение курсора от кнопки //
                            btn[k].MouseLeave += new EventHandler(button_MouseLeave);


                            // количество //
                            buyNum[k] = new Label();
                            buyNum[k].ForeColor = Color.FromArgb(31, 40, 51);
                            buyNum[k].BackColor = Color.FromArgb(102, 252, 241);
                            buyNum[k].Size = new Size(buyNumWidth, buyNumHeight);
                            buyNum[k].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
                            buyNum[k].TextAlign = ContentAlignment.TopLeft;
                            buyNum[k].Text = $"Кол-во:";
                            // вывод объекта по следующим координатам //
                            buyNum[k].Location = new Point(buyNumX, buyNumY);
                            panel1.Controls.Add(buyNum[k]);
                            // переход к след координатам //
                            buyNumY += PictureBackgroundHeight + 70;

                            // тип и кол-во запчасти //
                            infoType[k] = new Label();
                            infoType[k].ForeColor = Color.FromArgb(31, 40, 51);
                            infoType[k].BackColor = Color.FromArgb(102, 252, 241);
                            infoType[k].Size = new Size(infoTypeWidth, infoTypeHeight);
                            infoType[k].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
                            infoType[k].TextAlign = ContentAlignment.TopLeft;
                            infoType[k].Text = $"Тип: {S.type}";
                            if (S.number != 0)
                                infoType[k].Text += $"\n\nНа складе: {S.number}";
                            else
                                infoType[k].Text += "\n\nНет в начичии";
                            // вывод объекта по следующим координатам //
                            infoType[k].Location = new Point(infoTypeX, infoTypeY);
                            panel1.Controls.Add(infoType[k]);
                            // переход к след координатам //
                            infoTypeY += PictureBackgroundHeight + 70;

                            // имя и бренд запачсти //
                            infoName[k] = new Label();
                            infoName[k].ForeColor = Color.FromArgb(31, 40, 51);
                            infoName[k].BackColor = Color.FromArgb(102, 252, 241);
                            infoName[k].Size = new Size(infoNameWidth, infoNameHeight);
                            infoName[k].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
                            infoName[k].TextAlign = ContentAlignment.TopLeft;
                            infoName[k].Text = $"Имя: {S.name}\n\nБренд: {S.brend}";
                            // вывод объекта по следующим координатам //
                            infoName[k].Location = new Point(infoNameX, infoNameY);
                            panel1.Controls.Add(infoName[k]);
                            // переход к след координатам //
                            infoNameY += PictureBackgroundHeight + 70;

                            // инфа о купленном кол-во раз товаре //
                            infoNum[k] = new Label();
                            infoNum[k].ForeColor = Color.FromArgb(31, 40, 51);
                            infoNum[k].BackColor = Color.FromArgb(102, 252, 241);
                            infoNum[k].Size = new Size(infoNumWidth, infoNumHeight);
                            infoNum[k].Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                            infoNum[k].TextAlign = ContentAlignment.TopCenter;
                            infoNum[k].Text = "";
                            if (S.like == 0)
                                infoNum[k].Text = $"Куплено: 0 раз";
                            else
                                infoNum[k].Text = $"Куплено: {S.like} раз";
                            // вывод объекта по следующим координатам //
                            infoNum[k].Location = new Point(infoNumX, infoNumY);
                            panel1.Controls.Add(infoNum[k]);
                            // переход к след координатам //
                            infoNumY += PictureBackgroundHeight + 70;

                            // картинка //
                            PictureBackgroundPath[k] = new PictureBox();
                            try
                            {
                                PictureBackgroundPath[k].Image = Image.FromFile(S.path);
                            }
                            catch { }
                            PictureBackgroundPath[k].SizeMode = PictureBoxSizeMode.StretchImage;
                            PictureBackgroundPath[k].Size = new Size(PictureBackgroundPathWidth, PictureBackgroundPathHeight);
                            PictureBackgroundPath[k].BackColor = Color.DarkRed;
                            // вывод объекта по следующим координатам //
                            PictureBackgroundPath[k].Location = new Point(PictureBackgroundPathX, PictureBackgroundPathY);
                            panel1.Controls.Add(PictureBackgroundPath[k]);
                            // переход к след координатам //
                            PictureBackgroundPathY += PictureBackgroundHeight + 70;

                            // фон основной инфы
                            PictureBackground[k] = new PictureBox();
                            PictureBackground[k].Size = new Size(PictureBackgroundWidth, PictureBackgroundHeight);
                            PictureBackground[k].BackColor = Color.FromArgb(102, 252, 241);
                            // вывод объекта по следующим координатам //
                            PictureBackground[k].Location = new Point(PictureBackgroundX, PictureBackgroundY);
                            panel1.Controls.Add(PictureBackground[k]);
                            // переход к след координатам //
                            PictureBackgroundY += PictureBackgroundHeight + 70;


                           

                            k++;
                        }
                    }
                }
            }
        }

        // фильтр по поиску //
        private void filterTextProduct(int[] array, int i)
        {
            for (int j = 0; j < i; j++)
            {
                if (array[j] != 0)
                {
                    string s = textBox1.Text.Trim();
                    foreach (SparePart S in db.SpareParts)
                    {
                        if (S.id == array[j])
                        {
                            if (!S.name.Contains(s))
                                array[j] = 0;
                            break;
                        }
                    }
                }
            }
        }

        // сортировка о наличии на складе //
        private void sortedNumListProduct(int[] array, int i)
        {
            if (checkBox2.Checked == true)
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[j] != 0)
                    {
                        foreach (SparePart S in db.SpareParts)
                        {
                            if (array[j] == S.id)
                            {
                                if (S.number == 0)
                                    array[j] = 0;
                            }
                        }
                    }
                }
            }
        }

        // сортировка о наличии скидки //
        private void sortedDiscontListProduct(int[] array, int i)
        {
            if (checkBox1.Checked == true)
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[j] != 0)
                    {
                        foreach (SparePart S in db.SpareParts)
                        {
                            if (array[j] == S.id)
                            {
                                if (S.stock == 0)
                                    array[j] = 0;
                            }
                        }
                    }
                }
            }          
        }

        // сортировка по стоимости //
        private void sortedPriceListProduct(int[] array, int i)
        {
            for (int j = 0; j < i; j++)
            {
                if (array[j] != 0)
                {
                    foreach (SparePart P in db.SpareParts)
                    {
                        if (array[j] == P.id)
                        {
                            double money = 0;
                            if (P.stock != 0)
                            {
                                foreach (Stock D in db.Stocks)
                                {
                                    if (D.id == P.stock)
                                    {
                                        money = P.price - (P.price / 100 * D.discount);
                                        break;
                                    }
                                }
                            }
                            if (money == 0)
                                money = P.price;
                            if (money < (int)numericUpDown1.Value || money > (int)numericUpDown2.Value)
                                array[j] = 0;
                            break;
                        }
                    }
                }
            }
        }

        // сортировка по бренду //
        private void sortedBrendListProduct(int[] array, int i)
        {
            if (comboBox3.Text != "Все бренды")
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[j] != 0)
                    {
                        foreach (SparePart S in db.SpareParts)
                        {
                            if (S.id == array[j])
                            {
                                if (S.brend != comboBox3.Text)
                                    array[j] = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        // сортировка по типу //
        private void sortedTypeListProduct(int[] array, int i)
        {
            if (comboBox2.Text != "Все типы")
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[j] != 0)
                    {
                        foreach (SparePart S in db.SpareParts)
                        {
                            if (S.id == array[j])
                            {
                                if (S.type != comboBox2.Text)
                                    array[j] = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        // фильтр по стоимости и популярности //
        private void sortedListProduct(int[] array, int i)
        {
            // заносив все id продуктов в массив //
            int k = 0;
            foreach (SparePart S in db.SpareParts)
            {
                array[k] = S.id;
                k++;
            }

            // для сортировки //
            SparePart[] newList = new SparePart[i]; // массив объектов
            int num = 0; // для навигации по массиву
            foreach (SparePart S in db.SpareParts)
            {
                newList[num] = S;
                num++;
            }

            // сортировка //
            if (comboBox1.Text == "Сначала дешевые")
            {
                // сортировка по возрастанию цены //
                for (int i1 = 0; i1 < i; i1++)
                {
                    for (int i2 = 0; i2 < i; i2++)
                    {
                        double price1 = 0;
                        double price2 = 0;

                        if (newList[i1].stock != 0) // проверка на скидку
                        {
                            foreach (Stock d in db.Stocks)
                            {
                                if (d.id == newList[i1].stock)
                                {
                                    price1 = newList[i1].price - (newList[i1].price / 100 * d.discount);
                                    break;
                                }
                            }
                        }
                        else
                            price1 = newList[i1].price;

                        if (newList[i2].stock != 0) // проверка на скидку
                        {
                            foreach (Stock d in db.Stocks)
                            {
                                if (d.id == newList[i2].stock)
                                {
                                    price2 = newList[i2].price - (newList[i2].price / 100 * d.discount);
                                    break;
                                }
                            }
                        }
                        else
                            price2 = newList[i2].price;

                        if (price1 < price2) // сортировка пузырьком
                        {
                            SparePart help = newList[i1];
                            newList[i1] = newList[i2];
                            newList[i2] = help;
                        }
                    }
                }
                // заносим id в массив айдишников //
                for (int j = 0; j < i; j++)
                    array[j] = newList[j].id;
            }

            if (comboBox1.Text == "Сначала дорогие")
            {
                // сортировка по возрастанию цены //
                for (int i1 = 0; i1 < i; i1++)
                {
                    for (int i2 = 0; i2 < i; i2++)
                    {
                        double price1 = 0;
                        double price2 = 0;

                        if (newList[i1].stock != 0) // проверка на скидку
                        {
                            foreach (Stock d in db.Stocks)
                            {
                                if (d.id == newList[i1].stock)
                                {
                                    price1 = newList[i1].price - (newList[i1].price / 100 * d.discount);
                                    break;
                                }
                            }
                        }
                        else
                            price1 = newList[i1].price;

                        if (newList[i2].stock != 0) // проверка на скидку
                        {
                            foreach (Stock d in db.Stocks)
                            {
                                if (d.id == newList[i2].stock)
                                {
                                    price2 = newList[i2].price - (newList[i2].price / 100 * d.discount);
                                    break;
                                }
                            }
                        }
                        else
                            price2 = newList[i2].price;

                        if (price1 > price2) // сортировка пузырьком
                        {
                            SparePart help = newList[i1];
                            newList[i1] = newList[i2];
                            newList[i2] = help;
                        }
                    }
                }
                // заносим id в массив айдишников //
                for (int j = 0; j < i; j++)
                    array[j] = newList[j].id;
            }

            if (comboBox1.Text == "По размеру скидки")
            {
                // сортировка по размеру скидки //
                for (int i1 = 0; i1 < i; i1++)
                {
                    for (int i2 = 0; i2 < i; i2++)
                    {
                        double price1 = 0;
                        double price2 = 0;

                        if (newList[i1].stock != 0) // проверка на скидку
                        {
                            foreach (Stock d in db.Stocks)
                            {
                                if (d.id == newList[i1].stock)
                                {
                                    price1 = newList[i1].price / 100 * d.discount;
                                    break;
                                }
                            }
                        }
                        else
                            price1 = 0;

                        if (newList[i2].stock != 0) // проверка на скидку
                        {
                            foreach (Stock d in db.Stocks)
                            {
                                if (d.id == newList[i2].stock)
                                {
                                    price2 = newList[i2].price / 100 * d.discount;
                                    break;
                                }
                            }
                        }
                        else
                            price2 = 0;

                        if (price1 > price2) // сортировка пузырьком
                        {
                            SparePart help = newList[i1];
                            newList[i1] = newList[i2];
                            newList[i2] = help;
                        }
                    }
                }
                // заносим id в массив айдишников //
                for (int j = 0; j < i; j++)
                    array[j] = newList[j].id;
            }

            if (comboBox1.Text == "По популярности") // просмотры товара
            {
                for (int i1 = 0; i1 < i; i1++)
                {
                    for (int i2 = 0; i2 < i; i2++)
                    {
                        if (newList[i1].like > newList[i2].like) // сортировка пузырьком
                        {
                            SparePart help = newList[i1];
                            newList[i1] = newList[i2];
                            newList[i2] = help;
                        }
                    }
                }
                // заносим id в массив айдишников //
                for (int j = 0; j < i; j++)
                    array[j] = newList[j].id;
            }
        }

        // функция вывода подсказки в поле для поиска //
        private void helpTextProductSearch()
        {
            textBox1.Text = "Введите запрос"; // текст подсказки
            textBox1.ForeColor = Color.Gray; // цвет подсказки
        }

        // при нажатии на поле поиска для последующего ввода //
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.ForeColor == Color.Gray)
            {
                textBox1.Text = null; // содержимое становится пустым
                textBox1.ForeColor = Color.Black; // цвет текста черный
            }
        }

        // если форма для поиска перестает быть активной //
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                helpTextProductSearch();
        }

        // анимации //
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.FromArgb(102, 252, 241);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
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

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.DarkRed;
            pictureBox6.BackColor = Color.DarkRed;
            label4.BackColor = Color.DarkRed;
            pictureBox6.BackgroundImage = Properties.Resources.КорзинаБелая;
            label4.ForeColor = Color.White;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(102, 252, 241);
            pictureBox6.BackColor = Color.FromArgb(102, 252, 241);
            label4.BackColor = Color.FromArgb(102, 252, 241);
            pictureBox6.BackgroundImage = Properties.Resources.Корзина;
            label4.ForeColor = Color.FromArgb(31, 40, 51);
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.DarkRed;
            pictureBox6.BackColor = Color.DarkRed;
            label4.BackColor = Color.DarkRed;
            pictureBox6.BackgroundImage = Properties.Resources.КорзинаБелая;
            label4.ForeColor = Color.White;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(102, 252, 241);
            pictureBox6.BackColor = Color.FromArgb(102, 252, 241);
            label4.BackColor = Color.FromArgb(102, 252, 241);
            pictureBox6.BackgroundImage = Properties.Resources.Корзина;
            label4.ForeColor = Color.FromArgb(31, 40, 51);
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.DarkRed;
            pictureBox6.BackColor = Color.DarkRed;
            label4.BackColor = Color.DarkRed;
            pictureBox6.BackgroundImage = Properties.Resources.КорзинаБелая;
            label4.ForeColor = Color.White;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(102, 252, 241);
            pictureBox6.BackColor = Color.FromArgb(102, 252, 241);
            label4.BackColor = Color.FromArgb(102, 252, 241);
            pictureBox6.BackgroundImage = Properties.Resources.Корзина;
            label4.ForeColor = Color.FromArgb(31, 40, 51);
        }

        // фильтр - тестовые данные //
        private void label5_Click(object sender, EventArgs e)
        {
            testData();
        }

        private void testData()
        {
            SparePart S1 = new SparePart()
            {
                name = "Литье",
                type = "Колеса",
                brend = "Ford",
                price = 12000,
                like = 0,
                number = 500,
                promoCode = 0,
                stock = 0,
                copy = false,
                path = @"C:\Users\Серебряков\Desktop\диски.png",
                cod = false,
            };

            SparePart S2 = new SparePart()
            {
                name = "Колодки",
                type = "Тормозные колодки",
                brend = "Volvo",
                price = 4000,
                like = 0,
                number = 400,
                promoCode = 0,
                stock = 0,
                copy = false,
                path = @"C:\Users\Серебряков\Desktop\тормоза.png",
                cod = false,
            };

            SparePart S3 = new SparePart()
            {
                name = "Лукойл 2л",
                type = "Тормозная жидкость",
                brend = "Ваз",
                price = 600,
                like = 0,
                number = 1500,
                promoCode = 0,
                stock = 0,
                copy = false,
                path = @"C:\Users\Серебряков\Desktop\жидкость.png",
                cod = false,
            };

            PromoCode promo = new PromoCode()
            {
                name = "123",
                discount = 15
            };
            db.PromoCodes.Add(promo);
            db.SaveChanges();
            S1.promoCode = promo.id;
            db.SpareParts.Add(S1);
            db.SaveChanges();

            Stock stock = new Stock()
            {
                name = "скидон",
                discount = 10,
            };
            db.Stocks.Add(stock);
            db.SaveChanges();
            S3.stock = stock.id;          
            db.SpareParts.Add(S2);
            db.SpareParts.Add(S3);
            db.SaveChanges();
            MessageBox.Show("Тестовые данные загруженны");
            printData();
        }

        // основной флиьтр //
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            printData();
        }

        // фильтр типа продукции //
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            printData();
        }

        // фильтр по бренду //
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            printData();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            printData();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            printData();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            printData();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            printData();
        }

        // сбросить //
        private void button5_Click(object sender, EventArgs e)
        {
            filtrData();
        }

        // найти запчасть //
        private void button4_Click(object sender, EventArgs e)
        {
            printData();
        }

        // кнопка в корзину //
        private void button_Clicked(object sender, EventArgs e)
        {
            if (user == 0)
            {
                Authorization authorization = new Authorization();
                authorization.ShowDialog();
                userInfo();
            }
            else
            {
                Button Button = (Button)sender;
                idProduct = Convert.ToInt32(((System.Windows.Forms
                        .Button)sender).Name); // получаем айдишник выбранного продукта

                // поиск кол-во товара для покупки //
                foreach (Control contrl in panel1.Controls)
                {
                    if (contrl.GetType().GetProperty("Value") != null)
                    {
                        if(contrl.Name == idProduct.ToString())
                        {
                            numProduct = Convert.ToInt32(contrl.Text);
                            break;
                        }
                    }
                }

                Code code = new Code();
                code.ShowDialog();
                userInfo();
                printData();
            }
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            Button Button = (Button)sender;
            Button.BackColor = Color.DarkRed;
            Button.ForeColor = Color.White;
        }
        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button Button = (Button)sender;
            Button.BackColor = Color.FromArgb(69, 162, 158);
            Button.ForeColor = Color.FromArgb(31, 40, 51);
        }

        // контакты //
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Курсовая работа на тему: 'Информационные системы в сфере магазина автозапчастей'" +
                    $"\n\nВыполнил студент группы ИТ-202\nНикита Сергеев", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // условия пользования //
        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"1.1. В настоящем Соглашении и вытекающих или связанных с ним отношениях сторон применяются следующие термины и определения: " +
                $"\n1.1.1.Соглашение – это пользовательское соглашение между Правообладателем и Пользователем, устанавливающее правила пользования Приложением. " +
                $"\n1.1.2.Приложение – это мобильное приложение «Акселератор продаж», предназначенное для установки и использования на мобильном телефоне, планшете или ином устройстве, позволяющем использовать Мобильное приложение по его функциональному назначению. " +
                $"\n1.1.3.Правообладатель / Компания – это Общество с ограниченной ответственностью «Бизинтек», зарегистрированное под основным государственным регистрационным номером 1187746026753, которое предоставляет право использовать Приложение." +
                $"\n1.1.4.Пользователь – это физическое лицо, принимающее условия Пользовательского Соглашения, обладающее полной дееспособностью, имеющее свое собственное мобильное устройство с закрепленным за ним телефонным номером мобильного устройства." +
                $"\n1.1.5.Устройство – это мобильный̆ телефон, планшет или иное устройство, позволяющее использовать Приложение по его функциональному назначению." +
                $"\n1.1.6.Авторизация пользователя – это процесс проверки и предоставления доступа к сервисам Приложения на основании введенных пользователем реквизитов(фамилия, имя, отчество, логин, пароль и / или другие)." +
                $"\n1.1.7.Регистрация в Приложении – это совокупность мероприятий, выполняемых Пользователем, по введению необходимых данных в соответствующие поля Приложения.Регистрация Пользователем в Приложении означает подтверждение Пользователя соответствия требованиям, предъявляемым к Пользователю, изложенным в настоящем Соглашении." +
                $"\n1.1.8.Учётная запись – это сведения, необходимые для идентификации Пользователя при подключении к Приложению, информация для авторизации и учёта." +
                $"\n1.1.9.Персональные данные – это любая информация, относящаяся к прямо или косвенно определенному или определяемому физическому лицу(субъекту персональных данных)." +
                $"\n1.1.10.Идентификационные данные – это логин и пароль, а также иные данные(коды доступа и пр.), введение которых Пользователем позволяет идентифицировать Пользователя." +
                $"\n1.1.11.Лицензия на программное обеспечение – это правовой инструмент, определяющий использование и распространение программного обеспечения, защищённого авторским правом." +
                $"\n1.1.12.Лицензионный договор – это договор, по которому одна сторона – обладатель исключительного права на результат интеллектуальной деятельности или на средство индивидуализации(Лицензиар) предоставляет или обязуется предоставить другой стороне(Лицензиату) право использования такого результата или такого средства в предусмотренных договором пределах." +
                $"\n1.1.13.Лицензионное вознаграждение – это передача денежных средств от Лицензиата к Лицензиару за использование Лицензии и объекта Лицензионного договора." +
                $"\n1.1.14.Сервис – это комплекс услуг, предоставляемых Пользователю с использованием Приложения." +
                $"Контент – это изображения, текстовые, аудио - и видеоматериалы, а также сообщения любого характера, содержащиеся в Мобильном приложении." +
                $"\n1.1.15.Контент – это изображения, текстовые, аудио - и видеоматериалы, а также сообщения любого характера, содержащиеся в Мобильном приложении." +
                $"\n1.1.16.Pipeline – это список сделок в работе сотрудника, которые находятся на разных стадиях завершения с разной вероятностью оплаты." +
                $"\n1.1.17.Dashboard – это программное обеспечение Apple Inc, содержащее небольшие утилиты, называемые «виджетами»." +
                $"\n1.1.18.Ключевой показатель эффективности(KPI) – это показатель деятельности организации, который помогает компании достигать стратегические и тактические цели.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (user == 0)
            {
                Authorization authorization = new Authorization();
                authorization.ShowDialog();
                userInfo();
                printData();
            }
            else
            {
                user = 0;
                userInfo();
            }
            
        }

        // проверка вошел пользователь в аккаунт или нет //
        private void userInfo()
        {
            if (user != 0)
            {
                foreach (Client C in db.Clients)
                {
                    if (C.id == user)
                    {
                        label1.Text = $"{C.phone} выйти";
                        foreach (Buyer B in db.Buyers.Include(S=>S.SpareParts))
                        {
                            if (B.id == C.buyerId)
                            {
                                int num = 0;
                                foreach (SparePart S in B.SpareParts)
                                    num++;
                                label4.Text = $"В корзине товаров - {num}";
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                label4.Text = "В корзине товаров - 0";
                label1.Text = "- Авторизоваться";
            }                
        }

        // обратная связь //
        private void label2_Click(object sender, EventArgs e)
        {
            Mess mess = new Mess();
            mess.ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            openProfile();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            openProfile();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            openProfile();
        }

        // вход в профиль //
        private void openProfile()
        {
            if (user == 0)
            {
                Authorization authorization = new Authorization();
                authorization.ShowDialog();
                userInfo();
                printData();
            }
            else
            {
                Profile profile = new Profile();
                profile.ShowDialog();
                printData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openProfile();
        }

        // администратор //
        private void button3_Click(object sender, EventArgs e)
        {
            AdminOpen adminOpen = new AdminOpen();
            adminOpen.ShowDialog();
            userInfo();
            printData();
        }
    }
}
