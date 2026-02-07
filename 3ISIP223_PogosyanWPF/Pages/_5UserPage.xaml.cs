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
    /// Логика взаимодействия для _5UserPage.xaml
    /// </summary>
    public partial class _5UserPage : Page
    {

        public TicketsDetailView TicketsDetail { get; set; }
        public _5UserPage()
        {
            InitializeComponent();
            TicketsDetail = new TicketsDetailView();
            DataContext = TicketsDetail;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (TicketsDetail.GetCountTickets > 0)
            {
                lstMyTickets.Visibility = Visibility.Visible;
                txtNoneTickets.Visibility = Visibility.Collapsed;
            }
            else
            {
                lstMyTickets.Visibility = Visibility.Collapsed;
                txtNoneTickets.Visibility = Visibility.Visible;
            }
        }
    }
}
