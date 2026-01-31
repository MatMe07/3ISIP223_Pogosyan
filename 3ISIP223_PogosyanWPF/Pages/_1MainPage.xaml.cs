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
        public _1MainPage()
        {
            InitializeComponent();
            DataContext = this;
            comboSorts = new List<string>
            {
                "aba",
                "aba",
            };
            //comboSort.ItemsSource = comboSorts;
        }
    }
}
