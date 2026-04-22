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

        public int GetCountCart => 1;

        private List<User> _masterServes;
        public List<User> MasterServes
        {
            get
            {
                if (_masterServes == null) _masterServes = Users.Where(u => u.TypeRole.Name == "Мастер" && !u.IsFrozen).ToList();
                return _masterServes;
            }
            set { }

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

        private List<Product> _products;
        public List<Product> AllProducts
        {
            get
            {
                if (_products == null) _products = Core.Kosmetica.Products.Where(s=>!s.IsFrozen).ToList();
                return _products;
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

        public ObservableCollection<Appointment> GetAppointmentsMaster(User master, DateTime? date=null, int? ClientId = null, bool? isMaster = null)
        {
            List<Appointment> lst = null;
            if (isMaster != null && (bool)isMaster)
            {
                lst = AllAppointemntes.Where(serv => serv.Service.MasterSerives.Where(u => u.User == master).Count() > 0).OrderBy(a=>a.AppointmentDate.Date).ToList();

            }else 
                lst = AllAppointemntes.Where(serv => serv.Service.MasterSerives.Where(u => u.User == master).Count() > 0 && serv.Client_ID == ClientId && serv.AppointmentDate == date).ToList();
            //if (date != null) lst = lst.Where(s => s.AppointmentDate == date);
            

            return new ObservableCollection<Appointment>(lst);
        }

        private Cart CartCurrentUsert;

        public Cart GetCurrentUserCart
        {
            get
            {
                if (CartCurrentUsert == null) CartCurrentUsert = Core.Kosmetica.Carts.FirstOrDefault();
                return CartCurrentUsert;
            }
        }
        private List<ProdCart> ProdCarts;
        public List<ProdCart> GetCartItems(int cartId)
        {
            if (ProdCarts == null) ProdCarts = Core.Kosmetica.ProdCarts.Where(p=>p.Cart.User_ID == GetCurrentUserCart.User_ID).ToList();
            
            return ProdCarts;
        }



        public List<Service> Services;
        public List<MasterSerive> AllMasterServices;

        public List<Service> GetMasterServices()
        {

            var maServices = AllMasterServices.Where(s=>s.User == CurrentUser).Select(f=>f.Service).ToList();
            return maServices;
        }

        public void RemoveFromCart(int cartId, int productId)
        {
        }

        public void UpdateCartQuantity(int cartId, int productId, int quantity)
        {
        }

        public void UpdateCartTotal(Cart cart)
        {

        }

        public void CreateOrderFromCart(Cart cart, List<ProdCart> items)
        {

        }


        //public List<Service> GetMasterServices => Core.Kosmetica.MasterSerives.Join(Core.Kosmetica.Services, p => p.Service_ID, s => s.Service_ID, (p, s) => new { p.Service, s.TypeService, p.User, s.Price, s.DurationMinutes}).ToList();

        public void AddServiceToMaster(int IdMaster, Service service)
        {
            var mast =
                new MasterSerive()
                {
                    User = CurrentUser,
                    Service = service,
                };
            Core.Kosmetica.MasterSerives.Add(mast);
            Core.Kosmetica.SaveChanges();
            //var d = MasterServes.Select(m => m.MasterSerives);
            AllMasterServices.Add(mast);

        }
        public void RemoveServiceFromMaster(int IdMaster, Service service)
        {

        }

        public void UpdateService(Service service)
        {
        }

        public void UpdateAppointmentStatus(Appointment appointment, string status)
        {
        }

        public string GetTypeServiceName(int typeServiceId)
        {
            return "";
        }
        public Service GetServiceByTypeService(TypeService typeService) => Services.FirstOrDefault(s => s.TypeService == typeService);

        public List<TypeService> GetAllTypeServicesMaster(int MasterID)
        {
            var df = Services.Where(s => !MasterServes.SelectMany(m => m.MasterSerives.Select(ms => ms.Service)).Contains(s)).Select(s=>s.TypeService).ToList();
            return df;
        }

        public WorkDataBase()
        {
            TypeServes = new ObservableCollection<TypeService>( Core.Kosmetica.TypeServices);
            Services = Core.Kosmetica.Services.ToList();
            AllMasterServices = Core.Kosmetica.MasterSerives.ToList();
            CurrentUser = MasterServes.First();
            //TypeServes[0].Services.Where(s => s.MasterSerives.Where(m => m.User_ID == 1).Count() > 0);

        }
    }
}
