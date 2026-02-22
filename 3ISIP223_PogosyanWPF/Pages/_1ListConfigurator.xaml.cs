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
    /// Логика взаимодействия для _1ListConfigurator.xaml
    /// </summary>
    public partial class _1ListConfigurator : Page
    {
        public Action<Visibility, ComponentType> frameZamaz;
        public _1ListConfigurator(Action<Visibility, ComponentType> f)
        {
            DataContext = MarWorkWith.withDB;

            InitializeComponent();

            frameZamaz = f;

            MarWorkWith.withDB.PropertyChanged += WithDB_PropertyChanged;

            btnAddCpu.Tag = ComponentType.CPU;
            panelCPU.Tag = ComponentType.CPU;

            btnAddMother.Tag = ComponentType.Motherboard;
            panelMother.Tag = ComponentType.Motherboard;

            btnAddPitanie.Tag = ComponentType.PowerSupply;
            panelPitanie.Tag = ComponentType.PowerSupply;

            btnAddCorpus.Tag = ComponentType.Case;
            panelCorpus.Tag = ComponentType.Case;

            btnAddVideoCart.Tag = ComponentType.GPU;
            panelVideoCart.Tag = ComponentType.GPU;

            btnAddCuler.Tag = ComponentType.ProcessorCooler;
            panelCuler.Tag = ComponentType.ProcessorCooler;

            btnAddNakopitel.Tag = ComponentType.Storage;
            panelNakopitel.Tag = ComponentType.Storage;

            btnAddRAM.Tag = ComponentType.RAM;
            panelRAM.Tag = ComponentType.RAM;

        }

        private void WithDB_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
            switch(e.PropertyName)
            {
                case nameof(MarWorkWith.withDB.Cpu):
                    {
                        
                        if (MarWorkWith.withDB.Cpu != null)
                        {
                            panAddCPU.Visibility = Visibility.Collapsed;
                            panelCPU.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            panAddCPU.Visibility = Visibility.Visible;
                            panelCPU.Visibility = Visibility.Collapsed;


                        }
                        break;
                    }
                case nameof(MarWorkWith.withDB.GPU):
                    {
                        
                        if (MarWorkWith.withDB.GPU != null)
                        {
                            panAddVideoCart.Visibility = Visibility.Collapsed;
                            panelVideoCart.Visibility = Visibility.Visible;

                        }
                        else
                        {
                            panAddVideoCart.Visibility = Visibility.Visible;
                            panelVideoCart.Visibility = Visibility.Collapsed;

                        }
                        break;
                    }
                case nameof(MarWorkWith.withDB.Motherboard):
                    {
                        
                        if (MarWorkWith.withDB.Motherboard != null)
                        {
                            panAddMother.Visibility = Visibility.Collapsed;
                            panelMother.Visibility = Visibility.Visible;

                        }
                        else
                        {
                            panAddMother.Visibility = Visibility.Visible;
                            panelMother.Visibility = Visibility.Collapsed;

                        }
                        break;
                    }
                case nameof(MarWorkWith.withDB.RAM):
                    {
                        
                        if (MarWorkWith.withDB.RAM != null)
                        {
                            panAddRAM.Visibility = Visibility.Collapsed;
                            panelRAM.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            panAddRAM.Visibility = Visibility.Visible;
                            panelRAM.Visibility = Visibility.Collapsed;

                        }
                            break;
                    }
                case nameof(MarWorkWith.withDB.Case):
                    {
                        
                        if (MarWorkWith.withDB.Case != null)
                        {
                            panAddCorpus.Visibility = Visibility.Collapsed;
                            panelCorpus.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            panAddCorpus.Visibility = Visibility.Visible;
                            panelCorpus.Visibility = Visibility.Collapsed;

                        }
                            break;
                    }
                case nameof(MarWorkWith.withDB.Storage):
                    {
                        
                        if (MarWorkWith.withDB.Storage != null)
                        {
                            panAddNakopitel.Visibility = Visibility.Collapsed;
                            panelNakopitel.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            panAddNakopitel.Visibility = Visibility.Visible;
                            panelNakopitel.Visibility = Visibility.Collapsed;

                        }
                            break;
                    }
                case nameof(MarWorkWith.withDB.Processorcooler):
                    {
                        
                        if (MarWorkWith.withDB.Processorcooler != null)
                        {
                            panAddCuler.Visibility = Visibility.Collapsed;
                            panelCuler.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            panAddCuler.Visibility = Visibility.Visible;
                            panelCuler.Visibility = Visibility.Collapsed;

                        }
                            break;
                    }
                case nameof(MarWorkWith.withDB.Powersupply):
                    {
                        
                        if (MarWorkWith.withDB.Powersupply != null)
                        {
                            panAddPitanie.Visibility = Visibility.Collapsed;
                            panelPitanie.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            panAddPitanie.Visibility = Visibility.Visible;
                            panelPitanie.Visibility = Visibility.Collapsed;

                        }
                            break;
                    }
            }
            //progreses.upda
        }

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow
            Button btn = sender as Button;
            ComponentType type = (ComponentType)btn.Tag ;

            frameZamaz(Visibility.Visible, type);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ComponentType type = (ComponentType)button.Tag;

            MarWorkWith.withDB.DeleteComponent(type);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            //Border parent = ((((sender as Button).Parent as Border).Parent as WrapPanel).Parent as Grid).Parent as Border;
            Button button = sender as Button;
            ComponentType type = (ComponentType)button.Tag;

            frameZamaz(Visibility.Visible, type);
        }

        private void btnClearComp_Click(object sender, RoutedEventArgs e)
        {
            MarWorkWith.withDB.ClearKonfig();
        }

        private void progreses_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (progreses.Value != 0) IsRight.Visibility = Visibility.Visible;
            else IsRight.Visibility = Visibility.Collapsed;
        }
    }
}
