using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class ProfileNotificationViewModel : BaseViewModel
    {
        public ProfileNotificationViewModel()
        {
            User = dataBase.User;
        }

        public bool CheckIsUnfreezeReq()
        {
            return dataBase.CheckIsUnfreezeReq();
        }

        public User User { get; set; }

        public Visibility IsFrozen
        {
            get => User.IsFrozen ? Visibility.Visible : Visibility.Collapsed;
        }
        public Visibility IsUnFrozen
        {
            get => User.IsFrozen ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
