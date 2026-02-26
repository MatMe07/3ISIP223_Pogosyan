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
            //List<basepart> bs = Core.Context.baseparts.ToList().GetRange(2, 3);
            //UserComponents us = new UserComponents(bs);

            //MarWorkWith.withDB.MySborki.Add(us);

            frameMainPage.NavigationService.Navigate(new _0MainPage(frameMainPage));
            
        }

        //public void ChangeDisplay(Visibility visibility, ComponentType type)
        //{
        //    ZamazFon.Visibility = visibility;
        //    SelectPanel.Visibility = visibility;
        //    if (visibility == Visibility.Visible)
        //        frameShop.NavigationService.Navigate(new _2SelectComplect(ChangeDisplay, type));

        //}

        //private void CloseClick_Click(object sender, RoutedEventArgs e)
        //{
        //    ChangeDisplay(Visibility.Collapsed, ComponentType.NONE);

        //}

        //private void btnClearConfig_Click(object sender, RoutedEventArgs e)
        //{
        //    MarWorkWith.withDB.ClearKonfig();
        //}

        //private void btnMySborki_Click(object sender, RoutedEventArgs e)
        //{
            
        //}
    }
}
