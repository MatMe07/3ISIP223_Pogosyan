using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public class WorkWIthDatabase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Film> _films;
        private List<Film> _allfilms;

        public ObservableCollection<Film> Films
        {
            get { return _films; }
            set { 
                _films = value;
                OnPropertyChanged(nameof(Films));
                }
        }

        //public SessionDetailView sessionDetail {  get; set; }
        public WorkWIthDatabase()
        {
            Films = new ObservableCollection<Film>(Core.Context.Films.ToList());
            _allfilms = Core.Context.Films.ToList();
            //Core.Context.Films.ToList()[0].Age_Ratings.Name;
            _user = null;
            //sessionDetail = MarSessionDetail.SessionDetail;
        }


        private User _user;
        public User CurrUser
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(CurrUser));
            }
        }


        public void SearchAndSortFilm(string name, string sort)
        {

            var result = _allfilms;
            if (!string.IsNullOrEmpty(name)) { 
                result = _allfilms.Where(a => a.Name.ToLower().Contains(name.ToLower())).ToList();
            }

            switch (sort)
            {
                case "По названию":
                    {
                        result = result.OrderBy(a => a.Name).ToList();
                        break;
                    }
                case "По рейтингу":
                    {
                        result = result.OrderByDescending(a => a.Rating).ToList();
                        break;
                    }
            }
            Films = new ObservableCollection<Film>(result);
        }

        public void MakingTickets(Session selectSession, List<Seat> SelectSeats, decimal TotalPrice)
        {
            foreach (var seat in SelectSeats)
            {
                Core.Context.Tickets.Add(
                        new Ticket()
                        {
                            UserID = CurrUser.UserID,
                            SeatID = seat.SeatID,
                            Sessions_ID = selectSession.SessionID,
                            PurchaseDate = DateTime.Now,
                            FinalPrice = TotalPrice
                        }
                    );
                Core.Context.SaveChanges();
                
            }


        }
        public bool CheckEmail( string email)
        {
            return Core.Context.Users.FirstOrDefault(u => u.Email == email) != null;
        }        
        public bool CheckPassword( string email, string password)
        {
            var GetUser = Core.Context.Users.FirstOrDefault(u => u.Email == email);
            if (GetUser == null) return false;

            if ( GetUser.Password == password)
            {
                CurrUser = GetUser;
                //MarSesionDetail.SessionDetail.user = GetUser;
                return true;
            }
            return false;
        }        

        public bool CheckValueAndSignIn( string email, string password)
        {
            if (!CheckEmail(email)) return false;
            
            return CheckPassword(email, password);
        }


        public bool CheckValueAndSave(string name, string email, string password, DateTime dateTime)
        {
            if(CheckEmail(email)) return false;

            var user = new User()
            {
                Name = name,
                Email = email,
                Password = password,
                BirthDate = dateTime,
            };
            Core.Context.Users.Add(user);
            Core.Context.SaveChanges();
            CurrUser = user;
            return true;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    static class MarDatabase
    {
        static public WorkWIthDatabase WIthDatabase { get; set;  }
    }
}
