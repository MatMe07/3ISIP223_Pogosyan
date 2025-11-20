using _3ISIP223_Pogosyan.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal class CreaterSlug : Factory
    {
        public CreaterSlug() { }
        public override Enemy CreateEnemy()
        {
            return new Slug("Слизень", RandomCLS.Next(7, 15), RandomCLS.Next(1, 5), RandomCLS.Next(14, 23) );
        }
    }
}
