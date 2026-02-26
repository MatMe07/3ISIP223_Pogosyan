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
    /// Логика взаимодействия для _0MainPage.xaml
    /// </summary>
    public partial class _0MainPage : Page
    {
        public Frame frame;
        public _0MainPage(Frame fr)
        {
            InitializeComponent();
            frameMain.NavigationService.Navigate(new _1ListConfigurator(ChangeDisplay));
            this.frame = fr;
        }


        public void ChangeDisplay(Visibility visibility, ComponentType type)
        {
            ZamazFon.Visibility = visibility;
            SelectPanel.Visibility = visibility;
            if (visibility == Visibility.Visible)
                frameShop.NavigationService.Navigate(new _2SelectComplect(ChangeDisplay, type));

        }

        private void CloseClick_Click(object sender, RoutedEventArgs e)
        {
            ChangeDisplay(Visibility.Collapsed, ComponentType.NONE);

        }

        private void btnClearConfig_Click(object sender, RoutedEventArgs e)
        {
            MarWorkWith.withDB.ClearKonfig();
        }

        private void btnMySborki_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new _3UserPage());
        }
    }
}
