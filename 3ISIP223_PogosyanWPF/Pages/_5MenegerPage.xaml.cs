using System;
using System.Windows;
using System.Windows.Controls;
using _3ISIP223_PogosyanWPF.ViewModels;
using _3ISIP223_PogosyanWPF.Winodws;

namespace _3ISIP223_PogosyanWPF.Pages
{
    public partial class _5MenegerPage : Page
    {
        private ManagerViewModel _viewModel;

        public _5MenegerPage()
        {
            InitializeComponent();
            _viewModel = (ManagerViewModel)DataContext;
        }

        // ЗАПИСИ
        private void CreateAppointment_Click(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button)?.Tag as User;
            if (client != null)
            {
                var wind = new CreateAppointmentWindow(_viewModel, client);
                
                wind.ShowDialog();
            }
        }

        private void RescheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            var appointment = (sender as Button)?.Tag as Appointment;
            if (appointment != null)
            {
                var wind = new RescheduleAppointmentWindow(_viewModel, appointment);
                wind.ShowDialog();
            }
        }

        private void CancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            var appointment = (sender as Button)?.Tag as Appointment;
            if (appointment != null)
            {
                _viewModel.CancelAppointment(appointment);
            }
        }

        // ЗАКАЗЫ
        private void CloseOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = (sender as Button)?.Tag as Order;
            if (order != null)
            {
                _viewModel.CloseOrder(order);
            }
        }

        // ТОВАРЫ
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddEditProductWindow(_viewModel);
            wind.ShowDialog();
        }

        //private void EditProduct_Click(object sender, RoutedEventArgs e)
        //{
        //    var product = (sender as Button)?.Tag as Product;
        //    if (product != null)
        //    {
        //        //var wind = new AddEditProductWindow(_viewModel, product);
        //        //wind.ShowDialog();
        //    }
        //}

        private void ToggleFreezeProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button)?.Tag as Product;
            if (product != null)
            {
                _viewModel.ToggleFreezeProduct(product);
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button)?.Tag as Product;
            if (product != null)
            {
                var wind = new EditDiscountWindow(_viewModel, product);
                wind.ShowDialog();
            }
        }

        // ПРОИЗВОДИТЕЛИ
        private void AddManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddEditManufacturerWindow(_viewModel);
            wind.ShowDialog();
        }

        private void EditManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var manufacturer = (sender as Button)?.Tag as Manufacturer;
            if (manufacturer != null)
            {
                var wind = new AddEditManufacturerWindow(_viewModel, manufacturer);
                wind.ShowDialog();
            }
        }

        private void DeleteManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var manufacturer = (sender as Button)?.Tag as Manufacturer;
            if (manufacturer != null)
            {
                _viewModel.DeleteManufacturer(manufacturer);
            }
        }

        //ТИПЫ ТОВАРОВ
        private void AddProductType_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddEditProductTypeWindow(_viewModel);
            wind.ShowDialog();
        }

        private void EditProductType_Click(object sender, RoutedEventArgs e)
        {
            var productType = (sender as Button)?.Tag as ProductType;
            if (productType != null)
            {
                var wind = new AddEditProductTypeWindow(_viewModel, productType);
                wind.ShowDialog();
            }
        }

        private void DeleteProductType_Click(object sender, RoutedEventArgs e)
        {
            var productType = (sender as Button)?.Tag as ProductType;
            if (productType != null)
            {
                _viewModel.DeleteProductType(productType);
            }
        }

        // ТИПЫ УСЛУГ
        private void AddServiceType_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddEditServiceTypeWindow(_viewModel);
            wind.ShowDialog();
        }

        private void EditServiceType_Click(object sender, RoutedEventArgs e)
        {
            var serviceType = (sender as Button)?.Tag as TypeService;
            if (serviceType != null)
            {
                var wind = new AddEditServiceTypeWindow(_viewModel, serviceType);
                wind.ShowDialog();
            }
        }

        private void DeleteServiceType_Click(object sender, RoutedEventArgs e)
        {
            var serviceType = (sender as Button)?.Tag as TypeService;
            if (serviceType != null)
            {
                _viewModel.DeleteServiceType(serviceType);
            }
        }
    }
}