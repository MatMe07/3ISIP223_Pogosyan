using _3ISIP223_PogosyanWPF.ViewModels;
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
    /// Логика взаимодействия для _0FiltersStartedMainPage.xaml
    /// </summary>
    public partial class _0FiltersStartedMainPage : Page
    {

        public _0FiltersStartedMainPage()
        {
            InitializeComponent();
        }

        private void BtnConfirn_Click(object sender, RoutedEventArgs e)
        {
            //lst.SelectedItem
        }

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lstMasters.SelectedIndex = -1;
            _0StartedMainPage.GetMainPageFrame.NavigationService.Navigate(new _0MasterStartedMainPage());
            lstMasters.SelectedIndex = -1;
        }
    }
}
