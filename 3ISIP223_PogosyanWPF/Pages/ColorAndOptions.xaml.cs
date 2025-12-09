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
    /// Логика взаимодействия для ColorAndOptions.xaml
    /// </summary>
    public partial class ColorAndOptions : Page
    {
        private Image Image;
        private decimal TotalPrcePage = 0;
        private TextBlock TotalPrice;
        public ColorAndOptions(Image image , TextBlock totalPrice)
        {
            InitializeComponent();
            TotalPrice = totalPrice ;
            Image = image;

            //CheckBox
            foreach(var opt in CarConfig.Instance.dopOptinons)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = opt.Name;
                checkBox.Margin = new Thickness(5);
                checkBox.Click += new System.Windows.RoutedEventHandler(CheckBox_Click);
                StackOptions.Children.Add( checkBox );
            }
            ComboColor.ItemsSource = CarConfig.Instance.colors.Select(col => col.Name);
            ComboColor.SelectedIndex = CarConfig.Instance.colors.IndexOf(CarConfig.Instance.Car.Color);
            if (CarConfig.Instance.Car.DopOptinon.Count > 0)
                TotalPrcePage += CarConfig.Instance.Car.DopOptinon.Sum(d => d.Price);
            else
            {
                TotalPrcePage += CarConfig.Instance.Car.Color.Price;
            }
            TotalPrcePage += 
            TotalPrice.Text = $"Итого: {TotalPrcePage.ToString()} ₽";
        }

        private void ComboColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo == null) return;
            Image.Source = new BitmapImage( new Uri($"{CarConfig.Instance.Car.PathImage}", UriKind.Relative));
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            if (check == null) return;
            if (check.IsChecked == true)
            {
                CarConfig.Instance.Car.DopOptinon.Add(CarConfig.Instance.dopOptinons.FirstOrDefault(s => s.Name == check.Content.ToString()));

            }
            else
            {
                CarConfig.Instance.Car.DopOptinon.Remove(CarConfig.Instance.dopOptinons.FirstOrDefault(s => s.Name == check.Content.ToString()));

            }
            TotalPrcePage = CarConfig.Instance.Car.DopOptinon.Sum(d => d.Price) + CarConfig.Instance.Car.Color.Price;
            TotalPrice.Text = $"Итого: {TotalPrcePage.ToString()} ₽";


            //switch (check.Content)
            //{
            //    case "Персональная гравировка на порогах":
            //        {
            //            break;
            //        }
            //    case "Карбон-керамические тормоза":
            //        {
            //            break;
            //        }
            //    case "Режим отслеживания":
            //        {
            //            break;
            //        }
            //    case "Коврики с подсветкой":
            //        {
            //            break;
            //        }
            //}

        }
    }
}
