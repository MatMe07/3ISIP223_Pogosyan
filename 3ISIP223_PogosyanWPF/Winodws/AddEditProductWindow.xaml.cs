using System;
using System.Linq;
using System.Windows;
using _3ISIP223_PogosyanWPF.ViewModels;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class AddEditProductWindow : Window
    {
        private ManagerViewModel _viewModel;
        private Product _editingProduct;

        public AddEditProductWindow(ManagerViewModel viewModel, Product product = null)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _editingProduct = product;

            cmbProductType.ItemsSource = _viewModel.GetAllProductTypes();
            cmbManufacturer.ItemsSource = _viewModel.GetAllManufacturers();

            if (product != null)
            {
                TitleText.Text = "✏️ Редактирование товара";
                txtName.Text = product.Name;
                txtPrice.Text = product.Price.ToString();
                txtDiscount.Text = product.Discount.ToString();
                txtRating.Text = product.Rating.ToString();
                cmbProductType.SelectedValue = product.ProductType_ID;
                cmbManufacturer.SelectedValue = product.Manufacturer_ID;
            }
            else
            {
                txtDiscount.Text = "0";
                txtRating.Text = "0";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Введите корректную цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtDiscount.Text, out decimal discount))
            {
                MessageBox.Show("Введите корректную скидку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtRating.Text, out decimal rating))
            {
                MessageBox.Show("Введите корректный рейтинг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (discount < 0 || discount > 100)
            {
                MessageBox.Show("Скидка должна быть от 0 до 100", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (rating < 0 || rating > 5)
            {
                MessageBox.Show("Рейтинг должен быть от 0 до 5", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_editingProduct == null)
            {
                var product = new Product
                {
                    Name = txtName.Text,
                    Price = price,
                    Discount = discount,
                    Rating = rating,
                    ProductType_ID = (int)cmbProductType.SelectedValue,
                    Manufacturer_ID = (int)cmbManufacturer.SelectedValue,
                    IsFrozen = false
                };
                _viewModel.AddProduct(product);
            }
            else
            {
                _editingProduct.Name = txtName.Text;
                _editingProduct.Price = price;
                _editingProduct.Discount = discount;
                _editingProduct.Rating = rating;
                _editingProduct.ProductType_ID = (int)cmbProductType.SelectedValue;
                _editingProduct.Manufacturer_ID = (int)cmbManufacturer.SelectedValue;
                _viewModel.UpdateProduct(_editingProduct);
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