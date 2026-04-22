using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _3ISIP223_PogosyanWPF.ViewModels;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class RescheduleAppointmentWindow : Window
    {
        private ManagerViewModel _viewModel;
        private Appointment _appointment;
        private List<DateTime> _availableSlots;

        public RescheduleAppointmentWindow(ManagerViewModel viewModel, Appointment appointment)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _appointment = appointment;

            LoadAppointmentInfo();
            LoadAvailableSlots();

            dpNewDate.SelectedDateChanged += DpNewDate_SelectedDateChanged;


        }

        private void LoadAppointmentInfo()
        {
            txtCurrentDateTime.Text = _appointment.AppointmentDate.ToString("dd.MM.yyyy в HH:mm");

            txtClient.Text = $"👤 Клиент: {_appointment.User.LastName} {_appointment.User.FirstName}";

            txtService.Text = $"✨ Услуга: {_appointment.Service.TypeService.Name}";

            var master = _appointment.Service.MasterSerives.FirstOrDefault().User;
            txtMaster.Text = $"👩 Мастер: {master.FIO}";

            dpNewDate.SelectedDate = _appointment.AppointmentDate.Date;
        }

        private void LoadAvailableSlots()
        {
            _availableSlots = _viewModel.GetAvailableSlotsForMaster(_appointment,
                _appointment.Service.MasterSerives.FirstOrDefault()?.User.User_ID ?? 0,
                dpNewDate.SelectedDate ?? DateTime.Today
            );

            cmbNewTime.Items.Clear();
            foreach (var slot in _availableSlots)
            {
                cmbNewTime.Items.Add(slot.ToString("HH:mm"));
            }

            lstAvailableSlots.ItemsSource = _availableSlots.Select(s => s.ToString("HH:mm")).ToList();
        }

        private void DpNewDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dpNewDate.SelectedDate != null)
            {
                LoadAvailableSlots();
            }
        }

        private void AvailableSlots_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstAvailableSlots.SelectedItem != null)
            {
                var selectedTime = lstAvailableSlots.SelectedItem.ToString();
                var slot = _availableSlots.FirstOrDefault(s => s.ToString("HH:mm") == selectedTime);
                if (slot != null)
                {
                    cmbNewTime.Text = slot.ToString("HH:mm");
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (dpNewDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите новую дату", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbNewTime.Text))
            {
                MessageBox.Show("Выберите новое время", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(cmbNewTime.Text, out TimeSpan newTime))
            {
                MessageBox.Show("Введите корректное время", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newDateTime = dpNewDate.SelectedDate.Value.Date + newTime;


            if (!_availableSlots.Any(s => s.ToString("HH:mm") == cmbNewTime.Text))
            {
                MessageBox.Show("Выбранное время недоступно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Перенести запись с {_appointment.AppointmentDate:dd.MM.yyyy HH:mm} на {newDateTime:dd.MM.yyyy HH:mm}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _viewModel.RescheduleAppointment(_appointment, newDateTime);
                DialogResult = true;
                //Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}