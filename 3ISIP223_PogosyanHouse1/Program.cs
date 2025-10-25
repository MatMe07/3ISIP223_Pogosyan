
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

        public User User { get; set; }
        public List<Product> Products { get; set; }
        public List<Korzina> Korzina { get; set; }
        public List<OrderHistory> OrderHistories { get; set; }

        public WorkingWhithDateBase()
        {

            Products = Core.Marketplace.Products.ToList();
            OrderHistories = Core.Marketplace.OrderHistories.ToList();  
            Korzina = Core.Marketplace.Korzinas.ToList();

            //User = new User
            //{
            //    IsRegistered = false
            //};
            //Core.Marketplace.Users.Add(User);
            //Core.Marketplace.SaveChanges();
            User = Core.Marketplace.Users.FirstOrDefault();

        }

        // Регистрация пользователя
        // 1. Создать запись в БД с флагом IsRegistered = true
        // 2. Вернуться в главное меню
        public bool SignUp()
        {
            return true;
        }

        // Вход пользователя
        // 1. Проверить существование пользователя в БД
        // 2. Сравнить введенный пароль с хранимым
        // 3. Если успешно - войти в личный кабинет
        public bool SignIn()
        {
            return false;
        }




        // Поиск товаров
        // 1. Найти товары по названию/описанию
        // 2. Вернуть результаты
        public void FindProduct()
        {

        }


        // Добавление товара в корзину
        // 1. Добавить запись в таблицу Korzina
        public void AddProductToKorzina(int id, int count)
        {
            Korzina korzina = new Korzina
            {
                ID_Product = id,
                ID_User = User.ID_User,
                CountProduct = count
            };
            Korzina.Add(korzina);
            Core.Marketplace.Korzinas.Add(korzina);
            Core.Marketplace.SaveChanges();
        }

        // Просмотр корзины
        // 1. Вернуть список товаров
        public void ReadKorzina()
        {

        }


        // Оформление заказа
        // 1. Рассчитать итоговую стоимость
        // 2. Создать заказ в таблице Orders
        // 3. Перенести товары в OrderHistory
        // 4. Очистить корзину
        public void MakingOrder(int idPvz, bool OneProduct = false, )
        {

                foreach (var korz in Korzina)
                {
                    Order order = new Order
                    {
                        ID_PVZ = idPvz,
                        ID_User = User.ID_User,
                        Created_Date = DateTime.Now,
                        TotalPrice = korz.Product.Price * korz.CountProduct,
                    };
                    OrderHistory orderHistory = new OrderHistory
                    {
                        Order = order,
                        ID_Product = korz.ID_Product,
                        CountProduct = korz.CountProduct,
                    };
                    OrderHistories.Add(orderHistory);
                    Core.Marketplace.OrderHistories.Add(orderHistory);
                }
                Core.Marketplace.SaveChanges();

        }

        public void MakingOneOrder(int idPvz, Korzina korz) 
        {
            Order order = new Order
            {
                ID_PVZ = idPvz,
                ID_User = User.ID_User,
                Created_Date = DateTime.Now,
                TotalPrice = korz.Product.Price * korz.CountProduct,
            };
            OrderHistory orderHistory = new OrderHistory
            {
                Order = order,
                ID_Product = korz.ID_Product,
                CountProduct = korz.CountProduct,
            };
            OrderHistories.Add(orderHistory);
            Core.Marketplace.OrderHistories.Add(orderHistory);
            Core.Marketplace.SaveChanges();
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
        public void InputCheckWord(string text, out string n, List<string> words, int start = -1, int end =-1)
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
                            for(int i = start; i <= end; i++)
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
            return $"|{new string(' ', left)}{text}{new string(' ', right)}|";
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
                Console.WriteLine(" 1. Войти в аккаунт               ║\r\n║ 2. Зарегистрироваться            ║\r\n║ 3. Просмотреть товары            ║\r\n║ 4. Выйти из программы ");
                Input("Выберите действие [1-4]",out n, 1, 4);
                switch (n)
                {
                    case 1:
                        {
                            SignInMenu();
                            break;
                        }
                    case 2:
                        {
                            SignUpMenu();
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
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();

            }

        }


        // Выход из аккаунта
        // 1. Сбросить данные текущего пользователя
        // 2. Вернуться в главное меню
        public void LogOutAccMenu()
        {

        }

        // Меню Регистрации
        // 1. Запросить данные пользователя (логин, пароль, имя)
        // 2. Проверить уникальность логина
        // 3. Подтвердить пароль
        public void SignUpMenu()
        {

        }

        // Меню Входа
        public void SignInMenu()
        {

        }

        // Меню авторизованного пользователя
        // 1. Показать варианты: Каталог, Корзина, Мои заказы, Выйти
        // 2. Перейти к выбранному функционалу
        public void MenuSignInUser()
        {

        }

        // Работа с корзиной
        // 1. Получить товары из бд
        // 2. Отобразить список с количеством и стоимостью
        // 3. Предложить: изменить количество, удалить товар, оформить заказ, вернуться
        public void Korzina()
        {

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
        public void AllProductMenu()
        {
            Console.Clear();
            Console.WriteLine("AllProducts");
            int N = 0;
            foreach(var product in Db.Products)
            {
                Console.WriteLine($"{N+1} | {product.Name} | {product.Price} | {product.Categor.Name}");
                Console.WriteLine($"  | {product.Description.Substring(0, 17)}... |         |");
            }
            Console.WriteLine($"[1-{N}] Выбрать товар");
            Console.WriteLine("[Ф] Фильтр по категориям");
            Console.WriteLine("[В] Вернуться в меню");
            Console.WriteLine();
            string n;
            List<string> words = new List<string> { "Ф", "В" };
            InputCheckWord("Выберите действие", out n, words, 1, N);
            Console.WriteLine(n);

            switch (n)
            {
                case "Ф":
                    {
                        break;
                    }
                case "В":
                    {
                        break;
                    }
                default: {
                        InfoProduct(int.Parse(n));
                        break;
                    }
            }
        }

        public void InfoProduct(int idProd)
        {
            Console.Clear();
            var product = Db.Products.FirstOrDefault(s => s.ID_Product == idProd);
            if (product == null) { Console.WriteLine("error"); return; }
            Console.WriteLine($"{product.Name}");
            Console.WriteLine($"{product.Categor.Name}");
            Console.WriteLine($"{product.Price}");
            Console.WriteLine();
            Console.WriteLine($"{product.Description}");
            Console.WriteLine();
            Console.WriteLine("║ [1] Добавить в корзину           ║\r\n║ [2] Купить сейчас                ║\r\n║ [3] Вернуться в меню ");
            int n;
            Input("Выберите действие [1-3]", out n, 1, 3);
            switch (n)
            {
                case 1:
                    {
                        int count;
                        Input("Count", out count, 1, 20);
                        AddProductToKorzina(idProd, count);
                        break;
                    }
                case 2:
                    {
                        int count;
                        Input("Count", out count, 1, 20);
                        MakingOneProductOrder(idProd, count);    
                        break;
                    }
                case 3:
                    {

                        break;
                    }
            }

        }


        // Удаление товара из корзины
        // 1. Пользователь выбирает товар для удаления
        // 2. Подтверждает удаление
        // 3. Удаляет запись из базы данных
        // 4. Показывает обновленную корзину
        public void DeleteProduct()
        {

        }

        // Оформление заказа
        // 1. Показать список доступных ПВЗ
        // 2. Пользователь выбирает ПВЗ
        // 3. Рассчитать общую стоимость заказа
        // 4. Создать заказ в бд
        // 5. Показать подтверждение заказа
        public void MakingOrder()
        {

        }

        // Просмотр истории заказов
        // 1. Получить заказы текущего пользователя из БД
        // 2. Отобразить список заказов с деталями
        // 3. Показать состав каждого заказа
        public void OrderHistory()
        {

        }


        // Покупка одного товара
        // 1. Пользователь выбирает товар из каталога
        // 2. Запрашивается количество
        // 3. Показываются ПВЗ для выбора
        // 4. Создается заказ без добавления в корзину
        // 5. Показывается подтверждение покупки
        public void MakingOneProductOrder(int idProd, int count)
        {
            Order 
        }


        // Изменение количества товара в корзине
        // 1. Пользователь выбирает товар из корзины
        // 2. Запрашивает новое количество
        // 3. Обновляет запись в базе данных
        // 4. Показывает обновленную корзину
        public void ChageCountProductInKorzina()
        {

        }

    }

}
