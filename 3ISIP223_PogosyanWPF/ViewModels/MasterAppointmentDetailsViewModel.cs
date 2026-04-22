using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class MasterAppointmentDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WorkDataBase dataBase;

        public MasterAppointmentDetailsViewModel(Appointment appointment)
        {
            dataBase = WorkDataBase.Instance;
            Appointment = appointment;
            LoadData();
            
        }

        private Appointment _Appointment;
        public Appointment Appointment 
        { get => _Appointment;
            set
            {
                _Appointment = value;
            }
        }
        void LoadData()
        {
            GetClientFIO = Appointment.User.FIO;
            GetService = Appointment.Service.TypeService.Name;
            GetClientPhone = Appointment.User.Phone;
            GetAppointDate = Appointment.AppointmentDate.ToString("dd MMMM yyyy г.");
            GetAppointTime = Appointment.AppointmentDate.ToString("HH:mm");

        }
        private string _GetClientFIO;
        public string GetClientFIO
        {
            get
            {
                return _GetClientFIO;
            }
            set
            {
                _GetClientFIO = value;
                OnPropertyChanged(GetClientFIO);
            }
        }



        private string _GetService;
        public string GetService
        {
            get
            {
                return _GetService;
            }
            set
            {
                _GetService = value;
                OnPropertyChanged(GetService);
            }
        }





        private string _GetClientPhone;
        public string GetClientPhone
        {
            get
            {
                return _GetClientPhone;
            }
            set
            {
                _GetClientPhone = value;
                OnPropertyChanged(GetClientPhone);
            }
        }



        private string _GetAppointDate;
        public string GetAppointDate
        {
            get
            {
                return _GetAppointDate;
            }
            set
            {
                _GetAppointDate = value;
                OnPropertyChanged(GetAppointDate);
            }
        }


        private string _GetAppointTime;
        public string GetAppointTime
        {
            get
            {
                return _GetAppointTime;
            }
            set
            {
                _GetAppointTime = value;
                OnPropertyChanged(GetAppointTime);
            }
        }

    }
}
