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
    /// Логика взаимодействия для _2KorzinaPage.xaml
    /// </summary>
    public partial class _2KorzinaPage : Page
    {
        public WorkWithDatabase workDatabase = MarWorkWithDatabase.withDatabase;

        public _2KorzinaPage()
        {
            InitializeComponent();
            DataContext = workDatabase;
        }

        private void btnDeleteProd_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parents = (sender as Button).Parent as StackPanel;
            TextBlock textBlock = (parents.Children[0] as StackPanel).Children[0] as TextBlock;
            //txtName.Text = textBlock.Text;
            workDatabase.DeleteKorzina(textBlock.Text);
        }

        private void btnBackToAllProducts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnMakingOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new _3MakeOrderPage());
        }
    }
}
