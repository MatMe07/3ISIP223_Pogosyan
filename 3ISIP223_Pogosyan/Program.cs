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
        private List<Product> products;
        private int ID = 0;
        static void Main(string[] args)
        {
            List<string> category = new List<string>() { "продукты питания", "одежда", "игрушки", "электроника" };

            int n = 0;
            do
            {
                Console.Write("=== УЧЕТ ТОВАРОВ В МАГАЗИНЕ ===\nВыберите действие:\n\n1. Добавить новый товар\n2. Удалить товар\n3. Заказать поставку товара\n4. Продать товар\n5. Поиск товаров\n6. Показать все товары\n0. Выйти из программы\n\nВведите номер команды: ");
                n = Convert.ToInt32(Console.ReadLine());
                switch (n) { 
                case 0:break;
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
                case 6:
                        {
                            break;
                        }
                default: { Console.WriteLine("Такого пункта нет!!!!"); break; }
                }
            }
            while (n != 0);
            Console.WriteLine("Удачи");

        }
        int AddProd(string name, double price, int count, string categoric)
        {
            if (name.Length == 0) return 2;
            else if (price <= 0) return -1;
            else if (count <= 0) return 0;
            Product prod = new Product(name, price, count, categoric);
            if (!prod.Get_HaveStock()) return 3;
            products.Add(prod);
            return 1;
        }

        bool DelProd(int id)
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

        bool AddCountProd(int id, int count)
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

        public bool DelCountProd(int id, int count) {

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

        public bool FindProdByID(int id)
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
        public bool FindProdByName(string name)
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
        public bool FindProdByCategory(string category)
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

        void PrintProducts()
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
