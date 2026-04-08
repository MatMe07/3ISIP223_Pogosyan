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
    /// Логика взаимодействия для _1MainMenuButtons.xaml
    /// </summary>
    public partial class _1MainMenuButtons : Page
    {
        public Action<string> Action { get; set; }
        public _1MainMenuButtons(Action<string> action)
        {
            InitializeComponent();
            Action = action;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Action("Quit");
        }

        private void BorderBtnStart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Action("Start");

        }

        private void BorderBtnSettings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Action("Settings");
        }
    }
}
