using _3ISIP223_Pogosyan.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model.Bosses
{
    internal class VVG : Goblin
    {

        public VVG(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            HP *= 2.0;
            MaxHP *= 2.0;
            Attack *= 1.5;
            Defense *= 1.2;
            ProcentKritAttack += 10;
            UniqSkill = "Усиленный критический удар";
        }
        public override void Demo()
        {

            Console.WriteLine("Из тени появляется МАССИВНЫЙ гоблин в ржавых доспехах - это ВВГ!\n");
        }
    }

}
