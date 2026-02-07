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
        private Window ParentWindow;
        public _7SignUpPage()
        {
            InitializeComponent();
            //ParentWindow = window;
            //txtBirthDate.date.
            database = MarDatabase.WIthDatabase;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtBirthDate.SelectedDate.ToString())|| string.IsNullOrEmpty(txtPasswordAgain.Text)   )
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Некорректный формат email");
                txtEmail.Focus();
                txtEmail.SelectAll();
                return;
            }

            if (txtPassword.Text != txtPasswordAgain.Text)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }


            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Пароль слишком короткий (минимум 8 символов)");
                txtPassword.Focus();
                txtPassword.SelectAll();
                return;

            }
            else
            {
                if(txtPassword.Text.Sum(s=> Convert.ToInt32(s.ToString().ToUpper() == s.ToString())) == 0 )
                {
                    MessageBox.Show("Пароль должен содержать хотя бы одну заглавную букву");
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                    return;
                }
                if (txtPassword.Text.Sum(s => Convert.ToInt32(char.IsDigit(s) )) == 0)
                {
                    MessageBox.Show("Пароль должен содержать хотя бы одну цифру");
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                    return;
                }
            }


            if (database.CheckValueAndSave(txtName.Text, txtEmail.Text, txtPassword.Text, txtBirthDate.SelectedDate.Value))
            {
                MessageBox.Show("Аккаунт успешно создан");

                SignInUpWindow.Instance.Close();
                //return;
            }
            else
            {
                MessageBox.Show("Пользователь с таким email уже существует");
            }
            //NavigationService.GoBack();
            //ParentWindow.Close();
            //var mainWIndow = Application.Current.MainWindow as MainWindow;
            //mainWIndow.Close();

        }
    }
}
