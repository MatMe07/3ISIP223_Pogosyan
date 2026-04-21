using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WorkDataBase dataBase;
        private Cart _currentCart;
        private List<ProdCart> _allCartItems;

        public CartViewModel()
        {
            dataBase = WorkDataBase.Instance;

            CartItems = new ObservableCollection<ProdCart>();

            LoadAllData();
        }

        void LoadAllData()
        {
            _currentCart = dataBase.GetCurrentUserCart;

            if (_currentCart != null)
            {
                _allCartItems = dataBase.GetCartItems(_currentCart.Cart_ID).ToList();
            }
            else
            {
                _allCartItems = new List<ProdCart>();
            }

            UpdateCartItemsList();
            UpdateTotals();
        }

        //private ObservableCollection<ProdCart> _cartItems;
        public ObservableCollection<ProdCart> CartItems { get; set; }
        //{
        //    get { return _cartItems; }
        //    set
        //    {
        //        _cartItems = value;
        //        OnPropertyChanged(nameof(CartItems));

        //    }
        //}

        private int _totalQuantity;
        public int TotalQuantity
        {
            get { return _totalQuantity; }
            set
            {
                _totalQuantity = value;
                OnPropertyChanged(nameof(TotalQuantity));

            }
        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));

            }
        }

        void UpdateCartItemsList()
        {
            CartItems.Clear();
            foreach (var item in _allCartItems)
            {
                CartItems.Add(item);
            }
        }

        void UpdateTotals()
        {
            TotalQuantity = CartItems.Sum(i => i.Quantity);
            TotalPrice = CartItems.Sum(i => i.Product.Price * (100 - i.Product.Discount) / 100 * i.Quantity);
        }

        public void RemoveFromCart(ProdCart item)
        {
            _allCartItems.Remove(item);
            dataBase.RemoveFromCart(item.Cart_ID, item.Product_ID);
            UpdateCartItemsList();
            UpdateTotals();

        }

        public void UpdateQuantity(ProdCart item, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                RemoveFromCart(item);
                return;
            }

            item.Quantity = newQuantity;

            //var index = _allCartItems.FindIndex(i => i.ProdCarts == item.ProdCarts);
            //if (index >= 0)
            //{
            //_allCartItems[index].Quantity = newQuantity;

            dataBase.UpdateCartQuantity(item.Cart_ID, item.Product_ID, newQuantity);

            //CartItems[index] = _allCartItems[index] ;

            _currentCart.TotalPrice = TotalPrice;
            _currentCart.Quantity = TotalQuantity;
            dataBase.UpdateCartTotal(_currentCart);

            UpdateCartItemsList();
            UpdateTotals();

        }

        public void IncreaseQuantity(ProdCart item)
        {
            UpdateQuantity(item, item.Quantity + 1);
        }

        public void DecreaseQuantity(ProdCart item)
        {
            UpdateQuantity(item, item.Quantity - 1);
        }

        public void CreateOrder()
        {
            if (_allCartItems.Count == 0) return;

            dataBase.CreateOrderFromCart(_currentCart, _allCartItems);

            CartItems.Clear();
            _currentCart.TotalPrice = 0;
            _currentCart.Quantity = 0;
            dataBase.UpdateCartTotal(_currentCart);

            UpdateCartItemsList();
            UpdateTotals();
        }
    }
}