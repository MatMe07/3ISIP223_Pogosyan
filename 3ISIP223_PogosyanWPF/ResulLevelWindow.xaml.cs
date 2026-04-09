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
using System.Windows.Shapes;

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Логика взаимодействия для ResulLevelWindow.xaml
    /// </summary>
    public partial class ResulLevelWindow : Window
    {
        public ResulLevelWindow(string WinOrAgain)
        {
            DataContext = WorkGame.Game;
            InitializeComponent();
            //Owner = Main
            switch (WinOrAgain)
            {
                case "win":
                    {
                        gridWIN.Visibility = Visibility.Visible;
                        gridTRYAGAIN.Visibility = Visibility.Collapsed;
                        break;
                    }
                case "again":
                    {
                        gridWIN.Visibility = Visibility.Collapsed;
                        gridTRYAGAIN.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        private void BorderBtnAgain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
