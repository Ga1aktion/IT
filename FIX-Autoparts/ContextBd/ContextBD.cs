using System.Data.Entity;

namespace FIX_Autoparts
{
    public class ContextBD : DbContext
    {
        public ContextBD() : base("StoreCarV7")
        { }

        // заносим в БД наши модели (таблицы)
        public DbSet<Buyer> Buyers { get; set; } // корзины
        public DbSet<Cheque> Cheques { get; set; } // чеки
        public DbSet<Client> Clients { get; set; } // клиенты
        public DbSet<PromoCode> PromoCodes { get; set; } // промокоды
        public DbSet<SparePart> SpareParts { get; set; } // запчасти
        public DbSet<Stock> Stocks { get; set; } // акции
        public DbSet<Message> Messages { get; set; } // сообщения админам
    }
}
