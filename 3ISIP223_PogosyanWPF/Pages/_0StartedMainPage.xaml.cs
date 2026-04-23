using System.Windows;
using System.Windows.Controls;
using _3ISIP223_PogosyanWPF.ViewModels;
using _3ISIP223_PogosyanWPF.Winodws;

namespace _3ISIP223_PogosyanWPF.Pages
{
    public partial class _0StartedMainPage : Page
    {
        public static Frame GetMainPageFrame;
        private WorkDataBase _dataBase;

        public _0StartedMainPage()
        {
            InitializeComponent();
            GetMainPageFrame = mainPageFrame;
            _dataBase = WorkDataBase.Instance;

            UpdateButtonsVisibility();
        }

        private void UpdateButtonsVisibility()
        {
            if (_dataBase.CurrentUser == null)
            {
                AccountPageBtn.Visibility = Visibility.Collapsed;
                btnSign.Visibility = Visibility.Visible;
                btnOutAcc.Visibility = Visibility.Collapsed;
            }
            else
            {
                AccountPageBtn.Visibility = Visibility.Visible;
                btnSign.Visibility = Visibility.Collapsed;
                btnOutAcc.Visibility = Visibility.Visible;

            }

            if (_dataBase.CurrentUser != null && (_dataBase.CurrentUser.TypeRole.Name == "Администратор" || _dataBase.CurrentUser.TypeRole.Name == "Менеджер" || _dataBase.CurrentUser.TypeRole.Name == "Мастер"))
            {
                btnProducts.Visibility = Visibility.Collapsed;
                btnOutAcc.Visibility = Visibility.Visible;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageFrame.Navigate(new _2ProtductsPage());
        }

        private void GoToAcc_Click(object sender, RoutedEventArgs e)
        {
            if (_dataBase.CurrentUser == null)
            {
                MessageBox.Show("Войдите в аккаунт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_dataBase.CurrentUser.TypeRole?.Name == "Клиент")
            {
                mainPageFrame.Navigate(new _1AccountPage());
            }
            else if (_dataBase.CurrentUser.TypeRole?.Name == "Мастер")
            {
                mainPageFrame.Navigate(new _4MasterPage());
            }
            else if (_dataBase.CurrentUser.TypeRole?.Name == "Менеджер")
            {
                mainPageFrame.Navigate(new _5MenegerPage());
                //managerWindow.ShowDialog();
            }
            else if (_dataBase.CurrentUser.TypeRole?.Name == "Администратор")
            {
                mainPageFrame.Navigate(new _6AdminPage());
                //adminWindow.ShowDialog();
            }
        }
        private void BtnSignIN_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginRegisterWindow();
            loginWindow.Owner = Window.GetWindow(this);

            if (loginWindow.ShowDialog() == true)
            {
                UpdateButtonsVisibility();
                if (_dataBase.CurrentUser.TypeRole?.Name == "Мастер")
                {
                    mainPageFrame.Navigate(new _4MasterPage());
                }
                else if (_dataBase.CurrentUser.TypeRole?.Name == "Менеджер")
                {
                    mainPageFrame.Navigate(new _5MenegerPage());
                    //managerWindow.ShowDialog();
                }
                else if (_dataBase.CurrentUser.TypeRole?.Name == "Администратор")
                {
                    mainPageFrame.Navigate(new _6AdminPage());
                    //adminWindow.ShowDialog();
                }
            }
        }
        private void BtnAccOut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?",
                "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _dataBase.CurrentUser = null;
                _dataBase.ClearCartCache();

                UpdateButtonsVisibility();

                mainPageFrame.Navigate(new _0FiltersStartedMainPage());

                MessageBox.Show("Вы вышли из аккаунта", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}