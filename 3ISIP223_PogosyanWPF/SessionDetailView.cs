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
                OnPropertyChanged(nameof(GetSumPrice));
                //session.Film.

            }
        }


        public int GetCountRow => Seats.Max(s=>s.RowNumber);
        public int GetCountCol => Seats.Max(s=>s.SeatNumber);

        public decimal GetSumPrice  => session.BasePrice * SelectSeat.Count;

        public string StrTotalPrice => $"Далее: {GetSumPrice} ₽";

        private ObservableCollection<Seat> _selectSeat;
        public ObservableCollection<Seat> SelectSeat
        {
            get { return _selectSeat; }
            set
            {
                _selectSeat = value;
                OnPropertyChanged(nameof(SelectSeat));
                OnPropertyChanged(nameof(StrTotalPrice));
                OnPropertyChanged(nameof(GetSumPrice));

            }
        }

        public SessionDetailView(Session SelectSession)
        {
            session = SelectSession;
            _seats = new ObservableCollection<Seat>(Core.Context.Seats.Where(s=>s.HallID == session.HallID).ToList());
            _selectSeat = new ObservableCollection<Seat>();
            //selectSeat 
            SelectSeat.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(GetSumPrice));
                OnPropertyChanged(nameof(StrTotalPrice));
            };
        }

        //public void UpdateParams()
        //{
        //    OnPropertyChanged(nameof(GetSumPrice)); 
        //}

        public void AddSeat(int row, int col)
        {
            SelectSeat.Add(Seats.FirstOrDefault(s=>s.RowNumber ==  row && s.SeatNumber == col));
            //UpdateParams();
        }
        public void DeleteSeat(int row, int col)
        {
            SelectSeat.Remove(Seats.FirstOrDefault(s=>s.RowNumber ==  row && s.SeatNumber == col));
            //UpdateParams();
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
