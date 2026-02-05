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
using System.Xml.Linq;

namespace _3ISIP223_PogosyanWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для _6SignInPage.xaml
    /// </summary>
    public partial class _6SignInPage : Page
    {
        private WorkWIthDatabase database;
        public _6SignInPage()
        {
            InitializeComponent();
            database = MarDatabase.WIthDatabase;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (database.CheckValueAndSignIn(txtEmail.Text, txtPassword.Text))
            {
                MessageBox.Show("Вход выполнен успешно!");

                //return;
            }
            else
            {
                MessageBox.Show("Пользователь не найден / Неверный пароль");

            }
            NavigationService.Navigate(new _1MainPage());
        }
    }
}
