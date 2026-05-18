using _3ISIP223_PogosyanWPF.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public BookViewModel()
        {
            SelectedBook = dataBase.SelectedBook;
            LoadReviews();


            StatusesReadings = dataBase.StatusesReading;
            StatusesReadingsString = new ObservableCollection<string>(StatusesReadings.Select(a => a.Name));
            StatusesReadingsMItem = new ObservableCollection<MenuItem>();
            MyMessageQueue = new SnackbarMessageQueue();
            foreach (var it in StatusesReadingsString)
            {
                StatusesReadingsMItem.Add(new MenuItem() { Header = it, IsCheckable = true });
            }
            //GetCountReviews = ReviewsBooks.Count;
        }
        public void LoadReviews()
        {
            ReviewsBooks = dataBase.GetReviewsToBook(SelectedBook.BookId);

        }

        public ObservableCollection<StatusesReading> StatusesReadings { get; set; }

        public ObservableCollection<string> StatusesReadingsString { get; set; }
        public ObservableCollection<MenuItem> StatusesReadingsMItem { get; set; }

        public SnackbarMessageQueue MyMessageQueue { get; set; }

        public bool CheckReadigItemToList(Book book, string item)
        {
            var FirsBook = dataBase.ReadingLists.FirstOrDefault(b => b.Book == book);
            if (FirsBook == null) return false;
            var status = FirsBook.StatusesReading.Name;
            if (status != null)
            {
                return status == item;
            }
            return false;
        }
        public bool CheckHaveReview()
        {
            return dataBase.CheckHaveReview(SelectedBook.BookId);
        }

        public void RemoveBookFromReadingList(Book book)
        {
            dataBase.RemoveBookFromReadingList(book);
            ActionsClass.SnackBarEnqueue($"\"{book.Title}\" удалена из всех списков", "#fffff", PackIconKind.CancelBold, MyMessageQueue, true);
            //$"\"{book.Title}\" удалена из всех списков"

            //ActionsClass.RemoveBookFromReadingList(book, MyMessageQueue);

        }

        public void UpdateToReadingList(Book book, string status)
        {
            var updOrAdd = dataBase.UpdateBookStatus(book, status);
            string txt = updOrAdd ? $"\"{book.Title}\" перемещена в \"{status}\"" : $"\"{book.Title}\" добавлена в \"{status}\"";
            ActionsClass.SnackBarEnqueue(txt, "#42A757", PackIconKind.CancelBold, MyMessageQueue, true);
            //ActionsClass.UpdateToReadingList(book, status, updOrAdd, MyMessageQueue);

        }



        private double _totalRatingBook;
        public double TotalRatingBook
        {
            get { 
                _totalRatingBook = Math.Round( ReviewsBooks.Average(s=>s.Rating), 1);
                return _totalRatingBook; 
            }
            set
            {
                _totalRatingBook = value;
                OnPropertyChanged(nameof(TotalRatingBook));
            }
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
                _CountReviews = ReviewsBooks.Count;
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
                OnPropertyChanged(nameof(GetCountReviews));
                OnPropertyChanged(nameof(SelectedBook));
                OnPropertyChanged(nameof(TotalRatingBook));

            }
        }

        public string AllGenresBook => string.Join(", ", dataBase.GetBookGenres(SelectedBook.BookId).Select(b=>b.Genre.GenreName));


        public void ShowMessage(Review SelReview = null, Book SelBook = null, User Author = null)
        {
            MainWindow mainWindow = MainWindow.GetInstance();
            mainWindow.BlurAdd(true);
            FreezeRequestPage wind;
            bool? res;


            string titl = "";
            if (SelReview != null)
            {
                wind = new FreezeRequestPage(titl = "отзыв", SelReview, IsAdmin);
            }
            else if (SelBook != null)
            {
                wind = new FreezeRequestPage(titl = "книгу", SelBook, IsAdmin);
            }
            else
            {
                wind = new FreezeRequestPage(titl = "автора", Author, IsAdmin);
            }
            wind.Owner = mainWindow;
            res = wind.ShowDialog();

            mainWindow.BlurAdd(false);
            if (res == true)
            {
                ActionsClass.SnackBarEnqueue(
                    text: IsAdmin ? $"{titl} заморожен!": "Жалоба успешно отправлена" ,
                    foregroundHEX: "#FF04BE5A",
                    iconKind: PackIconKind.CheckCircle,
                    MyMessageQueue: MyMessageQueue,
                    main: true
                );
                LoadReviews();
            }

        }

    }
}
