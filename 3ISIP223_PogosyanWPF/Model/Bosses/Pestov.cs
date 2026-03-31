using _3ISIP223_PogosyanWPF.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF
{
    public class Pestov : Skeleton
    {
        public Pestov(string name, double attack, double defense, double hp, ModelUIElement3D mod, string imgQuiet, string imgAttack) : base(name, attack, defense, hp, mod, imgQuiet, imgAttack)
        {
            HP *= 1.3;
            MaxHP *= 1.3;
            Attack *= 1.8;
            Defense *= 0.6;
            //ProcentFrozen += 15;
            UniqSkill = "Игнор брони";
        }
        public override void Demo()
        {
            Console.WriteLine("Кости с грохотом слагаются в исполинскую фигуру - это ПЕСТОВ!!\n");
        }
    }
}
