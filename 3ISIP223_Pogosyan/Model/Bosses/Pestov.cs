using _3ISIP223_Pogosyan.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model.Bosses
{
    internal class Pestov : Magician
    {
        public Pestov(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            HP *= 1.3;
            MaxHP *= 1.3;
            Attack *= 1.8;
            Defense *= 0.6;
            ProcentFrozen += 15;
            UniqSkill = "Усиленная заморозка";
        }
        public override void Demo()
        {
            Console.WriteLine("Тень отделяется от стены - это ПЕСТОВ С--! Движется неестественно, как сломанная анимация.\n");
        }
    }

}
