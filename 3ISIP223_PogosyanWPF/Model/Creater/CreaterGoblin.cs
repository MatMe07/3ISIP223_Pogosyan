using _3ISIP223_PogosyanWPF.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF.Model.Creater
{
    public class CreaterGoblin : Factory
    {
        public override Enemy CreateEnemy(ModelUIElement3D model = null)
        {
            return new Goblin("Гоблин", 12, 3, 30, model, "pack://application:,,,/Images/EnemAttack/Goblin/goblinObich.png", "pack://application:,,,/Images/EnemAttack/Goblin/goblinAttack.png", "pack://application:,,,/Images/EnemAttack/Goblin/goblinPoluch.png");
        }

    }
}
