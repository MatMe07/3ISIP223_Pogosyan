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
    /// Логика взаимодействия для ModelAndTypeEngine.xaml
    /// </summary>
    public partial class ModelAndTypeEngine : Page
    {
        public ModelAndTypeEngine()
        {
            InitializeComponent();
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KreditParameters());
        }
    }
}
