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
    /// Логика взаимодействия для _2SelectComplect.xaml
    /// </summary>
    public partial class _2SelectComplect : Page
    {
        public Action<Visibility, ComponentType> frameZamaz;
        public ComponentType type;
        public TextBlock TextTtitlePage { get; set; }
        public _2SelectComplect(Action<Visibility, ComponentType> f, ComponentType t, TextBlock txtTitle)
        {
            DataContext = MarWorkWith.withDB;
            InitializeComponent();
            type = t;
            frameZamaz = f;
            //comboProizvod.ItemsSource = MarWorkWith.withDB.manufacturers;
            //comboProizvod.SelectedIndex = 0;
            TextTtitlePage = txtTitle;
        }


        


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case ComponentType.CPU:
                    {
                        TextTtitlePage.Text = "Процессоры";
                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.CpuList;

                        MarWorkWith.withDB.manufacturers = MarWorkWith.withDB.CpuList.Select(s => s.basepart.manufacturer.name).Distinct().ToList();
                        MarWorkWith.withDB.SelectListManufactures();
                        break;
                    }
                case ComponentType.GPU:
                    {

                        TextTtitlePage.Text = "Видеокарты";
                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.GpuList;
                        MarWorkWith.withDB.manufacturers = MarWorkWith.withDB.GpuList.Select(s=>s.basepart.manufacturer.name).Distinct().ToList();
                        break;
                    }
                case ComponentType.RAM:
                    {

                        TextTtitlePage.Text = "Оперативные памяти";
                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.RamList;
                        MarWorkWith.withDB.manufacturers = MarWorkWith.withDB.RamList.Select(s=>s.basepart.manufacturer.name).Distinct().ToList();
                        break;
                    }
                case ComponentType.Storage:
                    {

                        TextTtitlePage.Text = "Накопители";
                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.StorageList;
                        MarWorkWith.withDB.manufacturers = MarWorkWith.withDB.StorageList.Select(s=>s.basepart.manufacturer.name).Distinct().ToList();
                        break;
                    }
                case ComponentType.Case:
                    {

                        TextTtitlePage.Text = "Корпуса";
                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.CaseList;
                        MarWorkWith.withDB.manufacturers = MarWorkWith.withDB.CaseList.Select(s=>s.basepart.manufacturer.name).Distinct().ToList();
                        break;
                    }
                case ComponentType.Motherboard:
                    {

                        TextTtitlePage.Text = "Материнские платы";
                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.MotherboardList;
                        MarWorkWith.withDB.manufacturers = MarWorkWith.withDB.MotherboardList.Select(s=>s.basepart.manufacturer.name).Distinct().ToList();
                        break;
                    }
                case ComponentType.PowerSupply:
                    {
                        TextTtitlePage.Text = "Блоки питания";

                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.PowersupplyList;
                        MarWorkWith.withDB.manufacturers = MarWorkWith.withDB.PowersupplyList.Select(s=>s.basepart.manufacturer.name).Distinct().ToList();
                        break;
                    }
                case ComponentType.ProcessorCooler:
                    {

                        TextTtitlePage.Text = "Кулеры";
                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.ProcessorcoolerList;
                        MarWorkWith.withDB.manufacturers = MarWorkWith.withDB.ProcessorcoolerList.Select(s=>s.basepart.manufacturer.name).Distinct().ToList();
                        break;
                    }
            }
            MarWorkWith.withDB.manufacturers.Insert(0, "Все");
            comboProizvod.SelectedIndex = 0;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {


            //switch (type)
            //{
            //    case ComponentType.CPU:
            //        {
            //            cpu pp = button.DataContext as cpu;
            //            break;
            //        }
            //}
            //CloseClick_Click(sender, e);
            Button button = (Button)sender;
            string res = MarWorkWith.withDB.IsSovmest(button.DataContext, type);
            if(res != "")
            {
                var resultMes = MessageBox.Show(res+"\n\nХотите изменить?","Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultMes == MessageBoxResult.Yes) return;
            }
            MarWorkWith.withDB.SelectComponent(button.DataContext, type);
            frameZamaz(Visibility.Collapsed, ComponentType.NONE);

        }

        private void listComp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listComp.SelectedIndex = -1;
        }

        private void comboProizvod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtBoxSearch!= null && comboProizvod.SelectedItem != null)
            MarWorkWith.withDB.SearchFilterComponent(type, txtBoxSearch.Text, comboProizvod.SelectedItem.ToString());
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            MarWorkWith.withDB.SearchFilterComponent(type, txtBoxSearch.Text, comboProizvod.SelectedItem.ToString());

        }
    }
}
