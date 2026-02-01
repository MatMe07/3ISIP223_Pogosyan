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

        public WorkWIthDatabase()
        {
            Films = new ObservableCollection<Film>(Core.Context.Films.ToList());
            _allfilms = Core.Context.Films.ToList();
            //Core.Context.Films.ToList()[0].Age_Ratings.Name;
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

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    static class MarDatabase
    {
        static public WorkWIthDatabase WIthDatabase { get { return new WorkWIthDatabase(); } }
    }
}
