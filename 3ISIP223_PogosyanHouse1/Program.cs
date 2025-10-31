
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3ISIP223_PogosyanHouse1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Экзепляр магазина
            Marketplace marketplace = new Marketplace();
            marketplace.StartMenu();
        }
    }



    class WorkingWhithDateBase
    {

        public User User { get; set; } = null;
        public List<Product> Products { get; set; }
        public List<Korzina> Korzina { get; set; }
        public List<OrderHistory> OrderHistories { get; set; }
        public List<Order> Orders { get; set; }
        public List<PVZ> Pvzs { get; set; }

        public PVZ pvzUser { get; set; } = null;

        public WorkingWhithDateBase()
        {

            Products = Core.Marketplace.Products.ToList();
            //Korzina = Core.Marketplace.Korzinas.ToList();
            Pvzs = Core.Marketplace.PVZs.ToList();

            //User = new User
            //{
            //    IsRegistered = false
            //};
            //Core.Marketplace.Users.Add(User);
            //Core.Marketplace.SaveChanges();
            User = Core.Marketplace.Users.FirstOrDefault();

            if (User != null && User.PVZ != null) pvzUser = User.PVZ;
        }

        public bool UserRegistered() => User.IsRegistered;

        public void ChangePVZUser(int id)
        {
            pvzUser = Pvzs.FirstOrDefault(s => s.ID_PVZ == id);
            ChangePVZ(id);
        }

        public bool CheckLogin(string login)
        {
            if (Core.Marketplace.Users.ToList().FirstOrDefault(s => s.Login == login) != null)
            {
                return false;
            }
            return true;
        }

        public void OutUserAcc()
        {
            User = Core.Marketplace.Users.FirstOrDefault();

            pvzUser = null ;
            Korzina = null;
            OrderHistories = null;
            Orders = null;

        }

        // Регистрация пользователя
        // 1. Создать запись в БД с флагом IsRegistered = true
        // 2. Вернуться в главное меню
        public void SignUp(string login, string password, string name)
        {
            User newUser = new User
            {
                Login = login,
                Password = password,
                Name = name,
                IsRegistered = true,
                Created_Date = DateTime.Now,
            };
            Core.Marketplace.Users.Add(newUser);
            User = newUser;
            Core.Marketplace.SaveChanges();
            ChangeParametrs();
        }

        public void ChangePVZ(int idPVZ)
        {
            User.ID_PVZ = idPVZ;
            Core.Marketplace.Users.First(u => u.ID_User == User.ID_User).ID_PVZ = idPVZ;
            Core.Marketplace.SaveChanges();
        }

        // Вход пользователя
        // 1. Проверить существование пользователя в БД
        // 2. Сравнить введенный пароль с хранимым
        // 3. Если успешно - войти в личный кабинет
        public bool SignIn(string login, string password)
        {
            User us = Core.Marketplace.Users.FirstOrDefault(s => s.Login == login);
            if (us == null) { return false; }
            if (us.Password != password) { return false; }
            User = us;
            if (us.PVZ != null) pvzUser = us.PVZ;
            ChangeParametrs();
            return true;
        }

        public void ChangeParametrs()
        {
            Korzina = Core.Marketplace.Korzinas.Where(s => s.ID_User == User.ID_User).ToList();
            OrderHistories = Core.Marketplace.OrderHistories.Where(us => us.Order.ID_User == User.ID_User).ToList();
            Orders = Core.Marketplace.Orders.Where(s => s.ID_User == User.ID_User).ToList();
            pvzUser = User.PVZ; 
        }




        // Добавление товара в корзину
        // 1. Добавить запись в таблицу Korzina
        public void AddProductToKorzina(int id, int count)
        {
            Korzina korzina = new Korzina
            {
                ID_Product = id,
                ID_User = User.ID_User,
                CountProduct = count,
                TotalPrice = count * Products.First(s => s.ID_Product == id).Price,
            };
            Korzina.Add(korzina);
            Core.Marketplace.Korzinas.Add(korzina);
            Core.Marketplace.SaveChanges();
        }

        public void ClearKorzina()
        {
            Korzina.Clear();
            foreach (var kor in Core.Marketplace.Korzinas.Where(s => s.ID_User == User.ID_User).ToList())
            {
                Core.Marketplace.Korzinas.Remove(kor);
            }
            Core.Marketplace.SaveChanges();
        }

        // Удаление товара из корзины
        // 1. Пользователь выбирает товар для удаления
        // 2. Подтверждает удаление
        // 3. Удаляет запись из базы данных
        // 4. Показывает обновленную корзину
        public void DeleteProduct(int idProd)
        {
            Core.Marketplace.Korzinas.Remove(Korzina[idProd]);
            Core.Marketplace.SaveChanges();
            Korzina = Core.Marketplace.Korzinas.Where(s => s.ID_User == User.ID_User).ToList();
        }


        public void ChangeCountProduct(int idProd, int count)
        {
            var korz = Core.Marketplace.Korzinas.Where(s => s.ID_User == User.ID_User).ToList().First(s => s.ID_Korzina == Korzina[idProd].ID_Korzina);
            korz.CountProduct = count;
            korz.TotalPrice = count * korz.Product.Price;



            Core.Marketplace.SaveChanges();
            Korzina = Core.Marketplace.Korzinas.Where(s => s.ID_User == User.ID_User).ToList();
        }

        public int FilterCategory()
        {
            int N = 2;
            foreach (var cat in Core.Marketplace.Categors.ToList())
            {
                Console.WriteLine($"[{N++}] {cat.Name} ({Products.Count(s => s.ID_Category == cat.ID_Category)} тов.)");
            }
            N--;
            return N;
        }

        // Оформление заказа
        // 1. Рассчитать итоговую стоимость
        // 2. Создать заказ в таблице Orders
        // 3. Перенести товары в OrderHistory
        // 4. Очистить корзину
        public bool MakingOrder(int idPvz)
        {

            decimal totalPrice = Korzina.Sum(k => k.Product.Price * k.CountProduct);

            Order order = new Order
            {
                ID_PVZ = idPvz,
                ID_User = User.ID_User,
                Created_Date = DateTime.Now,
                TotalPrice = totalPrice,
            };
            Core.Marketplace.Orders.Add(order);
            Core.Marketplace.SaveChanges();

            foreach (var korz in Korzina)
            {

                OrderHistory orderHistory = new OrderHistory
                {
                    ID_Order = order.ID_Order,
                    ID_Product = korz.ID_Product,
                    CountProduct = korz.CountProduct,
                };
                OrderHistories.Add(orderHistory);
                Core.Marketplace.OrderHistories.Add(orderHistory);
            }
            //OrderHistories = Core.Marketplace.OrderHistories.ToList().Where(s=>s.Order.ID_User == User.ID_User).ToList();
            Core.Marketplace.SaveChanges();
            ChangeParametrs();

            return true;

        }


        public bool MakingOneOrder(int idPvz, int idProd, int count, decimal price)
        {
            Order order = new Order
            {
                ID_User = User.ID_User,
                ID_PVZ = idPvz,
                Created_Date = DateTime.Now,
                TotalPrice = price * count,
            };
            Core.Marketplace.Orders.Add(order);
            Core.Marketplace.SaveChanges();
            OrderHistory orderHistory = new OrderHistory
            {
                ID_Order = order.ID_Order,
                ID_Product = idProd,
                CountProduct = count,
            };
            //OrderHistories.Add(orderHistory);
            Core.Marketplace.OrderHistories.Add(orderHistory);
            Core.Marketplace.SaveChanges();
            ChangeParametrs();
            return true;
        }



        // Просмотр истории заказов
        // 1. Получить заказы пользователя из Orders
        // 2. Отсортировать по дате (новые/старые)
        // 3. Показать детали каждого заказа через OrderHistory
        public void ReadOrderHistory()
        {

        }


        // Выбор пункта выдачи
        // 1. Получить список всех ПВЗ из БД
        // 2. Показать адрес, телефон, график работы
        // 3. Сохранить выбранный ПВЗ для заказа
        public void ReadPVZ()
        {
            int i = 0;
            foreach (var pvz in Pvzs)
            {
                Console.WriteLine($"[{i + 1}] {pvz.Address}");
                Console.WriteLine($"    {pvz.Phone}");
                Console.WriteLine($"    {pvz.Schedule}\n");
            }
        }
    }

    class Marketplace
    {
        public WorkingWhithDateBase Db;

        public Marketplace()
        {
            Db = new WorkingWhithDateBase();
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
        public void InputCheckWord(string text, out string n, List<string> words, int start = -1, int end = -1)
        {
            while (true)
            {
                Console.Write($"{text}:\n> ");
                n = Console.ReadLine().ToUpper();
                //if (string.TryParse(Console.ReadLine().ToUpper(), out n))
                {
                    foreach (string word in words)
                    {
                        if (n == word) return;
                    }
                    if (start != -1)
                    {
                        int a;
                        if (int.TryParse(n, out a))
                        {
                            for (int i = start; i <= end; i++)
                            {
                                if (a == i) return;
                            }

                        }
                    }
                }
                Console.WriteLine("Неверное действие!\n");
            }
        }

        public string CenterText(string text, int width)
        {
            int left = (width - text.Length - 2) / 2;
            int right = width - text.Length - left - 2;
            return $"║{new string(' ', left)}{text}{new string(' ', right)}║";
        }

        // Главное меню приложения
        // 1. Показать варианты: Войти, Зарегистрироваться, Просмотреть товары, Выход
        // 2. Перейти к соответствующему методу
        public void StartMenu()
        {
            int n;

            while (true)
            {
                Console.Clear();
                string line = new string('═', 28);
                Console.WriteLine($"╔{ line}╗");
                Console.WriteLine(CenterText("Wildberries", 30));
                Console.WriteLine($"╠{ line}╣");
                Console.WriteLine($"║ {"1. Войти в аккаунт".PadRight(26)} ║");
                Console.WriteLine($"║ {"2. Зарегистрироваться".PadRight(26)} ║");
                Console.WriteLine($"║ {"3. Просмотреть товары".PadRight(26)} ║");
                Console.WriteLine($"║ {"4. Выйти из программы".PadRight(26)} ║");
                Console.WriteLine($"╠{ line}╣");



                Input("║ Выберите действие [1-4]", out n, 1, 4);
                switch (n)
                {
                    case 1:
                        {
                            SignInMenu();
                            if (Db.UserRegistered()) MenuSignInUser();
                            break;
                        }
                    case 2:
                        {
                            SignUpMenu();
                            if (Db.UserRegistered()) MenuSignInUser();
                            break;
                        }
                    case 3:
                        {
                            AllProductMenu();
                            break;
                        }
                    case 4:
                        {
                            return;
                        }
                }
                Console.WriteLine("\nНажмите [Enter] Вернуться в меню");
                Console.ReadLine();

            }

        }

        public bool CheckInputLogin(string login)
        {
            if (login.Length == 0)
            {
                Console.WriteLine("Ошибка: Логин не может быть пустым!");
                return false;
            }
            if (login == "0") return true;

            if (login.Length < 3 || login.Length > 20)
            {
                Console.WriteLine("Ошибка: Логин должен содержать от 3 до 20 символов!");
                return false;
            }
            if (!Db.CheckLogin(login))
            {
                Console.WriteLine("Ошибка: Этот логин уже занят!");
                return false;
            }
            Console.WriteLine("Логин доступен для регистрации!\n");
            return true;
        }
        public bool CheckInputPassword(string password)
        {
            if (password.Length == 0)
            {
                Console.WriteLine("Ошибка: Пароль не может быть пустым!");
                return false;
            }
            if (password == "0") return true;
            if (password.Length < 6 || password.Length > 20)
            {
                Console.WriteLine("Ошибка: Пароль должен содержать от 6 до 20 символов!");
                return false;
            }
            return true;
        }

        // Меню Регистрации
        // 1. Запросить данные пользователя (логин, пароль, имя)
        // 2. Проверить уникальность логина
        // 3. Подтвердить пароль
        public void SignUpMenu()
        {
            Console.Clear();
            string line = new string('═', 46);
            Console.WriteLine($"╔{line}╗");
            Console.WriteLine(CenterText("РЕГИСТРАЦИЯ", 48));
            Console.WriteLine($"╠{line}╣");




            //Console.WriteLine($"╠{line}╣");
            Console.WriteLine("Для возврата в меню введите '0' в любое поле\n");
            string login;
            while (true)
            {
                Console.Write("Login: \n>");
                if (CheckInputLogin(login = Console.ReadLine()))
                {
                    break;
                }
            }
            if (login == "0")
            {
                Console.WriteLine("\nРегистрация отменена");
                return;
            }
            string password;
            while (true)
            {
                Console.Write("Password: \n>");
                if (CheckInputPassword(password = Console.ReadLine()))
                {
                    break;
                }
            }
            if (password == "0")
            {
                Console.WriteLine("\nРегистрация отменена");
                return;
            }
            string confirmPassword;
            while (true)
            {
                Console.Write("Confirm Password: \n>");
                confirmPassword = Console.ReadLine();
                if (confirmPassword == password || password == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nОшибка: Пароли не совпадают!");
                }
            }
            if (confirmPassword == "0")
            {
                Console.WriteLine("\nРегистрация отменена");
                return;
            }
            string name;
            while (true)
            {
                Console.Write("Name: \n>");
                name = Console.ReadLine();
                if (name == "0")
                {
                    break;
                }
                else if (name.Length == 0) { Console.WriteLine("Ошибка: Имя не может быть пустым!"); }
                else if (name.Length < 3 || name.Length > 20) { Console.WriteLine("Ошибка: Имя должно содержать от 2 до 20 символов!"); }
                else break;
            }
            if (name == "0")
            {
                Console.WriteLine("Регистрация отменена");
                return;
            }

            Db.SignUp(login, password, name);
            Console.WriteLine("Регистрация прошла успешно!");
            return;
        }

        // Меню Входа
        public void SignInMenu()
        {
            Console.Clear();
            Console.Clear();
            string line = new string('═', 38);
            Console.WriteLine($"╔{line}╗");
            Console.WriteLine(CenterText("ВХОД В СИСТЕМУ", 40));
            Console.WriteLine($"╠{line}╣");

            string login, password;
            Console.Write("╠ Login: ");
            login = Console.ReadLine();
            Console.Write("╠ Password: ");
            password = Console.ReadLine();
            if (!Db.SignIn(login, password))
            {
                Console.WriteLine("\nНеверный логин или пароль!\nПопробуйте снова или зарегистрируйтесь.");
            }
            else
            {
                Console.WriteLine($"\nАвторизация успешна! Добро пожаловать, {Db.User.Name}!");
            }
        }

        // Меню авторизованного пользователя
        // 1. Показать варианты: Каталог, Корзина, Мои заказы, Выйти
        // 2. Перейти к выбранному функционалу
        public void MenuSignInUser()
        {
            int n;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"ДОБРО ПОЖАЛОВАТЬ, {Db.User.Name}!");
                Console.WriteLine("1. Каталог товаров");
                Console.WriteLine($"2. Моя корзина ({Db.Korzina.Count})");
                Console.WriteLine("3. История заказов");
                Console.WriteLine("4. Выйти из аккаунта");

                Input("Выберите действие [1-4]", out n, 1, 4);
                switch (n)
                {
                    case 1:
                        {
                            AllProductMenu();
                            break;
                        }
                    case 2:
                        {
                            Korzina();
                            break;
                        }
                    case 3:
                        {
                            int i = 1;
                            while (true)
                            {
                                i = OrderHistory(i);
                                if (i == 0) break;
                            }
                            break;
                        }
                    case 4:
                        {
                            Db.OutUserAcc();
                            return;
                        }
                }

            }
        }

        // Работа с корзиной
        // 1. Получить товары из бд
        // 2. Отобразить список с количеством и стоимостью
        // 3. Предложить: изменить количество, удалить товар, оформить заказ, вернуться
        public void Korzina()
        {
            Console.Clear();
            Console.WriteLine("КОРЗИНА");
            int N = 1;
            decimal totalPrice = 0;
            foreach (var korz in Db.Korzina)
            {

                Console.WriteLine($"{N++}. {korz.Product.Name}      x{korz.CountProduct}     {korz.TotalPrice} Руб. ");
                totalPrice += korz.TotalPrice;
            }
            Console.WriteLine();
            Console.WriteLine($"Итого:   {totalPrice} Руб. ");
            Console.WriteLine();
            N--;
            if (N != 0)
            {
                Console.WriteLine($"[1-{N}] Изменить товар\t\t[З] Оформить заказ\n[О] Очистить корзину\t\t[B] Вернуться");
            }
            else
            {
                Console.WriteLine($"[З] Оформить заказ\t\t[О] Очистить корзину\n[B] Вернуться             ");
            }
            string n;
            List<string> list = new List<string> { "З", "О", "В" };
            InputCheckWord("Выберите действие", out n, list, 1, N);
            switch (n)
            {
                case "З":
                    {
                        if (N == 0)
                        {
                            Console.WriteLine("\nКорзина пуста!");
                            Console.WriteLine("Нажмите [Enter] Вернуться в меню");
                            Console.ReadLine();
                        }
                        else
                        {
                            MakingOrder();
                        }
                        break;
                    }
                case "О":
                    {
                        if (N != 0) Db.ClearKorzina();
                        Korzina();
                        break;
                    }
                case "В":
                    {
                        break;
                    }
                default:
                    {
                        ChangeProduct(int.Parse(n) - 1);
                        break;
                    }
            }
        }


        // Добавление товара в корзину
        // 1. Получить ID выбранного товара
        // 2. Запросить количество
        // 3. Вызвать метод бд для добавления в корзину
        // 4. Показать сообщение о результате
        public void AddProductToKorzina(int idProd, int count)
        {
            Db.AddProductToKorzina(idProd, count);
        }


        // Просмотр каталога товаров
        // 1. Получить все товары из бд
        // 2. Отобразить список товаров с ценами и описаниями
        // 3. Предложить фильтрацию по категориям или по дате
        // 4. Для авторизованных пользователей показать опцию добавления в корзину
        public void AllProductMenu(int idCategor = 0)
        {
            Console.Clear();
            int N = 1;
            string line = new string('═', 79);
            Console.WriteLine($"╔{line}╗");
            Console.WriteLine(CenterText("КАТАЛОГ ТОВАРОВ", 81));
            Console.WriteLine($"╠{line}╣");

            List<int> listID = new List<int>();
            if (idCategor == 0)
            {
                foreach (var product in Db.Products)
                {
                    Console.WriteLine($"║ {(N++).ToString().PadRight(3)} ║ {product.Name.PadRight(28)} ║ {product.Price.ToString().PadRight(10)} ║ {product.Categor.Name.PadRight(27)} ║");
                    Console.WriteLine($"║ {" ".PadRight(3)} ║ {$"{product.Description.Substring(0, Math.Min(product.Description.Length, 25))}...".PadRight(28)} ║ {" ".PadRight(10)} ║ {" ".PadRight(27)} ║");
                    Console.WriteLine($"║ {" ".PadRight(3)} ║ {" ".PadRight(28)} ║ {" ".PadRight(10)} ║ {" ".PadRight(27)} ║");
                }
            }
            else
            {
                foreach (var product in Db.Products.Where(pr => pr.ID_Category == idCategor).ToList())
                {
                    Console.WriteLine($"║ {(N++).ToString().PadRight(3)} ║ {product.Name.PadRight(28)} ║ {product.Price.ToString().PadRight(10)} ║ {product.Categor.Name.PadRight(27)} ║");
                    Console.WriteLine($"║ {" ".PadRight(3)} ║ {$"{product.Description.Substring(0, Math.Min(product.Description.Length, 25))}...".PadRight(28)} ║ {" ".PadRight(10)} ║ {" ".PadRight(27)} ║");
                    Console.WriteLine($"║ {" ".PadRight(3)} ║ {" ".PadRight(28)} ║ {" ".PadRight(10)} ║ {" ".PadRight(27)} ║");
                    listID.Add(product.ID_Product);
                }
            }
            Console.WriteLine($"╚{line}╝");

            N--;
            Console.WriteLine($"\n[1-{N}] Выбрать товар");
            Console.WriteLine("[Ф] Фильтр по категориям");
            Console.WriteLine("[В] Вернуться в меню");
            Console.WriteLine();
            string n;
            List<string> words = new List<string> { "Ф", "В" };
            InputCheckWord("Выберите действие", out n, words, 1, N);

            switch (n)
            {
                case "Ф":
                    {
                        FilterCategoryMenu();
                        break;
                    }
                case "В":
                    {
                        break;
                    }
                default:
                    {
                        int int_n = int.Parse(n);
                        InfoProduct(idCategor == 0 ? int_n : listID[int_n - 1]);
                        break;
                    }
            }
        }

        public void InfoProduct(int idProd)
        {
            Console.Clear();
            var product = Db.Products.FirstOrDefault(s => s.ID_Product == idProd);
            if (product == null) { Console.WriteLine("error"); return; }

            string line = new string('═', 44);
            Console.WriteLine($"╔{line}╗");
            Console.WriteLine(CenterText($"{product.Name}", 46));
            Console.WriteLine($"╠{line}╣");

            Console.WriteLine($"║ {$"{product.Name}".PadRight(42)} ║");
            Console.WriteLine($"║ {$"{product.Categor.Name}".PadRight(42)} ║");
            Console.WriteLine($"║ {$"{product.Price}".PadRight(42)} ║");
            Console.WriteLine($"║ {" ".PadRight(42)} ║");
            Console.WriteLine($"║ {$"{product.Description}".PadRight(42)} ║");
            Console.WriteLine($"║ {" ".PadRight(42)} ║");
            Console.WriteLine($"╠{line}╣");
            if (Db.UserRegistered())
            {
                Console.WriteLine($"║ {"[1] Добавить в корзину".PadRight(42)} ║\r\n║ {"[2] Купить сейчас".PadRight(42)} ║\r\n║ {"[3] Вернуться в меню".PadRight(42)} ║");
                Console.WriteLine($"╚{line}╝");

                int n;
                Input("Выберите действие [1-3]", out n, 1, 3);
                switch (n)
                {
                    case 1:
                        {
                            int count;
                            Input("Введите количество", out count, 1, 20);
                            AddProductToKorzina(idProd, count);
                            break;
                        }
                    case 2:
                        {
                            int count;
                            Input("Введите количество", out count, 1, 20);
                            MakingOneProductOrder(idProd, count);
                            break;
                        }
                    case 3:
                        {

                            break;
                        }
                }

            }
            else
            {
                Console.WriteLine($"║ {"[1] Вернуться в меню".PadRight(42)} ║");
                Console.WriteLine($"╚{line}╝");

                int n;
                Input("Выберите действие", out n, 1, 1);
                switch (n)
                {
                    case 1:
                        {
                            break;
                        }
                }
            }

        }

        public void FilterCategoryMenu()
        {
            Console.Clear();
            Console.WriteLine("ФИЛЬТР КАТЕГОРИЙ ");
            Console.WriteLine("[1] Все категории (18 товаров)");
            int N = Db.FilterCategory();
            int n;
            Input($"Выберите категорию [1-{N}]", out n, 1, N);
            switch (n)
            {
                default:
                    {
                        AllProductMenu(n - 1);
                        break;
                    }
            }
        }




        // Оформление заказа
        // 1. Показать список доступных ПВЗ
        // 2. Пользователь выбирает ПВЗ
        // 3. Рассчитать общую стоимость заказа
        // 4. Создать заказ в бд
        // 5. Показать подтверждение заказа
        public void MakingOrder()
        {
            Console.Clear();
            Console.WriteLine("ОФОРМЛЕНИЕ ЗАКАЗА");
            int idPvz = -1;
            int N = Db.Pvzs.Count;
            if (Db.pvzUser != null)
            {
                idPvz = Db.pvzUser.ID_PVZ;
                Console.WriteLine($"Текущий ПВЗ: {Db.pvzUser.Address}");
                Console.WriteLine();
            }

            if (idPvz == -1)
            {
                Console.WriteLine("Выберите пункт выдачи:");
                Console.WriteLine();

                for (int i = 0; i < N; i++)
                {
                    var pvz = Db.Pvzs[i];
                    Console.WriteLine($"[{i + 1}] {pvz.Address}");
                    Console.WriteLine($"    {pvz.Schedule} | {pvz.Phone}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Состав заказа:");
            decimal totalPrice = 0;
            foreach (var korz in Db.Korzina)
            {
                Console.WriteLine($"  > {korz.Product.Name} x{korz.CountProduct} = {korz.TotalPrice} Руб.");
                totalPrice += korz.TotalPrice;
            }
            Console.WriteLine($"ИТОГО: {totalPrice} Руб. ");

            int choice;

            if (idPvz == -1)
            {
                Console.WriteLine($"\n[1-{N}] Выбрать ПВЗ");
                Console.WriteLine("[0] Отмена");
                Input("Выберите действие", out choice, 0, N);

                if (choice == 0)
                {
                    Console.WriteLine("\nЗаказ отменен");
                    Console.WriteLine("Нажмите [Enter] Вернуться в меню");
                    Console.ReadLine();
                    return;
                }

                idPvz = Db.Pvzs[choice - 1].ID_PVZ;
            }
            else
            {
                Console.WriteLine("\n[1] Подтвердить заказ");
                Console.WriteLine("[2] Изменить ПВЗ");
                Console.WriteLine("[0] Отмена");
                Input("Выберите действие: ", out choice, 0, 2);

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("\nЗаказ отменен");
                        Console.WriteLine("Нажмите [Enter] Вернуться в меню");
                        Console.ReadLine();
                        return;
                    case 1:
                        break;
                    case 2:
                        Console.WriteLine("\nВыберите новый пункт выдачи:");
                        for (int i = 0; i < N; i++)
                        {
                            var pvz = Db.Pvzs[i];
                            Console.WriteLine($"[{i + 1}] {pvz.Address}");
                        }

                        Input("Выберите ПВЗ", out int pvzChoice, 1, N);
                        idPvz = Db.Pvzs[pvzChoice - 1].ID_PVZ;
                        break;
                }
            }
            Db.ChangePVZUser(idPvz);

            bool success = Db.MakingOrder(idPvz);

            if (success)
            {
                Console.Clear();
                Console.WriteLine("ЗАКАЗ ОФОРМЛЕН\n");
                Console.WriteLine($"ПВЗ: {Db.Pvzs.First(p => p.ID_PVZ == idPvz).Address}");
                Console.WriteLine();
                Console.WriteLine("Состав заказа:");
                //Console.WriteLine($"Товар: {product.Name} x{count}"); // Все товары
                foreach (var korz in Db.Korzina)
                {
                    Console.WriteLine($"   > {korz.Product.Name} x{korz.CountProduct} = {korz.TotalPrice} Руб.");
                    totalPrice += korz.TotalPrice;
                }
                Console.WriteLine($"Сумма: {totalPrice} Руб.");

                Console.WriteLine();
                Db.ClearKorzina();
            }
            else
            {
                Console.WriteLine("Ошибка при оформлении заказа!");
            }

            Console.WriteLine("Нажмите [Enter] Вернуться в меню");
            Console.ReadLine();
        }

        public void ChangeProduct(int idProd)
        {

            Console.Clear();
            Korzina product = Db.Korzina[idProd];
            if (product == null)
            {
                Console.WriteLine("null");
                Console.ReadLine();
                return;
            }
            Console.WriteLine(product.Product.Name);
            Console.WriteLine($"Текущее количество: {product.CountProduct}");
            int newCount;
            Input("Новое количество", out newCount, 1, 20);
            Console.WriteLine("1. Сохранить\n2. Удалить\n3. Отмена");
            int n;
            Input("Выберите действие[1-3]", out n, 1, 3);
            switch (n)
            {
                case 1:
                    {
                        Db.ChangeCountProduct(idProd, newCount);
                        break;
                    }
                case 2:
                    {
                        Db.DeleteProduct(idProd);
                        break;
                    }
                case 3:
                    {
                        break;
                    }
            }

        }


        // Просмотр истории заказов
        // 1. Получить заказы текущего пользователя из БД
        // 2. Отобразить список заказов с деталями
        // 3. Показать состав каждого заказа
        public int OrderHistory(int sorting)
        {
            Console.Clear();
            Console.WriteLine("ИСТОРИЯ ЗАКАЗОВ");
            //var userOrders = Db.Orders.Where(o => o.ID_User == Db.User.ID_User);
            if (Db.Orders.Count() == 0)
            {
                Console.WriteLine("\nЗаказы не найдены! Сделайте первый заказ.");
                Console.WriteLine("\nНажмите [Enter] чтобы вернуться в меню");
                Console.ReadLine();
                return 0;
            }


            foreach (var order in  (sorting == 2 ?  Db.Orders.OrderByDescending(s=>s.Created_Date).ToList() : Db.Orders.OrderBy(s=>s.Created_Date).ToList())   ) 
            {
                var orderItems = Db.OrderHistories
                    .Where(oh => oh.ID_Order == order.ID_Order)
                    .ToList();

                Console.WriteLine($"\n#{order.ID_Order} | {order.Created_Date.ToString("dd.MM.yyyy")} | {order.PVZ.Address} | {order.TotalPrice} Руб.");

                var prodGroup = orderItems.GroupBy(s => s.ID_Product);
                foreach (var item in prodGroup)
                {
                    var prod = item.First();
                    Console.WriteLine($"  > {prod.Product.Name}   x{item.Count()}");
                }
                Console.WriteLine("──────────────────────────────────────────────────");
            }

            Console.WriteLine("");
            int n;
            Console.WriteLine($"1. Сортировать по дате ({(sorting == 1 ? "Убыванию" : "Возрастанию")})");
            Console.WriteLine("2. Вернуться в меню");

            Input("Выберите действие", out n, 1, 2);
            switch (n)
            {
                case 1:
                    {
                        return sorting == 1 ? 2 : 1;
                    }
                case 2:
                    {
                        return 0;
                    }
            }



            return 0;
        }

        // Покупка одного товара
        // 1. Пользователь выбирает товар из каталога
        // 2. Запрашивается количество
        // 3. Показываются ПВЗ для выбора
        // 4. Создается заказ без добавления в корзину
        // 5. Показывается подтверждение покупки
        public void MakingOneProductOrder(int idProd, int count)
        {
            Console.Clear();
            Console.WriteLine("ОФОРМЛЕНИЕ ЗАКАЗА");
            int idPvz = -1;
            int N = Db.Pvzs.Count;
            Product product = Db.Products.FirstOrDefault(s => s.ID_Product == idProd);
            if (Db.pvzUser != null)
            {
                idPvz = Db.pvzUser.ID_PVZ;
                Console.WriteLine($"Текущий ПВЗ: {Db.pvzUser.Address}");
                Console.WriteLine();
            }

            if (idPvz == -1)
            {
                Console.WriteLine("Выберите пункт выдачи:");
                Console.WriteLine();

                for (int i = 0; i < N; i++)
                {
                    var pvz = Db.Pvzs[i];
                    Console.WriteLine($"[{i + 1}] {pvz.Address}");
                    Console.WriteLine($"    {pvz.Schedule} | {pvz.Phone}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Состав заказа:");
            decimal totalPrice = product.Price * count;
            Console.WriteLine($"  > {product.Name} x{count} = {totalPrice} Руб.");
            Console.WriteLine($"ИТОГО: {totalPrice} Руб.");

            int choice;

            if (idPvz == -1)
            {
                Console.WriteLine($"\n[1-{N}] Выбрать ПВЗ");
                Console.WriteLine("[0] Отмена");
                Input("Выберите действие: ", out choice, 0, N);

                if (choice == 0)
                {
                    Console.WriteLine("Заказ отменен");
                    return;
                }

                idPvz = Db.Pvzs[choice - 1].ID_PVZ;
            }
            else
            {
                Console.WriteLine("\n[1] Подтвердить заказ");
                Console.WriteLine("[2] Изменить ПВЗ");
                Console.WriteLine("[0] Отмена");
                Input("Выберите действие", out choice, 0, 2);

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("\nЗаказ отменен");
                        Console.WriteLine("Нажмите [Enter] Вернуться в меню");
                        Console.ReadLine();
                        return;
                    case 1:
                        break;
                    case 2:
                        Console.WriteLine("\nВыберите новый пункт выдачи:");
                        for (int i = 0; i < N; i++)
                        {
                            var pvz = Db.Pvzs[i];
                            Console.WriteLine($"[{i + 1}] {pvz.Address}");
                        }

                        Input("Выберите ПВЗ: ", out int pvzChoice, 1, N);
                        idPvz = Db.Pvzs[pvzChoice - 1].ID_PVZ;
                        break;
                }
            }
            Db.ChangePVZUser(idPvz);

            bool success = Db.MakingOneOrder(idPvz, idProd, count, totalPrice);

            if (success)
            {
                Console.Clear();
                Console.WriteLine("ЗАКАЗ ОФОРМЛЕН");
                Console.WriteLine();
                Console.WriteLine($"ПВЗ: {Db.Pvzs.First(p => p.ID_PVZ == idPvz).Address}");
                Console.WriteLine($"Товар: {product.Name} x{count}");
                Console.WriteLine($"Сумма: {totalPrice} Руб.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Ошибка при оформлении заказа!");
            }

            Console.WriteLine("Нажмите [Enter] Вернуться в меню");
            Console.ReadLine();
        }


    }

}
