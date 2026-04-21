using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class MasterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WorkDataBase dataBase;
        private User _currentMaster;
        private List<Service> _allMasterServices;
        private List<Appointment> _allMasterAppointments;

        public MasterViewModel()
        {
            dataBase = WorkDataBase.Instance;
            _currentMaster = dataBase.CurrentUser;

            MasterServices = new ObservableCollection<Service>();
            MasterAppointments = new ObservableCollection<Appointment>();

            LoadAllData();
        }

        void LoadAllData()
        {
            _allMasterServices = dataBase.GetMasterServices().ToList();

            _allMasterAppointments = dataBase.GetMasterAppointments(_currentMaster.User_ID).ToList();

            UpdateServicesList();
            UpdateAppointmentsList();
        }

        private ObservableCollection<Service> _masterServices;
        public ObservableCollection<Service> MasterServices
        {
            get { return _masterServices; }
            set
            {
                _masterServices = value;
                OnPropertyChanged(nameof(MasterServices));
            }
        }

        private ObservableCollection<Appointment> _masterAppointments;
        public ObservableCollection<Appointment> MasterAppointments
        {
            get { return _masterAppointments; }
            set
            {
                _masterAppointments = value;
                OnPropertyChanged(nameof(MasterAppointments));
            }
        }

        void UpdateServicesList()
        {
            MasterServices.Clear();
            foreach (var service in _allMasterServices)
            {
                MasterServices.Add(service);
            }
        }

        void UpdateAppointmentsList()
        {
            MasterAppointments.Clear();
            foreach (var appointment in _allMasterAppointments)
            {
                MasterAppointments.Add(appointment);
            }
        }

        public void AddService(Service service)
        {
            dataBase.AddServiceToMaster(_currentMaster.User_ID, service); 
            _allMasterServices.Add(service);
            UpdateServicesList();
        }

        public void RemoveService(Service service)
        {
            dataBase.RemoveServiceFromMaster(_currentMaster.User_ID, service);
            _allMasterServices.Remove(service);
            UpdateServicesList();
        }

        public void UpdateService(Service service)
        {
            dataBase.UpdateService(service);
            var index = _allMasterServices.FindIndex(s => s.Service_ID == service.Service_ID);
            if (index != -1)
            {
                _allMasterServices[index] = service;
            }
            UpdateServicesList();
        }

        public void CompleteAppointment(Appointment appointment)
        {
            appointment.Status = "Completed";
            dataBase.UpdateAppointmentStatus(appointment, "Completed");

            var index = _allMasterAppointments.FindIndex(a => a.Appointment_ID == appointment.Appointment_ID);
            if (index != -1)
            {
                _allMasterAppointments[index] = appointment;
            }
            UpdateAppointmentsList();
        }

        public void CancelAppointment(Appointment appointment)
        {
            appointment.Status = "Cancelled";
            dataBase.UpdateAppointmentStatus(appointment, "Cancelled");

            var index = _allMasterAppointments.FindIndex(a => a.Appointment_ID == appointment.Appointment_ID);
            if (index != -1)
            {
                _allMasterAppointments[index] = appointment;
            }
            UpdateAppointmentsList();
        }

    }
}