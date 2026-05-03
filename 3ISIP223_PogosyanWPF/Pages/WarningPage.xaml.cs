using _3ISIP223_PogosyanWPF.Windows;
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

namespace _3ISIP223_PogosyanWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для WarningPage.xaml
    /// </summary>
    public partial class WarningPage : Page
    {
        public WarningPage()
        {
            InitializeComponent();
        }

        private void btnContest_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            var wind = new UnfreezeRequestsWindow();
            wind.Owner = mainWindow;
            var res = wind.ShowDialog();
            mainWindow.BlurAdd(false);
        }
    }
}
