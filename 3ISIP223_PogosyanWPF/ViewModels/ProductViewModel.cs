using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WorkDataBase dataBase;
        private List<Product> _allProducts;

        public ProductViewModel()
        {
            dataBase = WorkDataBase.Instance;

            Products = new ObservableCollection<Product>();
            TypeLst = new ObservableCollection<string>();
            ManufacturerLst = new ObservableCollection<string>();

            LoadAllData();

            SelectedSort = "По умолчанию";
        }

        void LoadAllData()
        {
            _allProducts = dataBase.AllProducts.ToList();

            TypeLst.Clear();
            TypeLst.Add("Все типы");

            if (_allProducts != null)
            {
                var distinctTypes = _allProducts
                    .Select(p => p.ProductType?.Name)
                    .Where(t => t != null)
                    .Distinct().ToList();

                foreach (var typeName in distinctTypes)
                {
                    TypeLst.Add(typeName);
                }
            }

            SelectedType = "Все типы";
            
            // 
            ManufacturerLst.Clear();
            ManufacturerLst.Add("Все производители");

            if (_allProducts != null)
            {
                var distinctManufacturers = _allProducts
                    .Select(p => p.Manufacturer?.Name)
                    .Where(m => m != null)
                    .Distinct();

                foreach (var manName in distinctManufacturers)
                {
                    ManufacturerLst.Add(manName);
                }
            }

            SelectedManufacturer = "Все производители";
            

            ApplyFilter();
        }

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<string> TypeLst { get; set; }

        public ObservableCollection<string> ManufacturerLst { get; set; }

        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
                ApplyFilter();
            }
        }

        private string _selectedManufacturer;
        public string SelectedManufacturer
        {
            get { return _selectedManufacturer; }
            set
            {
                _selectedManufacturer = value;
                OnPropertyChanged(nameof(SelectedManufacturer));
                ApplyFilter();
            }
        }

        private string _selectedSort;
        public string SelectedSort
        {
            get { return _selectedSort; }
            set
            {
                _selectedSort = value;
                OnPropertyChanged(nameof(SelectedSort));
                ApplyFilter();
            }
        }

        public List<string> SortLst { get; set; } = new List<string>
        {
            "По умолчанию",
            "По возрастанию",
            "По убыванию",
        };

        public void AddToCart(Product product)
        {
            Console.WriteLine(product.Name);
        }

        void ApplyFilter()
        {
            var filtered = _allProducts.AsEnumerable();

            if (SelectedType != null && SelectedType != "Все типы")
            {
                filtered = filtered.Where(p => p.ProductType?.Name == SelectedType);
            }

            if (SelectedManufacturer != null && SelectedManufacturer != "Все производители")
            {
                filtered = filtered.Where(p => p.Manufacturer?.Name == SelectedManufacturer);
            }

            if (SelectedSort == "По возрастанию")
            {
                filtered = filtered.OrderBy(p => p.Rating);
            }
            else if (SelectedSort == "По убыванию")
            {
                filtered = filtered.OrderByDescending(p => p.Rating);
            }

            Products.Clear();
            foreach (var product in filtered)
            {
                Products.Add(product);
            }
        }

        public int GetCountCart => dataBase.GetCountCart;

        public void ResetFilters()
        {
            SelectedType = "Все типы";
            SelectedManufacturer = "Все производители";
            SelectedSort = "По умолчанию";
        }
    }
}