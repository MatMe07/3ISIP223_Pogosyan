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
    /// Логика взаимодействия для ContactDetailsAndSummary.xaml
    /// </summary>
    public partial class ContactDetailsAndSummary : Page
    {
        private bool changeValue { get; set; } = false;


        public  bool  bntBack = false;
        public ContactDetailsAndSummary()
        {
            InitializeComponent();
            //bntBack = bnt;
        }

        private void textTelephon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]) || ( ((TextBox)sender).Text + e.Text).Length > 11;
        }

        private void textName_TextChanged(object sender, TextChangedEventArgs e)
        {

            UpdateButton();
        }

        private void textTelephon_TextChanged(object sender, TextChangedEventArgs e)
        {

            UpdateButton();
        }

        public void resetData()
        {
            textTelephon.Text = "";
            textName.Text = "";
            textEmail.Text = "";
            UpdateButton();
        }

        private void UpdateButton()
        {
            bool emailZnak = textEmail.Text.Contains("@");
            bool lenTelephon = textTelephon.Text.Length == 11;
            bool nameLen = textName.Text.Length > 1;
            btnSend.IsEnabled = ( emailZnak  ) && ( lenTelephon  ) && ( nameLen  );
            if (emailZnak) CarConfig.Instance.Car.ClientEmail = textEmail.Text;
            if ( lenTelephon ) CarConfig.Instance.Car.ClientTelephon = textTelephon.Text;
            if ( nameLen ) CarConfig.Instance.Car.ClientName = textName.Text;
            bntBack = textEmail.Text.Length > 0 || textName.Text.Length > 0 || textTelephon.Text.Length > 0;
        }


        private void textEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextBox textBox = sender as TextBox 
            UpdateButton();

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string text = $"Заявка успешно оформлена!\n\n" +
                 $"Клиент: {CarConfig.Instance.Car.ClientName}\n" +
                 $"Телефон: {CarConfig.Instance.Car.ClientTelephon}\n" +
                 $"Email: {CarConfig.Instance.Car.ClientEmail}\n\n" +
                 $"Выбранная модель: {CarConfig.Instance.Car.Model.Name}\n" +
                 $"Двигатель: {CarConfig.Instance.Car.Engine.Type}\n" +
                 $"Цвет: {CarConfig.Instance.Car.Color.Name}\n" +
                 $"Стоимость автомобиля: {CarConfig.Instance.Car.CalculateTotalPrice()} руб.\n" +
                 $"Срок кредита: {CarConfig.Instance.Car.SrokKredit} месяцев\n" +
                 $"Ежемесячный платеж: {CarConfig.Instance.Car.MonthPrice} руб.\n\n" +
                 $"Спасибо за заявку!";

            var result = MessageBox.Show(text, "Заявка", MessageBoxButton.OKCancel, MessageBoxImage.None);
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
