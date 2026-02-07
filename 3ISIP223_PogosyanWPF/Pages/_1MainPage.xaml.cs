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
    /// Логика взаимодействия для _1MainPage.xaml
    /// </summary>
    public partial class _1MainPage : Page
    {
        public List<string> comboSorts;
        public WorkWIthDatabase workDatabase {  get; set; }
        //public List<string> comboFilters;
        private int lastInd = -1;
        public _1MainPage()
        {
            workDatabase = MarDatabase.WIthDatabase;
            InitializeComponent();
            DataContext = workDatabase;
            comboSorts = new List<string>
            {
                "Все",
                "По названию",
                "По рейтингу",
            };
            comboSort.ItemsSource = comboSorts;
            comboSort.SelectedIndex = 0;    
        }


        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            //if (txtSearch == null && txtSearch.Text.Length == 0) return;

            //txt.Text = txtSearch.Text;
            workDatabase.SearchAndSortFilm(txtSearch.Text, comboSort.SelectedItem.ToString());
        }

        private void comboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //txt.Text =  comboSort.SelectedItem.ToString();
            workDatabase.SearchAndSortFilm(txtSearch.Text, comboSort.SelectedItem.ToString());

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //lstFilms.SelectedIndex = lastInd;
            if (lstFilms != null)
                lstFilms.SelectedIndex = -1;
        }


        private void lstFilms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var film = lstFilms.SelectedItem as Film;
            //txt.Text = wrap.Name;
            //lstFilms.SelectedValue = null;
            //lastInd = lstFilms.SelectedIndex;

            NavigationService.Navigate(new _2FilmPage(film));

        }
    }
}
