using _3ISIP223_PogosyanWPF.ViewModels.ProfileViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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

namespace _3ISIP223_PogosyanWPF.Pages.ProfilePages
{
    /// <summary>
    /// Логика взаимодействия для ProfileMainPage.xaml
    /// </summary>
    public partial class ProfileMainPage : Page
    {
        public ProfileMainPage()
        {
            InitializeComponent();
        }

        private void btnAuthorRole_Click(object sender, MouseButtonEventArgs e)
        {
            (DataContext as ProfileMainViewModel).SendAuthorRequest();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as ProfileMainViewModel).UpdateUser();

        }
    }
}
