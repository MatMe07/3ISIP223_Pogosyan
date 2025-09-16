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
        private static List<Product> products = new List<Product>();
        private int ID = 0;


        static void Main(string[] args)
        {
            List<string> category = new List<string>() { "продукты питания", "одежда", "игрушки", "электроника" };
            //int df = 
            for (int i = 0; i < 4; i++)
            {
                AddProd($"prod{i}", i+1.2, i+10, category[i]);
            }
            int n = 0;
            int id;
            do
            {
                Console.Clear();
                Console.Write("=== УЧЕТ ТОВАРОВ В МАГАЗИНЕ ===\nВыберите действие:\n\n1. Добавить новый товар\n2. Удалить товар\n3. Заказать поставку товара\n4. Продать товар\n5. Поиск товаров\n6. Показать все товары\n0. Выйти из программы\n\nВведите номер команды: ");
                n = Convert.ToInt32(Console.ReadLine());
                switch (n) { 
                case 0:break;
                case 1:
                        {
                            Console.Write("Введите все необходимые данные продукта\nНазвание: ");
                            string name = Console.ReadLine();
                            Console.Write("Цена: ");
                            double price = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Количество: ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Доступные категории:\n-----------------------------");
                            for (int i = 0; i < category.Count; i++)
                            {
                                Console.WriteLine($"{i}. {category[i]}");
                            }
                            Console.WriteLine("-----------------------------");
                            Console.Write("Выберите из списка категорию: ");
                            int catNum = Convert.ToInt32(Console.ReadLine());
                            string categor = "NULL";
                            if (catNum > category.Count) Console.WriteLine("Такой категории нет.");
                            else { categor = category[catNum]; }

                            AddProd(name, price, count, categor);
                            break;
                        }
                case 2:
                        {
                            Console.Write("Введите все необходимые данные продукта\nКод продукта (ID): ");
                            id = Convert.ToInt32(Console.ReadLine());
                            if (DelProd(id))
                            {
                                Console.WriteLine("Продукт успешно удален!");
                            }
                            else
                            {
                                Console.WriteLine("Товар не найден!");
                            }
                            break;
                        }
                case 3:
                        {
                            Console.Write("Введите все необходимые данные продукта\nКод продукта (ID): ");
                            id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Количество товаров: ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            if (ZakazProd(id, count))
                            {
                                Console.WriteLine("Продукт заказан!");
                            }
                            else
                            {
                                Console.WriteLine("Товар не найден или Некорректное количество.!");
                            }
                            break;
                        }
                case 4:
                        {
                            Console.Write("Введите все необходимые данные продукта\nКод продукта (ID): ");
                            id = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Количество товаров: ");
                            int count = Convert.ToInt32(Console.ReadLine());
                            if (SellProd(id, count))
                            {
                                Console.WriteLine("Продукт успешно продан!");
                            }
                            else
                            {
                                Console.WriteLine("Товар не найден или Некорректное количество.!");
                            }
                            break;
                        }
                case 5:
                        {
                            Console.WriteLine("=== ПОИСК ТОВАРОВ ===\nВыберите критерий поиска:\n\n1. Поиск по уникальному коду\n2. Поиск по названию\n3. Поиск по категории\n4. Вернуться в главное меню\n\nВведите номер команды: ");
                            int poisk = Convert.ToInt32(Console.ReadLine());
                            switch (poisk)
                            {
                                case 1:
                                    {
                                        Console.Write("Введите уникальный код товара (ID): ");
                                        id = Convert.ToInt32(Console.ReadLine());
                                        FindProdByID(id);
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.Write("Введите название товара (ID): ");
                                        string name = Console.ReadLine();
                                        FindProdByName(name);
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("Доступные категории:");
                                        for (int i = 0; i < category.Count; i++)
                                        {
                                            Console.WriteLine($"{i}. {category[i]}");
                                        }
                                        Console.Write("Выберите из списка категорию: ");
                                        int catNum = Convert.ToInt32(Console.ReadLine());
                                        string categor = "NULL";
                                        if (catNum > category.Count) Console.WriteLine("Такой категории нет.");
                                        else { categor = category[catNum]; }
                                        break;
                                    }
                                case 4:  { break; }
                                default: {
                                        Console.WriteLine("Такого пункта нет!!!!");
                                        break;
                                    };
                            }
                            break;
                        }
                case 6:
                        {
                            PrintProducts();
                            break;
                        }
                default: { Console.WriteLine("Такого пункта нет!!!!"); break; }
                }
                Console.WriteLine("\nНажмите Enter");
                Console.ReadLine();
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
            Product prod = new Product(name, price, count, categoric);
            products.Add(prod);
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
                Console.WriteLine("Товар успешно доабавлен!.");
            }
        }

        static bool DelProd(int id)
        {
            foreach (Product prod in products) { 
                if (prod.Get_ID() == id)
                {
                    products.Remove(prod);
                    return true;
                }
            }
            return false;
        }

        static public bool ZakazProd(int id, int count)
        {
            if (count < 0 ) return false;
            foreach (Product prod in products)
            {
                if (prod.Get_ID() == id)
                {
                    prod.UvelichCount(count);
                    return true;
                }
            }
            return false;
        }

        static public bool SellProd(int id, int count) {

            foreach (Product prod in products)
            {
                if (prod.Get_ID() == id)
                {
                    if (prod.Get_CountProdInStock() - count > 0) { 
                        prod.ReduceCount(count);
                        return true;
                    }
                    Console.WriteLine("На складе не хватает товара для продажи!!");
                    return false;
                }
            }
            return false;
        }

        static public bool FindProdByID(int id)
        {
            foreach (Product prod in products)
            {
                if (prod.Get_ID() == id)
                {
                    Console.WriteLine($"{prod.Get_ID} | {prod.Name} | {prod.Price} $ | {prod.Category}");
                    return true;
                }
            }
            return false;
        }
        static public bool FindProdByName(string name)
        {
            int n = 0;
            foreach (Product prod in products)
            {
                if (prod.Name.ToLower() == name.ToLower())
                {
                    Console.WriteLine($"{prod.Get_ID} | {prod.Name} | {prod.Price} $ | {prod.Category}");
                    n++;
                }
            }
            return n > 0;
        }
        static public bool FindProdByCategory(string category)
        {
            int n = 0;
            foreach (Product prod in products)
            {
                if (prod.Category.ToLower() == category.ToLower())
                {
                    Console.WriteLine($"{prod.Get_ID} | {prod.Name} | {prod.Price} $ | {prod.Category}");
                    n++;
                }
            }
            return n > 0;
        }

        static public void PrintProducts()
        {
            Console.WriteLine("_____________________________________");
            foreach (Product prod in products)
            {
                Console.WriteLine($"{prod.Get_ID()} | {prod.Name} | {prod.Price} $ | {prod.Category} | {prod.Get_CountProdInStock()}");
            }
            Console.WriteLine("_____________________________________");
        }
    }

    class Product
    {

        private static int nextId = 1;
        private  int ID;
        public string Name;
        public double Price;
        private int CountProdInStock;
        public string Category;

        public Product(string name, double price, int count, string category)
        {
            ID = nextId++;
            Name = name;
            Price = price;
            Category = category;
            CountProdInStock = count;
        }

        public bool Get_HaveProdInStock() { return CountProdInStock > 0; }
        public int Get_CountProdInStock() { return CountProdInStock; }
        public int Get_ID() { return ID;}

        public void UvelichCount(int count) { CountProdInStock += count; }
        public void ReduceCount(int count) { CountProdInStock -= count; }

    }



}
