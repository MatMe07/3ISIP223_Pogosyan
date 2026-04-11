using _3ISIP223_PogosyanWPF.Model.Bosses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF.Model.Creater
{
    public class CreaterVVG : Factory
    {
        public override Enemy CreateEnemy(ModelUIElement3D model = null)
        {
            return new VVG("ВВГ", 12, 3, 30, model, "pack://application:,,,/Images/EnemAttack/VVG/VVGProsto.png", "pack://application:,,,/Images/EnemAttack/VVG/VVGUdar.png", "pack://application:,,,/Images/EnemAttack/VVG/VVGPoluch.png");

        }
    }
}
