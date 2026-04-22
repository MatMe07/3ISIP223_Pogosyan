using System.Windows;
using _3ISIP223_PogosyanWPF.ViewModels;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class AddEditProductTypeWindow : Window
    {
        private ManagerViewModel _viewModel;
        private ProductType _editingProductType;

        public AddEditProductTypeWindow(ManagerViewModel viewModel, ProductType productType = null)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _editingProductType = productType;

            if (productType != null)
            {
                TitleText.Text = "✏️ Редактирование типа товара";
                txtName.Text = productType.Name;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название типа товара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_editingProductType == null)
            {
                var productType = new ProductType
                {
                    Name = txtName.Text
                };
                _viewModel.AddProductType(productType);
            }
            else
            {
                _editingProductType.Name = txtName.Text;
                _viewModel.UpdateProductType(_editingProductType);
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