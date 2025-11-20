using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model.Units
{
    internal class Slug : Enemy
    {
        
        public Slug(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            UniqSkill = "";
        }
        public override void AttackInfo(double attack)
        {
            Console.WriteLine($"\n{Name} атакует вас! Вы получаете {attack} урона.");
        }

        public override void LastWord()
        {
            Console.WriteLine("Слизень падает замертво.");
        }
    }
}
