using System;
using System.Linq;
using System.Windows;
using _3ISIP223_PogosyanWPF.ViewModels;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class EditDiscountWindow : Window
    {
        private ManagerViewModel _viewModel;
        private Product _product;

        public EditDiscountWindow(ManagerViewModel viewModel, Product product)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _product = product;

            cmbProductType.ItemsSource = _viewModel.GetAllProductTypes();
            cmbManufacturer.ItemsSource = _viewModel.GetAllManufacturers();

            txtName.Text = product.Name;
            txtPrice.Text = product.Price.ToString();
            txtDiscount.Text = product.Discount.ToString();
            cmbProductType.SelectedValue = product.ProductType_ID;
            cmbManufacturer.SelectedValue = product.Manufacturer_ID;
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

            if (discount < 0 || discount > 100)
            {
                MessageBox.Show("Скидка должна быть от 0 до 100", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _product.Name = txtName.Text;
            _product.Price = price;
            _product.Discount = discount;
            _product.ProductType_ID = (int)cmbProductType.SelectedValue;
            _product.Manufacturer_ID = (int)cmbManufacturer.SelectedValue;

            _viewModel.UpdateProduct(_product);

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