using _3ISIP223_PogosyanWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF
{
    public partial class User
    {
        public int? ReasonIdHesh { get; set; }

        public Visibility IsAuthorVisibility
        {
            get
            {
                return Role.RoleName == "Автор" ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility IsAdminVisibility
        {
            get
            {
                return Role.RoleName == "Администратор" ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility IsUserVisibility
        {
            get
            {
                return Role.RoleName == "Читатель" ? Visibility.Visible : Visibility.Collapsed;

            }
        }
        public Visibility IsUserAndAuthorVisibility
        {
            get
            {
                return Role.RoleName == "Читатель" || Role.RoleName == "Автор" ? Visibility.Visible : Visibility.Collapsed;

            }
        }
        public bool IsAdmin
        {
            get
            {
                return Role.RoleName == "Администратор";
            }
            set { }
        }

        public Visibility IsReason
        {
            get
            {
                return Reason == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public List<Role> AvailableRoles
        {
            get
            {
                return WorkDataBase.Instanse.GetRoles;
            }
            set { }
        }
    }
}
