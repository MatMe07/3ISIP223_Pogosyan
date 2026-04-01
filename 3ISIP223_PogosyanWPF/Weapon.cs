using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public class Weapon : ItemChest
    {
        public double Attack { get; set; }
        public Weapon(string name, double attack, string path) : base(name, path)
        {
            Attack = attack;
        }
    }
}
