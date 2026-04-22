using System.Windows;
using _3ISIP223_PogosyanWPF.ViewModels;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class AddEditServiceTypeWindow : Window
    {
        private ManagerViewModel _viewModel;
        private TypeService _editingServiceType;

        public AddEditServiceTypeWindow(ManagerViewModel viewModel, TypeService serviceType = null)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _editingServiceType = serviceType;

            if (serviceType != null)
            {
                TitleText.Text = "✏️ Редактирование типа услуги";
                txtName.Text = serviceType.Name;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название типа услуги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_editingServiceType == null)
            {
                var serviceType = new TypeService
                {
                    Name = txtName.Text
                };
                _viewModel.AddServiceType(serviceType);
            }
            else
            {
                _editingServiceType.Name = txtName.Text;
                _viewModel.UpdateServiceType(_editingServiceType);
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}