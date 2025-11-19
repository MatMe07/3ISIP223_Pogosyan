using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3ISIP223_Pogosyan.Model.Units;

namespace _3ISIP223_Pogosyan.Model
{
    internal class CreaterSkeleton : Factory
    {
        public override Enemy CreateEnemy()
        {
            return new Skeleton("Скелет", random.Next(7, 15), random.Next(1, 5), random.Next(14, 23));
        }
    }
}
