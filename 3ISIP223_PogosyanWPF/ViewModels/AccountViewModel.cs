using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WorkDataBase dataBase;
        private User _currentUser;

        private ObservableCollection<Appointment> _userAppointments;
        public ObservableCollection<Appointment> UserAppointments
        {
            get { return _userAppointments; }
            set
            {
                _userAppointments = value;
                OnPropertyChanged(nameof(UserAppointments));
            }
        }

        private ObservableCollection<Order> _userOrders;
        public ObservableCollection<Order> UserOrders
        {
            get { return _userOrders; }
            set
            {
                _userOrders = value;
                OnPropertyChanged(nameof(UserOrders));
            }
        }

        public string FullName => _currentUser != null ? $"{_currentUser.LastName} {_currentUser.FirstName} {_currentUser.SecondName}" : "";
        public string Phone => _currentUser?.Phone ?? "";
        public string Email => _currentUser?.Email ?? "";

        public AccountViewModel()
        {
            dataBase = WorkDataBase.Instance;
            _currentUser = dataBase.CurrentUser;

            UserAppointments = new ObservableCollection<Appointment>();
            UserOrders = new ObservableCollection<Order>();

            LoadAllData();
        }

        void LoadAllData()
        {
            LoadUserAppointments();
            LoadUserOrders();
        }

        void LoadUserAppointments()
        {
            if (_currentUser == null) return;

            var appointments = dataBase.AllAppointemntes
                .Where(a => a.Client_ID == _currentUser.User_ID)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

            UserAppointments.Clear();
            foreach (var appointment in appointments)
            {
                UserAppointments.Add(appointment);
            }
        }

        void LoadUserOrders()
        {
            if (_currentUser == null) return;

            var orders = dataBase.GetUserOrders(_currentUser.User_ID)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            UserOrders.Clear();
            foreach (var order in orders)
            {
                UserOrders.Add(order);
            }
        }

        public void RefreshData()
        {
            LoadUserAppointments();
            LoadUserOrders();
            OnPropertyChanged(nameof(FullName));
            OnPropertyChanged(nameof(Phone));
            OnPropertyChanged(nameof(Email));
        }
    }
}