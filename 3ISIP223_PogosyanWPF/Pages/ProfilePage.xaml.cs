using _3ISIP223_PogosyanWPF.Pages.ProfilePages;
using _3ISIP223_PogosyanWPF.ViewModels;
using _3ISIP223_PogosyanWPF.Windows;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            frameProfilePage.NavigationService.Navigate(new ProfileMainPage());
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectTabItem = (sender as TabControl).SelectedItem as TabItem;
            //if ()
            //lst.Clear();
            if (selectTabItem != null)
            {

                switch (selectTabItem.Tag.ToString())
            {
                case "Мои отзывы":
                    {
                        frameProfilePage.NavigationService.Navigate(new ProfileMyReviewsPage());
                        break;
                    }
                case "Главная":
                    {
                        frameProfilePage.NavigationService.Navigate(new ProfileMainPage());

                        break;
                    }
                case "Уведомления":
                    {
                        frameProfilePage.NavigationService.Navigate(new ProfileNotificationPage());

                        break;
                    }

            }
            }
        }

        public void BackToMainPage()
        {
            tabControlPage.SelectedIndex = 0;
        }


        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            frameProfilePage.NavigationService.Navigate(new ProfileEditPage(BackToMainPage));
            tabControlPage.SelectedIndex = -1;

        }

        private void btnExitAcc_Click(object sender, RoutedEventArgs e)
        {
            {
                var result = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?",
                    "Подтверждение выхода",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    WorkDataBase.Instanse.User = null;

                    var newMainWindow = new MainWindow();

                    MainWindow.GetInstance().Close();
                    newMainWindow.Show();

                }
            }
        }
    }
}
