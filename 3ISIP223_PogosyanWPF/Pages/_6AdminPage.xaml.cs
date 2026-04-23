using System.Windows;
using System.Windows.Controls;
using _3ISIP223_PogosyanWPF.ViewModels;
using _3ISIP223_PogosyanWPF.Winodws;

namespace _3ISIP223_PogosyanWPF.Pages
{
    public partial class _6AdminPage : Page
    {
        private AdminViewModel _viewModel;

        public _6AdminPage()
        {
            InitializeComponent();
            _viewModel = (AdminViewModel)DataContext;

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddEditUserWindow(_viewModel);
            wind.ShowDialog();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button)?.Tag as User;
            if (user != null)
            {
                var wind = new AddEditUserWindow(_viewModel, user);
                wind.ShowDialog();
            }
        }

        private void FreezeUser_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button)?.Tag as User;
            if (user != null)
            {
                var result = MessageBox.Show($"Заморозить пользователя {user.LastName} {user.FirstName}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _viewModel.FreezeUser(user);
                }
            }
        }

        private void UnfreezeUser_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button)?.Tag as User;
            if (user != null)
            {
                var result = MessageBox.Show($"Разморозить пользователя {user.LastName} {user.FirstName}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _viewModel.UnfreezeUser(user);
                }
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button)?.Tag as User;
            if (user != null)
            {
                _viewModel.DeleteUser(user);
            }
        }

        private void Role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var user = comboBox?.Tag as User;
            var newRole = comboBox?.SelectedItem as TypeRole;

            if (user != null && newRole != null)
            {
                _viewModel.ChangeUserRole(user, newRole);
            }
        }
    }
}