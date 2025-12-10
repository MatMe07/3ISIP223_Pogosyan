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
    /// Логика взаимодействия для CalculatTotalCostAndParameters.xaml
    /// </summary>
    public partial class CalculatTotalCostAndParameters : Page
    {
        private Image ImageCar;
        private List<DopOptinon> DopOptinonList;
        public CalculatTotalCostAndParameters(Image image)
        {
            InitializeComponent();
            ImageCar = image;
            Load();
            DopOptinonList = new List<DopOptinon>();

        }

        private void Load()
        {
            TextModel.Text = $"Модель: {CarConfig.Instance.Car.Model.Name}";
            TextEngine.Text = $"Тип двигателя: {CarConfig.Instance.Car.Engine.Type}";
            TextColor.Text = $"Цвет: {CarConfig.Instance.Car.Color.Name}";
            TextTotalPrice.Text = $"Итоговая стоимость: {CarConfig.Instance.Car.CalculateTotalPrice()} ₽";
            string dopOpt = "";
            foreach (var opt in CarConfig.Instance.Car.DopOptinon)
            {
                dopOpt += $"{(CarConfig.Instance.Car.DopOptinon.IndexOf(opt) == 0 ? "" : "\n") }✓ {opt.Name}";
            }
            StackDopOptions.Text = dopOpt.Length > 0 ? dopOpt : "Не выбраны";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }
    }
}
