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
using _3ISIP223_PogosyanWPF.Pages;

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int page = 0;
        private List<Page> pages = new List<Page>();

        public MainWindow()
        {

            InitializeComponent();
            CarConfig.Instance.Car.Model = CarConfig.Instance.carModels[0];
            CarConfig.Instance.Car.Color = CarConfig.Instance.colors[0];
            CarConfig.Instance.Car.Engine = CarConfig.Instance.Car.Model.Engines[0];
            CarConfig.Instance.Car.DopOptinon = new List<DopOptinon>();

            pages = new List<Page>
            {
               new  ModelAndTypeEngine(CarImage),
               new ColorAndOptions(CarImage),
               new CalculatTotalCostAndParameters(CarImage),
               new KreditParameters(),
               new ContactDetailsAndSummary(),
            };

            TotalFrame.Navigate(pages[0]);
            UpdateButtons();
        }

        private void ModelAndTypeEngine_Navigated(object sender, NavigationEventArgs e)
        {
            //if (CarImage == null) return;
            //CarImage.Source = new BitmapImage(new Uri(ImageCS.imgPath, UriKind.Relative));
        }

        private void ForwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (page < pages.Count)
            {
                page++;
                TotalFrame.Navigate(pages[page]);
            }
            UpdateButtons();

        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            //if ()
            if (page >= 0)
            {
                page--;
                TotalFrame.Navigate(pages[page]);
            }
            UpdateButtons();
        }

        private void UpdateButtons()
        {

        }
    }
}
