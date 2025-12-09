using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    internal class Engine
    {
        public string Type { get; set; }
        public decimal Price { get; set; }

        public Engine(string type, decimal price) {
            Type = type;
            Price = price;
        }
    }
}
