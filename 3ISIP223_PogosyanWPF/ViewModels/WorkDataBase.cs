using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class WorkDataBase
    {
        private static WorkDataBase _instance;
        public static WorkDataBase Instance => _instance ?? (_instance = new WorkDataBase());

        public User CurrentUser { get; set; }

        public User CurrentMaster { get; set; }


        private ObservableCollection<User> _masterServes;
        public ObservableCollection<User> MasterServes
        {
            get
            {
                if (_masterServes == null) _masterServes = new ObservableCollection<User>(Users.Where(u => u.TypeRole.Name == "Мастер" && !u.IsFrozen));
                return _masterServes;
            }

        }
        private List<Appointment> _allAppointments;
        public List<Appointment> AllAppointemntes
        {
            get
            {
                if (_allAppointments == null) _allAppointments = Core.Kosmetica.Appointments.ToList();
                return _allAppointments;
            }

        }


        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get
            {
                if (_masterServes == null) _users = new ObservableCollection<User>(Core.Kosmetica.Users);
                return _users;
            }

        }

        public ObservableCollection<TypeService> TypeServes { get; set; }

        public ObservableCollection<Appointment> GetAppointmentsMaster(User master, DateTime date)
        {

            return new ObservableCollection<Appointment>(AllAppointemntes.Where(serv => serv.Service.MasterSerives.Where(u => u.User == master).Count() > 0 && serv.AppointmentDate.Date == date));
        }

        public WorkDataBase()
        {
            TypeServes = new ObservableCollection<TypeService>( Core.Kosmetica.TypeServices);
            //TypeServes[0].Services.Where(s => s.MasterSerives.Where(m => m.User_ID == 1).Count() > 0);
            
        }
    }
}
