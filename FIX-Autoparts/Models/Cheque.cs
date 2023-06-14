using System;

namespace FIX_Autoparts
{
    public class Cheque
    {
        public int id { get; set; }
        public double price { get; set; } // стоимость покупки
        public DateTime date { get; set; } // дата покупки
        public bool delivery { get; set; } // с доставкой или без
    }
}
