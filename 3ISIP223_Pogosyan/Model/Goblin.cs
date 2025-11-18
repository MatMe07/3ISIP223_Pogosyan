using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal class Goblin : Enemy
    {
        public double ProcentKritAttack { get; set; }
        public bool IsKritAttack { get; set; }
        public bool KritAttack => random.Next(0, 100) < ProcentKritAttack;

        public Goblin(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            ProcentKritAttack = 15;
            UniqSkill = "Шанс критического удара";
            IsKritAttack = false;
        }
        public override void AttackInfo(double atack)
        {
            if (IsKritAttack)
            {
                IsKritAttack = false;
                Console.WriteLine($"\n{Name} издает боевой клич и наносит подлый удар! КРИТИЧЕСКИЙ УРОН! {atack} урона!");
            }
            else
            {
                Console.WriteLine($"\n{Name} атакует вас! Вы получаете {atack} урона.");

            }
        }

        public override void LastWord()
        {
            Console.WriteLine("Гоблин издает предсмертный визг и падает замертво.");
        }

    }

}
