using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanHouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //foreach (var arg in Core.Salon.Clients.ToList())
            //{
            //    Console.WriteLine(arg.Name);
            //}
            Game game = new Game();
            game.StartGame();
            Console.WriteLine(game.Money);
        }
    }

    class WorkingWithDatabase
    {
        public Salon salon;
        public WorkingWithDatabase()
        {
            salon = Core.MYSalon.Salons.FirstOrDefault();

            if (salon == null)
            {
                salon = new Salon
                {
                    Name = "MY Salon",
                    Money = 10000,
                };
                Core.MYSalon.Salons.Add(salon);
                Core.MYSalon.SaveChanges();
            }

        }

        public double GetMoney()
        {

            return salon.Money;
        }
    }

    class Game
    {
        public WorkingWithDatabase Database { get; set; }
        public double Money => Database.GetMoney();
        public Game()
        {
            Database = new WorkingWithDatabase();
            
        }

        public void StartMenu()
        {

        }


        public void StartGame()
        {
            string n = "0";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu");

                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }
    }
}
