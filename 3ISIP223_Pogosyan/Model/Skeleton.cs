using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal class Skeleton : Enemy
    {
        public Skeleton(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            UniqSkill = "Игнор брони";
        }

        public override void AttackInfo(double atack)
        {
            Console.WriteLine($"\n{Name} проходит сквозь вашу защиту! Ваши доспехи бесполезны! {atack} урона!");
        }

        public override void LastWord()
        {
            Console.WriteLine("Скелет рассыпается в кучу костей с оглушительным лязгом.");
        }
    }

}
