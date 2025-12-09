using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    internal class CarModel
    {
        public string Name { get; set; }
        public List<Engine> Engines { get; set; }
        public decimal BasePrice {  get; set; }

        public CarModel(string name, List<Engine> engine, decimal price) {
            Name = name;
            Engines = engine;   
            BasePrice = price;
        }
    }
}
