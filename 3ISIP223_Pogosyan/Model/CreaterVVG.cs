using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3ISIP223_Pogosyan.Model.Bosses;

namespace _3ISIP223_Pogosyan.Model
{
    internal class CreaterVVG : Factory
    {
        public override Enemy CreateEnemy()
        {
            return new VVG("ВВГ", RandomCLS.Next(10, 25), RandomCLS.Next(12, 15), RandomCLS.Next(30, 50));
        }
    }
}
