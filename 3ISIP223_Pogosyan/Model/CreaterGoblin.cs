using _3ISIP223_Pogosyan.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _3ISIP223_Pogosyan.Model
{
    internal class CreaterGoblin : Factory
    {
        public override Enemy CreateEnemy()
        {
            return new Goblin("Гоблин", random.Next(10, 20), random.Next(3, 7), random.Next(15, 25));
        }

    }
}
