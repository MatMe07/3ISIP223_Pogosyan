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

                        listComp.ItemsSource = MarWorkWith.withDB.CpuList;
                        break;
                    }
                case ComponentType.GPU:
                    {

                        listComp.ItemsSource = MarWorkWith.withDB.GpuList;
                        break;
                    }
                case ComponentType.RAM:
                    {

                        listComp.ItemsSource = MarWorkWith.withDB.RamList;
                        break;
                    }
                case ComponentType.Storage:
                    {

                        listComp.ItemsSource = MarWorkWith.withDB.StorageList;
                        break;
                    }
                case ComponentType.Case:
                    {

                        listComp.ItemsSource = MarWorkWith.withDB.CaseList;
                        break;
                    }
                case ComponentType.Motherboard:
                    {

                        listComp.ItemsSource = MarWorkWith.withDB.MotherboardList;
                        break;
                    }
                case ComponentType.PowerSupply:
                    {

                        listComp.ItemsSource = MarWorkWith.withDB.PowersupplyList;
                        break;
                    }
                case ComponentType.ProcessorCooler:
                    {

                        listComp.ItemsSource = MarWorkWith.withDB.ProcessorcoolerList;
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
    }
}
