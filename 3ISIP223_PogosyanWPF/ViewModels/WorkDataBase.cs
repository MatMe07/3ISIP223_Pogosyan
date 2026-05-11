using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class WorkDataBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static WorkDataBase _instance;
        public static WorkDataBase Instanse => _instance ?? (_instance = new WorkDataBase());

        public WorkDataBase()
        {

        }
        //-------------------------------------------------------------------------------------------------------------------------------------


        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get
            {
                if (_books == null) _books = new ObservableCollection<Book>(Core.kingEntities.Books);
                return _books;
            }

        }

        private List<Genre> _genres;
        public List<Genre> Genres
        {
            get {
                if (_genres == null)
                {
                    Genres = Core.kingEntities.Genres.ToList();
                }
                return _genres;
            
            }
            set
            {
                _genres = value;
                OnPropertyChanged(nameof(Genres));
            }
        }


    }
}
