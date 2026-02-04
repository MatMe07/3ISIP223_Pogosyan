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
    /// Логика взаимодействия для _4MakingTicketPage.xaml
    /// </summary>
    public partial class _4MakingTicketPage : Page
    {
        public SessionDetailView SessionDetail { get; set; }
        public WorkWIthDatabase database { get; set; }
        public _4MakingTicketPage(SessionDetailView session)
        {
            InitializeComponent();
            SessionDetail = session;
            DataContext = SessionDetail;
            database = MarDatabase.WIthDatabase;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            database.MakingTickets( SessionDetail.session, SessionDetail.SelectSeat.ToList(), SessionDetail.GetSumPrice );
        }
    }
}
