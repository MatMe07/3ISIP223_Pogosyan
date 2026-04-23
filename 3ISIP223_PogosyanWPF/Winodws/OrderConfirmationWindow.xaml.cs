using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class OrderConfirmationWindow : Window
    {
        private List<ProdCart> _cartItems;
        private decimal _totalAmount;
        private DateTime _selectedDeliveryDate;
        private int _selectedPaymentMethodId;

        public DateTime SelectedDeliveryDate => _selectedDeliveryDate;
        public int SelectedPaymentMethodId => _selectedPaymentMethodId;

        public OrderConfirmationWindow(List<ProdCart> cartItems, decimal totalAmount)
        {
            InitializeComponent();
            _cartItems = cartItems;
            _totalAmount = totalAmount;

            LoadOrderItems();
            LoadDeliveryDates();

            txtTotalAmount.Text = $"Итого: {_totalAmount:F2} ₽";
        }

        private void LoadOrderItems()
        {
            var itemsWithTotal = _cartItems.Select(item => new
            {
                Product = item.Product,
                Quantity = item.Quantity,
                TotalPrice = item.Product.Price * (100 - item.Product.Discount) / 100 * item.Quantity
            }).ToList();

            lstOrderItems.ItemsSource = itemsWithTotal;
        }

        private void LoadDeliveryDates()
        {
            var dates = new List<DeliveryDateItem>();
            var today = DateTime.Today;

            for (int i = 1; i <= 7; i++)
            {
                dates.Add(new DeliveryDateItem
                {
                    Date = today.AddDays(i),
                    IsSelected = (i == 1)
                });
            }

            lstDeliveryDates.ItemsSource = dates;
            _selectedDeliveryDate = dates.First(d => d.IsSelected).Date;
            btnConfirm.IsEnabled = true;
        }

        private void DeliveryDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = lstDeliveryDates.SelectedItem as DeliveryDateItem;
            if (selectedItem != null)
            {
                foreach (var item in lstDeliveryDates.Items)
                {
                    var dateItem = item as DeliveryDateItem;
                    if (dateItem != null)
                    {
                        dateItem.IsSelected = (dateItem == selectedItem);
                    }
                }
                _selectedDeliveryDate = selectedItem.Date;
            }
        }

        private void PaymentMethod_Checked(object sender, RoutedEventArgs e)
        {
            if (rbCash.IsChecked == true)
                _selectedPaymentMethodId = 1;
            else if (rbCard.IsChecked == true)
                _selectedPaymentMethodId = 2;
            else if (rbOnline.IsChecked == true)
                _selectedPaymentMethodId = 3;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

    public class DeliveryDateItem
    {
        public DateTime Date { get; set; }
        public bool IsSelected { get; set; }
    }
}