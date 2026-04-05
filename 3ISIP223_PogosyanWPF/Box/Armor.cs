using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public class Armor : ItemChest
    {
        public double ArmorHP { get; set; }
        public Armor(string name, double armorHP, string path) : base(name, path)
        {
            ArmorHP = armorHP;
        }
    }
}
