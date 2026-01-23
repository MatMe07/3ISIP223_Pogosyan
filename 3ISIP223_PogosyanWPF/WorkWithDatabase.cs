using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public class WorkWithDatabase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //private string _userName = Core.Context.Users.First().FullName;
        private ObservableCollection<Product> _products;
        private ObservableCollection<Korzina> _korzina;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ObservableCollection<Korzina> Korzinas
        {
            get { return _korzina; }
            set
            {
                _korzina = value;
                //Core.Context.Korzinas = 
                OnPropertyChanged(nameof(Korzinas));
            }
        }

        public int LenKorzina { 
            get
            {
                return Korzinas.Sum(s=>s.Quantity);
            }

        }
        public decimal TotalPrice
        {
            get
            {
                return Korzinas.Sum(p => p.Quantity * p.Product.Price);
            }
        }

        public WorkWithDatabase()
        {
            Products = new ObservableCollection<Product>(Core.Context.Products.ToList());
            Korzinas = new ObservableCollection<Korzina>(Core.Context.Korzinas.ToList());

        }

        public void AddKorzina(string productName)
        {
            var product = Products.FirstOrDefault(p=>p.Name ==  productName);
            var korzina1 = Korzinas.FirstOrDefault(p => p.Product.Name == productName);
            if ( korzina1 != null)
            {
                korzina1.Quantity++;
                korzina1.TotalPrice = korzina1.Quantity * korzina1.Product.Price;
                //return;
            }
            else
            {
                var newKorzina = new Korzina
                {
                    ProductID = product.ProductID,
                    Quantity = 1,
                    TotalPrice = product.Price
                };

                Korzinas.Add(newKorzina);
                Core.Context.Korzinas.Add(newKorzina);
            }
            Core.Context.SaveChanges();

            OnPropertyChanged(nameof(LenKorzina));
            OnPropertyChanged(nameof(TotalPrice));
        }

        public void DeleteKorzina(string productName)
        {
            var korzina1 = _korzina.FirstOrDefault(p => p.Product.Name == productName);
            if (korzina1.Quantity > 1)
            {
                korzina1.Quantity--;
                korzina1.TotalPrice = korzina1.Quantity * korzina1.Product.Price;
            }
            else
            {
                Korzinas.Remove(korzina1);
                Core.Context.Korzinas.Remove(korzina1);
            }
            Core.Context.SaveChanges();

            //Korzinas = new ObservableCollection<Korzina>(Korzinas.ToList());
            //Korzinas = _korzina;
            //OnPropertyChanged(nameof(Korzinas));
            OnPropertyChanged(nameof(LenKorzina));
            OnPropertyChanged(nameof(TotalPrice));
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class MarWorkWithDatabase
    {

        public static WorkWithDatabase withDatabase { get { return new WorkWithDatabase(); } }
    }
}
