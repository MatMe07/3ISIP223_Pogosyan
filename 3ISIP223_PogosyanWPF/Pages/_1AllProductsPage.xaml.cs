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
    /// Логика взаимодействия для _1AllProductsPage.xaml
    /// </summary>
    public partial class _1AllProductsPage : Page
    {
        public WorkWithDatabase workDatabase = MarWorkWithDatabase.withDatabase;
        public _1AllProductsPage()
        {
            InitializeComponent();
            DataContext = workDatabase;
            
        }

        private void btnKorzina_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new _2KorzinaPage());
        }

        private void WrapPanel_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parents = (sender as Button).Parent as StackPanel;
            TextBlock textBlock = parents.Children[1] as TextBlock;
            //txtName.Text = textBlock.Text;
            workDatabase.AddKorzina(textBlock.Text);
            
        }
    }
}
