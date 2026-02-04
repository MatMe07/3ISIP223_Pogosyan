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
    /// Логика взаимодействия для _7SignUpPage.xaml
    /// </summary>
    public partial class _7SignUpPage : Page
    {
        public WorkWIthDatabase database;
        public _7SignUpPage()
        {
            InitializeComponent();
            //txtBirthDate.date.
            database = MarDatabase.WIthDatabase;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtBirthDate.SelectedDate.ToString()))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            if (database.CheckValueAndSave(txtName.Text, txtEmail.Text, txtPassword.Text, txtBirthDate.SelectedDate.Value))
            {
                MessageBox.Show("Аккаунт успешно создан");

                //return;
            }
            else
            {
                MessageBox.Show("Пользователь с таким email уже существует");
            }
            NavigationService.GoBack();
        }
    }
}
