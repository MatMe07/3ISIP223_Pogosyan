using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

        public double GetMoney() => salon.Money;
        public string GetName() => salon.Name;
    }

    class Game
    {
        public WorkingWithDatabase Database { get; set; }
        public double Money => Database.GetMoney();
        public int Width { get; private set; } = 60;
        public Game()
        {
            Database = new WorkingWithDatabase();

        }
        public string CenterText(string text, int width)
        {
            int left = (Width - text.Length - 2) / 2;
            int right = Width - text.Length - left - 2;
            return $"|{new string(' ', left)}{text}{new string(' ', right)}|";
        }
        public void StartMenu()
        {
            string line = new string('=', Width);
            Console.WriteLine(line);
            Console.WriteLine(CenterText(Database.GetName(), Width));
            Console.WriteLine(line);
            Console.WriteLine($"| {$"Текущий баланс:     {Database.GetMoney()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Клиентов в очереди: {Database.GetMoney()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Состояние склада:   {Database.GetMoney()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Доступные действия:".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[1] Обслужить следующего клиента".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[2] Управление складом".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[3] Закупка запчастей".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[4] Просмотр статистики".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[5] Финансовая отчетность".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[0] Выход из программы".PadRight(Width - 4)} |");
            Console.WriteLine(line);
        }

        public void Input(string text, out int n, int start, int end)
        {
            while (true)
            {
                Console.Write($"{text}:\n> ");
                if (int.TryParse(Console.ReadLine(), out n))
                {
                    for (int i = start; i <= end; i++)
                    {
                        if (n == i) return;
                    }
                }
                Console.WriteLine("Неверное действие!\n");
            }
        }

        public void StartGame()
        {
            int n = 0;
            while (true)
            {
                Console.Clear();
                StartMenu();
                Input("Выберите действие", out n, 0, 5);
                switch (n)
                {
                    case 0:
                        {
                            break;
                        }
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
                            break;
                        }
                    default:
                        {

                            break;
                        }
                }
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();

            }
        }
    }
}
