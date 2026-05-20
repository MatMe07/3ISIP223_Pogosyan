using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels.ProfileViewModels
{
    public class ProfileEditViewModel : BaseViewModel
    {
        public ProfileEditViewModel()
        {
            originalUser = CloneUser(dataBase.User);
            User = dataBase.User;
            DisplayName = User.DisplayName;
            Email = User.Email;
            IsEnabledButton = false;
            MyMessageQueue = new SnackbarMessageQueue();
        }

        private User originalUser;
        private bool hasChanges;



        private User _user;
        public User User
        {
            get {  return _user; }
            set { 
                _user = value; 
                OnPropertyChanged(nameof(User));
                CheckChanges();
            }
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set
            {
                if (_displayName != value)
                {
                    _displayName = value;
                    OnPropertyChanged(nameof(DisplayName));
                    CheckChanges();
                }
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                    CheckChanges();
                }
            }
        }

        private User CloneUser(User us)
        {
            if (us == null) return null;

            return new User
            {
                UserId = us.UserId,
                Login = us.Login,
                Password = us.Password,
                Email = us.Email,
                DisplayName = us.DisplayName,
                Role = us.Role,
                IsFrozen = us.IsFrozen,
                Reason = us.Reason,
                CreatedAt = us.CreatedAt
            };
        }
        public SnackbarMessageQueue MyMessageQueue { get; set; }

        /// <summary>
        /// Сохранение изменений профиля
        /// </summary>
        /// <returns>True - сохранение успешно, False - ошибка валидации</returns>
        public bool SaveChanges()

        {
            if (string.IsNullOrWhiteSpace(User.DisplayName))
            {
                ShowSnackBar("Введите имя", "#FFBE0404", PackIconKind.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(User.Email))
            {
                ShowSnackBar("Введите Email", "#FFBE0404", PackIconKind.Error);
                return false;
            }
            dataBase.UpdateUserProfile(DisplayName, Email);

            originalUser = CloneUser(User);
            IsEnabledButton = false;

            ShowSnackBar("Профиль успешно обновлён!", "#FF04BE5A", PackIconKind.CheckCircle);
            return true;
        }


        private void CheckChanges()
        {
            if (originalUser == null) return;

            bool hasChanges = originalUser.DisplayName != DisplayName ||
                             originalUser.Email != Email;

            IsEnabledButton = hasChanges;
            OnPropertyChanged(nameof(IsEnabledButton));
        }

        public void CancelChanges()
        {
            if (IsEnabledButton)
            {
                ShowSnackBar("Изменения отменены", "#FFFFA500", PackIconKind.Cancel);
            }
        }
        public bool IsEnabledButton
        {
            get { return hasChanges; }
            set
            {
                hasChanges = value;
                OnPropertyChanged(nameof(IsEnabledButton));
            }
        }
        private void ShowSnackBar(string text, string colorHex, PackIconKind icon)
        {
            ActionsClass.SnackBarEnqueue(
                text: text,
                foregroundHEX: colorHex,
                iconKind: icon,
                MyMessageQueue: MyMessageQueue,true

            );
        }

    }
}
