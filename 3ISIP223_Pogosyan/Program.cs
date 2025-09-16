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
            List<string> categoric = new List<string>() { "продукты питания", "одежда", "игрушки", "электроника" };


        }
        int AddProd(string name, double price, int count, string categoric)
        {
            if (name.Length == 0) return 2;
            else if (price <= 0) return -1;
            else if (count <= 0) return 0;
            Product prod = new Product(ID, name, price, count, categoric);
            if (!prod.Get_HaveStock()) return 3;
            products.Add(prod);
            ID++;
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

    }

    class Product
    {
        private int ID;
        public string Name;
        public double Price;
        public int Count;
        private static int HaveStock = 100;
        public string Categoric;

        public Product(int iD, string name, double price, int count, string categoric)
        {
            ID = iD;
            Name = name;
            Price = price;
            Count = count;
            Categoric = categoric;
            HaveStock -= count;
        }

        public bool Get_HaveStock() { return HaveStock > 0; }
        public int Get_countHaveStock() { return HaveStock; }
        public int Get_ID() { return ID;}

        public void UvelichCount(int count) { HaveStock += count; }
        public void ReduceCount(int count) { HaveStock -= count; }

    }



}
