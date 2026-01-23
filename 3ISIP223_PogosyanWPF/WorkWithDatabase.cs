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
                //return;
            }
            else
            {
                var newKorzina = new Korzina
                {
                    ProductID = product.ProductID,
                    Quantity = 1
                };

                Korzinas.Add(newKorzina);
                Core.Context.Korzinas.Add(newKorzina);
            }
            Core.Context.SaveChanges();

            OnPropertyChanged(nameof(LenKorzina));
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
