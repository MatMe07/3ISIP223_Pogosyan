using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    internal class ColorOption
    {
        public string Name {  get; set; }
        public decimal Price { get; set; }

        public ColorOption(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
