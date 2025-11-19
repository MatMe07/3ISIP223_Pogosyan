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
            return new VVG("ВВГ", random.Next(10, 25), random.Next(12, 15), random.Next(30, 50));
        }
    }
}
