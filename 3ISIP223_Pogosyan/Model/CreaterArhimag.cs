using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3ISIP223_Pogosyan.Model.Bosses;

namespace _3ISIP223_Pogosyan.Model
{
    internal class CreaterArhimag : Factory
    {
        public override Enemy CreateEnemy()
        {
            return new Arhimag("Архимаг C++", random.Next(15, 25), random.Next(10, 15), random.Next(30, 60));
        }
    }
}
