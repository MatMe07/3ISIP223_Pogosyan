using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3ISIP223_PogosyanWPF
{
    public partial class Product
    {
        public bool DiscountMore15
        {
            get
            {
                Console.WriteLine($"{Name}: discount = {Discount}");
                return Discount > 15;
            }
        }

        public decimal PriceWithDiscount => Price - (Price * Discount / 100);
    }
}
