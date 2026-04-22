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
    /// <summary>
    /// Логика взаимодействия для _2ProtductsPage.xaml
    /// </summary>
    public partial class _2ProtductsPage : Page
    {
     
        public ProductViewModel ProductViewModel { get; set; }
        public _2ProtductsPage()
        {
            InitializeComponent();
            DataContext = ProductViewModel = new ProductViewModel();
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            var context = (sender as Button).Tag as Product;
            ProductViewModel.AddToCart(context);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _0StartedMainPage.GetMainPageFrame.NavigationService.GoBack();   
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            ProductViewModel.ResetFilters();
        }

        private void btnGoToCartPage_Click(object sender, RoutedEventArgs e)
        {
            _0StartedMainPage.GetMainPageFrame.NavigationService.Navigate(new _3CartsPage());
        }
    }
}
