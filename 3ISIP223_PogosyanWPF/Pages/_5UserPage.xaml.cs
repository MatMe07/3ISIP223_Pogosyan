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
    /// Логика взаимодействия для _5UserPage.xaml
    /// </summary>
    public partial class _5UserPage : Page
    {
        public _5UserPage()
        {
            InitializeComponent();
            if(MarDatabase.WIthDatabase.CurrUser == null)
            {
                gridUserPage.Visibility = Visibility.Collapsed;
                gridSign.Visibility = Visibility.Visible;
            }
            else
            {
                gridUserPage.Visibility = Visibility.Visible;
                gridSign.Visibility = Visibility.Collapsed;
            }
        }

        private void btnSignUpOrIn_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content)
            {
                case "Вход":
                    {
                        frameSign.NavigationService.Navigate(new _6SignInPage());
                        break;
                    }
                case "Регистрация":
                    {
                       frameSign.NavigationService.Navigate(new _7SignUpPage());
                        break;
                    }
            }
        }
    }
}
