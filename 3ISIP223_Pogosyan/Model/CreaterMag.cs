using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3ISIP223_Pogosyan.Model.Units;

namespace _3ISIP223_Pogosyan.Model
{
    internal class CreaterMag : Factory
    {
        public override Enemy CreateEnemy()
        {
            return new Magician("Маг", RandomCLS.Next(10, 16), RandomCLS.Next(1, 4), RandomCLS.Next(12, 19));
        }

    }
}
