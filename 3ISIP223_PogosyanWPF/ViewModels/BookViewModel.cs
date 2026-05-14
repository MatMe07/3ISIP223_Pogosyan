using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public BookViewModel()
        {
            SelectedBook = dataBase.SelectedBook;
            ReviewsBooks = dataBase.GetReviewsToBook(SelectedBook.BookId);
            GetCountReviews = ReviewsBooks.Count;
        }
        private Book _selectedBook;
        public Book SelectedBook
        {
            get
            {
                return _selectedBook;
            }
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }
        private int _CountReviews;
        public int GetCountReviews
        {
            get
            {
                return _CountReviews;
            }
            set
            {
                _CountReviews = value;
                OnPropertyChanged(nameof(GetCountReviews));
            }
        }

        public bool IsAuthor => dataBase.IsAuthor;
        public bool IsAdmin => dataBase.IsAdmin;


        public User GetUser => dataBase.User;

        private ObservableCollection<Review> _ReviewsBooks;
        public ObservableCollection<Review> ReviewsBooks
        {
            get
            {
                return _ReviewsBooks;
            }
            set
            {
                _ReviewsBooks = value; 
                OnPropertyChanged(nameof(ReviewsBooks));
            }
        }

        public string AllGenresBook => string.Join(", ", dataBase.GetBookGenres(SelectedBook.BookId).Select(b=>b.Genre.GenreName));


    }
}
