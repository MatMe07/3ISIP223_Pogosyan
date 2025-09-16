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

            int n = 0;
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
                            Console.WriteLine("-----------------------------");
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

                            int add = AddProd(name, price, count, categor);
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
                case 6:
                        {
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
        static int AddProd(string name, double price, int count, string categoric)
        {
            if (name.Length == 0) return 3;
            else if (price <= 0) return 2;
            else if (count <= 0) return 1;
            Product prod = new Product(name, price, count, categoric);
            if (!prod.Get_HaveStock()) return 4;
            products.Add(prod);
            return 0;
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

        static public bool AddCountProd(int id, int count)
        {
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

        static public bool DelCountProd(int id, int count) {

            foreach (Product prod in products)
            {
                if (prod.Get_ID() == id)
                {
                    if (prod.Get_countHaveStock() - count > 0) { 
                        prod.ReduceCount(count);
                        return true;
                    }
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
                Console.WriteLine($"{prod.Get_ID} | {prod.Name} | {prod.Price} $ | {prod.Category}");
            }
            Console.WriteLine("_____________________________________");
        }
    }

    class Product
    {
        private static int ID = 0;
        public string Name;
        public double Price;
        public int Count;
        private static int HaveStock = 100;
        public string Category;

        public Product(string name, double price, int count, string category)
        {
            ID++;
            Name = name;
            Price = price;
            Count = count;
            Category = category;
            HaveStock -= count;
        }

        public bool Get_HaveStock() { return HaveStock > 0; }
        public int Get_countHaveStock() { return HaveStock; }
        public int Get_ID() { return ID;}

        public void UvelichCount(int count) { HaveStock += count; }
        public void ReduceCount(int count) { HaveStock -= count; }

    }



}
