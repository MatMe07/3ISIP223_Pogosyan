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
            
        }
    }


    class WorkingWithDatabase
    {
        public WorkingWithDatabase() 
        {
            
        }

        public double GetMoney()
        {
            return Core.context.Salon
        } 
    }

    class Game
    {
        public WorkingWithDatabase WorkingWithDatabase { get; set; }
        public double Money => WorkingWithDatabase.GetMoney();
    }

}
