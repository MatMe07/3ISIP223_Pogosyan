using _3ISIP223_PogosyanWPF.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF.Model.Creater
{
    public class CreaterMag : Factory
    {
        public override Enemy CreateEnemy(ModelUIElement3D model = null)
        {
            return new Magician("Маг",15, 2, 25, model, "pack://application:,,,/Images/EnemAttack/Mag/MagObich.png", "pack://application:,,,/Images/EnemAttack/Mag/MagUdar.png", "pack://application:,,,/Images/EnemAttack/Mag/MagPoluch.png");

        }
    }
}
