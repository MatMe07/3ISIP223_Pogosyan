using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels.AdminViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        public UsersViewModel()
        {
        }

        public void LoadUser()
        { 
            Users = new ObservableCollection<User>(dataBase.Users);

        }

        private Role _roleSel;
        public Role RoleSel
        {
            get { return _roleSel; }
            set
            {
                _roleSel = value;
                OnPropertyChanged(nameof(RoleSel));
                
            }
        }
        public void SaveRole(User us, Role newRole)
        {
            if (us.Role == newRole) return;

            dataBase.SaveRole(us, newRole);
            LoadUser();

        }

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
    }
}
