using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    internal class DopOptinon
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public DopOptinon(string name, double price)
        {
            Name = name;    
            Price = price;
        }
    }
}
