using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private ObservableCollection<ProdCart> _cartItems;
        public ObservableCollection<ProdCart> CartItems
        {
            get { return _cartItems; }
            set
            {
                _cartItems = value;
                OnPropertyChanged(nameof(CartItems));
            }
        }

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

        public CartViewModel()
        {
            dataBase = WorkDataBase.Instance;
            CartItems = new ObservableCollection<ProdCart>();

            LoadAllData();
        }

        void LoadAllData()
        {
            UpdateCartItemsList();
            UpdateTotals();
        }

        void UpdateCartItemsList()
        {
            CartItems.Clear();
            if (dataBase.CurrentUserCartItems != null)
            {
                foreach (var item in dataBase.CurrentUserCartItems)
                {
                    CartItems.Add(item);
                }
            }
        }

        void UpdateTotals()
        {
            TotalQuantity = dataBase.GetCountCart;
            TotalPrice = dataBase.CurrentUserCart?.TotalPrice ?? 0;
        }

        public void RemoveFromCart(ProdCart item)
        {
            var result = MessageBox.Show($"Удалить товар {item.Product.Name} из корзины?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                dataBase.RemoveFromCart(item);
                UpdateCartItemsList();
                UpdateTotals();
                OnPropertyChanged(nameof(CartItems));
            }
        }

        public void UpdateQuantity(ProdCart item, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                RemoveFromCart(item);
                return;
            }

            dataBase.UpdateCartQuantity(item, newQuantity);
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
            if (dataBase.CurrentUserCartItems == null || dataBase.CurrentUserCartItems.Count == 0)
            {
                MessageBox.Show("Корзина пуста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dataBase.CurrentUser == null)
            {
                MessageBox.Show("Для оформления заказа необходимо войти в аккаунт",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Оформить заказ?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                dataBase.CreateOrderFromCart();
                UpdateCartItemsList();
                UpdateTotals();

                MessageBox.Show("Заказ успешно оформлен!",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}