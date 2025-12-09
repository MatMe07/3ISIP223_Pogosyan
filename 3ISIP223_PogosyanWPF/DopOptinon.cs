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
        public decimal Price { get; set; }

        public DopOptinon(string name, decimal price)
        {
            Name = name;    
            Price = price;
        }
    }
}
