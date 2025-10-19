using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            foreach (var arg in Core.context.Client.ToList())
            {
                Console.WriteLine(arg.Name);
            }
        }
    }


    class WorkingWithDatabase
    {
        public WorkingWithDatabase() 
        {
            
        }

        public double GetMoney()
        {
            return 2.6;
        } 
    }

    class Game
    {
        public WorkingWithDatabase WorkingWithDatabase { get; set; }
        public double Money => WorkingWithDatabase.GetMoney();
    }

}
