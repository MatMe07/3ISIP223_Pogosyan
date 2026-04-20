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
    /// Логика взаимодействия для _0StartedMainPage.xaml
    /// </summary>
    public partial class _0StartedMainPage : Page
    {
        public static Frame GetMainPageFrame {  get; set; }
        public _0StartedMainPage()
        {
            InitializeComponent();
            GetMainPageFrame = mainPageFrame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainPageFrame.NavigationService.Navigate(new _2ProtductsPage());
        }
    }
}
