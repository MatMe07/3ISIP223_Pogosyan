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
            return new Covalski("Ковальский", random.Next(15, 25), random.Next(10, 20), random.Next(40, 70));
        }
    }
}
