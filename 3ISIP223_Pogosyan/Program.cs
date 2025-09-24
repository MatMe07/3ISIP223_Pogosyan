using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan
{
    internal class Program
    {
        private static List<Book> library = new List<Book>();
        private int ID = 0;


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            List<string> janr = new List<string>() { "Комедия", "Драмма", "Ужасы", "Фантастика" };
            
            for (int i = 0; i < 4; i++)
            {
                AddProd($"prod{i}", i + 1.2, i + 10, janr[i]);
            }
            int n = 0;
            int id;
            int poisk = 0;
            do
            {
                Console.Clear();
                Console.Write("=== УЧЕТ ТОВАРОВ В МАГАЗИНЕ ===\nВыберите действие:\n\n1. Добавить новый товар\n2. Удалить товар\n3. Заказать поставку товара\n4. Продать товар\n5. Поиск товаров\n6. Показать все товары\n0. Выйти из программы\n\nВведите номер команды: ");
                n = Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 0: break;
                    case 1:
                        {

                            Console.Clear();
                            Console.Write("=== ДОБАВЛЕНИЕ НОВОГО ТОВАРА ===\n\nВведите название товара: : ");
                            string name = Console.ReadLine();
                            Console.Write("Введите цену товара: : ");
                            double price = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Введите количество товара: : ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Выберите категорию:");
                            for (int i = 0; i < janr.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {janr[i]}");
                            }
                            Console.Write("Введите номер категории: ");
                            int catNum = Convert.ToInt32(Console.ReadLine()); catNum--;
                            string categor = "NULL";
                            if (catNum > janr.Count) Console.WriteLine("Такой категории нет.");
                            else { categor = janr[catNum]; }

                            AddProd(name, price, count, categor);
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            Console.Write("=== УДАЛЕНИЕ ТОВАРА ===\n\nВведите уникальный код товара для удаления: ");
                            id = Convert.ToInt32(Console.ReadLine());
                            if (DelProd(id))
                            {
                                Console.WriteLine("Товар успешно удален!");
                            }
                            else
                            {
                                Console.WriteLine($"Товар с кодом {id} не найден!");
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            Console.Write("=== ЗАКАЗ ПОСТАВКИ ТОВАРА ===\n\nВведите уникальный код товара: ");
                            id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Введите количество для поставки: ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            if (ZakazProd(id, count))
                            {
                                Console.WriteLine("Поставка успешно оформлена!");
                            }
                            else
                            {
                                Console.WriteLine($"Товар с кодом {id} не найден!");
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();

                            Console.Write("=== ПРОДАЖА ТОВАРА ===\n\nВведите уникальный код товара: ");
                            id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Введите количество для продажи: ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            if (SellProd(id, count))
                            {
                                Console.WriteLine("Продажа успешно оформлена!");
                            }
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();

                            do
                            {
                                Console.Clear();
                                Console.Write("=== ПОИСК ТОВАРОВ ===\nВыберите критерий поиска:\n\n1. Поиск по уникальному коду\n2. Поиск по названию\n3. Поиск по категории\n4. Вернуться в главное меню\n\nВведите номер команды: ");
                                poisk = Convert.ToInt32(Console.ReadLine());
                                switch (poisk)
                                {
                                    case 1:
                                        {
                                            Console.Write("Введите уникальный код товара: ");
                                            id = Convert.ToInt32(Console.ReadLine());
                                            if (!FindProdByID(id))
                                            {
                                                Console.WriteLine($"Товар с кодом {id} не найден!");
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.Write("Введите название товара: ");
                                            string name = Console.ReadLine();
                                            if (!FindProdByName(name))
                                            {
                                                Console.Write("Товары не найдены!");
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            Console.WriteLine("Доступные категории:");
                                            for (int i = 1; i <= janr.Count; i++)
                                            {
                                                Console.WriteLine($"{i}. {janr[i]}");
                                            }
                                            Console.Write("Введите номер категории: ");
                                            int catNum = Convert.ToInt32(Console.ReadLine()); catNum--;
                                            string categor = "NULL";
                                            if (catNum > janr.Count) Console.WriteLine("Такой категории нет.");
                                            else { categor = janr[catNum]; }
                                            if (!FindProdByjanr(categor))
                                            {
                                                Console.WriteLine("В выбранной категории товаров нет!");
                                            }
                                            break;
                                        }
                                    case 4: { break; }
                                    default:
                                        {
                                            Console.WriteLine("Ошибка ввода! Пожалуйста, введите корректные данные.");
                                            break;
                                        }
                                        ;
                                }
                                if (poisk != 4)
                                {
                                    Console.WriteLine("\nНажмите Enter");
                                    Console.ReadLine();
                                }
                            }
                            while (poisk < 4);

                            break;
                        }
                    case 6:
                        {
                            Console.Clear();

                            Console.WriteLine($"=== ВСЕ ТОВАРЫ В СИСТЕМЕ ===\n\nОбщее количество товаров: {library.Count}");
                            PrintProducts();
                            break;
                        }
                    default: { Console.WriteLine("Ошибка ввода! Пожалуйста, введите корректные данные."); break; }
                }

                if (poisk != 4)
                {
                    Console.WriteLine("\nНажмите Enter");
                    Console.ReadLine();
                }
                poisk = 0;

            }
            while (n != 0);
            Console.WriteLine("Удачи");

        }
        static void AddProd(string name, double price, int count, string categoric)
        {
            int add = 0;
            if (name.Length == 0) add = 3;
            else if (price <= 0) add = 2;
            else if (count <= 0) add = 1;
            Book prod = new Book(name, price, count, categoric);
            library.Add(prod);
            if (add > 0)
            {
                if (add == 1)
                {
                    Console.WriteLine("Ошибка: Некорректное количество. Количество товара должно быть положительным числом.");
                }
                else if (add == 2)
                {
                    Console.WriteLine("Ошибка: Некорректная цена. Цена товара должна быть положительным числом.");
                }
                else
                {
                    Console.WriteLine("Ошибка: Отсутствует название товара. Название не может быть пустым.");
                }

            }
            else
            {
                Console.WriteLine("Товар успешно добавлен!");
                Console.WriteLine($"Код вашего товара: {prod.Get_ID()}");
            }
        }

        static bool DelProd(int id)
        {
            foreach (Product prod in library)
            {
                if (prod.Get_ID() == id)
                {
                    library.Remove(prod);
                    return true;
                }
            }
            return false;
        }

        static public bool ZakazProd(int id, int count)
        {
            if (count < 0) return false;
            foreach (Product prod in library)
            {
                if (prod.Get_ID() == id)
                {
                    prod.UvelichCount(count);
                    return true;
                }
            }
            return false;
        }

        static public bool SellProd(int id, int count)
        {

            foreach (Product prod in library)
            {
                if (prod.Get_ID() == id)
                {
                    if (prod.Get_CountProdInStock() - count > 0)
                    {
                        prod.ReduceCount(count);
                        return true;
                    }
                    Console.WriteLine("Недостаточно товара на складе!");
                    return false;
                }
            }
            Console.WriteLine($"Товар с кодом {id} не найден!");
            return false;
        }

        static public bool FindProdByID(int id)
        {
            foreach (Product prod in library)
            {
                if (prod.Get_ID() == id)
                {
                    Console.WriteLine($"Найден товар:\nКод: {prod.Get_ID()} | Название: {prod.Name} | Цена: {prod.Price} | В наличии: {prod.Get_CountProdInStock()} | Категория: {prod.janr}");

                    return true;
                }
            }
            return false;
        }
        static public bool FindProdByName(string name)
        {
            int n = 0;
            foreach (Product prod in library)
            {
                if (prod.Name.ToLower() == name.ToLower())
                {
                    Console.WriteLine($"Код: {prod.Get_ID()} | Название: {prod.Name} | Цена: {prod.Price} | В наличии: {prod.Get_CountProdInStock()} | Категория: {prod.janr}");

                    n++;
                }
            }
            return n > 0;
        }
        static public bool FindProdByjanr(string janr)
        {
            int n = 0;
            foreach (Product prod in library)
            {
                if (prod.janr.ToLower() == janr.ToLower())
                {
                    Console.WriteLine($"Код: {prod.Get_ID()} | Название: {prod.Name} | Цена: {prod.Price} | В наличии: {prod.Get_CountProdInStock()} | Категория: {prod.janr}");

                    n++;
                }
            }
            return n > 0;
        }

        static public void PrintProducts()
        {
            if (library.Count == 0) { Console.WriteLine("В системе нет товаров!"); return; }
            foreach (Product prod in library)
            {
                Console.WriteLine($"Код: {prod.Get_ID()} | Название: {prod.Name} | Цена: {prod.Price} | В наличии: {prod.Get_CountProdInStock()} | Категория: {prod.janr}");
            }
        }
    }

    class Book
    {

        private static int nextId = 1;
        private int ID;
        public string Name;
        public string Author;
        private DateTime Date;
        private double Price;
        public string Janr;
            

        public Book(string name, string author, DateTime date, double price, string janr)
        {
            ID = nextId++;
            Name = name;
            Author = author;
            Date = date;
            Price = price;
            Janr = janr;
        }
        

    }



}
