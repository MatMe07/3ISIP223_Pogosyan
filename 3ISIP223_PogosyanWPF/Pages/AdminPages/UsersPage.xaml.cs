using _3ISIP223_PogosyanWPF.ViewModels.AdminViewModels;
using _3ISIP223_PogosyanWPF.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _3ISIP223_PogosyanWPF.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();


        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            var wind = new UserEditWindow((sender as Button).Tag as User);
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
            (DataContext as UsersViewModel).LoadUser();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as UsersViewModel).LoadUser();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comb = (sender as ComboBox);
            (DataContext as UsersViewModel).SaveRole(comb.Tag as User, comb.SelectedItem as Role);

        }
    }
}
