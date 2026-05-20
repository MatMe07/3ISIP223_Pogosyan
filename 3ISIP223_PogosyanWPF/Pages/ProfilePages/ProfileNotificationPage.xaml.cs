using _3ISIP223_PogosyanWPF.ViewModels;
using _3ISIP223_PogosyanWPF.Windows;
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
    /// Логика взаимодействия для ProfileNotificationPage.xaml
    /// </summary>
    public partial class ProfileNotificationPage : Page
    {
        public ProfileNotificationPage()
        {
            InitializeComponent();
        }

        private void btnContest_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as ProfileNotificationViewModel).CheckIsUnfreezeReq())
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Запрос на оспаривание уже был отправлен ранее",
                    foregroundHEX: "#FFFFA500",
                    iconKind: PackIconKind.Information,
                    MyMessageQueue: new SnackbarMessageQueue(),
                    true
                );
                return;
            }
            var mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            var wind = new UnfreezeRequestsWindow("Account");
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
        }
    }
}
