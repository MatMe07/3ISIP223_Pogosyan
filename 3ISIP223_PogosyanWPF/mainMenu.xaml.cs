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
using _3ISIP223_PogosyanWPF.Pages;

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Логика взаимодействия для mainMenu.xaml
    /// </summary>
    public partial class mainMenu : Window
    {
        public mainMenu()
        {
            InitializeComponent();
            frameMenu.Navigate(new _1MainMenuButtons(ResFrame));
        }

        public void ResFrame(string types)
        {
            switch(types)
            {
                case "Start":
                    {
                        DialogResult = true;

                        break;
                    }
                case "Settings":
                    {
                        frameMenu.Navigate(new _2MeunSettings());
                        break;
                    }
                case "Quit":
                    {
                        DialogResult = false;

                        break;
                    }
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }

    }
}
