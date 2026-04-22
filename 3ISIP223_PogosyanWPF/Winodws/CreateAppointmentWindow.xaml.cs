using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using _3ISIP223_PogosyanWPF.ViewModels;

namespace _3ISIP223_PogosyanWPF.Winodws
{
    public partial class CreateAppointmentWindow : Window
    {
        private ManagerViewModel _viewModel;
        private User Client;
        private List<DateTime> _availableSlots;
        private List<Service> _services;

        public CreateAppointmentWindow(ManagerViewModel viewModel, User client)
        {
            InitializeComponent();
            _viewModel = viewModel;

            Client = client;
            LoadClients();
            LoadServiceTypes();
            LoadMasters();

            dpDate.SelectedDate = DateTime.Today.AddDays(1);
        }

        private void LoadClients()
        {
            //var clients = _viewModel.GetAllClients();
            cmbClient.Text = Client.FIO;
        }

        private void LoadServiceTypes()
        {
            var serviceTypes = _viewModel.GetAllServiceTypes();
            cmbServiceType.ItemsSource = serviceTypes;
            if (serviceTypes.Any())
                cmbServiceType.SelectedIndex = 0;
        }

        private void LoadMasters()
        {
            var masters = _viewModel.GetAllMasters();
            cmbMaster.ItemsSource = masters;
            if (masters.Any())
                cmbMaster.SelectedIndex = 0;
        }

        private void LoadServices()
        {
            if (cmbServiceType.SelectedItem is TypeService selectedType && cmbMaster.SelectedItem is User selectedMaster)
            {
                _services = _viewModel.GetServicesByTypeAndMaster(selectedType.TypeID, selectedMaster.User_ID);
            }
        }

        private void LoadAvailableSlots()
        {
            if (cmbMaster.SelectedItem is User selectedMaster && dpDate.SelectedDate != null)
            {
                _availableSlots = _viewModel.GetAvailableSlotsForMaster(
                    selectedMaster.User_ID,
                    dpDate.SelectedDate.Value,
                    _services?.FirstOrDefault()?.DurationMinutes ?? 60
                );

                lstAvailableSlots.ItemsSource = _availableSlots.Select(s => s.ToString("HH:mm")).ToList();

                // Обновляем выпадающий список времени
                cmbTime.Items.Clear();
                foreach (var slot in _availableSlots)
                {
                    cmbTime.Items.Add(slot.ToString("HH:mm"));
                }
            }
        }

        private void UpdatePrice()
        {
            if (cmbServiceType.SelectedItem is TypeService selectedType && cmbMaster.SelectedItem is User selectedMaster)
            {
                var service = _viewModel.GetServiceByTypeAndMaster(selectedType.TypeID, selectedMaster.User_ID);
                if (service != null)
                {
                    txtPrice.Text = $"{service.Price} ₽";
                }
                else
                {
                    txtPrice.Text = "—";
                }
            }
        }

        private void CmbClient_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }

        private void CmbServiceType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadServices();
            UpdatePrice();
            LoadAvailableSlots();
        }

        private void CmbMaster_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadServices();
            UpdatePrice();
            LoadAvailableSlots();
        }

        private void DpDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadAvailableSlots();
        }

        private void LstAvailableSlots_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstAvailableSlots.SelectedItem != null)
            {
                var selectedTime = lstAvailableSlots.SelectedItem.ToString();
                cmbTime.Text = selectedTime;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {


            if (cmbMaster.SelectedItem == null)
            {
                MessageBox.Show("Выберите мастера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbTime.Text))
            {
                MessageBox.Show("Выберите время", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(cmbTime.Text, out TimeSpan selectedTime))
            {
                MessageBox.Show("Введите корректное время (например, 14:30)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var appointmentDate = dpDate.SelectedDate.Value.Date + selectedTime;

            if (appointmentDate < DateTime.Now)
            {
                MessageBox.Show("Нельзя создать запись в прошлом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var serviceType = cmbServiceType.SelectedItem as TypeService;
            var master = cmbMaster.SelectedItem as User;

            var service = _viewModel.GetServiceByTypeAndMaster(serviceType.TypeID, master.User_ID);
            if (service == null)
            {
                MessageBox.Show("У выбранного мастера нет такой услуги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int paymentMethodId = rbCash.IsChecked == true ? 1 : 2;

            string comment = txtComment.Text;

            var result = MessageBox.Show(
                $"Создать запись для {Client.LastName} {Client.FirstName}?\n\n" +
                $"Услуга: {serviceType.Name}\n" +
                $"Мастер: {master.LastName} {master.FirstName}\n" +
                $"Дата и время: {appointmentDate:dd.MM.yyyy HH:mm}\n" +
                $"Цена: {service.Price} ₽\n" +
                $"Оплата: {(rbCash.IsChecked == true ? "Наличные" : "Карта")}",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _viewModel.CreateAppointment(Client, service, appointmentDate, paymentMethodId, comment);
                DialogResult = true;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}