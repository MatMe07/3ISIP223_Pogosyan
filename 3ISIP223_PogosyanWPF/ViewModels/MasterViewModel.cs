using _3ISIP223_PogosyanWPF.Winodws;
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

        private Appointment _CurrentAppointment;
        public Appointment CurrentAppointment
        {
            get
            {
                return _CurrentAppointment;
            }
            set
            {
                if (value != null)
                {
                    _CurrentAppointment = value;
                    OpenWindowAppointmentDeteail();
                }
            }
        }

        public MasterViewModel()
        {
            dataBase = WorkDataBase.Instance;
            _currentMaster = dataBase.CurrentUser;

            MasterServices = new ObservableCollection<Service>();
            MasterAppointments = new ObservableCollection<Appointment>();

            LoadAllData();
        }

        public int GetUserID => _currentMaster.User_ID;

        void LoadAllData()
        {
            _allMasterServices = dataBase.GetMasterServices().ToList();

            _allMasterAppointments = dataBase.GetAppointmentsMaster(_currentMaster, isMaster:true).ToList();

            UpdateServicesList();
            UpdateAppointmentsList();
        }

        void OpenWindowAppointmentDeteail()
        {
            var wind = new DatailAppointment(CurrentAppointment);
            var res = wind.ShowDialog();

            if (res == true)
            {

                CloseAppoint();
            }
        }

        //private ObservableCollection<Service> _masterServices;
        public ObservableCollection<Service> MasterServices { get; set; }
        //{
        //    get { return _masterServices; }
        //    set
        //    {
        //        _masterServices = value;
        //        OnPropertyChanged(nameof(MasterServices));
        //    }
        //}

        //private ObservableCollection<Appointment> _masterAppointments;
        public ObservableCollection<Appointment> MasterAppointments { get; set; }
        //{
        //    get { return _masterAppointments; }
        //    set
        //    {
        //        _masterAppointments = value;
        //        OnPropertyChanged(nameof(MasterAppointments));
        //    }
        //}

        public void CloseAppoint()
        {
            CurrentAppointment.Status = "Completed";
            MasterAppointments[MasterAppointments.IndexOf(CurrentAppointment)] = CurrentAppointment;
            UpdateAppointmentsList();
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
            foreach (var appointment in _allMasterAppointments.Where(m=>m.User != null))
            {
                MasterAppointments.Add(appointment);
            }
        }

        public void AddService(TypeService Tservice)
        {
            var serv = dataBase.GetServiceByTypeService(Tservice);
            dataBase.AddServiceToMaster(_currentMaster.User_ID, serv );
            _allMasterServices.Add(serv);
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