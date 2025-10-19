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
            List<Client> cl = Core.context.Client.ToList();
            foreach (var arg in cl)
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
