using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
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
            List<string> janrLst = new List<string>() { "Комедия", "Драмма", "Ужасы", "Фантастика" };
            AddBook($"book{1}", $"author{1}", 2000+1, 20.0+1, janrLst[1]);

            for (int i = 0; i < 4; i++)
            {
                AddBook($"book{i}", $"author{i}", 2000+i, 20.0+i, janrLst[i]);
            }
            int n = 0;
            int id;
            int poisk = 0;
            do
            {
                Console.Clear();
                Console.Write("=== УЧЕТ КНИГ В БИБЛИОТЕКЕ ===\nВыберите действие:\n\n1. Добавить новую книгу\n2. Удалить книгу\n3. Поиск книг\n4. Отсортировать книги по названию.\n5. Отсортировать книги по году.\n6. Вывести самую дорогую и самую дешёвую книгу.\n7. Сгруппировать книги по авторам.\n8. Показать все книги.\n0. Выйти из программы.\n\nВведите номер команды: ");
                n = Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 0: break;
                    case 1:
                        {

                            Console.Clear();
                            Console.Write("=== ДОБАВЛЕНИЕ НОВОЙ КНИГИ ===\n\nВведите название книги: ");
                            string name = Console.ReadLine();
                            Console.Write("Введите автора: ");
                            string author = Console.ReadLine();
                            Console.WriteLine("Выберите жанр:");
                            for (int i = 0; i < janrLst.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {janrLst[i]}");
                            }
                            Console.Write("Введите номер жанра: ");
                            int catNum = Convert.ToInt32(Console.ReadLine()); catNum--;
                            string janr = "NULL";
                            if (catNum > janrLst.Count) Console.WriteLine("Такого жанра нет.");
                            else { janr = janrLst[catNum]; }

                            Console.Write("Введите год издания: ");
                            int year = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Введите цену товара: ");
                            double price = Convert.ToDouble(Console.ReadLine());

                            AddBook(name, author,year, price , janr);
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            Console.Write("=== УДАЛЕНИЕ КНИГИ ===\n\nВведите уникальный код книги для удаления: ");
                            id = Convert.ToInt32(Console.ReadLine());
                            if (DelBook(id))
                            {
                                Console.WriteLine("Книга успешно удалена!");
                            }
                            else
                            {
                                Console.WriteLine($"Книга с кодом {id} не найдена!");
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();

                            Console.Write("=== ОТСОРТИРОВАННЫЕ КНИГИ ПО НАЗВАНИЮ ===\n\n");
                            SortLibraryByName();
                            PrintLibrary();
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();

                            Console.Write("=== ОТСОРТИРОВАННЫЕ КНИГИ ПО ГОДУ ИЗДАНИЯ ===\n\n");
                            SortLibraryByYear();
                            PrintLibrary();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            do
                            {
                                Console.Clear();
                                Console.Write("=== ПОИСК КНИГ ===\nВыберите критерий поиска:\n\n1. Поиск по автору\n2. Поиск по названию\n3. Поиск по жанру\n4. Вернуться в главное меню\n\nВведите номер команды: ");
                                poisk = Convert.ToInt32(Console.ReadLine());
                                switch (poisk)
                                {
                                    case 1:
                                        {
                                            Console.Write("Введите автора: ");
                                            string author = Console.ReadLine();
                                            if (!FindBook(author, 1))
                                            {
                                                Console.WriteLine("Книги не найдены!");
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.Write("Введите название книги: ");
                                            string name = Console.ReadLine();
                                            if (!FindBook(name))
                                            {
                                                Console.Write("Книги не найдены!");
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            Console.WriteLine("Доступные жанры:");
                                            for (int i = 0; i < janrLst.Count; i++)
                                            {
                                                Console.WriteLine($"{i+1}. {janrLst[i]}");
                                            }
                                            Console.Write("Введите номер жанра: ");
                                            int catNum = Convert.ToInt32(Console.ReadLine()); catNum--;
                                            string categor = "NULL";
                                            if (catNum > janrLst.Count) Console.WriteLine("Такого жанра нет.");
                                            else { categor = janrLst[catNum]; }
                                            if (!FindBook(categor, 2.0))
                                            {
                                                Console.WriteLine("Нет книг с выбранным жанром!");
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
                    case 8:
                        {
                            Console.Clear();

                            Console.WriteLine($"=== ВСЕ ТОВАРЫ В СИСТЕМЕ ===\n\nОбщее количество книг: {library.Count}");
                            PrintLibrary();
                            break;
                        }
                    case 6: 
                        {
                            Console.Clear();

                            Console.WriteLine($"=== САМАЯ ДОРОГАЯ И ДЕШЕВАЯ КНИГИ ===\n\n");
                            PrintCheapANDExpencive();
                            break;
                        }
                    case 7: 
                        {
                            Console.Clear();

                            Console.WriteLine($"=== СГРУППИРОВАННЫЕ КНИГИ ПО АВТОРАМ ===\n\n");
                            GroupByAuthor();
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
            else if (year < 1000 || year > 2025)
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

        static bool FindBook(string name)
        {
            var findBook = library.Where(p => p.Name == name).ToList();
            if (findBook != null)
            {
                foreach (var book in findBook)
                    Console.WriteLine($"ID: {book.ID}; Название: {book.Name}; Автор: {book.Author}; Жанр: {book.Janr}; Год издания: {book.Year}; Цена: {book.Price}");
                return true;
            }
            return false;
        }
        static bool FindBook(string author, int d)
        {
            var findBook = library.Where(p => p.Author == author).ToList();
            if (findBook != null) {
                foreach(var book in findBook) 
                    Console.WriteLine($"ID: {book.ID}; Название: {book.Name}; Автор: {book.Author}; Жанр: {book.Janr}; Год издания: {book.Year}; Цена: {book.Price}");
                return true;

            }
            return false;
        }
        static bool FindBook(string janr,double d)
        {
            var findBook = library.Where(p => p.Janr == janr).ToList();
            if (findBook != null)
            {
                foreach (var book in findBook)
                    Console.WriteLine($"ID: {book.ID}; Название: {book.Name}; Автор: {book.Author}; Жанр: {book.Janr}; Год издания: {book.Year}; Цена: {book.Price}");
                return true;
            }
            return false;
        }

        static void SortLibraryByName()
        {
            library = library.OrderBy(p => p.Name).ToList();
        }
        static void SortLibraryByYear()
        {
            library = library.OrderBy(p => p.Year).ToList();
        }

        static void PrintCheapANDExpencive()
        {
            Book CheapBook = library.OrderBy(p => p.Price).ToList()[0];
            Book ExpenciveBook = library.OrderByDescending(p => p.Price).ToList()[0];
            Console.WriteLine("Самая дешевая книга:");
            Console.WriteLine($"ID: {CheapBook.ID}; Название: {CheapBook.Name}; Автор: {CheapBook.Author}; Жанр: {CheapBook.Janr}; Год издания: {CheapBook.Year}; Цена: {CheapBook.Price}");
            Console.WriteLine("\nСамая дорогая книга:");
            Console.WriteLine($"ID: {ExpenciveBook.ID}; Название: {ExpenciveBook.Name}; Автор: {ExpenciveBook.Author}; Жанр: {ExpenciveBook.Janr}; Год издания: {ExpenciveBook.Year}; Цена: {ExpenciveBook.Price}");
        }

        static void GroupByAuthor()
        {
            var author = library.GroupBy(p => p.Author).Select(g => g.First()).ToList();

            var countBook = author.Select(p => library.Where(g => g.Author == p.Author).Count()).ToList();

            for(int i = 0; i < author.Count; i++) 
            {
                Console.WriteLine($"Автор: {author[i].Author}, Количество книг: {countBook[i]}.");
            }
        }

        static void PrintLibrary()
        {
            foreach (var book in library)
                Console.WriteLine($"ID: {book.ID}; Название: {book.Name}; Автор: {book.Author}; Жанр: {book.Janr}; Год издания: {book.Year}; Цена: {book.Price}");
        }
    }

    class Book
    {

        private static int nextId = 1;
        public int ID { get;  private set; }
        public string Name;
        public string Author;
        public int Year;
        public double Price;
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
