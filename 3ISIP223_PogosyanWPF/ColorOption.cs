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
        public double Price { get; set; }

        public ColorOption(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
