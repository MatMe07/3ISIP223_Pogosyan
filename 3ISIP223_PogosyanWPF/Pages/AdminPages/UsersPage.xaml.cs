using _3ISIP223_PogosyanWPF.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _3ISIP223_PogosyanWPF.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public class AdminUser
        {
            public int ID { get; set; }
            public string Login { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            public string CurrentRole { get; set; }

            // Для ComboBox
            public ObservableCollection<string> AvailableRoles { get; set; }

            // Для смены пароля
            public string NewPassword { get; set; }
        }
        public List<AdminUser> adminUsers { get; set; }
        public UsersPage()
        {

            adminUsers = new List<AdminUser>
    {
        new AdminUser
        {
            ID = 1,
            Login = "ivan.petrov",
            Email = "ivan@example.com",
            Name = "Иван Петров",
            CurrentRole = "User",
            AvailableRoles = new ObservableCollection<string> { "User", "Author", "Admin" }
        },
        new AdminUser
        {
            ID = 2,
            Login = "alex.k",
            Email = "alex@example.com",
            Name = "Алекс Ключевской",
            CurrentRole = "Author",
            AvailableRoles = new ObservableCollection<string> { "User", "Author", "Admin" }
        },
        new AdminUser
        {
            ID = 3,
            Login = "admin",
            Email = "admin@example.com",
            Name = "Администратор",
            CurrentRole = "Admin",
            AvailableRoles = new ObservableCollection<string> { "User", "Author", "Admin" }
        },
        new AdminUser
        {
            ID = 4,
            Login = "ya_872170147_jHHpR",
            Email = "me.m4t@yandex.ru",
            Name = "Mat Me",
            CurrentRole = "User",
            AvailableRoles = new ObservableCollection<string> { "User", "Author", "Admin" }
        },
        new AdminUser
        {
            ID = 4,
            Login = "ya_872170147_jHHpR",
            Email = "me.m4t@yandex.ru",
            Name = "Mat Me",
            CurrentRole = "User",
            AvailableRoles = new ObservableCollection<string> { "User", "Author", "Admin" }
        },
        new AdminUser
        {
            ID = 4,
            Login = "ya_872170147_jHHpR",
            Email = "me.m4t@yandex.ru",
            Name = "Mat Me",
            CurrentRole = "User",
            AvailableRoles = new ObservableCollection<string> { "User", "Author", "Admin" }
        },
        new AdminUser
        {
            ID = 4,
            Login = "ya_872170147_jHHpR",
            Email = "me.m4t@yandex.ru",
            Name = "Mat Me",
            CurrentRole = "User",
            AvailableRoles = new ObservableCollection<string> { "User", "Author", "Admin" }
        },
    };

            DataContext = this;
            InitializeComponent();


        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            var wind = new UserEditWindow();
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
        }
    }
}
