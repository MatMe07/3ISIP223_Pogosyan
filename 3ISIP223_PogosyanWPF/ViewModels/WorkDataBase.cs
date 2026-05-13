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
            bookGenres = Core.kingEntities.BookGenres.ToList();
            _AllReviews = new ObservableCollection<Review>( Core.kingEntities.Reviews);
            User = Core.kingEntities.Users.FirstOrDefault(u=>u.Role.RoleName == "Читатель");
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        public bool IsAuthor => User.Role.RoleName == "Автор";
        public bool IsAdmin => User.Role.RoleName == "Администратор";

        public User User { get; set; }


        private Book _selectedBook;
        public Book SelectedBook
        {
            get { 
                return _selectedBook; 
            }
            set { 
                _selectedBook = value; 
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public List<BookGenre> bookGenres {  get; set; }

        public List<BookGenre> GetBookGenres(int bookID) => bookGenres.Where(b=>b.BookId == bookID).ToList();


        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get
            {
                if (_books == null) _books = new ObservableCollection<Book>(Core.kingEntities.Books);
                return _books;
            }

        }


        private ObservableCollection<Review> _AllReviews;


        public ObservableCollection<Review> GetReviewsToBook(int bookID)
        {
            return new ObservableCollection<Review>(_AllReviews.Where(r => r.BookId == bookID));

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


        public void AddReview(double rating, string Comment)
        {

        }

    }
}
