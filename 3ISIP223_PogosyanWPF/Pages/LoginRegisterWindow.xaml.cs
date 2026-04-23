using _3ISIP223_PogosyanWPF.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class LoginRegisterWindow : Window
    {
        private WorkDataBase _dataBase;

        public LoginRegisterWindow()
        {
            InitializeComponent();
            _dataBase = WorkDataBase.Instance;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string phone = txtLoginPhone.Text.Trim();
            string password = txtLoginPassword.Password;

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Введите номер телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = _dataBase.Users.FirstOrDefault(u => u.Phone == phone);

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (user.password != password)
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (user.IsFrozen)
            {
                MessageBox.Show("Ваш аккаунт заморожен. Обратитесь к администратору.",
                    "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _dataBase.CurrentUser = user;

            MessageBox.Show($"Добро пожаловать, {user.FirstName}!",
                "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string lastName = txtRegLastName.Text.Trim();
            string firstName = txtRegFirstName.Text.Trim();
            string secondName = txtRegSecondName.Text.Trim();
            string phone = txtRegPhone.Text.Trim();
            string email = txtRegEmail.Text.Trim();
            string password = txtRegPassword.Password;
            string confirmPassword = txtRegConfirmPassword.Password;

            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Введите номер телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_dataBase.Users.Any(u => u.Phone == phone))
            {
                MessageBox.Show("Пользователь с таким номером телефона уже существует",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUser = new User
            {
                LastName = lastName,
                FirstName = firstName,
                SecondName = secondName,
                Phone = phone,
                Email = email,
                Role_ID = 1,
                IsFrozen = false,
                password = password
                
            };

            _dataBase.AddUser(newUser);

            MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти.",
                "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            txtLoginPhone.Text = phone;
            txtLoginPassword.Password = "";

            //var tabControl = (TabControl)((TabItem)((Grid)Content).Children[1]).Parent;
            //tabControl.SelectedIndex = 0;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}