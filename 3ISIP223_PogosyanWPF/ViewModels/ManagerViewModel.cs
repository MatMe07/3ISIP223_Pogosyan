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
    public class ManagerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WorkDataBase dataBase;
        private List<User> _allClients;
        private List<Appointment> _allAppointments;
        private List<Order> _allOrders;
        private List<Product> _allProducts;
        private List<Manufacturer> _allManufacturers;
        private List<ProductType> _allProductTypes;
        private List<TypeService> _allServiceTypes;

        // Поиск клиента
        private string _searchClientText;
        public string SearchClientText
        {
            get { return _searchClientText; }
            set
            {
                _searchClientText = value;
                OnPropertyChanged(nameof(SearchClientText));
                SearchClients();
            }
        }

        private ObservableCollection<User> _foundClients;
        public ObservableCollection<User> FoundClients
        {
            get { return _foundClients; }
            set
            {
                _foundClients = value;
                OnPropertyChanged(nameof(FoundClients));
            }
        }

        // Записи
        private ObservableCollection<Appointment> _appointments;
        public ObservableCollection<Appointment> Appointments
        {
            get { return _appointments; }
            set
            {
                _appointments = value;
                OnPropertyChanged(nameof(Appointments));
                //Appointments[0].Service.TypeService.Name
            }
        }

        // Заказы
        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        // Товары
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        // Производители
        private ObservableCollection<Manufacturer> _manufacturers;
        public ObservableCollection<Manufacturer> Manufacturers
        {
            get { return _manufacturers; }
            set
            {
                _manufacturers = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }

        // Типы товаров
        private ObservableCollection<ProductType> _productTypes;
        public ObservableCollection<ProductType> ProductTypes
        {
            get { return _productTypes; }
            set
            {
                _productTypes = value;
                OnPropertyChanged(nameof(ProductTypes));
            }
        }

        // Типы услуг
        private ObservableCollection<TypeService> _serviceTypes;
        public ObservableCollection<TypeService> ServiceTypes
        {
            get { return _serviceTypes; }
            set
            {
                _serviceTypes = value;
                OnPropertyChanged(nameof(ServiceTypes));
            }
        }

        public ManagerViewModel()
        {
            dataBase = WorkDataBase.Instance;

            FoundClients = new ObservableCollection<User>();
            Appointments = new ObservableCollection<Appointment>();
            Orders = new ObservableCollection<Order>();
            Products = new ObservableCollection<Product>();
            Manufacturers = new ObservableCollection<Manufacturer>();
            ProductTypes = new ObservableCollection<ProductType>();
            ServiceTypes = new ObservableCollection<TypeService>();

            LoadAllData();
        }

        void LoadAllData()
        {
            _allClients = dataBase.Users.Where(u => u.TypeRole.Name == "Клиент").ToList();

            foreach (var client in _allClients)
            {
                FoundClients.Add(client);
            }

            _allAppointments = dataBase.AllAppointemntes.ToList();

            _allOrders = dataBase.GetAllOrders().ToList();

            _allProducts = dataBase.AllProducts.ToList();

            _allManufacturers = dataBase.GetAllManufacturers().ToList();

            _allProductTypes = dataBase.GetAllProductTypes().ToList();

            _allServiceTypes = dataBase.TypeServes.ToList();

            UpdateAppointmentsList();
            UpdateOrdersList();
            UpdateProductsList();
            UpdateManufacturersList();
            UpdateProductTypesList();
            UpdateServiceTypesList();
        }

        //ПОИСК
        void SearchClients()
        {
            FoundClients.Clear();

            if (string.IsNullOrWhiteSpace(SearchClientText))
            {
                foreach (var client in _allClients)
                {
                    FoundClients.Add(client);
                }
                return;
            }

            var filtered = _allClients.Where(c =>
                (c.LastName.ToLower() + " " + c.FirstName.ToLower() + " " + c.SecondName.ToLower()).Contains(SearchClientText.ToLower()) ||
                c.Phone.Contains(SearchClientText));

            foreach (var client in filtered)
            {
                FoundClients.Add(client);
            }
        }

        // ЗАПИСИ
        void UpdateAppointmentsList()
        {
            Appointments.Clear();
            foreach (var appointment in _allAppointments.Where(s=>s.Status != "Cancelled").OrderBy(a => a.AppointmentDate))
            {
                Appointments.Add(appointment);
            }
        }

        public void RescheduleAppointment(Appointment appointment, DateTime newDate)
        {
            appointment.AppointmentDate = newDate;
            dataBase.UpdateAppointmentStatus(appointment, appointment.Status);
            UpdateAppointmentsList();
        }

        public void CancelAppointment(Appointment appointment)
        {
            var result = MessageBox.Show($"Отменить запись {appointment.AppointmentDate}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                appointment.Status = "Cancelled";
                dataBase.UpdateAppointmentStatus(appointment, "Cancelled");
                UpdateAppointmentsList();
            }
        }

        public List<DateTime> GetAvailableSlotsForMaster(Appointment appointment, int masterId, DateTime date)
        {
            var availableSlots = new List<DateTime>();

            var startTime = new TimeSpan(9, 0, 0);
            var endTime = new TimeSpan(18, 0, 0);

            var durationMinutes = appointment.Service.DurationMinutes;

            var existingAppointments = dataBase.AllAppointemntes
                .Where(a => a.Service.MasterSerives.Any(m => m.User.User_ID == masterId)
                            && a.AppointmentDate.Date == date.Date
                            && a.Status != "Cancelled")
                .ToList();

            var currentTime = startTime;
            while (currentTime + TimeSpan.FromMinutes(durationMinutes) <= endTime)
            {
                var slotStart = date.Date + currentTime;
                var slotEnd = slotStart + TimeSpan.FromMinutes(durationMinutes);

                var isBusy = existingAppointments.Any(a =>
                {
                    var existingStart = a.AppointmentDate;
                    var existingEnd = existingStart + TimeSpan.FromMinutes( (a.Service.DurationMinutes == null) ? 60 : a.Service.DurationMinutes);
                    return (slotStart < existingEnd && slotEnd > existingStart);
                });

                if (!isBusy && slotStart > DateTime.Now)
                {
                    availableSlots.Add(slotStart);
                }

                currentTime = currentTime.Add(TimeSpan.FromMinutes(30));
            }

            return availableSlots;
        }

        public List<User> GetAllMasters()
        {
            return dataBase.Users.Where(u => u.TypeRole.Name == "Мастер" && !u.IsFrozen).ToList();
        }

        public List<TypeService> GetAllServiceTypes()
        {
            return dataBase.TypeServes.ToList();
        }

        public List<Service> GetServicesByTypeAndMaster(int typeServiceId, int masterId)
        {
            return dataBase.Services
                .Where(s => s.TypeService.TypeID == typeServiceId
                            && s.MasterSerives.Any(m => m.User.User_ID == masterId))
                .ToList();
        }

        public Service GetServiceByTypeAndMaster(int typeServiceId, int masterId)
        {
            return dataBase.Services
                .FirstOrDefault(s => s.TypeService.TypeID == typeServiceId
                                     && s.MasterSerives.Any(m => m.User.User_ID == masterId));
        }

        public List<DateTime> GetAvailableSlotsForMaster(int masterId, DateTime date, int durationMinutes = 60)
        {
            var availableSlots = new List<DateTime>();

            var startTime = new TimeSpan(9, 0, 0);
            var endTime = new TimeSpan(18, 0, 0);

            var existingAppointments = dataBase.AllAppointemntes
                .Where(a => a.Service.MasterSerives.Any(m => m.User.User_ID == masterId)
                            && a.AppointmentDate.Date == date.Date
                            && a.Status != "Cancelled")
                .ToList();

            var currentTime = startTime;
            while (currentTime + TimeSpan.FromMinutes(durationMinutes) <= endTime)
            {
                var slotStart = date.Date + currentTime;
                var slotEnd = slotStart + TimeSpan.FromMinutes(durationMinutes);

                var isBusy = existingAppointments.Any(a =>
                {
                    var existingStart = a.AppointmentDate;
                    var existingEnd = existingStart + TimeSpan.FromMinutes(a.Service.DurationMinutes);
                    return (slotStart < existingEnd && slotEnd > existingStart);
                });

                if (!isBusy && slotStart > DateTime.Now)
                {
                    availableSlots.Add(slotStart);
                }

                currentTime = currentTime.Add(TimeSpan.FromMinutes(30));
            }

            return availableSlots;
        }

        public void CreateAppointment(User client, Service service, DateTime appointmentDate, int paymentMethodId, string comment)
        {
            var appointment = new Appointment
            {
                User = client,
                Service = service,
                AppointmentDate = appointmentDate,
                Status = "Scheduled",
                PaymentMethod_ID = paymentMethodId,
                Comment = comment,
                CreatedAt = DateTime.Now
            };


            dataBase.CreateAppointment(client, service, appointmentDate);
            _allAppointments = dataBase.AllAppointemntes.ToList();
            UpdateAppointmentsList();

        }

        // ЗАКАЗЫ
        void UpdateOrdersList()
        {
            Orders.Clear();
            foreach (var order in _allOrders.OrderByDescending(o => o.OrderDate))
            {
                Orders.Add(order);

            }
        }

        public void CloseOrder(Order order)
        {
            var result = MessageBox.Show($"Закрыть заказ №{order.Order_ID}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                dataBase.CloseOrder(order);
                _allOrders = dataBase.GetAllOrders().ToList();
                UpdateOrdersList();
            }
        }

        //ТОВАРЫ 
        void UpdateProductsList()
        {
            Products.Clear();
            foreach (var product in _allProducts)
            {
                Products.Add(product);
            }
        }

        public void AddProduct(Product product)
        {
            dataBase.AddProduct(product);
            _allProducts = dataBase.AllProducts.ToList();
            UpdateProductsList();
        }

        public void UpdateProduct(Product product)
        {
            dataBase.UpdateProduct(product);
            _allProducts = dataBase.AllProducts.ToList();
            UpdateProductsList();
        }

        public void ToggleFreezeProduct(Product product)
        {
            product.IsFrozen = !product.IsFrozen;
            UpdateProduct(product);
        }

        public void UpdateProductDiscount(Product product, decimal newDiscount)
        {
            product.Discount = newDiscount;
            UpdateProduct(product);
        }

        public List<ProductType> GetAllProductTypes()
        {
            return dataBase.GetAllProductTypes();
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            return dataBase.GetAllManufacturers();
        }

        // ==================== ПРОИЗВОДИТЕЛИ ====================
        void UpdateManufacturersList()
        {
            Manufacturers.Clear();
            foreach (var manufacturer in _allManufacturers)
            {
                Manufacturers.Add(manufacturer);
            }
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            dataBase.AddManufacturer(manufacturer);
            _allManufacturers = dataBase.GetAllManufacturers().ToList();
            UpdateManufacturersList();
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            dataBase.UpdateManufacturer(manufacturer);
            _allManufacturers = dataBase.GetAllManufacturers().ToList();
            UpdateManufacturersList();
        }

        public void DeleteManufacturer(Manufacturer manufacturer)
        {
            var usageInfo = dataBase.GetManufacturerUsageInfo(manufacturer.Manufacturer_ID);

            if (!string.IsNullOrEmpty(usageInfo))
            {
                MessageBox.Show($"Нельзя удалить производителя!\n\n{usageInfo}\n\nСначала удалите или измените эти товары.",
                    "Невозможно удалить", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить производителя {manufacturer.Name}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                dataBase.DeleteManufacturer(manufacturer);
                _allManufacturers = dataBase.GetAllManufacturers().ToList();
                UpdateManufacturersList();
            }
        }

        // ==================== ТИПЫ ТОВАРОВ ====================
        void UpdateProductTypesList()
        {
            ProductTypes.Clear();
            foreach (var type in _allProductTypes)
            {
                ProductTypes.Add(type);
            }
        }

        public void AddProductType(ProductType productType)
        {
            dataBase.AddProductType(productType);
            _allProductTypes = dataBase.GetAllProductTypes().ToList();
            UpdateProductTypesList();
        }

        public void UpdateProductType(ProductType productType)
        {
            dataBase.UpdateProductType(productType);
            _allProductTypes = dataBase.GetAllProductTypes().ToList();
            UpdateProductTypesList();
        }

        public void DeleteProductType(ProductType productType)
        {
            var usageInfo = dataBase.GetProductTypeUsageInfo(productType.ProductType_ID);

            if (!string.IsNullOrEmpty(usageInfo))
            {
                MessageBox.Show($"Нельзя удалить тип товара!\n\n{usageInfo}\n\nСначала удалите или измените эти товары.",
                    "Невозможно удалить", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить тип товара {productType.Name}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                dataBase.DeleteProductType(productType);
                _allProductTypes = dataBase.GetAllProductTypes().ToList();
                UpdateProductTypesList();
            }
        }

        // ТИПЫ УСЛУГ
        void UpdateServiceTypesList()
        {
            ServiceTypes.Clear();
            foreach (var type in _allServiceTypes)
            {
                ServiceTypes.Add(type);
            }
        }

        public void AddServiceType(TypeService serviceType)
        {
            dataBase.AddServiceType(serviceType);
            _allServiceTypes = dataBase.TypeServes.ToList();
            UpdateServiceTypesList();
        }

        public void UpdateServiceType(TypeService serviceType)
        {
            dataBase.UpdateServiceType(serviceType);
            _allServiceTypes = dataBase.TypeServes.ToList();
            UpdateServiceTypesList();
        }

        public void DeleteServiceType(TypeService serviceType)
        {
            var usageInfo = dataBase.GetServiceTypeUsageInfo(serviceType.TypeID);

            if (!string.IsNullOrEmpty(usageInfo))
            {
                MessageBox.Show($"Нельзя удалить тип услуги!\n\n{usageInfo}\n\nСначала удалите или измените эти услуги.",
                    "Невозможно удалить", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Удалить тип услуги {serviceType.Name}?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                dataBase.DeleteServiceType(serviceType);
                _allServiceTypes = dataBase.TypeServes.ToList();
                UpdateServiceTypesList();
            }
        }
    }
}