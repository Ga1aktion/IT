using System.Collections.Generic;

namespace FIX_Autoparts
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; } // телефон и он же логин для входа в профиль
        public string address { get; set; } // адрес доставки, доставка скаладывается от суммы (стоимость товара * 0.05)
        public string password { get; set; } // пароль для входа в личный кабинет
        public int buyerId { get; set; } // корзина покупателя
        public ICollection<Cheque> Cheques { get; set; } // список чеков клиента
        public Client()
        {
            Cheques = new List<Cheque>();
        }
    }
}
