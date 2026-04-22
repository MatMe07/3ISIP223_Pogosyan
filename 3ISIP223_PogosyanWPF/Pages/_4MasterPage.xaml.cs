using System;
using System.Windows;
using System.Windows.Controls;
using _3ISIP223_PogosyanWPF.ViewModels;
using _3ISIP223_PogosyanWPF.Winodws;

namespace _3ISIP223_PogosyanWPF.Pages
{
    public partial class _4MasterPage : Page
    {
        private MasterViewModel _viewModel;

        public _4MasterPage()
        {
            InitializeComponent();
            _viewModel = (MasterViewModel)DataContext;
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button)?.Tag as Service;
            if (service != null)
            {
                var result = MessageBox.Show($"Удалить услугу \"{service.TypeService?.Name}\"?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _viewModel.RemoveService(service);
                }
            }
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            var wind = new InsertServiceWindow(_viewModel);
            wind.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wind.ShowDialog();
        }

        private void lstAppoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstAppoints.SelectedIndex = -1;
        }
    }
}