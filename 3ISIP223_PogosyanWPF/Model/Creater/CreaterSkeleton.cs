using _3ISIP223_PogosyanWPF.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF.Model.Creater
{
    public class CreaterSkeleton : Factory
    {
        public override Enemy CreateEnemy(ModelUIElement3D model)
        {
            return new Skeleton("Скелет", 10, 5, 40, model, "pack://application:,,,/Icons/Armor.png", "pack://application:,,,/Icons/Armor1.png");

        }
    }
}
