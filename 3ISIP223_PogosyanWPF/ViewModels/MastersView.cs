using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class MastersView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private WorkDataBase dataBase;

        public MastersView()
        {
            dataBase = WorkDataBase.Instance;
            Master = dataBase.CurrentMaster;
            UslugiLst = new ObservableCollection<string>();
            Appointments = new ObservableCollection<Appointment>();
            //UslugiLst.Insert(0, "Все услуги");
            SelectedDate = DateTime.Today.Date;
            SelectedUslug = "Все услуги";
            //Appointments = dataBase.GetAppointmentsMaster(Master, SelectedDate);
            //UslugiLst.AddRange(Appointments.Select(a => a.Service.TypeService.Name).Distinct());
            //SelectedDate = DateTime.Now.Date;
        }
        void LoadDataForDate(DateTime date)
        {
            _allAppointmentsForDate = dataBase.GetAppointmentsMaster(Master, date).ToList();
            string currentSelected = SelectedUslug;

            UslugiLst.Clear();
            UslugiLst.Add("Все услуги");

            if (_allAppointmentsForDate != null)
            {
                var distinctServices = _allAppointmentsForDate.Select(a => a.Service.TypeService.Name).Distinct();

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

        private List<Appointment> _allAppointmentsForDate;
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value.Date;
                LoadDataForDate(_selectedDate);
            }
        }

        private string _SelectedUslug;
        public string SelectedUslug
        {
            get { return _SelectedUslug; }
            set
            {
                _SelectedUslug = value;
                ApplyFiltr();
                OnPropertyChanged(nameof(SelectedUslug));
            }
        }

        private List<string> _uslugiLst;
        public ObservableCollection<string> UslugiLst { get; set; }
        //{
        //    get { return _uslugiLst; }
        //    set
        //    {
        //        _uslugiLst = value;
        //        OnPropertyChanged(nameof(UslugiLst));
        //    }
        //}

        public User Master { get; set; }

        public ObservableCollection<Appointment> Appointments { get; set; } 

        void ApplyFiltr()
        {

            var appoints = _allAppointmentsForDate;
            if (SelectedUslug != null && SelectedUslug != "Все услуги")
            {
                appoints = appoints.Where(a=>a.Service.TypeService.Name == SelectedUslug).ToList();
            }
            Appointments.Clear();
            foreach (var appointment in appoints)
            {
                Appointments.Add(appointment);
            }
        }

    }
}
