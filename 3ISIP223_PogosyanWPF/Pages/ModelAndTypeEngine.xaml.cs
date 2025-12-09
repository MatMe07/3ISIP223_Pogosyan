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
        private Image imageCar;

        private List<string> engins;
        private List<string> models = new List<string> {
                "BMW",
                "Bugatti",
                "Camaro",
            };
        private decimal TotalPrcePage;
        public ModelAndTypeEngine(Image image)
        {
            InitializeComponent();
            imageCar = image;
            engins = CarConfig.Instance.Car.Model.Engines.Select(s=>s.Type).ToList();

            ComboModel.ItemsSource = models;
            ComboModel.SelectedIndex = models.IndexOf(CarConfig.Instance.Car.Model.Name);

            ComboTypeEngine.ItemsSource = engins;
            ComboTypeEngine.SelectedIndex = engins.IndexOf(CarConfig.Instance.Car.Engine.Type);
            //Car.Engine = options[0];
            //TotalPrice.Text = $"Итого: {MyCar.Auto.Price.ToString()} ₽";
            TotalPrcePage = CarConfig.Instance.Car.Model.BasePrice + CarConfig.Instance.Car.Engine.Price;
        }

        private void ComboModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;


            txtF.Text = comboBox.SelectedItem.ToString();

            CarConfig.Instance.Car.Model = CarConfig.Instance.carModels.FirstOrDefault(car => car.Name == comboBox.SelectedItem.ToString());


            engins = CarConfig.Instance.Car.Model.Engines.Select(s => s.Type).ToList();

            ComboTypeEngine.ItemsSource = engins;
            var typeEngine = engins.IndexOf(CarConfig.Instance.Car.Engine.Type);
            ComboTypeEngine.SelectedIndex = typeEngine == -1 ? 0 : typeEngine;

            imageCar.Source = new BitmapImage(new Uri($"{CarConfig.Instance.Car.PathImage}", UriKind.Relative));
            TotalPrcePage = CarConfig.Instance.Car.Model.BasePrice + CarConfig.Instance.Car.Engine.Price;
            TotalPrice.Text = $"Итого: {TotalPrcePage} ₽";

        }

        private void ComboTypeEngine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {     
            ComboBox comboBox = sender as ComboBox;
            //MyCar.Auto.Engine = comboBox.SelectedItem.ToString();

            if ( comboBox.SelectedItem != null ) 
                CarConfig.Instance.Car.Engine = CarConfig.Instance.Car.Model.Engines.FirstOrDefault(car => car.Type == comboBox.SelectedItem.ToString());

            DopPrice.Text = $"+ {CarConfig.Instance.Car.Engine.Price} ₽";
            TotalPrcePage = CarConfig.Instance.Car.Model.BasePrice + CarConfig.Instance.Car.Engine.Price;
            TotalPrice.Text = $"Итого: {TotalPrcePage} ₽";
        }
    }
}
