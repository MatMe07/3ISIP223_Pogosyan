using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public class SessionDetailView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Session session {  get; set; }

        private ObservableCollection<Seat> _seats;

        public ObservableCollection<Seat> Seats
        {
            get { return _seats; }
            set { 
                _seats = value; 
                OnPropertyChanged(nameof(Seats));
                }
        }

        public int GetCountRow => Seats.Max(s=>s.RowNumber);
        public int GetCountCol => Seats.Max(s=>s.SeatNumber);

        public decimal GetSumPrice => session.BasePrice * selectSeat.Count();

        public ObservableCollection<Seat> selectSeat {  get; set; }= new ObservableCollection<Seat>();

        public SessionDetailView(Session SelectSession)
        {
            session = SelectSession;
            _seats = new ObservableCollection<Seat>(Core.Context.Seats.Where(s=>s.HallID == session.HallID).ToList());
            //selectSeat 
            selectSeat.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(GetSumPrice));
            };
        }

        public void AddSeat(int row, int col)
        {
            selectSeat.Add(Seats.FirstOrDefault(s=>s.RowNumber ==  row && s.SeatNumber == col));
        }
        public void DeleteSeat(int row, int col)
        {
            selectSeat.Remove(Seats.FirstOrDefault(s=>s.RowNumber ==  row && s.SeatNumber == col));
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
