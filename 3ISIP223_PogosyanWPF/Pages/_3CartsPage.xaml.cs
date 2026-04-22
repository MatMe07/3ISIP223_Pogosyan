using _3ISIP223_PogosyanWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3ISIP223_PogosyanWPF.Pages
{
    public partial class _3CartsPage : Page
    {
        private CartViewModel _viewModel;

        public _3CartsPage()
        {
            InitializeComponent();
            _viewModel = (CartViewModel)DataContext;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CreateOrder();
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.Tag as ProdCart;
            if (item != null)
            {
                _viewModel.IncreaseQuantity(item);
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.Tag as ProdCart;
            if (item != null)
            {
                _viewModel.DecreaseQuantity(item);
            }
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.Tag as ProdCart;
            if (item != null)
            {
                _viewModel.RemoveFromCart(item);
            }
        }
    }
}