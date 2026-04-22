using _3ISIP223_PogosyanWPF.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace _3ISIP223_PogosyanWPF
{
    public partial class RecordAppoitmantWIndow : Window
    {
        private Appointment _appointment;
        private WorkDataBase _dataBase;

        public RecordAppoitmantWIndow(Appointment appointment)
        {
            InitializeComponent();
            _appointment = appointment;
            _dataBase = WorkDataBase.Instance;

            LoadAppointmentInfo();
        }

        private void LoadAppointmentInfo()
        {
            txtServiceType.Text = _appointment.Service?.TypeService?.Name ?? "—";

            var master = _appointment.Service?.MasterSerives?.FirstOrDefault()?.User;
            txtMaster.Text = master != null ? $"{master.LastName} {master.FirstName}" : "—";

            txtDate.Text = _appointment.AppointmentDate.ToString("dd MMMM yyyy г.");
            txtTime.Text = _appointment.AppointmentDate.ToString("HH:mm");
            txtPrice.Text = $"{_appointment.Service?.Price} ₽";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (_dataBase.CurrentUser == null)
            {
                MessageBox.Show("Для записи необходимо войти в аккаунт",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                DialogResult = false;
                Close();
                return;
            }

            if (_appointment.Client_ID != null)
            {
                MessageBox.Show("Это время уже занято!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                DialogResult = false;
                Close();
                return;
            }

            int paymentMethodId = rbCash.IsChecked == true ? 1 : 2;

            string comment = "";
            if (txtComment != null)
            {
                comment = new TextRange(txtComment.Document.ContentStart,
                    txtComment.Document.ContentEnd).Text.Trim();
            }

            _dataBase.BookAppointment(_appointment, _dataBase.CurrentUser, paymentMethodId, comment);

            MessageBox.Show("Вы успешно записаны!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }
    }
}