using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class LoginRegisterViewModel : BaseViewModel
    {

        public LoginRegisterViewModel()
        {
            _messageQueue = new SnackbarMessageQueue();
        }

        private SnackbarMessageQueue _messageQueue;
        private string _login;
        private string _password;
        private string _regLogin;
        private string _regDisplayName;
        private string _regEmail;
        private string _regPassword;
        private string _regConfirmPassword;
        public SnackbarMessageQueue MyMessageQueue
        {
            get => _messageQueue;
            set
            {
                _messageQueue = value;
                OnPropertyChanged(nameof(MyMessageQueue));
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string RegLogin
        {
            get => _regLogin;
            set
            {
                _regLogin = value;
                OnPropertyChanged(nameof(RegLogin));
            }
        }

        public string RegDisplayName
        {
            get => _regDisplayName;
            set
            {
                _regDisplayName = value;
                OnPropertyChanged(nameof(RegDisplayName));
            }
        }

        public string RegEmail
        {
            get => _regEmail;
            set
            {
                _regEmail = value;
                OnPropertyChanged(nameof(RegEmail));
            }
        }

        public string RegPassword
        {
            get => _regPassword;
            set
            {
                _regPassword = value;
                OnPropertyChanged(nameof(RegPassword));
            }
        }

        public string RegConfirmPassword
        {
            get => _regConfirmPassword;
            set
            {
                _regConfirmPassword = value;
                OnPropertyChanged(nameof(RegConfirmPassword));
            }
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>True - вход выполнен, False - ошибка</returns>
        public bool LoginToAccc(string login, string password)

        {
            if (string.IsNullOrWhiteSpace(login))
            {
                ShowMessage("Введите логин", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowMessage("Введите пароль", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            var user = dataBase.Users.FirstOrDefault(u => u.Login == login);

            if (user == null)
            {
                ShowMessage("Пользователь не найден", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            if (user.Password != password)
            {
                ShowMessage("Неверный пароль", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            if (user.IsFrozen)
            {
                string reason = string.IsNullOrEmpty(user.Reason.Name) ? "без указания причины" : $"Причина: {user.Reason.Name}";
                ShowMessage($"Аккаунт заморожен. {reason}", "#FFBE0404", PackIconKind.Block);
                return false;
            }

            dataBase.User = user;
            ShowMessage($"Добро пожаловать, {user.DisplayName}!", "#FF04BE5A", PackIconKind.CheckCircle);
            return true;
        }
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="displayName">Отображаемое имя</param>
        /// <param name="email">Email</param>
        /// <param name="password">Пароль</param>
        /// <param name="confirmPassword">Подтверждение пароля</param>
        /// <returns>True - регистрация успешна, False - ошибка</returns>
        public bool Register(string login, string displayName, string email, string password, string confirmPassword)

        {
            if (string.IsNullOrWhiteSpace(login) || login.Length < 3)
            {
                ShowMessage("Логин должен быть от 3", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            if (dataBase.Users.Any(u => u.Login == login))
            {
                ShowMessage("Пользователь с таким логином уже существует", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(displayName))
            {
                ShowMessage("Введите отображаемое имя", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                ShowMessage("Введите email", "#FFBE0404", PackIconKind.Error);
                return false;
            }


            if (dataBase.Users.Any(u => u.Email == email))
            {
                ShowMessage("Пользователь с таким email уже существует", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                ShowMessage("Пароль должен быть не менее 6 символов", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            if (password != confirmPassword)
            {
                ShowMessage("Пароли не совпадают", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            dataBase.RegisterUser(login, displayName, email, password);
            return true;
        }

        private void ShowMessage(string text, string colorHex, PackIconKind icon)
        {
            ActionsClass.SnackBarEnqueue(text, colorHex, icon, _messageQueue);
        }


    }
    
}
