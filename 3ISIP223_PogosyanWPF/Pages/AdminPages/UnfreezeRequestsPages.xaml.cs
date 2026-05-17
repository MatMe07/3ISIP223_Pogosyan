using _3ISIP223_PogosyanWPF.ViewModels.AdminViewModels;
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

namespace _3ISIP223_PogosyanWPF.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для UnfreezeRequestsPages.xaml
    /// </summary>
    public partial class UnfreezeRequestsPages : Page
    {
        //public List<string> lstRand { get; set; }

        public UnfreezeRequestsPages()
        {
            //lstRand = new List<string> { "fdf", "sdf", "sdf", "sdf" };
            //DataContext = this;
            InitializeComponent();
        }
        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            var complaint = (sender as Button).Tag as UnfreezeRequest;
            (DataContext as UnfreezeRequestsViewModel).AcceptClick(complaint);
        }

        private void BtnReject_Click(object sender, RoutedEventArgs e)
        {
            var compl = (sender as Button).Tag as UnfreezeRequest;
            (DataContext as UnfreezeRequestsViewModel).RejectClick(compl);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as UnfreezeRequestsViewModel).LoadData();
        }

    }
}
