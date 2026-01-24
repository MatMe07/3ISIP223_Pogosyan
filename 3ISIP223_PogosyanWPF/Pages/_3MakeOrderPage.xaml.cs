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
    /// Логика взаимодействия для _3MakeOrderPage.xaml
    /// </summary>
    public partial class _3MakeOrderPage : Page
    {
        public WorkWithDatabase withDatabase = MarWorkWithDatabase.withDatabase;
        public _3MakeOrderPage()
        {
            InitializeComponent();
            DataContext = withDatabase;
        }


        private void btnBackToKorzina_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show($"Вы уверены, что хотите оформить заказ на сумму {withDatabase.TotalPrice:N2} ₽?",
                                        "Подтверждение заказа",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    MessageBox.Show($"Заказ успешно оформлен!\nСумма: {withDatabase.TotalPrice:N2} ₽",
                                  "Заказ принят",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);

                    withDatabase.MakingOrder(txtFIO.Text, txtEmail.Text, txtAddress.Text);
                    txtFIO.Text = "";
                    txtEmail.Text = "";
                    txtAddress.Text = "";
                    NavigationService.Navigate(new _1AllProductsPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении заказа: {ex.Message}",
                                  "Ошибка",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Error);
                }
            }
        }
        public void CheckInputBox()
        {
            btnMakingOrder.IsEnabled = (withDatabase.LenKorzina > 0)&& (txtFIO.Text.Length > 3) && (txtAddress.Text.Length > 3) && (txtEmail.Text.Length > 3) && txtEmail.Text.Contains("@");
        }

        private void txtFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInputBox();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInputBox();
        }

        private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckInputBox();
        }
    }
}
