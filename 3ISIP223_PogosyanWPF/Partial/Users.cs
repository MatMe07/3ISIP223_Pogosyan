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
        public Visibility IsAuthorVisibility => Role.RoleName == "Автор" ? Visibility.Visible : Visibility.Collapsed ;
        public Visibility IsAdminVisibility => Role.RoleName == "Администратор" ? Visibility.Visible : Visibility.Collapsed ;
        public Visibility IsUserVisibility => Role.RoleName == "Пользователь" ? Visibility.Visible : Visibility.Collapsed ;
    }
}
