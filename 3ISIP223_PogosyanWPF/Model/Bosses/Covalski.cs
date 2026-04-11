using _3ISIP223_PogosyanWPF.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.Model.Bosses
{
    public class Covalski : Skeleton
    {
        public Covalski(string name, double attack, double defense, double hp, System.Windows.Media.Media3D.ModelUIElement3D mod, string imgQuiet, string imgAttack, string imgPoluch) : base(name, attack, defense, hp, mod, imgQuiet, imgAttack, imgPoluch)
        {
            HP *= 2.5;
            MaxHP *= 2.5;
            Attack *= 1.3;
            Defense *= 1.4;
            UniqSkill = "Игнор брони";
        }
        public override void Demo()
        {
            Console.WriteLine("Кости с грохотом слагаются в исполинскую фигуру - это КОВАЛЬСКИЙ!!\n");
        }
    }
}
