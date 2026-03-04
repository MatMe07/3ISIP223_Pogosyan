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
    /// Логика взаимодействия для _3UserPage.xaml
    /// </summary>
    public partial class _3UserPage : Page
    {
        public _3UserPage()
        {
            DataContext = MarWorkWith.withDB;


            InitializeComponent();
            UpdatePageStatus();


        }

        private void WithDB_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName ==  nameof(MarWorkWith.withDB.MySborki))
            {
                if (MarWorkWith.withDB.MySborki.Count > 0)
                {
                    stkNetSborki.Visibility = Visibility.Collapsed;
                    lstSborki.Visibility = Visibility.Visible;
                }
                else
                {
                    stkNetSborki.Visibility = Visibility.Visible;
                    lstSborki.Visibility = Visibility.Collapsed;

                }
            }
        }



        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as ListBox).SelectedIndex = -1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int id = (int) button.Tag;
            MarWorkWith.withDB.DeleteUserComponent(id);
            UpdatePageStatus();
        }

        public void UpdatePageStatus()
        {

            if (MarWorkWith.withDB.MySborki.Count > 0)
            {
                stkNetSborki.Visibility = Visibility.Collapsed;
                lstSborki.Visibility = Visibility.Visible;
            }
            else
            {
                stkNetSborki.Visibility = Visibility.Visible;
                lstSborki.Visibility = Visibility.Collapsed;

            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //MarWorkWith.withDB.PropertyChanged += WithDB_PropertyChanged;
        }
    }
}
