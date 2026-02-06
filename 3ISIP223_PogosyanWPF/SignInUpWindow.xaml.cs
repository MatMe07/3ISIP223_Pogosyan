using _3ISIP223_PogosyanWPF.Pages;
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
using System.Windows.Shapes;

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Логика взаимодействия для SignInUpWindow.xaml
    /// </summary>
    public partial class SignInUpWindow : Window
    {

        public static SignInUpWindow Instance { get; private set; }


        public SignInUpWindow()
        {
            InitializeComponent();
            Instance = this;
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
