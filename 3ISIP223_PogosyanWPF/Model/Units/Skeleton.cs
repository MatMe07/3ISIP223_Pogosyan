using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.Model.Units
{
    public class Skeleton : Enemy
    {
        public Skeleton(string name, double attack, double defense, double hp, System.Windows.Media.Media3D.ModelUIElement3D mod, string imgQuiet, string imgAttack) : base(name, attack, defense, hp, mod, imgQuiet, imgAttack)
        {
            UniqSkill = "Игнор брони";
        }

        public override void AttackInfo(double atack)
        {
            Console.WriteLine($"\n{Name} проходит сквозь вашу защиту! Ваши доспехи бесполезны! {atack} урона!");
        }

        public override string LastWord()
        {
            Console.WriteLine("Скелет рассыпается в кучу костей с оглушительным лязгом.");
            return "Скелет рассыпается в кучу костей с оглушительным лязгом.";
        }
    }
}
