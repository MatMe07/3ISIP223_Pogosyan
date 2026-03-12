using _3ISIP223_PogosyanWPF.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.Model.Creater
{
    internal class CreaterGoblin : Factory
    {
        public override Enemy CreateEnemy()
        {
            return new Goblin("Гоблин", RandomCLS.Next(10, 20), RandomCLS.Next(3, 7), RandomCLS.Next(15, 25));
        }

    }
}
