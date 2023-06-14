using System.Collections.Generic;

namespace FIX_Autoparts
{
    public class Buyer
    {
        public int id { get; set; }
        public double price { get; set; }
        public ICollection<SparePart> SpareParts { get; set; } // список товаров
        public Buyer()
        {
            SpareParts = new List<SparePart>();
        }
    }
}
