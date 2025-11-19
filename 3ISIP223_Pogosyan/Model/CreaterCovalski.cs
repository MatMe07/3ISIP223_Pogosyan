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
            return new Covalski("Ковальский", 12, 2, 20);
        }
    }
}
