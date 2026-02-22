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
        public _2SelectComplect(Action<Visibility, ComponentType> f, ComponentType t)
        {
            DataContext = MarWorkWith.withDB;
            InitializeComponent();
            type = t;
            frameZamaz = f;
            comboProizvod.ItemsSource = MarWorkWith.withDB.manufacturers;
            comboProizvod.SelectedIndex = 0;
        }


        

        private void CloseClick_Click(object sender, RoutedEventArgs e)
        {
            frameZamaz(Visibility.Collapsed, ComponentType.NONE);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case ComponentType.CPU:
                    {

                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.CpuList;
                        break;
                    }
                case ComponentType.GPU:
                    {

                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.GpuList;
                        break;
                    }
                case ComponentType.RAM:
                    {

                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.RamList;
                        break;
                    }
                case ComponentType.Storage:
                    {

                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.StorageList;
                        break;
                    }
                case ComponentType.Case:
                    {

                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.CaseList;
                        break;
                    }
                case ComponentType.Motherboard:
                    {

                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.MotherboardList;
                        break;
                    }
                case ComponentType.PowerSupply:
                    {

                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.PowersupplyList;
                        break;
                    }
                case ComponentType.ProcessorCooler:
                    {

                        MarWorkWith.withDB.CurrentList = MarWorkWith.withDB.ProcessorcoolerList;
                        break;
                    }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           Button button = (Button)sender;
           MarWorkWith.withDB.SelectComponent(button.DataContext, type);

            //switch (type)
            //{
            //    case ComponentType.CPU:
            //        {
            //            cpu pp = button.DataContext as cpu;
            //            break;
            //        }
            //}
            CloseClick_Click(sender, e);
        }

        private void listComp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listComp.SelectedIndex = -1;
        }

        private void comboProizvod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MarWorkWith.withDB.SearchFilterComponent(type, txtBoxSearch.Text, comboProizvod.SelectedItem.ToString());
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            MarWorkWith.withDB.SearchFilterComponent(type, txtBoxSearch.Text, comboProizvod.SelectedItem.ToString());

        }
    }
}
