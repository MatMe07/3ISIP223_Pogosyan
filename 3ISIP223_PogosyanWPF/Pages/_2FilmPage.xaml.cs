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
    /// Логика взаимодействия для _2FilmPage.xaml
    /// </summary>
    public partial class _2FilmPage : Page
    {
        //public Film selectFilm {  get; set; }
        public FilmDetailView FilmDetail { get; set; }
        public List<string> lstSorts;
        public _2FilmPage(Film film)
        {
            InitializeComponent();
            lstSorts = new List<string>
            {
                "Все",
                "Standard",
                "Comfort",
                "VIP",
                "IMAX",
                "3D",
            };

            FilmDetail = new FilmDetailView(film);
            DataContext = FilmDetail;
            comboSort.ItemsSource = lstSorts; 
            comboSort.SelectedIndex = 0;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void comboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilmDetail.SortSession(comboSort.SelectedItem.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MarDatabase.WIthDatabase.user == null) {
                MessageBox.Show(
                    "Для покупки билетов необходимо войти в систему!",
                    "Требуется авторизация",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            
            var session = ListSessionBox.SelectedItem as Session;

            NavigationService.Navigate(new _3SessionPage(session));

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListSessionBox.SelectedIndex = -1;
        }
    }
}
