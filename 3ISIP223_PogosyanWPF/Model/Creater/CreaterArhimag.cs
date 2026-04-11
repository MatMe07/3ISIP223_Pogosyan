using _3ISIP223_PogosyanWPF.Model.Bosses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF.Model.Creater
{
    public class CreaterArhimag : Factory
    {
        public override Enemy CreateEnemy(ModelUIElement3D model = null)
        {
            return new Arhimag("Архимаг C++", 15, 2, 25, model, "pack://application:,,,/Images/EnemAttack/Arhimag/ArchimagProsto.png", "pack://application:,,,/Images/EnemAttack/Arhimag/ArchimagUdar.png", "pack://application:,,,/Images/EnemAttack/Arhimag/ArchimagPoluch.png");

        }
    }
}
