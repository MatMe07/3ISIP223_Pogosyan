using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public class FilmDetailView : INotifyPropertyChanged
    {
        public Film Film { get; set; }
        public List<FilmGenre> filmGenres { get; set; }
        private ObservableCollection<Session> _sessionsList;
        private ObservableCollection<Session> _allsessionsList;
        public ObservableCollection<Session> SessionsList
        {
            get { return _sessionsList; }
            set { 
                _sessionsList = value;
                OnPropertyChanged(nameof(SessionsList));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string GetGenre => string.Join(", ", filmGenres.Select(s=>s.Genre.Name));

        public FilmDetailView(Film film)
        {
            Film = film;
            filmGenres = Core.Context.FilmGenres.Where(f=>f.FilmID == film.FilmsID).ToList();
            _sessionsList = new ObservableCollection<Session>(Core.Context.Sessions.Where(f=>f.FilmID == film.FilmsID && f.IsActive).OrderBy(f=>f.StartDateTime).ToList());
            _allsessionsList = new ObservableCollection<Session>(Core.Context.Sessions.Where(f=>f.FilmID == film.FilmsID && f.IsActive).OrderBy(f=>f.StartDateTime).ToList());
        }


        public void SortSession(string ses)
        {
            switch (ses)
            {
                case "Все":
                    {
                        SessionsList = new ObservableCollection<Session>(_allsessionsList);

                        break;
                    }
                default:
                    {
                        var sort = _allsessionsList.Where(f=>f.Hall.HallRating.Name == ses).ToList();
                        SessionsList = new ObservableCollection<Session>(sort);
                        break;
                    }
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
