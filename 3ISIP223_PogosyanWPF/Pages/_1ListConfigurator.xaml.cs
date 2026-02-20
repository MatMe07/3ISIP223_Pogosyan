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
        }

        private void WithDB_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(MarWorkWith.withDB.Cpu):
                    {
                        panAddCPU.Visibility = Visibility.Collapsed;
                        panelCPU.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow
            Button btn = sender as Button;
            ComponentType type = (ComponentType)btn.Tag ;

            frameZamaz(Visibility.Visible, type);
        }
    }
}
