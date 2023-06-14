namespace FIX_Autoparts
{
    public class SparePart
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string brend { get; set; }
        public double price { get; set; } // стоимость за 1 штуку
        public int like { get; set; } // количество купленного товара (популярность)
        public int number { get; set; } // количество на складе
        public int promoCode { get; set; } // промокод. 0 - нет его, 1 - есть
        public int stock { get; set; } // акция. 0 - нет, 1 - есть
        // акция и промокод на товар может быть только по 1 //
        public bool copy { get; set; } // переменная копирования для корзины
        public bool cod { get; set; } // переменная заюзания промокода
        public string path { get; set; } // путь к фото
    }
}
