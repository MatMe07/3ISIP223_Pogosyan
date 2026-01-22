using System;
using System.Collections.Generic;
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
        private List<Product> _products = Core.Context.Products.ToList();
        public List<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
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
