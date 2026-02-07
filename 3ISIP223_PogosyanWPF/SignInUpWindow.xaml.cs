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
            //var parWindow =  Window.GetWindow(this).Owner;
            
            switch ((sender as Button).Content)
            {
                case "Вход":
                    {
                        Title = "Вход";
                        frameSign.NavigationService.Navigate(new _6SignInPage());
                        Height = 305;
                        Top = Owner.Top + 157;
                        //Left = Owner.Left + Owner.Left/2;

                        break;
                    }
                case "Регистрация":
                    {
                        Title = "Регистрация";
                        frameSign.NavigationService.Navigate(new _7SignUpPage());
                        Height = 525;
                        Top = Owner.Top + 50;
                        //Left = Owner.Left + Owner.Left / 2;
                        break;
                    }
            }
        }
    }
}
