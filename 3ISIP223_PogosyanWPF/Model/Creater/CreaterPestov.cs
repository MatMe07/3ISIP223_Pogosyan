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
        public override Enemy CreateEnemy(ModelUIElement3D model)
        {
            return new Pestov("Гоблин", RandomCLS.Next(10, 20), RandomCLS.Next(3, 7), RandomCLS.Next(4, 50), model);

        }

    }
}
