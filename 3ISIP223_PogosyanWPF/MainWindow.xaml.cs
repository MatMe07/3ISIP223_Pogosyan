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
using _3ISIP223_PogosyanWPF.Pages;

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mainFrame.NavigationService.Navigate(new _1MainPage());
        }

        private void btnSignUpOrIn_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content)
            {
                case "Вход":
                    {
                        mainFrame.NavigationService.Navigate(new _6SignInPage());
                        break;
                    }
                case "Регистрация":
                    {
                        mainFrame.NavigationService.Navigate(new _7SignUpPage());
                        break;
                    }
            }
        }

        private void btnPersonalAcc_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
