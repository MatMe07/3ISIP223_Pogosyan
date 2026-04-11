using _3ISIP223_PogosyanWPF.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF.Model.Creater
{
    public class CreaterPestov : Factory
    {
        public override Enemy CreateEnemy(ModelUIElement3D model = null)
        {
            return new Pestov("Пестов С--", 10, 5, 40, model, "pack://application:,,,/Images/EnemAttack/Pestov/PestovProsto.png", "pack://application:,,,/Images/EnemAttack/Pestov/PestovUdar.png", "pack://application:,,,/Images/EnemAttack/Pestov/PestovPoluch.png");

        }

    }
}
