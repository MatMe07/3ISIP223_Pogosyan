using _3ISIP223_PogosyanWPF.ViewModels;
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

namespace _3ISIP223_PogosyanWPF.Winodws
{
    /// <summary>
    /// Логика взаимодействия для InsertServiceWindow.xaml
    /// </summary>
    public partial class InsertServiceWindow : Window
    {
        public MasterViewModel masterView;
        public string typeServiceSelectItemStirng {  get; set; }
        public List<TypeService> typeServices;
        public List<string> typeServicesStrings {  get; set; }
        public InsertServiceWindow(MasterViewModel viewModel)
        {
            InitializeComponent();
            DataContext = this;
            masterView = viewModel;
            typeServices = WorkDataBase.Instance.GetAllTypeServicesMaster(viewModel.GetUserID);
            typeServicesStrings = WorkDataBase.Instance.GetAllTypeServicesMaster(viewModel.GetUserID).Select(s=>s.Name).ToList();
            comboTypes.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (typeServiceSelectItemStirng == null)
            {
                MessageBox.Show($"Выберите услугу!!!",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                masterView.AddService(typeServices.FirstOrDefault(s => s.Name == typeServiceSelectItemStirng));
                DialogResult = true;

            }
        }

        private void BtnOtmena_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
