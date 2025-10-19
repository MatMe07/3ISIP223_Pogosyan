using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public Order order;
        public List<ClientQueue> clientQueues;
        public List<Zapchast> zapchasts;
        public List<Sklad> sklad;

        public WorkingWithDatabase()
        {
            salon = Core.MYSalon.Salons.FirstOrDefault();
            zapchasts = Core.MYSalon.Zapchasts.ToList();
            sklad = Core.MYSalon.Sklads.ToList();
            clientQueues = Core.MYSalon.ClientQueues.OrderBy(s => s.Position).ToList();

            if (salon == null)
            {
                salon = new Salon
                {
                    Name = "MY Salon",
                    Money = 10000,
                    Nacenka = .3,
                    PenaltyOtkaz = 500,
                    PenaltyWrong = 2.0,
                    CountClients = 0,
                    SuccessfulRemont = 0,
                    CountFaild = 0,
                    CountOtkaz = 0,
                };
                Core.MYSalon.Salons.Add(salon);
                Core.MYSalon.SaveChanges();
            }

            //order = CreateOrderFromQueue(GetNextClient());

        }

        public int GetCountINSkladZapchast() => sklad.Sum(s => s.Count);
        public int GetCountINZapchastDelivery() => sklad.Sum(s => s.InDelivery);
        public int GetCountClientQueue() => clientQueues.Count;
        public int GetZapchastCount(int id)
        {
            var skladIt = sklad.FirstOrDefault(s => s.ID_Zapchast== id);
            return skladIt == null ? 0 : skladIt.Count;
        }
        public bool CheckZapchast(int id)
        {

            return true;
        }

        public ClientQueue GetNextClient()
        {
            return clientQueues.FirstOrDefault();
        }

        public Order CreateOrderFromQueue(ClientQueue clientQueue)
        {
            //Console.WriteLine()
            var zapchast = zapchasts.FirstOrDefault(s => s.ID_Zapchast == clientQueue.ID_Zapchast);
            //Console.WriteLine("hello");
            if (zapchast == null) return null;
            //Console.WriteLine($"{clientQueue.ID_Client}, {clientQueue.ID_Zapchast}, {zapchast.SalePrice}, {DateTime.Now}");
            var order = new Order
            {
                ID_Client = clientQueue.ID_Client,
                ID_Zapchast = clientQueue.ID_Zapchast,
                Price = zapchast.SalePrice,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };

            return order;
        }

        public double GetMoney() => salon.Money;
        public string GetName() => salon.Name;
    }

    class Game
    {
        public WorkingWithDatabase db { get; set; }
        public double Money => db.GetMoney();
        public int Width { get; private set; } = 60;
        public Game()
        {
            db = new WorkingWithDatabase();

        }
        public string CenterText(string text, int width)
        {
            int left = (Width - text.Length - 2) / 2;
            int right = Width - text.Length - left - 2;
            return $"|{new string(' ', left)}{text}{new string(' ', right)}|";
        }
        public void StartMenu()
        {
            Console.Clear();
            string line = new string('=', Width);
            Console.WriteLine(line);
            Console.WriteLine(CenterText(db.GetName(), Width));
            Console.WriteLine(line);
            Console.WriteLine($"| {$"Текущий баланс:     {db.GetMoney()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Клиентов в очереди: {db.GetCountClientQueue()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Состояние склада:   {db.GetCountINSkladZapchast()} дет.".PadRight(Width - 4)} |");
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
        public void ServiceMenu()
        {
            Console.Clear();

            string line = new string('=', Width);
            Console.WriteLine(line);
            Console.WriteLine(CenterText("ОБСЛУЖИВАНИЕ КЛИЕНТА", Width));
            Console.WriteLine(line);
            var nextClient = db.GetNextClient();
            //if (nextClient != null)
            //{
            var order = db.CreateOrderFromQueue(nextClient);
            bool hasZapchast = db.CheckZapchast(order.ID_Zapchast);
            int zapchastCount = db.GetZapchastCount(order.ID_Zapchast);
            //}
            
            Console.WriteLine($"| {$"Клиент:            {nextClient.Client.Name}".PadRight(Width - 4)} |");
            //}
            Console.WriteLine($"| {$"Требуемая деталь:  {nextClient.Zapchast.Name}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Стоимость ремонта: {order.Price} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            if (hasZapchast)
            {
                Console.WriteLine($"| {$"Статус детали:     В НАЛИЧИИ ({zapchastCount} шт.)".PadRight(Width - 4)} |");
                Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
                Console.WriteLine($"| {$"Варианты действий:".PadRight(Width - 4)} |");
                Console.WriteLine($"| {$"[1] Выполнить ремонт".PadRight(Width - 4)} |");
            }
            else { 

                Console.WriteLine($"| {$"Статус детали:     ОТСУТСТВУЕТ НА СКЛАДЕ".PadRight(Width - 4)} |");
                Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
                Console.WriteLine($"| {$"Варианты действий:".PadRight(Width - 4)} |");
                Console.WriteLine($"| {$"[1] Заказать деталь и принять заказ (риск)".PadRight(Width - 4)} |");
            }
            Console.WriteLine($"| {$"[2] Отказать в обслуживании".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[3] Вернуться в главное меню".PadRight(Width - 4)} |");
            Console.WriteLine(line);

            int n = 0;
            Input("Ваш выбор", out n, 1, 3);
            switch (n)
            {
                case 1:
                    {
                        if (hasZapchast)
                        {

                        }
                        else
                        {

                        }
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
                default:
                    {
                        break;
                    }
            }

        }
        public void ManageSkladMenu()
        {
            Console.Clear();
            string lineRavno = new string('=', Width);
            string line = new string('-', Width-2);
            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("УПРАВЛЕНИЕ СКЛАДОМ", Width));
            Console.WriteLine(lineRavno);
            int col1 = (Width - 6) / 2;
            int col2 = Width / 6;
            int col3 = Width -4;
            Console.WriteLine($"| {$"{"Деталь".PadRight(col1)}| {"Наличие".PadRight(col2)} | В доставке".PadRight(col3)} |");
            Console.WriteLine($"|{line}|");
            foreach( var element in db.sklad)
            {
                Console.WriteLine($"| {$"{element.Zapchast.Name.PadRight(col1)}| {element.Count.ToString().PadRight(col2)} | {(element.InDelivery == 0 ? "-" : element.InDelivery.ToString())}".PadRight(col3)} |");
            }
            Console.WriteLine($"|{line}|");
            Console.WriteLine($"| {$"Итого на складе: {db.GetCountINSkladZapchast()} дет.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Ожидается поставок: {db.GetCountINZapchastDelivery()} дет.".PadRight(Width - 4)} |");

            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[1] Вернуться в главное меню".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            int n = 0;
            Input("Ваш выбор", out n, 1, 1);
            switch (n)
            {
                case 1:
                    {
                        break;
                    }
            }
        }
        public void BuyZapchastMenu()
        {
            Console.Clear();
            string lineRavno = new string('=', Width);
            string line = new string('-', Width - 2);
            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("УПРАВЛЕНИЕ СКЛАДОМ", Width));
            Console.WriteLine(lineRavno);
            int col0 = 3;
            int col1 = (Width - 9) / 2;
            int col2 = Width / 7;
            int col3 = Width - col2-1;
            Console.WriteLine($"| {"№".ToString().PadRight(col0)}| {$"{"Деталь".PadRight(col1)}| {"Цена".PadRight(col2)} | Наличие".PadRight(col3)} |");
            Console.WriteLine($"|{line}|");

            foreach (var element in db.zapchasts)
            {
                Console.WriteLine($"| {element.ID_Zapchast.ToString().PadRight(col0)}| {$"{element.Name.PadRight(col1)}| {element.PokupkaPrice.ToString().PadRight(col2)} | {db.GetZapchastCount(element.ID_Zapchast)}".PadRight(col3)} |");
            }
            Console.WriteLine($"|{line}|");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Баланс:  {db.GetMoney()} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            int numZapch = 0;
            Input("| Введите номер детали для заказа", out numZapch, 1, db.zapchasts.Count());
            int countZapch = 0;
            Input("| Введите количество", out countZapch, 1, 30);
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[1] Подтвердить заказ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[0] Вернуться в главное меню".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            int n = 0;
            Input("Ваш выбор", out n, 1, 1);
            switch (n)
            {
                case 1:
                    {
                        break;
                    }
            }
        }
        public void StatisticMenu()
        {
            Console.Clear();
            string line = new string('=', Width);
            Console.WriteLine(line);
            Console.WriteLine(CenterText("СТАТИСТИКА", Width));
            Console.WriteLine(line);
            Console.WriteLine($"| {$"Общие показатели:".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Всего клиентов:          {db.GetCountClientQueue()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Успешных ремонтов:       {db.GetCountClientQueue()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Отказов:                 {db.GetCountClientQueue()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Неудачных ремонтов:      {db.GetCountClientQueue()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Финансовые показатели:".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Общий доход:             {db.GetCountClientQueue()} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Общие расходы:           {db.GetCountClientQueue()} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Чистая прибыль:          {db.GetCountClientQueue()} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[1] Вернуться в главное меню".PadRight(Width - 4)} |");
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
                            ServiceMenu();  
                            break;
                        }
                    case 2:
                        {
                            ManageSkladMenu();
                            break;
                        }
                    case 3:
                        {
                            BuyZapchastMenu();  
                            break;
                        }
                    case 4:
                        {
                            StatisticMenu();
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
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();

            }
        }
    }
}
