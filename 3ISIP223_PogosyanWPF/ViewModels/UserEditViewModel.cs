using _3ISIP223_PogosyanWPF.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class UserEditViewModel : BaseViewModel
    {
        private User origUser;
        private User _editingUser;
        //private bool _isChanged = false;
        public UserEditViewModel()
        {
            //User = dataBase.User;
            //changeM = new List<string>();

            //changeM.Add(); ;
            MyMessageQueue = new SnackbarMessageQueue();
        }

        public User EditingUser
        {
            get => _editingUser;
            set { 
                _editingUser = value;
                OnPropertyChanged(nameof(EditingUser));
                //OnPropertyChanged(nameof(statusFrozen));
                OnPropertyChanged(nameof(IsNotReason));
                OnPropertyChanged(nameof(GetEdintUserReason));
                OnPropertyChanged(nameof(ButtonName));
            }
        }

        public string statusFrozen
        {
            get => EditingUser?.IsFrozen == true ? "Заморожено" : "Активно";
        }

        public string ButtonName
        {
            get => _editingUser?.IsFrozen == true ? "Разморозить" : "Заморозить";
            set
            {
                OnPropertyChanged(nameof(ButtonName));
            }
        }
        public Visibility IsNotReason
        {
            get => EditingUser?.Reason != null ? Visibility.Visible : Visibility.Collapsed;
        }
        public Reason GetEdintUserReason
        {
            get => EditingUser?.Reason;
        }
        //private User _user;
        public User CloneUs(User us)
        {
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
                CreatedAt = us.CreatedAt,
            };
        }
        public User User
        {
            get { return origUser; }
            set
            {
                origUser = CloneUs(value);
                EditingUser = CloneUs(value);
                OnPropertyChanged(nameof(User));
                OnPropertyChanged(nameof(ButtonName));
            }
        }
        public void Save()
        {
            dataBase.SaveUserEdit(User.UserId,  EditingUser);
        }
        public SnackbarMessageQueue MyMessageQueue { get; set; }

        public void Cancel()
        { 
            dataBase.SaveUserEdit(EditingUser.UserId,  User);

        }
        public void ShowMessage()
        {
            MainWindow mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            FreezeRequestPage wind;
            //bool? res;


            string titl = "Пользователь";
            wind = new FreezeRequestPage("пользователя", EditingUser, true);
            
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();

            mainWindow.BlurAdd(false);
            if (res == true)
            {
                //ButtonName = "Разморозить";

                ActionsClass.SnackBarEnqueue(
                    text:  $"{titl} заморожен!",
                    foregroundHEX: "#FF04BE5A",
                    iconKind: PackIconKind.CheckCircle,
                    MyMessageQueue: MyMessageQueue
                );
                //EditingUser.IsFrozen = true;
                //EditingUser.Reason = User.Reason;
                //LoadReviews();
            }
            //OnPropertyChanged(nameof(statusFrozen));
            OnPropertyChanged(nameof(IsNotReason));
            OnPropertyChanged(nameof(GetEdintUserReason));
            OnPropertyChanged(nameof(ButtonName));

        }

        public void UnfreezeUser()
        {
            //ButtonName = "Заморозить";
            EditingUser.IsFrozen = false;
            EditingUser.Reason = null;

            //OnPropertyChanged(nameof(statusFrozen));
            OnPropertyChanged(nameof(IsNotReason));
            OnPropertyChanged(nameof(GetEdintUserReason));
            OnPropertyChanged(nameof(ButtonName));

            dataBase.UnfreezeUser(EditingUser.UserId);
            ActionsClass.SnackBarEnqueue(
                text: $"Пользователь разморожен!",
                foregroundHEX: "#FF04BE5A",
                iconKind: PackIconKind.CheckCircle,
                MyMessageQueue: MyMessageQueue
            );
        }


        public bool HasChanges()
        {
            if (origUser == null || EditingUser == null) return false;

            return origUser.DisplayName != EditingUser.DisplayName || origUser.Login != EditingUser.Login ||
                   origUser.Email != EditingUser.Email || origUser.Role != EditingUser.Role ||
                   origUser.Password != EditingUser.Password || origUser.IsFrozen != EditingUser.IsFrozen;
        }

    }
}
