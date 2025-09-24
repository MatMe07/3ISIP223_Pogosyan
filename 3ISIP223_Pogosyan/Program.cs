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
                AddBook($"book{i}", $"author{i}", 2000+i, 20.0+i, janr[i]);
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
        static void AddBook(string name, string author, int year, double price, string janr)
        {
            if (name.Length == 0)
            {
                Console.WriteLine("Ошибка: Отсутствует название книги. Название не может быть пустым.");
            }
            else if (author.Length == 0)
            {
                Console.WriteLine("Ошибка: Отсутствует название автора. Название не может быть пустым.");
            }
            else if (year < 1000 && year > 2025)
            {
                Console.WriteLine("Ошибка: Некорректный год. Год издания должен быть в диапазоне (1000 - 2025).");
            }
            else if (price <= 0)
            {
                Console.WriteLine("Ошибка: Некорректная цена. Цена товара должна быть положительным числом.");
            }
            else
            {
                Book prod = new Book(name, author, year, price, janr);
                library.Add(prod);
                Console.WriteLine("Книга успешно добавлена!");
                Console.WriteLine($"Код вашей книги: {prod.ID }");
            }
        }

        static bool DelBook(int id)
        {
            var findBook = library.FirstOrDefault(p => p.ID == id);
            if (findBook == null) { return false; }
            else
            {
                library.Remove(findBook);
                return true;
            }
        }

        static bool FindBook(string name, ref Book book)
        {
            var findBook = library.FirstOrDefault(p => p.Name == name);
            if (findBook != null) { book = findBook;  return true; }
            return false;
        }
        static bool FindBook(string author, ref Book book, int d)
        {
            var findBook = library.FirstOrDefault(p => p.Author == author);
            if (findBook != null) { book = findBook; return true; }
            return false;
        }
        static bool FindBook(string janr, ref Book book, double d)
        {
            var findBook = library.FirstOrDefault(p => p.Janr == janr);
            if (findBook != null) { book = findBook; return true; }
            return false;
        }
    }

    class Book
    {

        private static int nextId = 1;
        public int ID { get;  private set; }
        public string Name;
        public string Author;
        private int Year;
        private double Price;
        public string Janr;
            
        public Book(string name, string author, int year, double price, string janr)
        {
            ID = nextId++;
            Name = name;
            Author = author;
            Year = year;
            Price = price;
            Janr = janr;
        }

    }



}
