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
using _3ISIP223_PogosyanWPF.Pages;

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MarDatabase.WIthDatabase = new WorkWIthDatabase();
            MarSesionDetail.SessionDetail = new SessionDetailView();

            mainFrame.NavigationService.Navigate(new _1MainPage());
        }



        private void btnPersonalAcc_Click(object sender, RoutedEventArgs e)
        {
            if (MarDatabase.WIthDatabase.CurrUser == null)
            {
                //mainFrame.NavigationService.Navigate(new _5UserPage());
                var signWindow = new SignInUpWindow();
                signWindow.Owner = this;

                signWindow.ShowDialog();

            }
            else
            {
                if (mainFrame.Content.GetType().Name == "_5UserPage")
                {
                    return;
                }
                mainFrame.NavigationService.Navigate(new _5UserPage());
            }
        }
    }
}
