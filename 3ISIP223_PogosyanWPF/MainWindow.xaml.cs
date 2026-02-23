using _3ISIP223_PogosyanWPF.Pages;
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

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //DataContext = MarWorkWith.withDB;
            InitializeComponent();
            frameMain.NavigationService.Navigate(new _1ListConfigurator(ChangeDisplay));
        }

        public void ChangeDisplay(Visibility visibility, ComponentType type)
        {
            ZamazFon.Visibility = visibility;
            SelectPanel.Visibility = visibility;
            if (visibility == Visibility.Visible)
                frameShop.NavigationService.Navigate(new _2SelectComplect(ChangeDisplay, type));

        }

        private void btnClearConfig_Click(object sender, RoutedEventArgs e)
        {
            MarWorkWith.withDB.ClearKonfig();
        }
    }
}
