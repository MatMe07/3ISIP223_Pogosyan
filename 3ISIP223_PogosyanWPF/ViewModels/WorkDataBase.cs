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
                _allAppointments = Core.Kosmetica.Appointments.ToList();
                return _allAppointments;
            }

        }

        private List<Product> _products;
        public List<Product> AllProductsNotFrizzen
        {
            get
            {
                if (_products == null) _products = AllProducts.Where(s=>!s.IsFrozen).ToList();
                return _products;
            }

        }
        private List<Product> _AllProducts;
        public List<Product> AllProducts
        {
            get
            {
                if (_AllProducts == null) _AllProducts = Core.Kosmetica.Products.ToList();
                return _AllProducts;
            }

        }



        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get
            {
                if (_users == null) _users = new ObservableCollection<User>(Core.Kosmetica.Users);
                return _users;
            }

        }

        public ObservableCollection<TypeService> TypeServes { get; set; }

        public ObservableCollection<Appointment> GetAppointmentsMaster(User master, DateTime? date = null, int? ClientId = null, bool? isMaster = null)
        {
            List<Appointment> lst = null;

            if (isMaster != null && (bool)isMaster)
            {
                lst = AllAppointemntes
                    .Where(serv => serv.Service.MasterSerives.Where(u => u.User == master).Count() > 0)
                    .OrderBy(a => a.AppointmentDate.Date)
                    .ToList();
            }
            else
            {
                lst = AllAppointemntes
                    .Where(serv => serv.Service.MasterSerives.Where(u => u.User == master).Count() > 0
                            && serv.Client_ID == ClientId)
                    .ToList();
            }

            if (date != null)
            {
                lst = lst.Where(s => s.AppointmentDate.Date == date.Value.Date).ToList();
            }

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

        public void RemoveFromCart(ProdCart cartItem)
        {
            if (CurrentUserCart == null || CurrentUserCartItems == null) return;

            Core.Kosmetica.ProdCarts.Remove(cartItem);
            CurrentUserCartItems.Remove(cartItem);

            CurrentUserCart.Quantity = CurrentUserCartItems.Sum(pc => pc.Quantity);
            CurrentUserCart.TotalPrice = CurrentUserCartItems.Sum(pc => pc.Quantity * pc.Product.Price * (100 - pc.Product.Discount) / 100);

            Core.Kosmetica.SaveChanges();
        }
        public void UpdateCartQuantity(ProdCart cartItem, int newQuantity)
        {
            if (CurrentUserCart == null || CurrentUserCartItems == null) return;

            if (newQuantity <= 0)
            {
                RemoveFromCart(cartItem);
                return;
            }

            cartItem.Quantity = newQuantity;

            CurrentUserCart.Quantity = CurrentUserCartItems.Sum(pc => pc.Quantity);
            CurrentUserCart.TotalPrice = CurrentUserCartItems.Sum(pc => pc.Quantity * pc.Product.Price * (100 - pc.Product.Discount) / 100);

            Core.Kosmetica.SaveChanges();
        }

        public void UpdateCartTotal(Cart cart)
        {

        }

        public void ClearCart()
        {
            if (CurrentUserCart == null || CurrentUserCartItems == null) return;

            foreach (var item in CurrentUserCartItems.ToList())
            {
                Core.Kosmetica.ProdCarts.Remove(item);
            }

            CurrentUserCartItems.Clear();
            CurrentUserCart.Quantity = 0;
            CurrentUserCart.TotalPrice = 0;

            Core.Kosmetica.SaveChanges();
           
        }


        public void CreateOrderFromCart()
        {
            if (CurrentUserCart == null || CurrentUserCartItems == null || CurrentUserCartItems.Count == 0) return;

            var order = new Order
            {
                User_ID = CurrentUser.User_ID,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now.AddDays(3),
                PaymentMethod_ID = 1,
                Status = false,
                TotalAmount = CurrentUserCart.TotalPrice
            };

            Core.Kosmetica.Orders.Add(order);
            Core.Kosmetica.SaveChanges();

            foreach (var item in CurrentUserCartItems)
            {
                var orderItem = new OrderItem
                {
                    Order_ID = order.Order_ID,
                    Product_ID = item.Product_ID,
                    Quantity = item.Quantity,
                    PriceAtOrder = item.Product.Price * (100 - item.Product.Discount) / 100
                };
                Core.Kosmetica.OrderItems.Add(orderItem);
            }

            Core.Kosmetica.SaveChanges();

            ClearCart();
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
            var masterService = AllMasterServices.FirstOrDefault(ms => ms.User.User_ID == IdMaster && ms.Service.Service_ID == service.Service_ID);
            if (masterService != null)
            {
                Core.Kosmetica.MasterSerives.Remove(masterService);
                Core.Kosmetica.SaveChanges();
                AllMasterServices.Remove(masterService);
            }
        }

        public void UpdateService(Service service)
        {
            var existingService = Core.Kosmetica.Services.FirstOrDefault(s => s.Service_ID == service.Service_ID);
            if (existingService != null)
            {
                existingService.Price = service.Price;
                existingService.DurationMinutes = service.DurationMinutes;
                Core.Kosmetica.SaveChanges();
            }
        }

        public void UpdateAppointmentStatus(Appointment appointment, string status)
        {
            var existingAppointment = Core.Kosmetica.Appointments.FirstOrDefault(a => a.Appointment_ID == appointment.Appointment_ID);
            if (existingAppointment != null)
            {
                existingAppointment.Status = status;
                Core.Kosmetica.SaveChanges();
            }
        }


        public Service GetServiceByTypeService(TypeService typeService)
        {
            return Services.FirstOrDefault(s => s.TypeService.TypeID == typeService.TypeID);
        }
        //public Service GetServiceByTypeService(TypeService typeService) => Services.FirstOrDefault(s => s.TypeService == typeService);

        public List<TypeService> GetAllTypeServicesMaster(int MasterID)
        {
            var df = Services.Where(s => !MasterServes.SelectMany(m => m.MasterSerives.Select(ms => ms.Service)).Contains(s)).Select(s=>s.TypeService).ToList();
            return df;
        }



        // ЗАКАЗЫ
        public List<Order> GetAllOrders()
        {
            return Core.Kosmetica.Orders.ToList();
        }

        public void CloseOrder(Order order)
        {
            var existingOrder = Core.Kosmetica.Orders.FirstOrDefault(o => o.Order_ID == order.Order_ID);
            if (existingOrder != null)
            {
                existingOrder.Status = true;
                Core.Kosmetica.SaveChanges();
            }
        }
        public void BookAppointment(Appointment appointment, User client, int paymentMethodId, string comment)
        {
            var existingAppointment = Core.Kosmetica.Appointments.FirstOrDefault(a => a.Appointment_ID == appointment.Appointment_ID);

            if (existingAppointment != null)
            {
                existingAppointment.User = client;
                existingAppointment.Client_ID = client.User_ID;
                existingAppointment.PaymentMethod_ID = paymentMethodId;
                existingAppointment.Comment = comment;
                existingAppointment.Status = "Scheduled";

                Core.Kosmetica.SaveChanges();
                _allAppointments = null;
            }
        }


        public void CreateAppointment(User client, Service service, DateTime date)
        {
            var appointment = new Appointment
            {
                User = client,
                Service = service,
                AppointmentDate = date,
                Status = "Scheduled",
                PaymentMethod_ID = 1,
                CreatedAt = DateTime.Now
            };
            _allAppointments.Add(appointment);
            Core.Kosmetica.Appointments.Add(appointment);
            Core.Kosmetica.SaveChanges();
        }

        // ПРОИЗВОДИТЕЛИ
        public List<Manufacturer> GetAllManufacturers()
        {
            return Core.Kosmetica.Manufacturers.ToList();
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            Core.Kosmetica.Manufacturers.Add(manufacturer);
            Core.Kosmetica.SaveChanges();
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            var existing = Core.Kosmetica.Manufacturers.FirstOrDefault(m => m.Manufacturer_ID == manufacturer.Manufacturer_ID);
            if (existing != null)
            {
                existing.Name = manufacturer.Name;
                Core.Kosmetica.SaveChanges();
            }
        }

        public void DeleteManufacturer(Manufacturer manufacturer)
        {
            var existing = Core.Kosmetica.Manufacturers.FirstOrDefault(m => m.Manufacturer_ID == manufacturer.Manufacturer_ID);
            if (existing != null)
            {
                Core.Kosmetica.Manufacturers.Remove(existing);
                Core.Kosmetica.SaveChanges();
            }
        }

        // ТИПЫ ТОВАРОВ
        public List<ProductType> GetAllProductTypes()
        {
            return Core.Kosmetica.ProductTypes.ToList();
        }

        public void AddProductType(ProductType productType)
        {
            Core.Kosmetica.ProductTypes.Add(productType);
            Core.Kosmetica.SaveChanges();
        }

        public void UpdateProductType(ProductType productType)
        {
            var existing = Core.Kosmetica.ProductTypes.FirstOrDefault(pt => pt.ProductType_ID == productType.ProductType_ID);
            if (existing != null)
            {
                existing.Name = productType.Name;
                Core.Kosmetica.SaveChanges();
            }
        }

        public void DeleteProductType(ProductType productType)
        {
            var existing = Core.Kosmetica.ProductTypes.FirstOrDefault(pt => pt.ProductType_ID == productType.ProductType_ID);
            if (existing != null)
            {
                Core.Kosmetica.ProductTypes.Remove(existing);
                Core.Kosmetica.SaveChanges();
            }
        }

        // ТИПЫ УСЛУГ
        public void AddServiceType(TypeService serviceType)
        {
            Core.Kosmetica.TypeServices.Add(serviceType);
            Core.Kosmetica.SaveChanges();
            TypeServes.Add(serviceType);
        }

        public void UpdateServiceType(TypeService serviceType)
        {
            var existing = Core.Kosmetica.TypeServices.FirstOrDefault(st => st.TypeID == serviceType.TypeID);
            if (existing != null)
            {
                existing.Name = serviceType.Name;
                Core.Kosmetica.SaveChanges();
            }
        }

        public void DeleteServiceType(TypeService serviceType)
        {
            var existing = Core.Kosmetica.TypeServices.FirstOrDefault(st => st.TypeID == serviceType.TypeID);
            if (existing != null)
            {
                Core.Kosmetica.TypeServices.Remove(existing);
                Core.Kosmetica.SaveChanges();
                TypeServes.Remove(existing);
            }
        }

        // ТОВАРЫ
        public void AddProduct(Product product)
        {
            Core.Kosmetica.Products.Add(product);
            Core.Kosmetica.SaveChanges();
            
            _AllProducts = null;
        }

        public void UpdateProduct(Product product)
        {
            var existing = Core.Kosmetica.Products.FirstOrDefault(p => p.Product_ID == product.Product_ID);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Discount = product.Discount;
                existing.Rating = product.Rating;
                existing.Description = product.Description;
                existing.ProductType_ID = product.ProductType_ID;
                existing.Manufacturer_ID = product.Manufacturer_ID;
                existing.IsFrozen = product.IsFrozen;
                Core.Kosmetica.SaveChanges();
                _products = null;
            }
        }

        public bool CanDeleteManufacturer(int manufacturerId)
        {
            return !Core.Kosmetica.Products.Any(p => p.Manufacturer_ID == manufacturerId);
        }

        public bool CanDeleteProductType(int productTypeId)
        {
            return !Core.Kosmetica.Products.Any(p => p.ProductType_ID == productTypeId);
        }

        public bool CanDeleteServiceType(int serviceTypeId)
        {
            return !Core.Kosmetica.Services.Any(s => s.TypeService_ID == serviceTypeId);
        }

        public string GetManufacturerUsageInfo(int manufacturerId)
        {
            var products = Core.Kosmetica.Products.Where(p => p.Manufacturer_ID == manufacturerId).ToList();
            if (products.Any())
            {
                return $"Производитель используется в {products.Count} товарах:\n" +
                       string.Join("\n", products.Take(5).Select(p => $"• {p.Name}"));
            }
            return null;
        }

        public string GetProductTypeUsageInfo(int productTypeId)
        {
            var products = Core.Kosmetica.Products.Where(p => p.ProductType_ID == productTypeId).ToList();
            if (products.Any())
            {
                return $"Тип товара используется в {products.Count} товарах:\n" +
                       string.Join("\n", products.Take(5).Select(p => $"• {p.Name}"));
            }
            return null;
        }

        public string GetServiceTypeUsageInfo(int serviceTypeId)
        {
            var services = Core.Kosmetica.Services.Where(s => s.TypeService_ID == serviceTypeId).ToList();
            if (services.Any())
            {
                return $"Тип услуги используется в {services.Count} услугах";
            }
            return null;
        }






        public void AddUser(User user)
        {
            Core.Kosmetica.Users.Add(user);
            Core.Kosmetica.SaveChanges();
            _users = null;
        }

        public void UpdateUser(User user)
        {
            var existing = Core.Kosmetica.Users.FirstOrDefault(u => u.User_ID == user.User_ID);
            if (existing != null)
            {
                existing.FirstName = user.FirstName;
                existing.LastName = user.LastName;
                existing.SecondName = user.SecondName;
                existing.Phone = user.Phone;
                existing.Email = user.Email;
                existing.Role_ID = user.Role_ID;
                existing.IsFrozen = user.IsFrozen;
                Core.Kosmetica.SaveChanges();
                _users = null;
            }
        }

        public void DeleteUser(User user)
        {
            var existing = Core.Kosmetica.Users.FirstOrDefault(u => u.User_ID == user.User_ID);
            if (existing != null)
            {
                Core.Kosmetica.Users.Remove(existing);
                Users.Remove(existing);
                Core.Kosmetica.SaveChanges();
                _users = null;
            }
        }

        public string GetUserUsageInfo(int userId)
        {
            var hasAppointments = Core.Kosmetica.Appointments.Any(a => a.Client_ID == userId);
            var hasOrders = Core.Kosmetica.Orders.Any(o => o.User_ID == userId);
            var hasMasterServices = Core.Kosmetica.MasterSerives.Any(ms => ms.User_ID == userId);

            if (hasAppointments)
                return "У пользователя есть активные записи";
            if (hasOrders)
                return "У пользователя есть заказы";
            if (hasMasterServices)
                return "Пользователь является мастером и у него есть услуги";

            return null;
        }


        private Cart _currentUserCart;
        private List<ProdCart> _currentUserCartItems;

        public Cart CurrentUserCart
        {
            get
            {
                if (_currentUserCart == null && CurrentUser != null)
                {
                    _currentUserCart = Core.Kosmetica.Carts.FirstOrDefault(c => c.User_ID == CurrentUser.User_ID);
                }
                return _currentUserCart;
            }
            private set
            {
                _currentUserCart = value;
            }
        }

        public List<ProdCart> CurrentUserCartItems
        {
            get
            {
                if (_currentUserCartItems == null && CurrentUserCart != null)
                {
                    _currentUserCartItems = Core.Kosmetica.ProdCarts
                        .Where(pc => pc.Cart_ID == CurrentUserCart.Cart_ID)
                        .ToList();
                }
                return _currentUserCartItems;
            }
            private set
            {
                _currentUserCartItems = value;
            }
        }

        public int GetCountCart
        {
            get
            {
                if (CurrentUser == null) return 0;
                if (CurrentUserCartItems == null) return 0;
                return CurrentUserCartItems.Sum(pc => pc.Quantity);
            }
        }

        public void AddToCart(Product product)
        {
            if (CurrentUser == null) return;

            if (CurrentUserCart == null)
            {
                CurrentUserCart = new Cart
                {
                    User_ID = CurrentUser.User_ID,
                    Quantity = 0,
                    TotalPrice = 0
                };
                Core.Kosmetica.Carts.Add(CurrentUserCart);
                Core.Kosmetica.SaveChanges();
                CurrentUserCartItems = new List<ProdCart>();
            }

            var cartItem = CurrentUserCartItems.FirstOrDefault(pc => pc.Product_ID == product.Product_ID);

            if (cartItem == null)
            {
                cartItem = new ProdCart
                {
                    Cart_ID = CurrentUserCart.Cart_ID,
                    Product_ID = product.Product_ID,
                    Quantity = 1,
                    Product = product
                };
                Core.Kosmetica.ProdCarts.Add(cartItem);
                CurrentUserCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            CurrentUserCart.Quantity = CurrentUserCartItems.Sum(pc => pc.Quantity);
            CurrentUserCart.TotalPrice = CurrentUserCartItems.Sum(pc => pc.Quantity * pc.Product.Price);

            Core.Kosmetica.SaveChanges();
        }


        public List<Order> GetUserOrders(int userId)
        {
            return Core.Kosmetica.Orders
                .Where(o => o.User_ID == userId)
                .ToList();
        }

        private List<TypeRole> _allroles;
        public List<TypeRole> Allroles
        {
            get
            {
                if (_allroles == null) _allroles = Core.Kosmetica.TypeRoles.ToList();
                return _allroles;
            }
            set
            {
                _allroles = value;
            }
        }


        public void CreateOrderFromCartWithDetails(DateTime deliveryDate, int paymentMethodId)
        {
            if (CurrentUserCart == null || CurrentUserCartItems == null || CurrentUserCartItems.Count == 0) return;

            var order = new Order
            {
                User_ID = CurrentUser.User_ID,
                OrderDate = DateTime.Now,
                DeliveryDate = deliveryDate,
                PaymentMethod_ID = paymentMethodId,
                Status = false,
                TotalAmount = CurrentUserCart.TotalPrice
            };

            Core.Kosmetica.Orders.Add(order);
            Core.Kosmetica.SaveChanges();

            foreach (var item in CurrentUserCartItems)
            {
                var orderItem = new OrderItem
                {
                    Order_ID = order.Order_ID,
                    Product_ID = item.Product_ID,
                    Quantity = item.Quantity,
                    PriceAtOrder = item.Product.Price * (100 - item.Product.Discount) / 100
                };
                Core.Kosmetica.OrderItems.Add(orderItem);
            }

            Core.Kosmetica.SaveChanges();
            ClearCart();
        }


        public void ClearCartCache()
        {
            _currentUserCart = null;
            _currentUserCartItems = null;
        }
        public WorkDataBase()
        {
            TypeServes = new ObservableCollection<TypeService>( Core.Kosmetica.TypeServices);
            Services = Core.Kosmetica.Services.ToList();
            AllMasterServices = Core.Kosmetica.MasterSerives.ToList();
            //CurrentUser = MasterServes.First();
            //TypeServes[0].Services.Where(s => s.MasterSerives.Where(m => m.User_ID == 1).Count() > 0);

        }
    }
}
