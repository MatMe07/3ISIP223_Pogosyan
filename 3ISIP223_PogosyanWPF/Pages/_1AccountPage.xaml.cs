using System.Windows;
using System.Windows.Controls;
using _3ISIP223_PogosyanWPF.ViewModels;

namespace _3ISIP223_PogosyanWPF.Pages
{
    public partial class _1AccountPage : Page
    {
        private AccountViewModel _viewModel;

        public _1AccountPage()
        {
            InitializeComponent();
            _viewModel = (AccountViewModel)DataContext;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}