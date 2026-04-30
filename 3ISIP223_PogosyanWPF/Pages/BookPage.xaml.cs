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
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        public List<string> lstRand {  get; set; }
        public BookPage()
        {
            lstRand = new List<string> { "fdf", "sdf"};
            DataContext = this;
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LstReviews.SelectedIndex = -1;
        }

        private void btnReviewPoints_Click(object sender, RoutedEventArgs e)
        {
            var cont = FindResource("ReviewListMenu");
            
        }
    }
}
