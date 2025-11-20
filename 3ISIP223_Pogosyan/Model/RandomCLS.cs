using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal static class RandomCLS
    {
        static private Random Random = new Random();

        static public int Next(int min, int max) {
            return Random.Next(min, max);
        }
        
    }
}
