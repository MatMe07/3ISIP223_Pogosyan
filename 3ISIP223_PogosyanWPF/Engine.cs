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
        public double Price { get; set; }

        public Engine(string type, double price) {
            Type = type;
            Price = price;
        }
    }
}
