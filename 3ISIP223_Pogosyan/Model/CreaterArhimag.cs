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
            return new Arhimag("Архимаг C++", RandomCLS.Next(15, 25), RandomCLS.Next(10, 15), RandomCLS.Next(30, 60));
        }
    }
}
