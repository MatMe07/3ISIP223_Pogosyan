using System.Windows;
using _3ISIP223_PogosyanWPF.ViewModels;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class AddEditManufacturerWindow : Window
    {
        private ManagerViewModel _viewModel;
        private Manufacturer _editingManufacturer;

        public AddEditManufacturerWindow(ManagerViewModel viewModel, Manufacturer manufacturer = null)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _editingManufacturer = manufacturer;

            if (manufacturer != null)
            {
                TitleText.Text = "✏️ Редактирование производителя";
                txtName.Text = manufacturer.Name;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название производителя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_editingManufacturer == null)
            {
                var manufacturer = new Manufacturer
                {
                    Name = txtName.Text
                };
                _viewModel.AddManufacturer(manufacturer);
            }
            else
            {
                _editingManufacturer.Name = txtName.Text;
                _viewModel.UpdateManufacturer(_editingManufacturer);
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