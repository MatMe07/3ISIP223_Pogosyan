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
                if (_products == null) _products = Core.Kosmetica.Products.ToList();
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

        public ObservableCollection<Appointment> GetAppointmentsMaster(User master, DateTime date)
        {

            return new ObservableCollection<Appointment>(AllAppointemntes.Where(serv => serv.Service.MasterSerives.Where(u => u.User == master).Count() > 0 && serv.AppointmentDate.Date == date && serv.Client_ID == null));
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
        //{
        //    // Логика получения корзины по CurrentUser.User_ID
        //    return 
        //}
        private List<ProdCart> ProdCarts;
        public List<ProdCart> GetCartItems(int cartId)
        {
            if (ProdCarts == null) ProdCarts = Core.Kosmetica.ProdCarts.Where(p=>p.Cart.User_ID == GetCurrentUserCart.User_ID).ToList();
            // Логика получения товаров из ProdCarts по Cart_ID
            return ProdCarts;
        }

        // Удалить товар из корзины
        public void RemoveFromCart(int cartId, int productId)
        {
            // DELETE FROM ProdCarts WHERE Cart_ID = cartId AND Product_ID = productId
        }

        // Обновить количество товара
        public void UpdateCartQuantity(int cartId, int productId, int quantity)
        {
            // UPDATE ProdCarts SET Quantity = quantity WHERE Cart_ID = cartId AND Product_ID = productId
        }

        // Обновить общую сумму корзины
        public void UpdateCartTotal(Cart cart)
        {
            // UPDATE Carts SET Quantity = cart.Quantity, TotalPrice = cart.TotalPrice WHERE Cart_ID = cart.Cart_ID
        }

        // Создать заказ из корзины
        public void CreateOrderFromCart(Cart cart, List<ProdCart> items)
        {
            // 1. Создать заказ в Orders
            // 2. Добавить товары в OrderItems
            // 3. Очистить корзину (удалить все из ProdCarts)
        }


        public WorkDataBase()
        {
            TypeServes = new ObservableCollection<TypeService>( Core.Kosmetica.TypeServices);
            //TypeServes[0].Services.Where(s => s.MasterSerives.Where(m => m.User_ID == 1).Count() > 0);
            
        }
    }
}
