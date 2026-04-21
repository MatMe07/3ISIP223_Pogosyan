using System.Windows;
using System.Windows.Controls;
using _3ISIP223_PogosyanWPF.ViewModels;

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

        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            var service = (sender as Button)?.Tag as Service;
            if (service != null)
            {
                _viewModel.UpdateService(service);
            }
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}