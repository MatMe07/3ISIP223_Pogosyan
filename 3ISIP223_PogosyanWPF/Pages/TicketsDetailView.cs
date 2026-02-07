using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.Pages
{
    public class TicketsDetailView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private User _user;
        public User User
        {
            get { return _user; }
            set { 
                _user = value; 
                OnPropertyChanged(nameof(User));
            }
        }

        private ObservableCollection<Ticket> _tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
            set
            {
                _tickets = value;
                OnPropertyChanged(nameof(Tickets));
                OnPropertyChanged(nameof(GetCountTickets));
            }
        }

        public int GetCountTickets => Tickets.Count;

        public TicketsDetailView()
        {
            _user = MarDatabase.WIthDatabase.CurrUser;
            _tickets = new ObservableCollection<Ticket>( Core.Context.Tickets.Where(t=>t.UserID == User.UserID).ToList() );
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
