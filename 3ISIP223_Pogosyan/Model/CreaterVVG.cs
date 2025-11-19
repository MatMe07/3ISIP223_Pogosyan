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
            return new VVG("ВВГ", 10, 3, 20);
        }
    }
}
