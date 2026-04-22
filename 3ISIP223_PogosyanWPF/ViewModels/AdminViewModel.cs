using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WorkDataBase dataBase;
        private List<User> _allUsers;

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        private List<string> _allRoles;
        public List<string> AllRoles
        {
            get { return _allRoles; }
            set
            {
                _allRoles = value;
                OnPropertyChanged(nameof(AllRoles));
            }
        }

        public AdminViewModel()
        {
            dataBase = WorkDataBase.Instance;
            Users = new ObservableCollection<User>();
            AllRoles = new List<string> { "Клиент", "Мастер", "Менеджер", "Администратор" };
            LoadAllData();
        }

        private void LoadAllData()
        {
            _allUsers = dataBase.Users.ToList();
            UpdateUsersList();
        }

        private void UpdateUsersList()
        {
            Users.Clear();
            foreach (var user in _allUsers)
            {
                Users.Add(user);
            }
        }

        public void AddUser(User user)
        {
            dataBase.AddUser(user);
            _allUsers = dataBase.Users.ToList();
            UpdateUsersList();
        }

        public void UpdateUser(User user)
        {
            dataBase.UpdateUser(user);
            _allUsers = dataBase.Users.ToList();
            UpdateUsersList();
        }

        public void DeleteUser(User user)
        {
            var usageInfo = dataBase.GetUserUsageInfo(user.User_ID);

            if (!string.IsNullOrEmpty(usageInfo))
            {
                MessageBox.Show($"Нельзя удалить пользователя!\n\n{usageInfo}\n\nСначала удалите связанные данные.",
                    "Невозможно удалить", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить пользователя {user.LastName} {user.FirstName}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                dataBase.DeleteUser(user);
                _allUsers = dataBase.Users.ToList();
                UpdateUsersList();
            }
        }

        public void FreezeUser(User user)
        {
            user.IsFrozen = true;
            dataBase.UpdateUser(user);
            _allUsers = dataBase.Users.ToList();
            UpdateUsersList();
        }

        public void UnfreezeUser(User user)
        {
            user.IsFrozen = false;
            dataBase.UpdateUser(user);
            _allUsers = dataBase.Users.ToList();
            UpdateUsersList();
        }

        public void ChangeUserRole(User user, string newRoleName)
        {
            var roleId = AllRoles.IndexOf(newRoleName) + 1;
            user.Role_ID = roleId;
            dataBase.UpdateUser(user);
            _allUsers = dataBase.Users.ToList();
            UpdateUsersList();
        }
    }
}