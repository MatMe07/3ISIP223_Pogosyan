using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class AppointmentsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WorkDataBase dataBase;
        private List<Appointment> _allAppointmentsForDate;

        private ObservableCollection<Appointment> _appointments;
        public ObservableCollection<Appointment> Appointments
        {
            get { return _appointments; }
            set
            {
                _appointments = value;
                OnPropertyChanged(nameof(Appointments));
            }
        }

        private ObservableCollection<string> _uslugiLst;
        public ObservableCollection<string> UslugiLst
        {
            get { return _uslugiLst; }
            set
            {
                _uslugiLst = value;
                OnPropertyChanged(nameof(UslugiLst));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value.Date;
                OnPropertyChanged(nameof(SelectedDate));
                LoadDataForDate(_selectedDate);
            }
        }

        private string _selectedUslug;
        public string SelectedUslug
        {
            get { return _selectedUslug; }
            set
            {
                _selectedUslug = value;
                OnPropertyChanged(nameof(SelectedUslug));
                ApplyFiltr();
            }
        }

        public User Master { get; set; }

        public AppointmentsViewModel()
        {
            dataBase = WorkDataBase.Instance;
            Master = dataBase.CurrentMaster;

            UslugiLst = new ObservableCollection<string>();
            Appointments = new ObservableCollection<Appointment>();

            SelectedDate = DateTime.Today.Date;
            SelectedUslug = "Все услуги";

            LoadDataForDate(SelectedDate);
        }

        void LoadDataForDate(DateTime date)
        {
            _allAppointmentsForDate = dataBase.GetAppointmentsMaster(Master, date).ToList();

            string currentSelected = SelectedUslug;

            UslugiLst.Clear();
            UslugiLst.Add("Все услуги");

            if (_allAppointmentsForDate != null)
            {
                var distinctServices = _allAppointmentsForDate
                    .Select(a => a.Service?.TypeService?.Name)
                    .Where(n => n != null)
                    .Distinct()
                    .OrderBy(n => n);

                foreach (var serviceName in distinctServices)
                {
                    UslugiLst.Add(serviceName);
                }
            }

            if (currentSelected != null && UslugiLst.Contains(currentSelected))
            {
                SelectedUslug = currentSelected;
            }
            else
            {
                SelectedUslug = "Все услуги";
            }

            ApplyFiltr();
        }

        void ApplyFiltr()
        {
            if (_allAppointmentsForDate == null)
            {
                Appointments.Clear();
                return;
            }

            var appoints = _allAppointmentsForDate.AsEnumerable();

            if (SelectedUslug != null && SelectedUslug != "Все услуги")
            {
                appoints = appoints.Where(a => a.Service?.TypeService?.Name == SelectedUslug);
            }

            Appointments.Clear();
            foreach (var appointment in appoints.OrderBy(a => a.AppointmentDate))
            {
                Appointments.Add(appointment);
            }
        }
    }
}