
using _3ISIP223_Pogosyan2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3ISIP223_Pogosyan2
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
        public List<Client> clients;
        public List<Zapchast> zapchasts;
        public List<Sklad> sklad;
        public List<Transaction> transactions;
        public Random random = new Random();

        public bool IsDelivery { get; set; }

        public WorkingWithDatabase()
        {
            salon = Core.MYSalon.Salons.FirstOrDefault();
            zapchasts = Core.MYSalon.Zapchasts.ToList();
            sklad = Core.MYSalon.Sklads.ToList();
            clientQueues = Core.MYSalon.ClientQueues.OrderBy(s => s.Position).ToList();
            transactions = Core.MYSalon.Transactions.ToList();
            clients = Core.MYSalon.Clients.ToList();


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
                    StartDate = DateTime.Now,
                };
                Core.MYSalon.Salons.Add(salon);
                Core.MYSalon.SaveChanges();
            }
            if (transactions.Count == 0)
            {
                Transaction transationSalon = new Transaction
                {
                    TypeOperations = "Стартовый капитал",
                    Summa = "+10000",
                    Description = "Начало работы",
                    Date = salon.StartDate,
                };
                transactions.Add(transationSalon);
                Core.MYSalon.Transactions.Add(transationSalon);
                Core.MYSalon.SaveChanges();
            }

            AddClientTOQueu();
            UpdateValues();
            //order = CreateOrderFromQueue(GetNextClient());
            JoinDublicZapch();

        }

        public int GetCountINSkladZapchast() => sklad.Sum(s => s.Count);
        public double TotalProceeds = 0;
        public double TotalExpences = 0;
        public int GetCountINZapchastDelivery() => sklad.Sum(s => s.InDelivery);
        public int GetCountClientQueue() => clientQueues.Count;
        public int GetTotalClient() => salon.CountClients;

        public void UpdateValues()
        {
            foreach (var tr in transactions)
            {
                switch (tr.TypeOperations)
                {
                    case "Стартовый капитал": break;
                    case "Доход":
                        {
                            TotalProceeds += Math.Abs(Convert.ToDouble(tr.Summa));
                            break;
                        }
                    default:
                        {
                            TotalExpences += Math.Abs(Convert.ToDouble(tr.Summa));
                            break;
                        }
                }
            }
        }

        public void AddCountClients()
        {
            salon.CountClients++;
            Core.MYSalon.SaveChanges();
        }

        public void AddTotalSuccesfulRemont()
        {
            salon.SuccessfulRemont++;
            Core.MYSalon.SaveChanges();
        }
        public void AddTotalCountOtkaz()
        {
            salon.CountOtkaz++;
            Core.MYSalon.SaveChanges();
        }
        public void AddTotalCountFaild()
        {
            salon.CountFaild++;
            Core.MYSalon.SaveChanges();
        }



        public bool DecreaseCountZapchast(int id)
        {
            if (Core.MYSalon.Sklads.FirstOrDefault(s => s.ID_Zapchast == id) != null && Core.MYSalon.Sklads.FirstOrDefault(s => s.ID_Zapchast == id).Count != 0)
            {
                Core.MYSalon.Sklads.First(s => s.ID_Zapchast == id).Count--;
                Core.MYSalon.SaveChanges();
                sklad = Core.MYSalon.Sklads.ToList();
                return true;
            }
            return false;
        }

        public void BuyZapchast(int id, int countZapchast)
        {
            var zapchast = zapchasts.First(s => s.ID_Zapchast == id);
            var skladZapchast = Core.MYSalon.Sklads.ToList().LastOrDefault(s => s.ID_Zapchast == id);
            if (skladZapchast == null)
            {
                Sklad zapch = new Sklad
                {
                    ID_Zapchast = id,
                    Count = 0,
                    InDelivery = countZapchast,
                    DeliveryProgress = 2,
                };
                Core.MYSalon.Sklads.Add(zapch);
                Core.MYSalon.SaveChanges();
            }
            else
            {
                if (skladZapchast.DeliveryProgress == -1)
                {
                    skladZapchast.DeliveryProgress = 2;
                    skladZapchast.InDelivery = countZapchast;

                }

                else
                {
                    Sklad zapch = new Sklad
                    {
                        ID_Zapchast = id,
                        Count = 0,
                        InDelivery = countZapchast,
                        DeliveryProgress = 2,
                    };
                    Core.MYSalon.Sklads.Add(zapch);
                }
                Core.MYSalon.SaveChanges();
            }
            sklad = Core.MYSalon.Sklads.ToList();
        }
        public void ChangeDeliveryProgress()
        {
            foreach (var sklad in Core.MYSalon.Sklads.ToList())
            {
                if (sklad.DeliveryProgress > 0) sklad.DeliveryProgress--;
                if (sklad.DeliveryProgress == 0)
                {
                    sklad.Count = sklad.InDelivery;
                    IsDelivery = true;
                }

            }
            Core.MYSalon.SaveChanges();
            sklad = Core.MYSalon.Sklads.ToList();
        }


        public void StatusDelivered()
        {
            var zapchIsDelivered = Core.MYSalon.Sklads
                .Where(s => s.DeliveryProgress == 0)
                .ToList();

            foreach (var sklad in zapchIsDelivered)
            {
                sklad.DeliveryProgress = -1;
                sklad.InDelivery = 0;
            }

            Core.MYSalon.SaveChanges();

            IsDelivery = false;
            JoinDublicZapch();
        }

        public void JoinDublicZapch()
        {
            var ZapchInSklad = Core.MYSalon.Sklads
                .Where(s => s.DeliveryProgress == -1)
                .ToList();
            var groupZapch = ZapchInSklad
                .GroupBy(s => s.ID_Zapchast)
                .Where(g => g.Count() > 1);

            foreach (var group in groupZapch)
            {
                var zap = group.First();

                int totalCount = group.Sum(s => s.Count);

                zap.Count = totalCount;
                var duplicates = group.Skip(1).ToList();
                foreach (var dupl in duplicates)
                {
                    Core.MYSalon.Sklads.Remove(dupl);
                }
            }

            Core.MYSalon.SaveChanges();
            sklad = Core.MYSalon.Sklads.ToList();
        }



        public void AddClientTOQueu()
        {
            if (clientQueues.Count >= 3) return;
            int countClient = clients.Count;
            int idClient = random.Next(1, countClient);
            int countZapch = zapchasts.Count;
            int idZapch = random.Next(1, countZapch);
            ClientQueue cli = new ClientQueue
            {
                ID_Client = idClient,
                ID_Zapchast = idZapch,
                Position = (clientQueues.Count == 0 ? 0 : clientQueues.Last().Position + 1),
            };
            Core.MYSalon.ClientQueues.Add(cli);
            Core.MYSalon.SaveChanges();
            clientQueues = Core.MYSalon.ClientQueues.ToList();
        }

        public double GetPenaltyOtkaz() => salon.PenaltyOtkaz;
        public double GetFailWrong() => salon.PenaltyWrong;
        public int GetTotalSuccesfulRemont() => salon.SuccessfulRemont;
        public int GetTotalCountOtkaz() => salon.CountOtkaz;
        public int GetTotalCountFaild() => salon.CountFaild;
        public int GetZapchastCount(int id)
        {
            var skladIt = sklad.FirstOrDefault(s => s.ID_Zapchast == id);
            return skladIt == null ? 0 : skladIt.Count;
        }
        public bool CheckZapchast(int id)
        {
            var zapch = Core.MYSalon.Sklads.FirstOrDefault(s => s.ID_Zapchast == id);
            return (zapch != null && zapch.Count > 0);
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

        public void GoodByeClient(int pos)
        {
            var cli = Core.MYSalon.ClientQueues.First(s => s.Position == pos);
            clientQueues.Remove(cli);
            Core.MYSalon.ClientQueues.Remove(cli);
            Core.MYSalon.SaveChanges();
        }

        public void ChangeMoney(double money)
        {
            salon.Money += money;
            Core.MYSalon.SaveChanges();
        }

        public void AddTransaction(string typeOperations, string summa, string descrip)
        {
            Transaction transaction = new Transaction
            {
                TypeOperations = typeOperations,
                Summa = summa,
                Description = descrip,
                Date = DateTime.Now,
            };
            transactions.Add(transaction);
            Core.MYSalon.Transactions.Add(transaction);
            Core.MYSalon.SaveChanges();
        }
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
            int left = (width - text.Length - 2) / 2;
            int right = width - text.Length - left - 2;
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
            Console.WriteLine($"| {$"[2] Список ожидающих клиентов".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[3] Управление складом".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[4] Закупка запчастей".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[5] Просмотр статистики".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[6] Финансовая отчетность".PadRight(Width - 4)} |");
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
            else
            {

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
                            if (db.DecreaseCountZapchast(order.ID_Zapchast))
                            {
                                SuccesfulRemont(nextClient.ID_ClientQueue);
                            }
                            else
                            {
                                FaildRemont(nextClient.ID_ClientQueue);

                            }

                        }
                        else
                        {
                            BuyZapchast(order.ID_Zapchast, 1);

                            FaildRemont(nextClient.ID_ClientQueue);
                        }
                        break;
                    }
                case 2:
                    {
                        OtkazRemont(nextClient.ID_ClientQueue);
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
            string line = new string('-', Width - 2);
            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("УПРАВЛЕНИЕ СКЛАДОМ", Width));
            Console.WriteLine(lineRavno);
            int col1 = (Width - 6) / 2;
            int col2 = Width / 6;
            int col3 = Width - 4;
            Console.WriteLine($"| {$"{"Деталь".PadRight(col1)}| {"Наличие".PadRight(col2)} | В доставке".PadRight(col3)} |");
            Console.WriteLine($"|{line}|");
            foreach (var element in db.sklad)
            {
                Console.WriteLine($"| {$"{element.Zapchast.Name.PadRight(col1)}| {element.Count.ToString().PadRight(col2)} | {(element.DeliveryProgress == -1 ? "-" : element.InDelivery.ToString())}".PadRight(col3)} |");
            }
            Console.WriteLine($"|{line}|");
            Console.WriteLine($"| {$"Итого на складе: {db.GetCountINSkladZapchast()} дет.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Ожидается поставок: {db.GetCountINZapchastDelivery()} дет.".PadRight(Width - 4)} |");

            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[1] Вернуться в главное меню".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[2] Прогресс доставки".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            int n = 0;
            Input("Ваш выбор", out n, 1, 2);
            switch (n)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        DeliveryZapchast();
                        break;
                    }
            }
        }
        public void BuyZapchastMenu(int idZapchast = -1)
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
            int col3 = Width - col2 - 1;
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
            if (idZapchast == -1)
            {
                Input("| Введите номер детали для заказа", out numZapch, 1, db.zapchasts.Count());
            }
            else
            {
                numZapch = idZapchast;
                Console.WriteLine($"| Введите номер детали для заказа: {idZapchast.ToString().PadRight(Width - 4)} |");
            }
            int countZapch = 0;
            Input("| Введите количество", out countZapch, 0, 30);
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[1] Подтвердить заказ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[0] Вернуться в главное меню".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            int n = 0;
            Input("Ваш выбор", out n, 0, 1);
            switch (n)
            {
                case 1:
                    {
                        BuyZapchast(numZapch, countZapch);
                        break;
                    }
                default:
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
            Console.WriteLine($"| {$"- Всего клиентов:        {db.GetTotalClient()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Успешных ремонтов:     {db.GetTotalSuccesfulRemont()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Отказов:               {db.GetTotalCountOtkaz()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Неудачных ремонтов:    {db.GetTotalCountFaild()}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Финансовые показатели:".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Общий доход:           {db.TotalProceeds} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Общие расходы:         {db.TotalExpences} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"- Чистая прибыль:        {db.TotalProceeds - db.TotalExpences} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[1] Вернуться в главное меню".PadRight(Width - 4)} |");
            Console.WriteLine(line);
            int n = 0;
            Input("Ваш выбор", out n, 1, 1);
            switch (n)
            {
                default:
                    {
                        break;
                    }
            }
        }

        public void InfoClient(int pos)
        {
            Console.Clear();
            string lineRavno = new string('=', Width);
            string line = new string('-', Width - 2);
            var client = db.clientQueues.First(s => s.Position == pos);
            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("ПОДРОБНАЯ ИНФОРМАЦИЯ О КЛИЕНТЕ", Width));
            Console.WriteLine(lineRavno);
            Console.WriteLine($"| {$"Клиент:            {client.Client.Name}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Позиция в очереди: {client.Position}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Требуемая деталь:  {client.Zapchast.Name}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Стоимость ремонта: {client.Zapchast.SalePrice} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Цена закупки:      {client.Zapchast.PokupkaPrice} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Наценка:           {client.Zapchast.SalePrice - client.Zapchast.PokupkaPrice} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            int countZapchast = db.GetZapchastCount(client.ID_Zapchast);
            Console.WriteLine($"| {$"Наличие на складе: {countZapchast}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Статус доставки:   {(countZapchast == 0 ? "Требуется" : "Не требуется")}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Действия:".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[1] Вернуться в главное меню".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[2] Перейти к закупке этой детали".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            int n = 0;
            Input("Ваш выбор", out n, 1, 2);
            switch (n)
            {
                default:
                    {
                        break;
                    }
                case 2:
                    {
                        BuyZapchastMenu(client.ID_Zapchast);
                        break;
                    }
            }
        }

        public void ListClientQueueMenu()
        {
            Console.Clear();
            string lineRavno = new string('=', Width);
            string line = new string('-', Width - 2);
            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("ОЧЕРЕДЬ ОЖИДАЮЩИХ КЛИЕНТОВ", Width));
            Console.WriteLine(lineRavno);
            int col1 = 4;
            int col2 = (Width - 4) / 3;
            int col3 = Width - 4;
            Console.WriteLine($"| {$"{"Поз.".PadRight(col1)}| {"Клиент".PadRight(col2)} | Деталь".PadRight(col3)} |");
            Console.WriteLine($"|{line}|");
            int i = 1;
            foreach (var element in db.clientQueues)
            {
                Console.WriteLine($"| {$"{i++.ToString().PadRight(col1)}| {element.Client.Name.ToString().PadRight(col2)} | {element.Zapchast.Name}".PadRight(col3)} |");
            }
            Console.WriteLine($"|{line}|");
            Console.WriteLine($"| {$"Всего в очереди: {db.GetCountClientQueue()} кл.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            for (int k = 1; k < i; k++)
            {

                Console.WriteLine($"| {$"[{k}] Подробнее о клиенте {k}".PadRight(Width - 4)} |");
            }
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"[0] Вернуться в главное меню".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            int n = 0;
            Input("Ваш выбор", out n, 0, i);
            switch (n)
            {
                case 0:
                    {
                        break;
                    }
                default:
                    {
                        int pos = db.clientQueues[n - 1].Position;
                        InfoClient(pos);
                        break;
                    }
            }
        }

        public void TransactionInfo()
        {
            Console.Clear();
            int TransactWidth = Width + 20;
            string lineRavno = new string('=', TransactWidth);
            string line = new string('-', TransactWidth - 2);
            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("ФИНАНСОВАЯ ОТЧЕТНОСТЬ", TransactWidth));
            Console.WriteLine(lineRavno);
            int col0 = 11;
            int col1 = (TransactWidth - 4) / 3;
            int col2 = (TransactWidth) / 9;
            int col3 = TransactWidth - col0 - 6;
            Console.WriteLine($"| {"Дата".ToString().PadRight(col0)}| {$"{"Тип операции".PadRight(col1)}| {"Сумма".PadRight(col2)} | Описание".PadRight(col3)} |");
            Console.WriteLine($"|{line}|");

            double proceeds = 0;
            double expenses = 0;
            //double dox

            if (db.transactions.Count > 0)
            {
                foreach (var element in db.transactions)
                {
                    Console.WriteLine($"| {element.Date.ToString("dd.MM.yyyy").PadRight(col0)}| {$"{element.TypeOperations.PadRight(col1)}| {element.Summa.ToString().PadRight(col2)} | {element.Description}".PadRight(col3)} |");
                    switch (element.TypeOperations)
                    {
                        case "Доход":
                            {
                                proceeds += Convert.ToDouble(element.Summa);
                                break;
                            }
                        case "Расход":
                            {
                                expenses += Math.Abs(Convert.ToDouble(element.Summa));
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }


            Console.WriteLine($"|{line}|");
            Console.WriteLine($"| {$" ".PadRight(TransactWidth - 4)} |");
            Console.WriteLine($"| {$"Итого доходов:  {db.TotalProceeds} руб.".PadRight(TransactWidth - 4)} |");
            Console.WriteLine($"| {$"Итого расходов:  {db.TotalExpences} руб.".PadRight(TransactWidth - 4)} |");
            Console.WriteLine($"| {$"Текущий баланс:  {db.GetMoney()} руб.".PadRight(TransactWidth - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(TransactWidth - 4)} |");
            Console.WriteLine($"| {$"Период:  с {db.salon.StartDate.ToString("dd.MM.yyyy")} по {DateTime.Now.ToString("dd.MM.yyyy")}".PadRight(TransactWidth - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(TransactWidth - 4)} |");
            Console.WriteLine($"| {$"[0] Вернуться в главное меню".PadRight(TransactWidth - 4)} |");
            Console.WriteLine(lineRavno);
            int n = 0;
            Input("Ваш выбор", out n, 0, 0);
            switch (n)
            {
                default:
                    {
                        break;
                    }
            }
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

        public void SuccesfulRemont(int id)
        {
            Console.Clear();
            string lineRavno = new string('-', Width);
            var client = db.clientQueues.First(s => s.ID_ClientQueue == id);
            var zapchast = client.Zapchast;
            db.AddTransaction(
                "Доход",
                $"+{zapchast.SalePrice}",
                $"Ремонт: {client.Client.Name}"
                );
            db.ChangeMoney(zapchast.SalePrice);
            db.TotalProceeds += zapchast.SalePrice;
            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("ОПЕРАЦИЯ УСПЕШНА", Width));
            Console.WriteLine(lineRavno);
            Console.WriteLine($"| {$"Клиент: {client.Client.Name}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Деталь: {zapchast.Name}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Получено: {zapchast.SalePrice} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Остаток на складе: {db.GetZapchastCount(zapchast.ID_Zapchast)} шт.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Для продолжения нажмите Enter...".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            Console.ReadLine();
            db.ChangeDeliveryProgress();
            SuccesfulDeliveryZapchast();
            db.GoodByeClient(client.Position);
            db.AddClientTOQueu();
            db.AddCountClients();
            db.AddTotalSuccesfulRemont();
        }
        public void OtkazRemont(int id)
        {
            Console.Clear();
            string lineRavno = new string('-', Width);
            var client = db.clientQueues.First(s => s.ID_ClientQueue == id);
            var zapchast = client.Zapchast;
            db.AddTransaction(
                "Штраф",
                $"-{db.GetPenaltyOtkaz()}",
                $"Отказ: {client.Client.Name}"
                );
            db.ChangeMoney(-db.GetPenaltyOtkaz());
            db.TotalExpences += db.GetPenaltyOtkaz();
            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("ОТКАЗ В ОБСЛУЖИВАНИИ", Width));
            Console.WriteLine(lineRavno);
            Console.WriteLine($"| {$"Клиент: {client.Client.Name}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Деталь: {zapchast.Name}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Штраф: {db.GetPenaltyOtkaz()} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Текущий баланс: {db.GetMoney()} шт.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Для продолжения нажмите Enter...".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            Console.ReadLine();
            db.ChangeDeliveryProgress();

            SuccesfulDeliveryZapchast();
            db.GoodByeClient(client.Position);
            db.AddClientTOQueu();
            db.AddCountClients();
            db.AddTotalCountOtkaz();
        }
        public void FaildRemont(int id)
        {
            Console.Clear();
            string lineRavno = new string('-', Width);
            var client = db.clientQueues.First(s => s.ID_ClientQueue == id);
            var zapchast = client.Zapchast;
            double fail = zapchast.PokupkaPrice * db.GetFailWrong();
            db.AddTransaction(
                "Штраф",
                $"-{fail}",
                $"Ошибка: {client.Client.Name}"
                );
            db.ChangeMoney(-fail);
            db.TotalExpences += db.GetFailWrong();

            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("НЕУДАЧНЫЙ РЕМОНТ", Width));
            Console.WriteLine(lineRavno);
            Console.WriteLine($"| {$"Клиент: {client.Client.Name}".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Ошибка: установлена неверная деталь".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Штраф: {fail} руб.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Текущий баланс: {db.GetMoney()} шт.".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Для продолжения нажмите Enter...".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            Console.ReadLine();
            db.ChangeDeliveryProgress();

            SuccesfulDeliveryZapchast();
            db.GoodByeClient(client.Position);
            db.AddClientTOQueu();
            db.AddCountClients();
            db.AddTotalCountFaild();
        }
        public void DeliveryZapchast()
        {
            Console.Clear();
            string lineRavno = new string('-', Width);
            //var client = db.clientQueues.First(s => s.ID_ClientQueue == id);
            Console.WriteLine(lineRavno);
            Console.WriteLine(CenterText("ДОСТАВКА", Width));
            Console.WriteLine(lineRavno);
            Console.WriteLine($"| {$"Статус поставки:".PadRight(Width - 4)} |");
            foreach (var zapch in db.sklad)
            {
                if (zapch.DeliveryProgress != -1)
                    Console.WriteLine($"| {$"> {zapch.Zapchast.Name} ({zapch.InDelivery} шт.) - через {zapch.DeliveryProgress} клиента".PadRight(Width - 4)} |");
            }
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
            Console.WriteLine($"| {$"Для продолжения нажмите Enter...".PadRight(Width - 4)} |");
            Console.WriteLine(lineRavno);
            Console.ReadLine();
        }
        public void SuccesfulDeliveryZapchast()
        {
            if (db.IsDelivery)
            {
                Console.Clear();
                string lineRavno = new string('-', Width);
                //var client = db.clientQueues.First(s => s.ID_ClientQueue == id);
                Console.WriteLine(lineRavno);
                Console.WriteLine(CenterText("ПОСТАВКА ПРИБЫЛА!", Width));
                Console.WriteLine(lineRavno);
                Console.WriteLine($"| {$"Получены детали:".PadRight(Width - 4)} |");
                foreach (var zapch in db.sklad)
                {
                    //if (zapch.DeliveryProgress)
                    if (zapch.DeliveryProgress == 0)
                        Console.WriteLine($"| {$"> {zapch.Zapchast.Name} - {zapch.InDelivery} шт.".PadRight(Width - 4)} |");
                }
                Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
                Console.WriteLine($"| {$" ".PadRight(Width - 4)} |");
                Console.WriteLine($"| {$"Для продолжения нажмите Enter...".PadRight(Width - 4)} |");
                Console.WriteLine(lineRavno);
                Console.ReadLine();
                db.StatusDelivered();

            }
        }

        public void BuyZapchast(int id, int count)
        {
            var zapchast = db.zapchasts.First(s => s.ID_Zapchast == id);
            double pokupka = zapchast.PokupkaPrice * count;
            if (db.GetMoney() < pokupka)
            {
                Console.WriteLine("Недостаточно средств для покупки!");
            }
            db.AddTransaction(
                "Расход",
                $"-{pokupka}",
                $"Закупка деталей"
                );
            db.ChangeMoney(-pokupka);
            db.BuyZapchast(id, count);
            db.TotalExpences -= pokupka;

        }



        public void StartGame()
        {
            int n = 0;
            while (true)
            {
                StartMenu();
                Input("Выберите действие", out n, 0, 6);
                switch (n)
                {
                    case 0:
                        {
                            return;
                        }
                    case 1:
                        {
                            ServiceMenu();
                            break;
                        }
                    case 2:
                        {
                            ListClientQueueMenu();
                            break;
                        }
                    case 3:
                        {
                            ManageSkladMenu();
                            break;
                        }
                    case 4:
                        {
                            BuyZapchastMenu();
                            break;
                        }
                    case 5:
                        {
                            StatisticMenu();
                            break;
                        }
                    case 6:
                        {
                            TransactionInfo();
                            break;
                        }
                    default:
                        {

                            break;
                        }
                }
                //Console.WriteLine("\nНажмите Enter для продолжения...");
                //Console.ReadLine();

            }
        }
    }
}
