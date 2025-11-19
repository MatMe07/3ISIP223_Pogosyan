using _3ISIP223_Pogosyan.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model.Bosses
{
    internal class Arhimag : Magician
    {
        public Arhimag(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            HP *= 1.8;
            MaxHP *= 1.8;
            Attack *= 1.6;
            Defense *= 1.1;
            ProcentFrozen += 10;
            UniqSkill = "Усиленная заморозка";
        }
        public override void Demo()
        {
            Console.WriteLine("Воздух замерзает. По стенам расползается иней. Это АРХИМАГ C++!\n");
        }
    }

}
