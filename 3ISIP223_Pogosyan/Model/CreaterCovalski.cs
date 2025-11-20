using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3ISIP223_Pogosyan.Model.Bosses;

namespace _3ISIP223_Pogosyan.Model
{
    internal class CreaterCovalski : Factory
    {
        public override Enemy CreateEnemy()
        {
            return new Covalski("Ковальский", RandomCLS.Next(15, 25), RandomCLS.Next(10, 20), RandomCLS.Next(40, 70));
        }
    }
}
